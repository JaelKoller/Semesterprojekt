using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Semesterprojekt
{
    public partial class KontaktErstellen : Form
    {
        // Initalisierung String "typeOfContact" für "Speichern und neuer Kontakt erstellen"
        string typeOfContactNew;
        
        public KontaktErstellen(string typeOfContact)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Initalisierung Radio-Button bzgl. Kontaktart
            typeOfContactNew = typeOfContact;
            if (typeOfContactNew == "mitarbeitende")
            {
                RdbCreatKntktMa.Checked = true;
            }
            else if (typeOfContactNew == "kunde")
            {
                RdbCreatKntktKunde.Checked = true;
            }

        }

        private void CmdCreateKntktKontaktErstellen_Click(object sender, EventArgs e)
        {
            this.FormClosed += (s, arg) =>
            {
                // Erstellung neues Form "KontaktErstellen"
                var kontaktErstellenForm = new KontaktErstellen(typeOfContactNew);
                kontaktErstellenForm.Show();
            };
            this.Close();
        }

        private void CmdCreateKntktDashboard_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
