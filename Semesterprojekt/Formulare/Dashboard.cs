using System;
using System.Drawing;
using System.Windows.Forms;

namespace Semesterprojekt
{
    public partial class Dashboard : Form
    {
        // Initialisierung Klasse "DashboardLabelAndControlGroups"
        private DashboardLabelAndControlGroups groups;

        // Initialisierung Control-Gruppe "Buttons"
        private Control[] groupButtons;

        public Dashboard()
        {
            InitializeComponent();
            this.Size = new Size(485, 410);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;

            // Erstellung Array für Buttons und Platzierung (inkl. TabIndex)
            groups = new DashboardLabelAndControlGroups();
            groupButtons = groups.GroupButtons(this);
            DashboardDesign.PlacementButton(this, groupButtons);

            // Initialisierung (Registrierung) ESC für Beendung Programm (analog Button)
            this.CancelButton = BtnDashClose;
        }

        // Methode für Aufruf Form "KontaktErstellen" (Mitarbeiter und/oder Kunde hinzufügen)
        // Form "Dashboard" wird dabei in den Hintergrund "gestellt"
        private void KontaktErstellen(string typeOfContact)
        {
            // Initialisierung "KontaktErstellen" für Absprung via Buttons "Mitarbeiter hinzufügen" und "Kunde hinzufügen"
            var kontaktErstellenForm = new KontaktErstellen(typeOfContact);

            // Stabilisierung für das Zurückkehren zum "Dashboard"
            kontaktErstellenForm.FormClosed += (s, arg) =>
            {
                this.Show();
                this.Activate();
                this.PerformLayout();
                this.Invalidate(true);
                this.Update();
                this.Refresh();
            };

            kontaktErstellenForm.Show();
            this.Hide();
        }

        // Klick Button "Kunde hinzufügen"
        private void BtnDashKndNew_Click(object sender, EventArgs e)
        {
            KontaktErstellen("kunde");
        }

        // Klick Button "Mitarbeiter hinzufügen"
        private void BtnDashMaNew_Click(object sender, EventArgs e)
        {
            KontaktErstellen("mitarbeiter");
        }

        // Klick Button "Alle Kontakte"
        private void BtnDashAllKntkt_Click(object sender, EventArgs e)
        {
            // Initialisierung "AlleKontakte" für Absprung via Button "Alle Kontakte"
            var alleKontakteForm = new AlleKontakte();
            alleKontakteForm.FormClosed += (s, arg) => this.Show();
            alleKontakteForm.Show();
            this.Hide();
        }

        // Klick Button "Programm beenden"
        private void BtnDashClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
