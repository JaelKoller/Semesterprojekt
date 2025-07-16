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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        // Methode für Aufruf Form "KontaktErstellen" (Mitarbeiter und/oder Kunde hinzufügen)
        // Form "Dashboard" wird dabei in den Hintergrund "gestellt"
        private void KontaktErstellen()
        {
            // Initialisierung "KontaktErstellen" für Absprung via Buttons "Mitarbeiter hinzufügen" und "Kunde hinzufügen"
            var kontaktErstellenForm = new KontaktErstellen();
            kontaktErstellenForm.FormClosed += (s, arg) => this.Show();
            kontaktErstellenForm.Show();
            this.Hide();
        }

        private void BtnDashKndNew_Click(object sender, EventArgs e)
        {
            KontaktErstellen();
        }

        private void BtnDashMaNew_Click(object sender, EventArgs e)
        {
            KontaktErstellen();
        }

        private void BtnDashAllKntkt_Click(object sender, EventArgs e)
        {
            // Initialisierung "AlleKontakte" für Absprung via Button "Alle Kontakte"
            var alleKontakteForm = new AlleKontakte();
            alleKontakteForm.FormClosed += (s, arg) => this.Show();
            alleKontakteForm.Show();
            this.Hide();
        }
    }
}
