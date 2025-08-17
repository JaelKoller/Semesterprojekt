using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Semesterprojekt.ClientAndEmployeeNumber;

namespace Semesterprojekt
{
    internal class ClientAndEmployeeNumber
    {
        // Dateipfad für JSON "clientAndEmployeeNumbers" (Liste für Kunden/Mitarbeiter Nrn.)
        private static readonly string fileName = "clientAndEmployeeNumbers";
        private static readonly string clientAndEmployeeNumbersPath = InitializationDataPathJson.DataPath(fileName);

        // Initialisierung nächste Kunden-/Mitarbeiter Nr., d.h. immer letzte Nummer + 1
        private static string nextNumber;

        public class NumberData
        {
            public List<string> ClientNumbers { get; set; } = new List<string>();
            public List<string> EmployeeNumbers { get; set; } = new List<string>();
        }

        // Auslesen JSON für Ermittlung, Speicherung und Löschung Kunden/Mitarbeiter Nr.
        private static bool LoadData(out NumberData numberData)
        {
            try
            {
                if (File.Exists(clientAndEmployeeNumbersPath))
                {
                    string clientAndEmployeeNumbersJSON = File.ReadAllText(clientAndEmployeeNumbersPath);
                    numberData = JsonSerializer.Deserialize<NumberData>(clientAndEmployeeNumbersJSON) ?? new NumberData();
                }

                else
                {
                    numberData = new NumberData();
                }

                return true;
            }

            catch (Exception exception)
            {
                // Ausgabe Fehler beim Laden (Ausnahmebehandlung)
                ShowMessageBox($"Fehler beim Laden der JSON-Datei '{fileName}': {exception}");
                numberData = null;
                return false;
            }
        }

        // Ermittlung nächste Kunden/Mitarbeiter Nr.
        public static string GetNumberNext(bool isEmployee)
        {
            // Abbruch bei Fehler beim Laden der JSON-Datei
            if (!LoadData(out var numberData))
                return string.Empty;

            // Initialisierung Präfix und Listentyp (Kunde vs. Mitarbeiter)
            string numberPrefix = isEmployee ? "MA" : "KD";
            var numberDataList = isEmployee ? numberData.EmployeeNumbers : numberData.ClientNumbers;

            // Ermittlung letzte Kunden-/Mitarbeiter Nr.            
            string lastNumber = numberDataList.LastOrDefault();

            // Parsen (dynamisch) der Kunden/Mitarbeiter Nr. nach Präfix
            int newNumber = 1;
            if (!string.IsNullOrEmpty(lastNumber) && int.TryParse(lastNumber.Substring(numberPrefix.Length), out int parsedNumber))
            {
                newNumber = parsedNumber + 1;
            }

            // D = Decimal (Ganzzahl) und 4 = mindestens 4 Stellen, mit führenden Nullen, falls nötig
            nextNumber = $"{numberPrefix}{newNumber:D4}";

            return nextNumber;
        }

        // Speichervorgang aktuelle (nächste) Kunden/Mitarbeiter Nr.
        public static void SaveNumberCurrent(bool isEmployee)
        {
            // Abbruch bei fehlender aktuellen (nächster) Kunden/Mitarbeiter Nr. ODER Abbruch bei Fehler beim Laden der JSON-Datei
            if (string.IsNullOrEmpty(nextNumber) || !LoadData(out var numberData))
                return;
            
            // Hinzufügen aktueller (nächster) Kunden/Mitarbeiter Nr.
            if (isEmployee)
            {
                numberData.EmployeeNumbers.Add(nextNumber);
            }

            else
            {
                numberData.ClientNumbers.Add(nextNumber);
            }
            
            // Speicherung JSON
            SaveData(numberData);
        }

        // Löschung aktuelle Kunden/Mitarbeiter Nr.
        public static void DeleteNumber(string number)
        {
            // Abbruch bei Fehler beim Laden der JSON-Datei
            if (!LoadData(out var numberData))
                return;

            // Auswahl der entsprechenden Liste auf Basis Präfix KD (Kunde) vs. MA (Mitarbeiter)
            var listData = number.StartsWith("MA") ? numberData.EmployeeNumbers : numberData.ClientNumbers;

            // Entfernung Kontakt Nr. aus entsprechender Liste
            listData.Remove(number);

            // Speicherung JSON 
            SaveData(numberData);
        }

        // Speicherung neue oder zu löschende Kunden/Mitarbeiter Nr.
        private static void SaveData(NumberData clientAndEmployeeNumbersData)
        {
            try
            {
                // Erzeugung data-Ordner, falls noch nicht vorhanden (Vermeidung von Exception)
                var directory = Path.GetDirectoryName(clientAndEmployeeNumbersPath);
                if (!string.IsNullOrEmpty(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string clientAndEmployeeNumbersJSON = JsonSerializer.Serialize(clientAndEmployeeNumbersData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(clientAndEmployeeNumbersPath, clientAndEmployeeNumbersJSON);
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
