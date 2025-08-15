using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Semesterprojekt.ClientAndEmployeeNumber;

namespace Semesterprojekt
{
    internal class ContactData
    {
        // Dateipfad für JSON "contacts" (Kontaktdaten-Listen)
        private static readonly string fileName = "contacts.json";
        private static readonly string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
        private static readonly string contactDataPath = Path.Combine(projectRoot, "data", fileName);

        // Auslesen JSON für Ermittlung, Speicherung und Löschung Kontaktdaten
        private static bool LoadData(out List<InitializationContactData> contactDataList)
        {
            try
            {
                if (File.Exists(contactDataPath))
                {
                    
                    string contactsJSON = File.ReadAllText(contactDataPath);
                    contactDataList = JsonSerializer.Deserialize<List<InitializationContactData>>(contactsJSON) ?? new List<InitializationContactData>();
                }

                else
                {
                    contactDataList = new List<InitializationContactData>();
                }

                return true;
            }

            catch (Exception exception)
            {
                // Ausgabe Fehler beim Laden (Ausnahmebehandlung)
                ShowMessageBox($"Fehler beim Laden der JSON-Datei '{fileName}': {exception}");
                contactDataList = null;
                return false;
            }
        }


        // Speicherung der Kontaktdaten in JSON "contacts"
        public static bool SaveContactData(string typeOfContactNew, string contactNumberNew, Control[] groupFieldEmployeesAndCustomers, Control[] groupFieldEmployees)
        {
            // Abbruch bei Fehler beim Laden der JSON-Datei
            if (!LoadData(out var contactDataList))
                return false;

            var contact = new InitializationContactData
            {
                // Erfassung mit Default-Kontaktstatus "Aktiv"
                ContactStatus = "active",
                // Erfassung Kontakttyp mit Gross- und Kleinbuchstaben
                TypeOfContact = $"{char.ToUpper(typeOfContactNew[0])}{typeOfContactNew.Substring(1)}",
                // Erfassung Kontaktnummer für spätere Zuweisung der Notizen
                ContactNumber = contactNumberNew
            };

            foreach (Control field in groupFieldEmployeesAndCustomers)
            {
                contact.Fields[field.AccessibleName] = GetControlValue(field);
            }

            if (typeOfContactNew == "mitarbeiter")
            {
                foreach (Control field in groupFieldEmployees)
                {
                    contact.Fields[field.AccessibleName] = GetControlValue(field);
                }
            }

            // Duplikatencheck mit Bestätigung durch User (bei Nein "Abbruch")
            if (!CheckDuplicateContact(contactDataList, contact))
                return false;

            // Hinzufügen neuer Kontakt zur neuen Liste und Speicherung in JSON
            contactDataList.Add(contact);
            SaveData(contactDataList);

            return true;
        }

        // Auslesen der Werte für Speicherung der Kontaktdaten in JSON-Datei
        private static string GetControlValue(Control field)
        {
            if (field is System.Windows.Forms.TextBox txtbxField)
                return txtbxField.Text;

            if (field is System.Windows.Forms.ComboBox cmbxField)
                return cmbxField.Text;

            if (field is NumericUpDown numField)
                return numField.Value.ToString();

            return string.Empty;
        }

        // Abgleich neuer Kontakt mit bestehenden Kontaktdaten        
        private static bool CheckDuplicateContact(List<InitializationContactData> contactList, InitializationContactData newContact)
        {
            // Regex für Split Vorname und Nachname bei Bindestrich und/oder Leerzeichen
            string regex = @"[\s\-]";

            newContact.Fields.TryGetValue("FirstName", out var newFirstNameRaw);
            newContact.Fields.TryGetValue("LastName", out var newLastNameRaw);
            newContact.Fields.TryGetValue("Birthday", out var newDateOfBirthRaw);

            string newFirstName = Regex.Split(newFirstNameRaw?.Trim().ToLower() ?? "", regex)[0];
            string newLastName = Regex.Split(newLastNameRaw?.Trim().ToLower() ?? "", regex)[0];
            string newDateOfBirth = newDateOfBirthRaw ?? "";

            // Liste mit allen möglichen Duplikaten (für Anzeige)
            List<string> duplicates = new List<string>();

            foreach (InitializationContactData oldContact in contactList)
            {
                oldContact.Fields.TryGetValue("FirstName", out var oldFirstNameRaw);
                oldContact.Fields.TryGetValue("LastName", out var oldLastNameRaw);
                oldContact.Fields.TryGetValue("Birthday", out var oldDateOfBirthRaw);

                string oldFirstName = Regex.Split(oldFirstNameRaw?.Trim().ToLower() ?? "", regex)[0];
                string oldLastName = Regex.Split(oldLastNameRaw?.Trim().ToLower() ?? "", regex)[0];
                string oldDateOfBirth = oldDateOfBirthRaw ?? "";

                // Abgleich nur auf Basis des ersten Namens, falls z.B. noch ein zweiter Name erfasst ist
                if (newFirstName == oldFirstName && newLastName == oldLastName && newDateOfBirth == oldDateOfBirth)
                {
                    duplicates.Add($"- {oldFirstNameRaw} {oldLastNameRaw}, {oldDateOfBirthRaw}");
                }
            }

            // Sammelausgabe der ähnlichen Kontakte auf Basis Vorname, Nachname und Geburtsdatum
            if (duplicates.Any())
            {
                string message = "Folgende ähnliche Kontakte existieren bereits:\r\n\r\n" + string.Join("\n", duplicates) + "\r\n\r\nTrotzdem speichern?";
                DialogResult result = MessageBox.Show(message, "Duplikatencheck", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                return result == DialogResult.Yes;
            }

            return true;
        }

        // Löschung aktuelle Kontaktdaten
        public static void DeleteContactData(string number)
        {
            // Abbruch bei Fehler beim Laden der JSON-Datei
            if (!LoadData(out var contactDataList))
                return;

            // Entfernung Kontaktdaten (Block) auf Basis Kontakt Nr.
            contactDataList.RemoveAll(contact => contact.ContactNumber.Equals(number));

            // Speicherung JSON 
            SaveData(contactDataList);
        }

        // Speicherung neue, zu ändernde oder zu löschende Kundendaten
        private static void SaveData(List<InitializationContactData> contactDataList)
        {
            try
            {
                // Erzeugung data-Ordner, falls noch nicht vorhanden (Vermeidung von Exception)
                var directory = Path.GetDirectoryName(contactDataPath);
                if (!string.IsNullOrEmpty(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string contactsJSON = JsonSerializer.Serialize(contactDataList, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(contactDataPath, contactsJSON);

                // Ausgabe erfolgreiche Speicherung (userfreundlich)
                MessageBox.Show("Kontakt erfolgreich gespeichert!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception exception)
            {
                // Ausgabe Fehler beim Laden (Ausnahmebehandlung)
                ShowMessageBox($"Fehler beim Speichern der JSON-Datei '{fileName}': {exception}");
            }
        }

        // Erzeugung MessageBox (Popup) bei JSON-Fehler
        private static void ShowMessageBox(string message)
        {
            MessageBox.Show(message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
