using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Semesterprojekt
{
    internal class ClientAndEmployeeNumber
    {
        // Dateipfad für JSON "clientAndEmployeeNumbers"
        private static readonly string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
        private static readonly string clientAndEmployeeNumbersPath = Path.Combine(projectRoot, "data", "clientAndEmployeeNumbers.json");
        
        // Initialisierung nächste Kunden-/Mitarbeiter Nr., d.h. immer letzte Nummer + 1
        private static string nextNumber;

        public class NumberData
        {
            public List<string> ClientNumbers { get; set; } = new List<string>();
            public List<string> EmployeeNumbers { get; set; } = new List<string>();
        }

        // Auslesen JSON für Ermittlung und Speicherung Kunden-/Mitarbeiter Nr.
        private static NumberData LoadData()
        {
            if (File.Exists(clientAndEmployeeNumbersPath))
            {
                string clientAndEmployeeNumbersJSON = File.ReadAllText(clientAndEmployeeNumbersPath);
                var clientAndEmployeeNumbersData = JsonSerializer.Deserialize<NumberData>(clientAndEmployeeNumbersJSON);
                return clientAndEmployeeNumbersData ?? new NumberData();
            }

            return new NumberData();
        }

        // Ermittlung nächste Kunden-/Mitarbeiter Nr.
        public static string GetNumberNext(bool isEmployee)
        {
            // Auslesen JSON
            var numberData = LoadData();

            // Initialisierung Präfix und Listentyp (Kunde vs. Mitarbeiter)
            string numberPrefix = isEmployee ? "MA" : "KD";
            var numberDataList = isEmployee ? numberData.EmployeeNumbers : numberData.ClientNumbers;

            // Ermittlung letzte Kunden-/Mitarbeiter Nr.            
            string lastNumber = numberDataList.LastOrDefault();

            // Parsen (dynamisch) der Kunden-/Mitarbeiter Nr. nach Präfix
            int newNumber = 1;
            if (!string.IsNullOrEmpty(lastNumber) && int.TryParse(lastNumber.Substring(numberPrefix.Length), out int parsedNumber))
            {
                newNumber = parsedNumber + 1;
            }

            // D = Decimal (Ganzzahl) und 4 = mindestens 4 Stellen, mit führenden Nullen, falls nötig
            nextNumber = $"{numberPrefix}{newNumber:D4}";

            return nextNumber;
        }

        // Speichervorgang aktuelle (nächste) Kunden-/Mitarbeiter Nr.
        public static void SaveNumberCurrent(bool isEmployee)
        {
            if (string.IsNullOrEmpty(nextNumber))
                return;

            // Auslesen JSON
            var numberData = LoadData();

            
            // Hinzufügen aktueller (nächster) Kunden-/Mitarbeiter Nr.
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

        // Speicherung aktuelle (nächste Kunden-/Mitarbeiter Nr.
        private static void SaveData(NumberData clientAndEmployeeNumbersData)
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

        // Löschung aktuelle Kunden-/Mitarbeiter Nr.
        public static void DeleteNumber(string number)
        {
            // Auslesen JSON
            var numberData = LoadData();

            // Auswahl der entsprechenden Liste auf Basis Präfix KD (Kunde) vs. MA (Mitarbeiter)
            var listData = number.StartsWith("MA") ? numberData.EmployeeNumbers : numberData.ClientNumbers;

            // Entfernung Kontakt Nr. aus entsprechender Liste
            listData.Remove(number);

            // Speicherung JSON 
            SaveData(numberData);
        }
    }
}
