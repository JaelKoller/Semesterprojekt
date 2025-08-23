using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace Semesterprojekt.Testing
{
    public partial class Testing_ClientAndEmployeeNumber : Form
    {
        // Dateipfad für JSON "clientAndEmployeeNumbers"
        private static readonly string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
        private static readonly string clientAndEmployeeNumbersPath = Path.Combine(projectRoot, "data", "clientAndEmployeeNumbers.json");

        // Initialisierung Counter-Variablen
        int employeeNumberCount;
        int clientNumberCount;

        public Testing_ClientAndEmployeeNumber()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            // Ermittlung nächste Mitarbeiter Nr. (1. Lauf)
            string employeeNumberNewFirst = ClientAndEmployeeNumber.GetNumberNext(true);
            TxtBxMaNrFirst.Text = employeeNumberNewFirst;

            // Speicherung nächste Mitarbeiter Nr.
            ClientAndEmployeeNumber.SaveNumberCurrent(true);

            // Ermittlung nächste Mitarbeiter Nr. (2. Lauf)
            string employeeNumberNewSecond = ClientAndEmployeeNumber.GetNumberNext(true);
            TxtBxMaNrSecond.Text = employeeNumberNewSecond;

            // Ermittlung nächste Kunden Nr. (1. Lauf)
            string clienteNumberNewFirst = ClientAndEmployeeNumber.GetNumberNext(false);
            TxtBxKdNrFirst.Text = clienteNumberNewFirst;

            // Speicherung nächste Kunden Nr.
            ClientAndEmployeeNumber.SaveNumberCurrent(false);

            // Ermittlung nächste Kunden Nr. (2. Lauf)
            string clienteNumberNewSecond = ClientAndEmployeeNumber.GetNumberNext(false);
            TxtBxKdNrSecond.Text = clienteNumberNewSecond;

            // Ermittlung Anzahl Mitarbeiter und Kunden (vor Löschung letzter Nummern)
            ClientAndEmployeeNumberCounter();
            NumMaCountFirst.Value = employeeNumberCount;
            NumKdCountFirst.Value = clientNumberCount;

            // Löschung letzter Nummern
            ClientAndEmployeeNumber.DeleteNumber(employeeNumberNewFirst);
            ClientAndEmployeeNumber.DeleteNumber(clienteNumberNewFirst);

            // Ermittlung Anzahl Mitarbeiter und Kunden (nach Löschung letzter Nummern)
            ClientAndEmployeeNumberCounter();
            NumMaCountSecond.Value = employeeNumberCount;
            NumKdCountSecond.Value = clientNumberCount;
        }

        private void ClientAndEmployeeNumberCounter()
        {
            var json = File.ReadAllText(clientAndEmployeeNumbersPath);
            var data = JsonSerializer.Deserialize<ClientAndEmployeeNumber.NumberData>(json);
            employeeNumberCount = data.EmployeeNumbers.Count;
            clientNumberCount = data.ClientNumbers.Count();
            
        }
    }
}
