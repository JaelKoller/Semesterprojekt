using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Semesterprojekt
{
    public partial class AlleKontakte : Form
    {
        // Initialisierung Klasse "AlleKontakteLabelAndControlGroups"
        private AlleKontakteLabelAndControlGroups groups;

        // Initialisierung Label-/Control-Gruppen
        internal System.Windows.Forms.Label[] groupLabel;
        internal Control[] groupField;
        internal System.Windows.Forms.Label[] groupLabelToolTip;

        // Initialisierung Index-Counter für TabStop
        internal int tabIndexCounter = 1;

        // Initialisierung BackColor (Hintergrundfarbe)
        private Color backColorOK = SystemColors.Window;
        private Color backColorNOK = Color.LightPink;

        // Initialisierung letzte Trefferliste (Suchresultat) für Doppelklick auf Listeneintrag
        internal List<InitializationContactData> lastContactSearchResult;

        public AlleKontakte()
        {
            InitializeComponent();
            this.Size = new Size(450, 685);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;

            groups = new AlleKontakteLabelAndControlGroups();
            groupLabel = groups.GroupLabel(this);
            groupField = groups.GroupField(this);
            groupLabelToolTip = groups.GroupLabelToolTip(this);

            AlleKontakteDesign.Design(this);
            AlleKontakteDesign.InitializationLabelToolTip(this);

            // Initialisierung (Registrierung) ESC für Rückkehr zu Dashboard (analog Button)
            this.CancelButton = BtnAllKntktHome;
            // Initialisierung (Registrierung) Enter für Suche (analog Button)
            this.AcceptButton = BtnAllKntktSuchen;
            // Initialisierung (Registrierung) Doppelklick auf Listeneintrag
            LbAllKntktSuchAusg.DoubleClick += LbAllKntktSuchAusg_DoubleClick;
        }

        // Klick Button "Suchen"
        private void BtnAllKntktSuchen_Click(object sender, EventArgs e)
        {
            // Bereinigung der Trefferausgabe
            CleanSearchResult();

            bool checkDateOfBirth = CheckDateOfBirth();

            if (checkDateOfBirth)
            {
                // Kontaktsuche auf Basis der erfassten Parameter
                var contactSearchResult = ContactDataSearch.SearchContactData(AlleKontakteInitializations.GroupSeach(this));
                // Zwischenspeicherung Trefferliste für Doppelklick auf Listeneintrag
                lastContactSearchResult = contactSearchResult;
                // Filterung Trefferliste für Ausgabe
                var showContactSearchResult = contactSearchResult.Select(contacts =>
                $"{contacts.Fields["FirstName"]} {contacts.Fields["LastName"]}, {contacts.Fields["Birthday"]}, {contacts.Fields["City"]}").ToArray();

                // Ausgabe der Trefferliste in ListBox
                LbAllKntktSuchAusg.Items.AddRange(showContactSearchResult);
                LblAllKntktAnzSuchAusg.Text = $"Anzahl Treffer: {contactSearchResult.Count}";
                
                if (contactSearchResult.Count == 1)
                    OpenAnsichtKontakt(contactSearchResult[0]);
            }
        }

        // Doppelklick auf Listeneintrag in Trefferliste
        private void LbAllKntktSuchAusg_DoubleClick(object sender, EventArgs e)
        {
            int indexContact = LbAllKntktSuchAusg.SelectedIndex;
            // Sicherheitsabfrage für Verhinderung Abstürze
            if (indexContact < 0 || lastContactSearchResult == null || indexContact >= lastContactSearchResult.Count)
                return;

            OpenAnsichtKontakt(lastContactSearchResult[indexContact]);
        }

        // Öffnen des Kontakts mit allen Kontaktdaten (in AnsichtKontakt)
        private void OpenAnsichtKontakt(InitializationContactData contact)
        {
            // Bereinigung der Trefferausgabe (Verhinderung Fehlanzeige bei Rückkehr)
            CleanSearchResult();

            // Initialisierung "AnsichtKontakt" für Absprung via Buttons "Suchen" und "Doppelklick in Trefferliste"
            var ansichtKontaktForm = new AnsichtKontakt(contact);

            // Stabilisierung für das Zurückkehren zu "AlleKontakte"
            ansichtKontaktForm.FormClosed += (s, arg) =>
            {
                this.Show();
                this.Activate();
                this.PerformLayout();
                this.Invalidate(true);
                this.Update();
                this.Refresh();

                // Fokus auf erstes Feld (analog Start)
                this.BeginInvoke((Action)(() => groupField.First(field => field.TabIndex == 1).Focus()));
            };

            // Mitgabe "this" als Owner für "AnsichtKontakt", damit beide Fenster bei "zurück zum Dashboard" geschlossen werden
            ansichtKontaktForm.Show(this);
            this.Hide();
        }

        // Klick Button "Zurück zum Dashboard"
        private void BtnAllKntktHome_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Prüfung Format auf TT.MM.JJJJ (für OK-Fall Rückgabe "true")
        private bool CheckDateOfBirth()
        {
            string errorMessage = string.Empty;
            bool dateOfBirth = CheckAndValidationDateFields.CheckDateField(TxtAllKntktBirthday, "Geburtsdatum", false, out errorMessage);
            
            if (dateOfBirth)
            {
                TxtAllKntktBirthday.BackColor = backColorOK;
                return true;
            }

            TxtAllKntktBirthday.BackColor = backColorNOK;

            // Erzeugung MessageBox (Popup) bei fehlerhaften Eingaben (exkl. leeres Feld)
            if (!string.IsNullOrWhiteSpace(errorMessage))
                MessageBox.Show(errorMessage, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);

            TxtAllKntktBirthday.Focus();
            return false;
        }

        // Klick Button "Suche zurücksetzen"
        private void BtnAllKntktSucheReset_Click(object sender, EventArgs e)
        {
            CleanGroupField();
            CleanSearchResult();

            // Fokus auf erstes Feld (analog Start)
            groupField.First(field => field.TabIndex == 1).Focus();
        }

        // Bereinigung der Eingabefelder
        private void CleanGroupField()
        {
            foreach (Control field in groupField)
            {
                switch (field)
                {
                    case System.Windows.Forms.TextBox txtbxField:
                        txtbxField.Clear();
                        field.BackColor = backColorOK;
                        break;

                    case CheckBox chckbxField:
                        chckbxField.Checked = false;
                        break;
                }
            }
        }

        // Bereinigung der Trefferausgabe (Resultat der Suche)
        private void CleanSearchResult()
        {
            LbAllKntktSuchAusg.Items.Clear();
            LblAllKntktAnzSuchAusg.Text = "Anzahl Treffer: 0";
            lastContactSearchResult = null;
        }
    }
}