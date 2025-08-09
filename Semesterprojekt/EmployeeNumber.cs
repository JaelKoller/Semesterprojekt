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
    internal class EmployeeNumber
    {
        // Dateipfad für JSON
        private static readonly string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
        private static readonly string employeeNumbersPath = Path.Combine(projectRoot, "data", "employeeNumbers.json");
        private static string employeeNumberNext;

        public class EmployeeNumberData
        {
            public List<string> EmployeeNumbers { get; set; } = new List<string>();
        }

        public static string GetEmployeeNumberNext()
        {
            var employeeNumberData = LoadData();
            string employeeNumberLast = employeeNumberData.EmployeeNumbers.LastOrDefault();

            int employeeNumberNew = 1;
            if (!string.IsNullOrEmpty(employeeNumberLast) && int.TryParse(employeeNumberLast.Substring(2), out int employeeNumberParsed))
            {
                employeeNumberNew = employeeNumberParsed + 1;
            }

            // D = Decimal (Ganzzahl) und 4 = mindestens 4 Stellen, mit führenden Nullen, falls nötig
            employeeNumberNext = $"MJ{employeeNumberNew:D4}";
            return employeeNumberNext;
        }

        public static void SaveEmployeeNumberCurrent()
        {
            if (!string.IsNullOrEmpty(employeeNumberNext))
            {
                var employeeNumberData = LoadData();
                employeeNumberData.EmployeeNumbers.Add(employeeNumberNext);
                SaveData(employeeNumberData);
            }
        }

        private static EmployeeNumberData LoadData()
        {
            if (File.Exists(employeeNumbersPath))
            {
                string employeeNumbersJSON = File.ReadAllText(employeeNumbersPath);
                var employeeNumbersData = JsonSerializer.Deserialize<EmployeeNumberData>(employeeNumbersJSON);
                return employeeNumbersData ?? new EmployeeNumberData();
            }

            return new EmployeeNumberData();
        }

        private static void SaveData(EmployeeNumberData employeeNumberData)
        {
            string employeeNumbersJSON = JsonSerializer.Serialize(employeeNumberData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(employeeNumbersPath, employeeNumbersJSON);
        }

        // TEST
    }
}
