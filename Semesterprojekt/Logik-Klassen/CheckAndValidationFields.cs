using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Semesterprojekt
{
    internal class CheckAndValidationFields
    {
        // Initialisierung mehrfach verwendeter BackColor (Hintergrundfarbe)
        private readonly Color backColorOK = SystemColors.Window;
        private readonly Color backColorNOK = Color.LightPink;

        // Initialisierung mehrfach verwendetes Tag
        private readonly string tagOK = "true";
        private readonly string tagNOK = "false";

        // Prüfung Felder gemäss Erwartungen (leere Felder, Defaultwerte usw.)
        public bool ValidationFields(InitializationCheckAndValidationFields content)
        {
            // Prüfung "Grundlagen inkl. Sonderzeichen"
            foreach (Control field in content.GroupFieldEmployeesAndCustomers)
            {
                CheckFields(field, content.CheckFieldIgnore);
                CheckFieldSpecialCharacters(field, content.CheckFieldSpecialCharactersWithoutNumbers, content.CheckFieldSpecialCharactersWithNumbers);
            }

            if (content.IsEmployee)
            {
                foreach (Control field in content.GroupFieldEmployees)
                {
                    CheckFields(field, content.CheckFieldIgnore);
                    CheckFieldSpecialCharacters(field, content.CheckFieldSpecialCharactersWithoutNumbers, content.CheckFieldSpecialCharactersWithNumbers);
                }
            }

            // Prüfung "erweitert"
            List<Control> groupFieldAll = new List<Control>();
            groupFieldAll.AddRange(content.GroupFieldEmployeesAndCustomers);
            groupFieldAll.AddRange(content.GroupFieldEmployees);
            ValidationFieldsExtension(groupFieldAll, content);

            // Ausgabe Validierungsstatus (für Speichervorgang)
            bool checkFieldTag = true;

            foreach (Control field in groupFieldAll)
                checkFieldTag = field.Tag == tagNOK ? false : checkFieldTag;

            return checkFieldTag;
        }

        // Prüfung einzelner Felder gemäss Erwartungen (leere Felder, Defaultwerte usw.)
        private void CheckFields(Control field, Control[] checkFieldIgnore)
        {
            if (checkFieldIgnore.Contains(field))
            {
                field.BackColor = backColorOK;
                field.Tag = tagOK;
                return;
            }

            if (field is System.Windows.Forms.ComboBox cmbxField && string.IsNullOrWhiteSpace(cmbxField.Text))
            {
                // Einfärbung technisch nicht möglich (diverse Versuche gescheitert)
                cmbxField.Tag = tagNOK;
                return;
            }

            if (field is NumericUpDown numField && (numField.Value == numField.Minimum))
            {
                numField.BackColor = backColorNOK;
                numField.Tag = tagNOK;
                return;
            }

            if (string.IsNullOrWhiteSpace(field.Text))
            {
                field.BackColor = backColorNOK;
                field.Tag = tagNOK;
                return;
            }

            field.BackColor = backColorOK;
            field.Tag = tagOK;
        }

        // Prüfung einzelner Felder bezüglich Sonderzeichen (Apostroph, Bindestrich, Buchstabe (inkl. Umlaut), Komma, Leerzeichen, Punkt und je nachdem Zahl erlaubt)
        private void CheckFieldSpecialCharacters(Control field, Control[] specialCharactersWithoutNumbers, Control[] specialCharactersWithNumbers)
        {
            if (!string.IsNullOrWhiteSpace(field.Text))
            {
                string patternWithoutNumbers = @"^[\p{L} \-'\.,]+$";

                if (specialCharactersWithoutNumbers.Contains(field) && !Regex.IsMatch(field.Text.Trim(), patternWithoutNumbers))
                {
                    field.BackColor = backColorNOK;
                    field.Tag = tagNOK;
                    return;
                }

                string patternWithNumbers = @"^[\p{L}0-9 \-'\.,]+$";

                if (specialCharactersWithNumbers.Contains(field) && !Regex.IsMatch(field.Text.Trim(), patternWithNumbers))
                {
                    field.BackColor = backColorNOK;
                    field.Tag = tagNOK;
                }
            }
        }

        // Prüfung einzelner Spezifalfelder gemäss Erwartungen inkl. Popup
        private void ValidationFieldsExtension(List<Control> groupFieldAll, InitializationCheckAndValidationFields content)
        {
            foreach (Control field in groupFieldAll)
            {
                if (field.BackColor == backColorNOK)
                {
                    ShowMessageBox("Bitte fülle alle Pflichtfelder korrekt aus!\r\n\r\n" +
                        "Es sind keine Sonderzeichen erlaubt, ausser:\r\n" +
                        "Apostroph, Bindestrich, Buchstabe (inkl. Umlaut), Komma, Leerzeichen, Punkt und je nachdem Zahl");
                    return;
                }
            }

            if (content.Salutation.Tag == tagNOK)
            {
                ShowMessageBox("Anrede fehlt");
                return;
            }

            CheckDateField(content.Birthday, "Geburtsdatum", true);
            if (content.Birthday.Tag == tagNOK)
                return;

            if (content.Gender.Tag == tagNOK)
            {
                ShowMessageBox("Geschlecht fehlt");
                return;
            }

            CheckPLZNumber(content.PostalCode, false);
            if (content.PostalCode.Tag == tagNOK)
                return;

            CheckPhone(content.BusinessNumber, "Geschäft Nr.");
            if (content.BusinessNumber.Tag == tagNOK)
                return;

            CheckPhone(content.MobileNumber, "Mobile Nr.");
            if (content.MobileNumber.Tag == tagNOK)
                return;

            CheckEMail(content.Email);
            if (content.Email.Tag == tagNOK)
                return;

            if (content.IsEmployee)
            {
                CheckAHVNumber(content.AHVNumber);
                if (content.AHVNumber.Tag == tagNOK)
                    return;

                CheckPLZNumber(content.PostalCodeOffice, true);
                if (content.PostalCodeOffice.Tag == tagNOK)
                    return;

                CheckDateField(content.DateOfEntry, "Eintrittsdatum", true);
                if (content.DateOfEntry.Tag == tagNOK)
                    return;

                CheckDateField(content.DateOfExit, "Austrittsdatum", false);
                if (content.DateOfExit.Tag == tagNOK)
                    return;
            }
        }

        // Prüfung Datum-Format auf TT.MM.JJJJ
        private void CheckDateField(TextBox content, string labelName, bool textRequired)
        {
            string errorMessage = string.Empty;
            bool dateField = CheckAndValidationDateFields.CheckDateField(content, labelName, textRequired, out errorMessage);

            if (dateField)
            {
                content.BackColor = backColorOK;
                content.Tag = tagOK;
            }
            else
            {
                content.BackColor = backColorNOK;
                content.Tag = tagNOK;

                // Erzeugung MessageBox (Popup) bei fehlerhaften Eingaben (exkl. leeres Feld)
                if (!string.IsNullOrWhiteSpace(errorMessage))
                    ShowMessageBox(errorMessage);

                content.Focus();
            }
        }

        // Prüfung PLZ-Format auf 4 bis 5 Ziffern (Standard für Schweiz und umliegende Länder)
        // Einschränkung Prüfung PLZ-Format (Geschäft) auf 4 Ziffern ohne führende 0 (Standard für Schweiz)
        private void CheckPLZNumber(TextBox plz, bool isOffice)
        {
            string pattern = isOffice ? @"^[1-9][0-9]{3}$" : @"^[0-9]{4,5}$";

            if (Regex.IsMatch(plz.Text.Trim(), pattern))
            {
                plz.BackColor = backColorOK;
                plz.Tag = tagOK;
            }
            else
            {
                string messageAdd = isOffice ? "(4 Ziffern ohne führende 0 nötig)" : "(4-5 Ziffern nötig)";
                
                plz.BackColor = backColorNOK;
                plz.Tag = tagNOK;
                ShowMessageBox($"PLZ '{plz.Text.Trim()}' ist ungültig\r\n{messageAdd}");
                plz.Focus();
            }
        }

        // Prüfung Telefon-Format auf + mit 6 bis 15 Ziffern ohne führende 0 (Standard für Schweiz und umliegende Länder)
        private void CheckPhone(TextBox phoneNumber, string typeOfPhone)
        {
            string pattern = @"^\+[1-9][0-9]{5,14}$";

            // Entfernung Leerzeichen für Vergleich mit Regex
            if (Regex.IsMatch(phoneNumber.Text.Replace(" ",""), pattern))
            {
                phoneNumber.BackColor = backColorOK;
                phoneNumber.Tag = tagOK;
            }
            else
            {
                phoneNumber.BackColor = backColorNOK;
                phoneNumber.Tag = tagNOK;
                ShowMessageBox($"{typeOfPhone} '{phoneNumber.Text.Trim()}' ist ungültig\r\n\r\nz.B. +41 79 123 44 55\r\n(ohne Sonderzeichen, nur Plus an 1. Stelle erlaubt)");
                phoneNumber.Focus();
            }
        }
        
        // Prüfung E-Mail-Format auf Text@Text.Text (auch Ziffern anstelle des Text erlaubt)
        // Sonderzeichen (exkl. Bindestrich, Punkt und Unterlinie) sind NICHT erlaubt
        private void CheckEMail(TextBox email)
        {
            string pattern = @"^[A-Za-z0-9._-]+@[A-Za-z0-9._-]+\.[A-Za-z]{2,}$";

            if (Regex.IsMatch(email.Text.Trim(), pattern))
            {
                email.BackColor = backColorOK;
                email.Tag = tagOK;
            }
            else
            {
                email.BackColor = backColorNOK;
                email.Tag = tagNOK;
                ShowMessageBox($"E-Mail '{email.Text.Trim()}' ist ungültig\r\n\r\nz.B. Test@Testmail.ch\r\n(ohne Sonderzeichen, nur Bindestrich, Punkt und Unterlinie erlaubt)");
                email.Focus();
            }
        }

        // Prüfung AHV-Format auf 756.xxxx.xxxx.xx (CH-Norm gemäss BSV)
        private void CheckAHVNumber(TextBox ahvNumber)
        {          
            string pattern = @"^756\.[0-9]{4}\.[0-9]{4}\.[0-9]{2}$";
            bool isMatch = Regex.IsMatch(ahvNumber.Text.Trim(), pattern);
            bool comparison = false;

            if (isMatch)
            {
                // Entfernung Punkte (als Vorbereitung für Prüfziffer)
                string ahvNumberNoPoints = ahvNumber.Text.Replace(".", "").Trim();

                // Extraktion Ziffern (als Vorbereitung für Prüfziffer)
                int total = 0;

                for (int i = 0; i < 12; i++)
                {
                    int digit = int.Parse(ahvNumberNoPoints[i].ToString());
                    int weight = (i % 2 == 0) ? 1 : 3;
                    total += digit * weight;
                }

                // Prüfung Prüfziffer gemäss Norm EAN-13 (BSV)
                int checkDigit = int.Parse(ahvNumberNoPoints[12].ToString());
                int expectation = (10 - (total % 10)) % 10;
                comparison = checkDigit == expectation;
            }

            if (comparison)
            {
                ahvNumber.BackColor = backColorOK;
                ahvNumber.Tag = tagOK;
            }
            else
            {
                ahvNumber.BackColor = backColorNOK;
                ahvNumber.Tag = tagNOK;
                string message = isMatch ? "Validierung Prüfziffer (CH-Norm) fehlgeschlagen" : "\r\nz.B. 756.8800.5641.37";
                ShowMessageBox($"AHV-Nummer '{ahvNumber.Text.Trim()}' ist ungültig\r\n{message}");
                ahvNumber.Focus();
            }
        }

        // Erzeugung MessageBox (Popup) bei fehlenden und/oder fehlerhaften Eingaben gemäss Erwartungen
        private void ShowMessageBox(string message)
        {
            MessageBox.Show(message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
