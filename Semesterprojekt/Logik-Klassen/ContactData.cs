using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Semesterprojekt
{
    internal class ContactData
    {
        // Dateipfad für JSON "contacts" (Kontaktdaten-Liste)
        private static readonly string fileName = "contacts";
        private static readonly string contactDataPath = InitializationDataPathJson.DataPath(fileName);

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
                    contactDataList = new List<InitializationContactData>();

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

        // Speichervorgang der Kontaktdaten
        public static bool SaveContactData(string saveMode, string contactStatus, string typeOfContact, string contactNumber, Control[] groupFieldEmployeesAndCustomers, Control[] groupFieldEmployees)
        {
            // Abbruch bei Fehler beim Laden der JSON-Datei
            if (!LoadData(out var contactDataList))
                return false;

            InitializationContactData contactData = null;

            switch (saveMode.ToLower())
            {
                case "save":
                    contactData = new InitializationContactData
                    {
                        // Erfassung mit Default-Kontaktstatus "Aktiv"
                        ContactStatus = contactStatus,
                        // Erfassung Kontakttyp mit Gross- und Kleinbuchstaben
                        TypeOfContact = $"{char.ToUpper(typeOfContact[0])}{typeOfContact.Substring(1)}",
                        // Erfassung Kontakt Nr. für spätere Zuweisung der Notizen
                        ContactNumber = contactNumber
                    };
                    break;

                case "update":
                    // Ermittlung bestehender Kontakt auf Basis Kontakt Nr.
                    contactData = contactDataList.FirstOrDefault(contact => contact.ContactNumber.Equals(contactNumber));
                    contactData.ContactStatus = contactStatus;
                    break;
            }
            
            foreach (Control field in groupFieldEmployeesAndCustomers)
                contactData.Fields[field.AccessibleName] = GetControlValue(field);

            if (typeOfContact == "mitarbeiter")
                foreach (Control field in groupFieldEmployees)
                    contactData.Fields[field.AccessibleName] = GetControlValue(field);

            // Duplikatencheck mit Bestätigung durch User (bei Nein "Abbruch")
            if (!CheckDuplicateContact(contactDataList, contactData, contactNumber))
                return false;

            // Hinzufügen (inkl. Speicherung) neuer Kontakt
            if (saveMode.ToLower() == "save")
            {
                contactDataList.Add(contactData);
                SaveData(contactDataList, "save");
            }
            // Speicherung geänderter Kontakt
            else
                SaveData(contactDataList, "update");

            return true;
        }

        // Auslesen der Werte für Speicherung der Kontaktdaten
        private static string GetControlValue(Control field)
        {
            if (field is System.Windows.Forms.TextBox txtbxField)
                return txtbxField.Text.Trim();

            if (field is System.Windows.Forms.ComboBox cmbxField)
                return cmbxField.Text;

            if (field is NumericUpDown numField)
                return numField.Value.ToString();

            return string.Empty;
        }

        // Abgleich neuer Kontakt mit bestehenden Kontaktdaten        
        private static bool CheckDuplicateContact(List<InitializationContactData> contactList, InitializationContactData newContact, string ignoreContactNumber)
        {
            // Regex für Split Vorname und Nachname bei Bindestrich und/oder Leerzeichen
            string pattern = @"[\s\-]";

            newContact.Fields.TryGetValue("FirstName", out var newFirstNameRaw);
            newContact.Fields.TryGetValue("LastName", out var newLastNameRaw);
            newContact.Fields.TryGetValue("Birthday", out var newDateOfBirthRaw);

            string newFirstName = Regex.Split(newFirstNameRaw?.Trim().ToLower() ?? "", pattern)[0];
            string newLastName = Regex.Split(newLastNameRaw?.Trim().ToLower() ?? "", pattern)[0];
            string newDateOfBirth = newDateOfBirthRaw ?? "";

            // Liste mit allen möglichen Duplikaten (für Anzeige)
            List<string> duplicates = new List<string>();

            foreach (InitializationContactData oldContact in contactList)
            {
                // Ausschluss Duplikatenprüfung bei eigener Kontat Nr. (nur für Update relevant)
                if (oldContact.ContactNumber == ignoreContactNumber)
                    continue;

                oldContact.Fields.TryGetValue("FirstName", out var oldFirstNameRaw);
                oldContact.Fields.TryGetValue("LastName", out var oldLastNameRaw);
                oldContact.Fields.TryGetValue("Birthday", out var oldDateOfBirthRaw);

                string oldFirstName = Regex.Split(oldFirstNameRaw?.Trim().ToLower() ?? "", pattern)[0];
                string oldLastName = Regex.Split(oldLastNameRaw?.Trim().ToLower() ?? "", pattern)[0];
                string oldDateOfBirth = oldDateOfBirthRaw ?? "";

                // Abgleich nur auf Basis des ersten Namens, falls z.B. noch ein zweiter Name erfasst ist
                if (newFirstName == oldFirstName && newLastName == oldLastName && newDateOfBirth == oldDateOfBirth)
                    duplicates.Add($"- {oldFirstNameRaw} {oldLastNameRaw}, {oldDateOfBirthRaw}");
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
        public static bool DeleteContactData(string contactNumber)
        {
            // Abbruch bei Fehler beim Laden der JSON-Datei
            if (!LoadData(out var contactDataList))
                return false;

            // Entfernung Kontaktdaten (Block) auf Basis Kontakt Nr.
            contactDataList.RemoveAll(contact => contact.ContactNumber.Equals(contactNumber));

            // Speicherung JSON 
            SaveData(contactDataList, "delete");
            return true;
        }

        // Speicherung neue, zu ändernde oder zu löschende Kundendaten (Schreibprozess)
        private static void SaveData(List<InitializationContactData> contactDataList, string saveMode)
        {
            try
            {
                // Erzeugung data-Ordner, falls noch nicht vorhanden (Vermeidung von Exception)
                var directory = Path.GetDirectoryName(contactDataPath);
                if (!string.IsNullOrEmpty(directory))
                    Directory.CreateDirectory(directory);

                string contactsJSON = JsonSerializer.Serialize(contactDataList, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(contactDataPath, contactsJSON);

                // Ausgabe erfolgreiche Speicherung (abhängig von Auftragsart
                string message = saveMode == "save" ? "gespeichert" : saveMode == "update" ? "geändert" : saveMode == "delete" ? "gelöscht" : "unbekannt";
                MessageBox.Show($"Kontakt erfolgreich {message}!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                // Ausgabe Fehler beim Speichern (Ausnahmebehandlung)
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