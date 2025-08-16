using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Semesterprojekt
{
    public partial class AlleKontakte : Form
    {
        // Initialisierung verwendete Label-/Control-Gruppen
        private System.Windows.Forms.Label[] groupLabel;
        private Control[] groupField;
        private System.Windows.Forms.Label[] groupLabelToolTip;
        private Dictionary<string, object> groupSearch;

        // Initialisierung verwendeter Index-Counter
        private int tabIndexCounter = 1;

        // Initialisierung verwendeter BackColor
        private Color backColorOK = SystemColors.Window;
        private Color backColorNOK = Color.LightPink;

        public AlleKontakte()
        {
            InitializeComponent();
            this.Size = new Size(450, 640);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;

            // Initialisierung verwendete Label-/Control-Gruppen
            groupLabel = GroupLabel();
            groupField = GroupField();
            groupLabelToolTip = GroupLabelToolTip();
            groupSearch = GroupSeach();

            Design();
            InitializationLabelToolTip();
        }

        // Design (Platzierung) der Eingabe-Felder usw.
        private void Design()
        {
            // Platzierung Titel "Suchen nach:"
            LblAllKntktSuchen.Location = new Point(50, 50);

            // Platzierung Labels und Eingabefelder
            // Zählerstart (Index) für Labels und Eingabefelder bei 1
            PlacementLabelAndField(groupLabel, groupField, tabIndexCounter);

            // Platzierung Buttons "Suchen" und "zurück zu Dashboard"
            BtnAllKntktSuchen.Size = new Size(340, 30);
            BtnAllKntktSuchen.Location = new Point(50, 290);
            BtnAllKntktHome.Size = new Size(90, 50);
            BtnAllKntktHome.Location = new Point(320, 20);

            // Platzierung Suchausgabe
            LbAllKntktSuchAusg.Size = new Size(340, 200);
            LbAllKntktSuchAusg.Location = new Point(50, 340);

            LblAllKntktAnzSuchAusg.Size = new Size(50, 20);
            LblAllKntktAnzSuchAusg.Location = new Point(50, 550);
        }

        // Platzierung Labels und Eingabefelder (dynamisch)
        private void PlacementLabelAndField(System.Windows.Forms.Label[] groupLabel, Control[] groupField, int indexCounter)
        {
            int startLocation = 100;
            int labelXAchse = 50;
            int controlXAchse = 200;
            int tabIndexCounter = indexCounter;

            for (int i = 0; i < groupField.Length; i++)
            {
                groupLabel[i].Location = new Point(labelXAchse, startLocation);
                groupField[i].Location = new Point(controlXAchse, startLocation);

                startLocation += 30;

                // Label irrelevant für Tab und daher fix mit 0
                groupLabel[i].TabIndex = 0;
                // Eingabefeld relevant für Tab und daher durchnummeriert (Start bei 1)
                groupField[i].TabIndex = tabIndexCounter++;
            }
        }

        // Erstellung Array für Labels
        private System.Windows.Forms.Label[] GroupLabel()
        {
            groupLabel = new System.Windows.Forms.Label[]
            {
                LblAllKntktVorname,
                LblAllKntktName,
                LblAllKntktBirthday,
                LblAllKntktMa,
                LblAllKntktKunde,
                LblAllKntktInaktiv
            };

            return groupLabel;
        }

        // Erstellung Array für Labels für ToolTip
        private System.Windows.Forms.Label[] GroupLabelToolTip()
        {
            groupLabelToolTip = new System.Windows.Forms.Label[]
            {
                LblAllKntktBirthday,
                LblAllKntktMa,
                LblAllKntktKunde,
                LblAllKntktInaktiv
            };

            return groupLabelToolTip;
        }

        // Erstellung Array für Eingabefelder
        private Control[] GroupField()
        {
            groupField = new Control[]
            {
                TxtAllKntktVorname,
                TxtAllKntktName,
                TxtAllKntktBirthday,
                ChkBAllKntktMa,
                ChkBAllKntktKunde,
                ChkBAllKntktInaktiv
            };

            return groupField;
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
            return groupSearch = new Dictionary<string, object>
            {
                { "FirstName", TxtAllKntktVorname.Text },
                { "LastName", TxtAllKntktName.Text },
                { "Birthday", TxtAllKntktBirthday.Text },
                { "CheckEmployee", ChkBAllKntktMa.Checked },
                { "CheckClient", ChkBAllKntktKunde.Checked },
                { "CheckInactive", ChkBAllKntktInaktiv.Checked }
            };
        }

        private void BtnAllKntktSuchen_Click(object sender, EventArgs e)
        {
            bool checkDateOfBirth = CheckDateOfBirth();

            if (checkDateOfBirth)
            {
                // Kontaktsuche auf Basis der erfassten Parameter
                List<InitializationContactData> contactSearchResult = ContactDataSearch.SeachContactData(GroupSeach());
                var showContactSearchResult = contactSearchResult.Select(contacts =>
                $"{contacts.Fields["FirstName"]} {contacts.Fields["LastName"]}, {contacts.Fields["Birthday"]}, {contacts.Fields["City"]}").ToArray();

                // Ausgabe des Resultats der Kontaktsuche
                LbAllKntktSuchAusg.Items.Clear();
                LbAllKntktSuchAusg.Items.AddRange(showContactSearchResult);
                LblAllKntktAnzSuchAusg.Text = $"Anzahl Treffer: {contactSearchResult.Count}";
                
                if (contactSearchResult.Count == 1)
                {
                    // Ausgabe und Weiterverarbeitung Kontakt Nr. von Resultat der Kontaktsuche
                    string showContactNumberSearchResult = contactSearchResult[0].ContactNumber.ToString();
                    List <InitializationContactData> contactShowhResult = ContactDataSearch.ShowContactData(new Dictionary<string, object> { { "ContactNumber", showContactNumberSearchResult } });
                    
                    // Initialisierung "AnsichtKontakt" für Absprung via Button "Suchen"
                    var ansichtKontaktForm = new AnsichtKontakt();
                    ansichtKontaktForm.FormClosed += (s, arg) => this.Show();
                    // Mitgabe "this" als Owner für "AnsichtKontakt", damit beide Fenster bei "zurück zum Dashboard" geschlossen werden
                    ansichtKontaktForm.Show(this);
                    this.Hide();
                }
            }
        }

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
    }
}