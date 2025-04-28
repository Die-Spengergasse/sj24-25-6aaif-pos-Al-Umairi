using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SPG_Fachtheorie.Aufgabe1.Commands
{
    public record UpdateManagerCmd(
        [Range(1, 999999, ErrorMessage = "Ungültige Registrierungsnummer.")]
        int RegistrationNumber,

        [StringLength(255, MinimumLength = 1, ErrorMessage = "Vorname ist ungültig.")]
        string FirstName,

        [StringLength(255, MinimumLength = 1, ErrorMessage = "Nachname ist ungültig.")]
        string LastName,

        AddressCmd? Address,

        [StringLength(255, MinimumLength = 1, ErrorMessage = "Autotyp ist ungültig.")]
        string CarType,

        DateTime? LastUpdate
    ) : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Vor- und Nachname zusammen müssen mindestens 3 Zeichen lang sein
            if ((FirstName + LastName).Length < 3)
            {
                yield return new ValidationResult(
                    "Vor- und Nachname zusammen sind zu kurz.",
                    new[] { nameof(FirstName), nameof(LastName) }
                );
            }
        }
    }
}
