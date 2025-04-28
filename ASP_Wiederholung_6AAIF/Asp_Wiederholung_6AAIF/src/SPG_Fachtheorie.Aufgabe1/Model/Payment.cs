using System;
using System.Collections.Generic;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Payment
    {
        // Parameterloser Konstruktor – wird intern von EF Core verwendet
#pragma warning disable CS8618
        protected Payment() { }
#pragma warning restore CS8618

        // Konstruktor zur Initialisierung einer Zahlung
        public Payment(
            CashDesk cashDesk,
            DateTime paymentDateTime,
            Employee employee,
            PaymentType paymentType
        )
        {
            CashDesk = cashDesk;
            PaymentDateTime = paymentDateTime;
            Employee = employee;
            PaymentType = paymentType;
        }

        // Eindeutige ID der Zahlung
        public int Id { get; set; }

        // Kasse, über die die Zahlung abgewickelt wurde
        public CashDesk CashDesk { get; set; }

        // Datum und Uhrzeit der Zahlung
        public DateTime PaymentDateTime { get; set; }

        // Mitarbeiter, der die Zahlung bearbeitet hat
        public Employee Employee { get; set; }

        // Zahlungsart (z. B. Bar, Karte, etc.)
        public PaymentType PaymentType { get; set; }

        // Zeitpunkt der Bestätigung (optional)
        public DateTime? Confirmed { get; set; }

        // Alle Positionen, die zu dieser Zahlung gehören
        public List<PaymentItem> PaymentItems { get; } = new();
    }
}
