using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Semesterprojekt
{
    internal class CheckAndValidationFields
    {
        // Initialisierung mehrfach verwendeter BackColor
        private readonly Color backColorOK = SystemColors.Window;
        private readonly Color backColorNOK = Color.LightPink;

        // Initialisierung mehrfach verwendetes Tag
        private readonly string tagOK = "true";
        private readonly string tagNOK = "false";

        // Prüfung Felder gemäss Erwartungen (leere Felder, Defaultwerte usw.)
        public bool ValidationFields(InitializationCheckAndValidationFields content)
        {
            // Prüfung (Grundlagen)
            foreach (Control field in content.GroupFieldEmployeesAndCustomers)
            {
                CheckFields(field, content.CheckFieldIgnore);
            }

            if (content.IsEmployee)
            {
                foreach (Control field in content.GroupFieldEmployees)
                {
                    CheckFields(field, content.CheckFieldIgnore);
                }
            }

            // Prüfung (vertieft)
            List<Control> groupFieldAll = new List<Control>();
            groupFieldAll.AddRange(content.GroupFieldEmployeesAndCustomers);
            groupFieldAll.AddRange(content.GroupFieldEmployees);

            ValidationFieldsExtension(groupFieldAll, content);

            // Ausgabe Validierungsstatus (für Speichervorgang)
            bool checkFieldTag = true;

            foreach (Control field in groupFieldAll)
            {
                checkFieldTag = field.Tag == tagNOK ? false : checkFieldTag;
            }

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

        // Prüfung einzelner Spezifalfelder gemäss Erwartungen inkl. Popup
        private void ValidationFieldsExtension(List<Control> groupFieldAll, InitializationCheckAndValidationFields content)
        {
            foreach (Control field in groupFieldAll)
            {
                if (field.BackColor == backColorNOK)
                {
                    ShowMessageBox("Bitte fülle alle Pflichtfelder korrekt aus!");
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

            CheckPLZNumber(content.PostalCode);
            if (content.PostalCode.Tag == tagNOK)
                return;

            if (content.IsEmployee || !string.IsNullOrWhiteSpace(content.BusinessNumber.Text))
            {
                CheckPhone(content.BusinessNumber, "Geschäft Nr.");
                if (content.BusinessNumber.Tag == tagNOK)
                    return;
            }

            CheckPhone(content.MobileNumber, "Mobile Nr.");
            if (content.MobileNumber.Tag == tagNOK)
                return;


            CheckEMail(content.Email);
            if (content.Email.Tag == tagNOK)
                return;

            CheckAHVNumber(content.AHVNumber, content.Nationality, content.IsClient);
            if (content.AHVNumber.Tag == tagNOK)
                return;

            if (content.IsEmployee)
            {
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
                {
                    ShowMessageBox(errorMessage);
                }

                content.Focus();
            }
        }

        // Prüfung PLZ-Format auf 4 bis 5 Ziffern (Standard für Schweiz und umliegende Länder)
        private void CheckPLZNumber(TextBox plz)
        {
            if (Regex.IsMatch(plz.Text.Trim(), @"^\d{4,5}$"))
            {
                plz.BackColor = backColorOK;
                plz.Tag = tagOK;
            }

            else
            {
                plz.BackColor = backColorNOK;
                plz.Tag = tagNOK;
                ShowMessageBox($"PLZ '{plz.Text.Trim()}' ist ungültig\r\n(4-5 Ziffern nötig)");
                plz.Focus();
            }
        }

        // Prüfung Telefon-Format auf + mit 6 bis 15 Ziffern (Standard für Schweiz und umliegende Länder)
        private void CheckPhone(TextBox phone, string typeOfPhone)
        {            
            // Entfernung Sonderzeichen (exkl. + und Ziffern)
            string phoneNumberNo = Regex.Replace(phone.Text, @"[^\d+]", "");

            if (Regex.IsMatch(phoneNumberNo, @"^\+\d{6,15}$"))
            {
                phone.BackColor = backColorOK;
                phone.Tag = tagOK;
            }

            else
            {
                phone.BackColor = backColorNOK;
                phone.Tag = tagNOK;
                ShowMessageBox($"{typeOfPhone} '{phone.Text.Trim()}' ist ungültig\r\n\r\nz.B. +41 79 123 44 55");
                phone.Focus();
            }
        }
        
        // Prüfung E-Mail-Format Text@Text.Text (auch Sonderzeichen und Ziffern anstelle des Text erlaubt)
        private void CheckEMail(TextBox email)
        {
            if (Regex.IsMatch(email.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                email.BackColor = backColorOK;
                email.Tag = tagOK;
            }

            else
            {
                email.BackColor = backColorNOK;
                email.Tag = tagNOK;
                ShowMessageBox($"E-Mail '{email.Text.Trim()}' ist ungültig\r\n\r\nz.B. Test@Testmail.ch");
                email.Focus();
            }
        }

        // Prüfung AHV-Nummer auf Korrektheit und Vollständigkeit (1. Schritt)
        private void CheckAHVNumber(TextBox ahvNumber, TextBox nationality, bool isClient)
        {
            if (isClient || string.IsNullOrWhiteSpace(nationality.Text.Trim()) || nationality.Text.Trim().ToUpper() != "CH")
            {
                ahvNumber.BackColor = backColorOK;
                ahvNumber.Tag = tagOK;
                return;
            }

            if (!ValidationAHVNumber(ahvNumber.Text.Trim()))
            {
                ahvNumber.BackColor = backColorNOK;
                ahvNumber.Tag = tagNOK;
                ShowMessageBox($"AHV-Nummer '{ahvNumber.Text.Trim()}' ist ungültig");
                ahvNumber.Focus();

            }

            else
            {
                ahvNumber.BackColor = backColorOK;
                ahvNumber.Tag = tagOK;
            }
        }

        // Prüfung AHV-Nummer auf Korrektheit und vollständigkeit (2. Schritt)
        private bool ValidationAHVNumber(string ahvNumber)
        {
            // Prüfung Format gemäss CH-Norm (BSV): 756.xxxx.xxxx.xx
            string pattern = @"^756\.\d{4}\.\d{4}\.\d{2}$";

            if (!Regex.IsMatch(ahvNumber.Trim(), pattern))
                return false;

            // Entfernung Punkte
            string ahvNumberNoPoints = ahvNumber.Replace(".", "").Trim();

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

            return checkDigit == expectation;
        }

        // Erzeugung MessageBox (Popup) bei fehlenden und/oder fehlerhaften Eingaben gemäss Erwartungen
        private void ShowMessageBox(string message)
        {
            MessageBox.Show(message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
