using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Commands;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe1.Services
{
    public class PaymentService
    {
        private readonly AppointmentContext _db;
        public IQueryable<Payment> Payments => _db.Payments.AsQueryable();

        public PaymentService(AppointmentContext db)
        {
            _db = db;
        }

        private CashDesk GetCashDesk(int number)
        {
            var cashDesk = _db.CashDesks.FirstOrDefault(c => c.Number == number);
            if (cashDesk is null)
                throw new PaymentServiceException("Invalid cash desk");
            return cashDesk;
        }


        public void ConfirmPayment(int paymentId)
        {
            var payment = GetPaymentById(paymentId);
            payment.Confirmed = DateTime.UtcNow;
            SaveOrThrow();
        }

        public void AddPaymentItem(NewPaymentItemCommand cmd)
        {
            var payment = GetPaymentById(cmd.PaymentId);

            if (payment.Confirmed is not null)
                throw new PaymentServiceException("Payment already confirmed.");

            var paymentItem = new PaymentItem(cmd.ArticleName, cmd.Amount, cmd.Price, payment);
            _db.PaymentItems.Add(paymentItem);
            SaveOrThrow();
        }

        public void DeletePayment(int paymentId, bool deleteItems)
        {
            var payment = _db.Payments.FirstOrDefault(p => p.Id == paymentId);
            if (payment is null) return;

            var paymentItems = _db.PaymentItems.Where(p => p.Payment.Id == paymentId).ToList();

            if (deleteItems && paymentItems.Count > 0)
            {
                TryDelete(() => _db.PaymentItems.RemoveRange(paymentItems));
            }

            TryDelete(() => _db.Payments.Remove(payment));
        }

        // ----------------- Hilfsmethoden -----------------

       
        


        private Employee GetEmployee(string registrationNumber)
        {
            var employee = _db.Employees
                .FirstOrDefault(e => e.RegistrationNumber.ToString() == registrationNumber);

            if (employee is null)
                throw new PaymentServiceException("Invalid employee");

            return employee;
        }


        private static PaymentType ParsePaymentType(string paymentTypeStr)
        {
            if (!Enum.TryParse<PaymentType>(paymentTypeStr, out var paymentType))
                throw new PaymentServiceException("Invalid payment type");
            return paymentType;
        }

        private bool HasOpenPayment(int cashDeskNumber)
        {
            return _db.Payments.Any(p => p.CashDesk.Number == cashDeskNumber && p.Confirmed == null);
        }

        private Payment GetPaymentById(int id)
        {
            return _db.Payments.FirstOrDefault(p => p.Id == id)
                ?? throw new PaymentServiceException("Payment not found") { NotFoundException = true };
        }

        private void TryDelete(Action deleteAction)
        {
            try
            {
                deleteAction();
                _db.SaveChanges();
            }
            catch (Exception e) when (e is DbUpdateException || e is InvalidOperationException)
            {
                throw new PaymentServiceException(e.InnerException?.Message ?? e.Message);
            }
        }

        private void SaveOrThrow()
        {
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new EmployeeServiceException(e.InnerException?.Message ?? e.Message);
            }
        }
    }
}
