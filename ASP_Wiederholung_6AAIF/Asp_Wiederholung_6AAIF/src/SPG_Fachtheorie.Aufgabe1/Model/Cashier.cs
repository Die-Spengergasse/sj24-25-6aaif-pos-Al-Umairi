using System;
using System.ComponentModel.DataAnnotations;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Cashier : Employee
    {
        // Parameterloser Konstruktor (für Entity Framework)
#pragma warning disable CS8618
        protected Cashier() { }
#pragma warning restore CS8618

        // Konstruktor mit allen relevanten Werten
        public Cashier(
            int registrationNumber,
            string firstName,
            string lastName,
            DateOnly birthday,
            decimal? salary,
            Address? address,
            string jobSpezialisation
        ) : base(registrationNumber, firstName, lastName, birthday, salary, address)
        {
            JobSpezialisation = jobSpezialisation;
        }

        // Tätigkeitsbereich bzw. Spezialisierung des Kassiers
        [MaxLength(255)]
        public string JobSpezialisation { get; set; }
    }
}
