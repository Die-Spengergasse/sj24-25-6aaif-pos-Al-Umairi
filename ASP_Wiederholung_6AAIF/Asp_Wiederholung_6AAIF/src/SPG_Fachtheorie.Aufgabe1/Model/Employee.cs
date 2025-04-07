using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public abstract class Employee
    {
        // Parameterloser Konstruktor (wird von EF benötigt)
#pragma warning disable CS8618
        protected Employee() { }
#pragma warning restore CS8618

        // Hauptkonstruktor mit allen Eigenschaften
        public Employee(
            int registrationNumber,
            string firstName,
            string lastName,
            DateOnly birthday,
            decimal? salary,
            Address? address
        )
        {
            RegistrationNumber = registrationNumber;
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            Salary = salary;
            Address = address;
        }

        // Primärschlüssel (manuell gesetzt)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RegistrationNumber { get; set; }

        // Vorname des Mitarbeiters
        [MaxLength(255)]
        public string FirstName { get; set; }

        // Nachname
        [MaxLength(255)]
        public string LastName { get; set; }

        // Geburtsdatum
        public DateOnly Birthday { get; set; }

        // Gehalt (optional)
        public decimal? Salary { get; set; }

        // Adresse (optional)
        public Address? Address { get; set; }

        // Zeitpunkt des letzten Logins (optional)
        public DateTime? LastLogin { get; set; }

        // ⚠️ Wird von EF als Vererbungsdiscriminator verwendet
        public string Type { get; set; } = null!;

        // Zeitpunkt der letzten Änderung (z. B. bei Update)
        public DateTime? LastUpdate { get; set; }

        // Zahlungen, die der Mitarbeiter abgewickelt hat
        public List<Payment> Payments { get; } = new();
    }
}
