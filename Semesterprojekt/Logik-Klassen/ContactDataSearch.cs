using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Semesterprojekt
{
    internal class ContactDataSearch
    {
        // Dateipfad für JSON "contacts" (Kontaktdaten-Listen)
        private static readonly string fileName = "contacts.json";
        private static readonly string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
        private static readonly string contactDataPath = Path.Combine(projectRoot, "data", fileName);

        // Auslesen JSON für Ermittlung Kontaktdaten
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
                MessageBox.Show($"Fehler beim Laden der JSON-Datei '{fileName}': {exception}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                contactDataList = null;
                return false;
            }
        }

        // Suche der Kontaktdaten
        public static List<String> SeachContactData(Dictionary<string, object> searchContactData)
        {
            // Initialisierung Filterkriterien
            string firstName = Convert.ToString(searchContactData["FirstName"]).Trim();
            string lastName = Convert.ToString(searchContactData["LastName"]).Trim();
            string birthday = Convert.ToString(searchContactData["Birthday"]).Trim();
            bool checkEmployee = Convert.ToBoolean(searchContactData["CheckEmployee"]);
            bool checkClient = Convert.ToBoolean(searchContactData["CheckClient"]);
            bool checkInactive = Convert.ToBoolean(searchContactData["CheckInactive"]);


            // Abbruch bei Fehler beim Laden der JSON-Datei
            if (!LoadData(out var contactDataList))
                return new List<string>();

            // Vorbereitung Kontaktdaten-Liste für direkte Filteranwendungen (je nach Ausgangslage)
            var filteredContactDataList = contactDataList.AsEnumerable();


            // Suche standardmässig ohne inaktive Kontaktdaten (nur aktive)
            if (!checkInactive)
            {
                filteredContactDataList = filteredContactDataList.Where(contact => contact.ContactStatus.Equals("active"));
            }
          
            // Einschränkung Suche "nur" Mitarbeiter
            if (checkEmployee && !checkClient)
            {
                filteredContactDataList = filteredContactDataList.Where(contact => contact.TypeOfContact.Equals("Mitarbeiter"));
            }

            // Einschränkung Suche "nur" Kunde
            if (checkClient && !checkEmployee)
            {
                filteredContactDataList = filteredContactDataList.Where(contact => contact.TypeOfContact.Equals("Kunde"));
            }

            // Einschränkung Suche "FirstName"
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                filteredContactDataList = filteredContactDataList.Where(contact => contact.Fields["FirstName"].ToLower().Contains(firstName.ToLower()));
            }

            // Einschränkung Suche "LastName"
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                filteredContactDataList = filteredContactDataList.Where(contact => contact.Fields["LastName"].ToLower().Contains(lastName.ToLower()));
            }

            // Einschränkung Suche "Birthday"
            if (!string.IsNullOrWhiteSpace(birthday))
            {
                filteredContactDataList = filteredContactDataList.Where(contact => contact.Fields["Birthday"].Contains(birthday));
            }

            // Ausgabe Such-Resultat mit Vorname, Name und Geburtsdatum
            var contactSearchResult = filteredContactDataList.Select(contacts =>
            { return $"{contacts.Fields["FirstName"]} {contacts.Fields["LastName"]}, {contacts.Fields["Birthday"]}"; }).ToList();

            // Ausgabe 0 Treffer bei Suche
            if (contactSearchResult.Count == 0)
            {
                MessageBox.Show("keine Kontakte gefunden", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return contactSearchResult;
        }
    }
}
