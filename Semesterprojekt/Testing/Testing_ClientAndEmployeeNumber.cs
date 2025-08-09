using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Semesterprojekt.Testing
{
    public partial class Testing_ClientAndEmployeeNumber : Form
    {
        public Testing_ClientAndEmployeeNumber()
        {
            InitializeComponent();
            
            // Ermittlung nächste Mitarbeiter Nr. (1. Lauf)
            string employeeNumberNew = ClientAndEmployeeNumber.GetNumberNext(true);
            TxtBxTestMA1.Text = employeeNumberNew;

            // Speicherung nächste Mitarbeiter Nr.
            ClientAndEmployeeNumber.SaveNumberCurrent(true);

            // Ermittlung nächste Mitarbeiter Nr. (2. Lauf)
            employeeNumberNew = ClientAndEmployeeNumber.GetNumberNext(true);
            TxtBxTestMA2.Text = employeeNumberNew;

            // Ermittlung nächste Kunden Nr. (1. Lauf)
            string clienteNumberNew = ClientAndEmployeeNumber.GetNumberNext(false);
            TxtBxTestKD1.Text = clienteNumberNew;

            // Speicherung nächste Kunden Nr.
            ClientAndEmployeeNumber.SaveNumberCurrent(false);

            // Ermittlung nächste Kunden Nr. (2. Lauf)
            clienteNumberNew = ClientAndEmployeeNumber.GetNumberNext(false);
            TxtBxTestKD2.Text = clienteNumberNew;
        }
    }
}
