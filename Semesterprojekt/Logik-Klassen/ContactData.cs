using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Semesterprojekt
{
    internal class ContactData
    {
        // Dateipfad für Kontaktdaten-Listen
        private static readonly string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
        private static readonly string contactDataPath = Path.Combine(projectRoot, "data", "contacts.json");

        // Speicherung der Kontaktdaten in JSON "contacts"
        public static bool SaveContactData(string typeOfContactNew, string contactNumberNew, Control[] groupFieldEmployeesAndCustomers, Control[] groupFieldEmployees)
        {
            try
            {
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

                // Laden der JSON-Datei (falls vorhanden)
                List<InitializationContactData> contactList = new List<InitializationContactData>();

                if (File.Exists(contactDataPath))
                {
                    string contatcsJSON = File.ReadAllText(contactDataPath);

                    if (!string.IsNullOrWhiteSpace(contatcsJSON))
                    {
                        contactList = JsonSerializer.Deserialize<List<InitializationContactData>>(contatcsJSON) ?? new List<InitializationContactData>();
                    }
                }

                // Duplikatencheck mit Bestätigung durch User (bei Nein "Abbruch")
                if (!CheckDuplicateContact(contactList, contact))
                {
                    return false;
                }

                // Hinzufügen neuer Kontakt zur neuen Liste
                contactList.Add(contact);

                // Konvertierung neue Liste in JSON
                string updatedJson = JsonSerializer.Serialize(contactList, new JsonSerializerOptions { WriteIndented = true });

                // (Über-)Schreibung der JSON-Datei mit neuer Liste
                File.WriteAllText(contactDataPath, updatedJson);

                // Ausgabe erfolgreiche Speicherung (userfreundlich)
                MessageBox.Show("Kontakt erfolgreich gespeichert!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            catch (Exception exception)
            {
                // Ausgabe Fehler beim Speichern (Ausnahmebehandlung)
                MessageBox.Show($"Fehler beim Speichern:{exception}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
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
    }
}
