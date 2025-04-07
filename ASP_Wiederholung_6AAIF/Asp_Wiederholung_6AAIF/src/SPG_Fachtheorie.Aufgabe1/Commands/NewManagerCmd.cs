using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SPG_Fachtheorie.Aufgabe1.Commands
{
    /*
     * Beispiel-Anfrage:
     * {
     *   "registrationNumber": 1003,
     *   "firstName": "FN1",
     *   "lastName": "LN1",
     *   "address": {
     *     "street": "Spengergasse 20",
     *     "zip": "1050",
     *     "city": "Wien"
     *   },
     *   "carType": "SUV"
     * }
     */
    public record NewManagerCmd(
        [Range(1, 999999, ErrorMessage = "Ungültige Registrierungsnummer.")]
        int RegistrationNumber,

        [StringLength(255, MinimumLength = 1, ErrorMessage = "Vorname ungültig.")]
        string FirstName,

        [StringLength(255, MinimumLength = 1, ErrorMessage = "Nachname ungültig.")]
        string LastName,

        DateOnly Birthdate,

        [Range(0, 1_000_000)]
        decimal? Salary,

        AddressCmd? Address,

        [StringLength(255, MinimumLength = 1, ErrorMessage = "Autotyp ungültig.")]
        string CarType
    ) : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Vor- und Nachname zusammen müssen mindestens 3 Zeichen lang sein
            if ((FirstName + LastName).Length < 3)
            {
                yield return new ValidationResult(
                    "Name ist zu kurz.",
                    new[] { nameof(FirstName), nameof(LastName) }
                );
            }
        }
    }
}
