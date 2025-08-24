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

        // Initialisierung verwendete Label-/Control-Gruppen
        private System.Windows.Forms.Label[] groupLabel;
        private Control[] groupField;
        private System.Windows.Forms.Label[] groupLabelToolTip;

        // Initialisierung verwendeter Index-Counter
        private int tabIndexCounter = 1;

        // Initialisierung verwendeter BackColor
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

            // Erstellung Arrays für Labels und Eingabefelder
            groups = new AlleKontakteLabelAndControlGroups();

            // Initialisierung verwendete Label-/Control-Gruppen
            groupLabel = groups.GroupLabel(this);
            groupField = groups.GroupField(this);
            groupLabelToolTip = groups.GroupLabelToolTip(this);

            Design();
            InitializationLabelToolTip();

            // Initialisierung (Registrierung) ESC für Rückkehr zu Dashboard (analog Button)
            this.CancelButton = BtnAllKntktHome;
            // Initialisierung (Registrierung) Enter für Suche (analog Button)
            this.AcceptButton = BtnAllKntktSuchen;
            // Initialisierung (Registrierung) Doppelklick auf Listeneintrag
            LbAllKntktSuchAusg.DoubleClick += LbAllKntktSuchAusg_DoubleClick;
        }

        // Design (Platzierung) der Eingabe-Felder usw.
        private void Design()
        {
            // Platzierung Titel "Suchen nach:"
            LblAllKntktSuchen.Location = new Point(50, 50);

            // Platzierung Labels und Eingabefelder
            // Zählerstart (Index) für Labels und Eingabefelder bei 1
            PlacementLabelAndField(groupLabel, groupField, ref tabIndexCounter);

            // Platzierung Buttons "Suchen", "Suche zurücksetzen" und "zurück zu Dashboard"
            // Zählerstart (Index) für Buttons fortführend
            BtnAllKntktSuchen.Size = new Size(340, 30);
            BtnAllKntktSuchen.Location = new Point(50, 290);
            BtnAllKntktSuchen.TabIndex = tabIndexCounter++;
            BtnAllKntktSucheReset.Size = new Size(340, 30);
            BtnAllKntktSucheReset.Location = new Point(50, 585);
            BtnAllKntktSucheReset.TabIndex = tabIndexCounter++;
            BtnAllKntktHome.Size = new Size(90, 50);
            BtnAllKntktHome.Location = new Point(320, 20);
            BtnAllKntktHome.TabIndex = tabIndexCounter++;

            // Platzierung Suchausgabe und Anzahl Treffer
            // Zählerstart (Index) für restliche Labels mit 0
            LbAllKntktSuchAusg.Size = new Size(340, 200);
            LbAllKntktSuchAusg.Location = new Point(50, 340);
            LbAllKntktSuchAusg.TabStop = false;
            LblAllKntktAnzSuchAusg.Size = new Size(50, 20);
            LblAllKntktAnzSuchAusg.Location = new Point(50, 550);
            LblAllKntktAnzSuchAusg.TabStop = false;
        }

        // Platzierung Labels und Eingabefelder (dynamisch)
        private void PlacementLabelAndField(System.Windows.Forms.Label[] groupLabel, Control[] groupField, ref int tabIndexCounter)
        {
            int startLocation = 100;
            int labelXAchse = 50;
            int controlXAchse = 200;

            for (int i = 0; i < groupField.Length; i++)
            {
                groupLabel[i].Location = new Point(labelXAchse, startLocation);
                groupField[i].Location = new Point(controlXAchse, startLocation);

                startLocation += 30;

                // Label irrelevant für Tab und daher mit TabStop "false"
                groupLabel[i].TabStop = false;
                // Eingabefeld relevant für Tab und daher durchnummeriert (Start bei 1)
                groupField[i].TabIndex = tabIndexCounter++;
            }
        }

        // Erstellung ToolTip für spezifische Labels (zur besseren Verständlichkeit)
        private void InitializationLabelToolTip()
        {
            var initializationSetToolTip = SetToolTip();
            var setToolTip = new SetToolTip();
            setToolTip.SetLabelToolTip(initializationSetToolTip);
        }

        // Initialisierung Argumente (Inhalt) für Klasse "SetToolTip"
        private InitializationLabelsToolTip SetToolTip()
        {
            return new InitializationLabelsToolTip
            {
                GroupLabelToolTip = groupLabelToolTip,
                Birthday = LblAllKntktBirthday,
                SearchEmployeeContacts = LblAllKntktMa,
                SearchClientContacts = LblAllKntktKunde,
                SearchInactiveContacts = LblAllKntktInaktiv
            };
        }

        // Erstellung Dictonary für Suche
        private Dictionary<string, object> GroupSeach()
        {
            return new Dictionary<string, object>
            {
                { "FirstName", TxtAllKntktVorname.Text.Trim() },
                { "LastName", TxtAllKntktName.Text.Trim() },
                { "Birthday", TxtAllKntktBirthday.Text.Trim() },
                { "CheckEmployee", ChkBAllKntktMa.Checked },
                { "CheckClient", ChkBAllKntktKunde.Checked },
                { "CheckInactive", ChkBAllKntktInaktiv.Checked }
            };
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
                List<InitializationContactData> contactSearchResult = ContactDataSearch.SearchContactData(GroupSeach());
                // Zwischenspeicherung Trefferliste für Doppelklick auf Listeneintrag
                lastContactSearchResult = contactSearchResult;
                // Filterung Trefferliste für Ausgabe
                var showContactSearchResult = contactSearchResult.Select(contacts =>
                $"{contacts.Fields["FirstName"]} {contacts.Fields["LastName"]}, {contacts.Fields["Birthday"]}, {contacts.Fields["City"]}").ToArray();

                // Ausgabe der Trefferliste in ListBox
                LbAllKntktSuchAusg.Items.AddRange(showContactSearchResult);
                LblAllKntktAnzSuchAusg.Text = $"Anzahl Treffer: {contactSearchResult.Count}";
                
                if (contactSearchResult.Count == 1)
                {
                    OpenAnsichtKontakt(contactSearchResult[0]);
                }
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

        // Prüfung Format auf TT.MM.JJJJ (für OK-Fall Rückgabe "TRUE")
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
            {
                MessageBox.Show(errorMessage, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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