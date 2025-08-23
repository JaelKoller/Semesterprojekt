using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Semesterprojekt
{
    internal class CheckAndValidationDateFields
    {
        // Initialisierung Minimal- und Maximaldatum
        private static readonly DateTime MinDate = new DateTime(1900, 1, 1);
        private static readonly DateTime MaxDate = new DateTime(2099, 12, 31);

        // Prüfung Format auf TT.MM.JJJJ (für OK-Fall Rückgabe "TRUE")
        public static bool CheckDateField(TextBox txtbxDate, string labelName, bool textRequired, out string errorMessage)
        {
            // Initialisierung OUT-Argument
            errorMessage = string.Empty;

            // Bereinigung (Trim) IN-Argument
            string date = txtbxDate.Text.Trim();

            // Prüfung mit Rückgabe
            if (string.IsNullOrWhiteSpace(date))
                // Datum "leer", jedoch NICHT erforderlich = Rückgabe "TRUE" (Gegenteil von textRequired)
                return !textRequired;

            if (!(Regex.IsMatch(date, @"^\d{2}\.\d{2}\.\d{4}$")))
            {
                errorMessage = $"{labelName} '{date}' entspricht nicht den Vorgaben 'TT.MM.JJJJ'";
                return false;
            }

            if (!(DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime)))
            {
                errorMessage = $"{labelName} '{date}' ist kein gültiges Datum";
                return false;
            }

            if (dateTime < MinDate || dateTime > MaxDate)
            {
                errorMessage = $"{labelName} muss zwischen {MinDate:dd.MM.yyyy} und {MaxDate:dd.MM.yyyy} liegen";
                return false;
            }

            return true;
        }
    }
}
