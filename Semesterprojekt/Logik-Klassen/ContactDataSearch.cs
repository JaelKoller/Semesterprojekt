using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace Semesterprojekt
{
    internal class ContactDataSearch
    {
        // Dateipfad für JSON "contacts" (Kontaktdaten-Liste)
        private static readonly string fileName = "contacts";
        private static readonly string contactDataPath = InitializationDataPathJson.DataPath(fileName);

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
                    contactDataList = new List<InitializationContactData>();

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

        // Suche und Filterung der Kontaktdaten
        public static List<InitializationContactData> SearchContactData(Dictionary<string, object> searchContactData)
        {
            // Initialisierung Filterkriterien
            string contactNumber = searchContactData.ContainsKey("ContactNumber") ? Convert.ToString(searchContactData["ContactNumber"]) : string.Empty;
            string firstName = searchContactData.ContainsKey("FirstName") ? Convert.ToString(searchContactData["FirstName"]).Trim() : string.Empty;
            string lastName = searchContactData.ContainsKey("LastName") ? Convert.ToString(searchContactData["LastName"]).Trim() : string.Empty;
            string birthday = searchContactData.ContainsKey("Birthday") ? Convert.ToString(searchContactData["Birthday"]).Trim() : string.Empty;
            bool checkEmployee = searchContactData.ContainsKey("CheckEmployee") ? Convert.ToBoolean(searchContactData["CheckEmployee"]) : false;
            bool checkClient = searchContactData.ContainsKey("CheckClient") ? Convert.ToBoolean(searchContactData["CheckClient"]) : false;
            bool checkInactive = searchContactData.ContainsKey("CheckInactive") ? Convert.ToBoolean(searchContactData["CheckInactive"]) : false;


            // Abbruch bei Fehler beim Laden der JSON-Datei
            if (!LoadData(out var contactDataList))
                return new List<InitializationContactData>();

            // Vorbereitung Kontaktdaten-Liste für direkte Filteranwendungen (je nach Ausgangslage)
            var filteredContactDataList = contactDataList.AsEnumerable();


            if (!string.IsNullOrWhiteSpace(contactNumber))
                // Suche "nur" nach Kontakt Nr. (für Anzeige)
                filteredContactDataList = filteredContactDataList.Where(contact => contact.ContactNumber.Equals(contactNumber));
            else
            {
                // Suche standardmässig ohne inaktive Kontaktdaten (nur aktive)
                if (!checkInactive)
                    filteredContactDataList = filteredContactDataList.Where(contact => contact.ContactStatus.Equals("active"));

                // Einschränkung Suche "nur" Mitarbeiter
                if (checkEmployee && !checkClient)
                    filteredContactDataList = filteredContactDataList.Where(contact => contact.TypeOfContact.Equals("Mitarbeiter"));

                // Einschränkung Suche "nur" Kunde
                if (checkClient && !checkEmployee)
                    filteredContactDataList = filteredContactDataList.Where(contact => contact.TypeOfContact.Equals("Kunde"));

                // Einschränkung Suche "FirstName"
                if (!string.IsNullOrWhiteSpace(firstName))
                    filteredContactDataList = filteredContactDataList.Where(contact => contact.Fields["FirstName"].ToLower().Contains(firstName.ToLower()));

                // Einschränkung Suche "LastName"
                if (!string.IsNullOrWhiteSpace(lastName))
                    filteredContactDataList = filteredContactDataList.Where(contact => contact.Fields["LastName"].ToLower().Contains(lastName.ToLower()));

                // Einschränkung Suche "Birthday"
                if (!string.IsNullOrWhiteSpace(birthday))
                    filteredContactDataList = filteredContactDataList.Where(contact => contact.Fields["Birthday"].Contains(birthday));
            }

            // Ausgabe Such-Resultat als Liste
            var contactSearchResult = filteredContactDataList.ToList();

            // Ausgabe 0 Treffer bei Suche mit Popup
            if (contactSearchResult.Count == 0)
                MessageBox.Show("keine Kontakte gefunden", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return contactSearchResult;
        }
    }
}