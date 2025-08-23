using System;
using System.Drawing;
using System.Windows.Forms;

namespace Semesterprojekt
{
    public partial class Dashboard : Form
    {
        // Initialisierung Control-Gruppe "Buttons"
        private Control[] groupButtons;

        public Dashboard()
        {
            InitializeComponent();
            this.Size = new Size(485, 410);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;

            // Platzierung Buttons "Mitarbeiter hinzufügen, Kunde hinzufügen, Alle Kontakte" (inkl. Erfassung Index)
            PlacementButton(GroupButtons());
        }

        // Platzierung Buttons (fix)
        private void PlacementButton(Control[] groupField)
        {
            int tabIndexCounter = 1;
            int width = 150;
            int height = 100;
            int location = 80;

            for (int i = 0; i < groupField.Length; i++)
            {
                groupField[i].Size = new Size(width, height);

                // Buttons relevant für Tab und daher durchnummeriert
                groupField[i].TabIndex = tabIndexCounter++;
            }

            BtnDashMaNew.Location = new Point(location, location);
            BtnDashKndNew.Location = new Point(location, location + height + 10);
            BtnDashAllKntkt.Location = new Point(location + width + 10, location);
        }

        // Erstellung Array für Buttons
        private Control[] GroupButtons()
        {
            return groupButtons = new Control[]
            {
                BtnDashMaNew,
                BtnDashKndNew,
                BtnDashAllKntkt
            };
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
    }
}
