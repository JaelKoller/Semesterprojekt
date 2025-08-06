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
    public partial class AnsichtKontakt : Form
    {
        // Initialisierung mehrfach verwendeter Label-/Control-Gruppen
        private System.Windows.Forms.Label[] groupLabelEmployeesAndCustomers;
        private Control[] groupFieldEmployeesAndCustomers;
        private System.Windows.Forms.Label[] groupLabelEmployees;
        private Control[] groupFieldEmployees;
        private Control[] groupFieldNotes;
        private Control[] groupButtons;
        private Control[] checkFieldIgnore;

        // Initialisierung mehrfach verwendeter Index-Counter
        private int tabIndexCounter = 1;

        // Initialisierung mehrfach verwendeter BackColor
        private Color backColorOK = SystemColors.Window;
        private Color backColorNOK = Color.LightPink;

        // Initialisierung mehrfach verwendetes Tag
        private string tagOK = "true";
        private string tagNOK = "false";

        public AnsichtKontakt()
        {
            InitializeComponent();
            this.Size = new Size(750, 890);
            this.WindowState = FormWindowState.Maximized;
            this.AutoScroll = true;

            // Initialisierung mehrfach verwendeter Label-/Control-Gruppen
            groupLabelEmployeesAndCustomers = GroupLabelEmployeesAndCustomers();
            groupFieldEmployeesAndCustomers = GroupFieldEmployeesAndCustomers();
            groupLabelEmployees = GroupLabelEmployees();
            groupFieldEmployees = GroupFieldEmployees();
            groupFieldNotes = GroupFieldNotes();
            groupButtons = GroupButtons();

            Design();
            InitializationLabelToolTip();
            InitializationGroupAndField();

            // Datumsfeld immer auf Heute setzten
            DateAnsichtKntktDateProtokol.Value = DateTime.Today;
        }

        private void Design()
        {
            // Platzierung Titel "Mitarbeiter / Kunde: ..." (dynamisch)
            LblAnsichtKntktNameAnzeige.Size = new Size(200, 40);
            LblAnsichtKntktNameAnzeige.Location = new Point(10, 20);

            // Platzierung Gruppe Mitarbeiter UND Kunde (alle)
            GrpBxDatenAlle.Size = new Size(350, 390);
            GrpBxDatenAlle.Location = new Point(10, 55);

            // Platzierung Labels und Eingabefelder der Gruppe Mitarbeiter UND Kunde (alle)
            // Zählerstart (Index) für Labels und Eingabefelder der Gruppe Mitarbeiter UND Kunde (alle) mit 1 
            // Erfassung Default-Tag als Vorbereitung für Validierung Eingabefelder der Gruppe Mitarbeiter UND Kunde (alle) mit TRUE (für OK-Fall) 
            tabIndexCounter = PlacementLabelAndField(groupLabelEmployeesAndCustomers, groupFieldEmployeesAndCustomers, tabIndexCounter);

            // Platzierung Gruppe NUR Mitarbeiter (ohne Kunde)
            GrpBxDatenMA.Size = new Size(350, 390);
            GrpBxDatenMA.Location = new Point(10, 450);

            // Platzierung Labels und Eingabefelder der Gruppe Mitarbeiter (ohne Kunde)
            // Zählerstart (Index) für Labels und Eingabefelder der Gruppe Mitarbeiter (ohne Kunde) fortführend
            // Erfassung Default-Tag als Vorbereitung für Validierung Eingabefelder der Gruppe Mitarbeiter (ohne Kunde) mit TRUE (für OK-Fall) 
            PlacementLabelAndField(groupLabelEmployees, groupFieldEmployees, tabIndexCounter);

            // Platzierung Gruppe Radio-Button (Aktiv vs. Inaktiv)
            GrpBxAnsichtKntktAktiv.Size = new Size(130, 40);
            GrpBxAnsichtKntktAktiv.Location = new Point(375, 10);

            // Platzierung Radio-Buttons (Aktiv vs. Inaktiv)
            // Zählerstart (Index) für Radio-Buttons (Aktiv vs. Inaktiv) fortführend (nur auf ersten Radio-Button möglich)
            RdbAnsichtKntktAktiv.Location = new Point(10, 15);
            RdbAnsichtKntktAktiv.TabIndex = tabIndexCounter++;
            RdbAnsichtKntktInaktiv.Location = new Point(70, 15);
            RdbAnsichtKntktInaktiv.TabStop = false;

            // Platzierung Gruppe "Notizen zu Person"
            GrpBxAnsichtKntktNotiz.Size = new Size(350, 530);
            GrpBxAnsichtKntktNotiz.Location = new Point(375, 55);

            // Platzierung Felder der Gruppe "Notizen zu Person"
            // Zählerstart (Index) für Felder der Gruppe "Notizen zu Person" fortführend
            PlacementFieldNote(groupFieldNotes, tabIndexCounter);

            // Platzierung Buttons "Bearbeiten, Löschen, Speichern, Zurück zum Dashboard"
            PlacementButton(groupButtons, tabIndexCounter);
        }

        // Platzierung Labels und Eingabefelder (dynamisch)
        private int PlacementLabelAndField(System.Windows.Forms.Label[] groupLabel, Control[] groupField, int indexCounter)
        {
            int tabIndexCounter = indexCounter;
            int startLocation = 20;
            int labelXAchse = 10;
            int controlXAchse = 135;

            for (int i = 0; i < groupField.Length; i++)
            {
                groupLabel[i].Location = new Point(labelXAchse, startLocation);
                groupField[i].Location = new Point(controlXAchse, startLocation);

                startLocation += 30;

                // Label irrelevant für Tab und daher fix mit 0
                groupLabel[i].TabIndex = 0;
                // Eingabefeld relevant für Tab und daher durchnummeriert
                groupField[i].TabIndex = tabIndexCounter++;

                // Default-Tag relevant für Validierung Eingabefelder (Start mit TRUE)
                groupField[i].Tag = tagOK;
            }

            return tabIndexCounter;
        }

        // Platzierung Notiz-Felder (fix)
        private int PlacementFieldNote(Control[] groupField, int indexCounter)
        {
            int tabIndexCounter = indexCounter;
            int width = 330;
            int height = 250;
            int locationX = 10;
            int locationY = 20;

            for (int i = 0; i < groupField.Length; i++)
            {
                // Notiz-Felder relevant für Tab und daher durchnummeriert
                groupField[i].TabIndex = tabIndexCounter++;
            }

            LbAnsichtKntktProtokolAusg.Size = new Size(width, height);
            LbAnsichtKntktProtokolAusg.Location = new Point(locationX, locationY);
            TxtAnsichtKntktProtokolTitel.Size = new Size(width, 20);
            TxtAnsichtKntktProtokolTitel.Location = new Point(locationX, height = height + 20);
            TxtAnsichtKntktProtokolEing.Size = new Size(width, 150);
            TxtAnsichtKntktProtokolEing.Location = new Point(locationX, height = height + 30);
            DateAnsichtKntktDateProtokol.Size = new Size(width, 40);
            DateAnsichtKntktDateProtokol.Location = new Point(locationX, height = height + 160);
            CmdAnsichtKntktSaveProtokol.Size = new Size(width, 30);
            CmdAnsichtKntktSaveProtokol.Location = new Point(locationX, height + 30);

            return tabIndexCounter;
        }

        // Platzierung Buttons (fix)
        private int PlacementButton(Control[] groupField, int indexCounter)
        {
            int tabIndexCounter = indexCounter;
            int width = 150;
            int height = 60;
            int locationX = 395;
            int locationY = 620;

            for (int i = 0; i < groupField.Length; i++)
            {
                groupField[i].Size = new Size(width, height);
                
                // Buttons relevant für Tab und daher durchnummeriert
                groupField[i].TabIndex = tabIndexCounter++;
            }

            CmdAnsichtKntktEdit.Location = new Point(locationX, locationY);
            CmdAnsichtKntktDeletAll.Location = new Point(locationX + width + 10, locationY);
            CmdAnsichtKntktSaveAll.Location = new Point(locationX, locationY + height + 10);
            CmdAnsichtKntktDashboard.Location = new Point(locationX + width + 10, locationY + height + 10);


            return tabIndexCounter;
        }

        // Erstellung Array für Labels der Gruppe Mitarbeiter UND Kunde (alle)
        private System.Windows.Forms.Label[] GroupLabelEmployeesAndCustomers()
        {
            groupLabelEmployeesAndCustomers = new System.Windows.Forms.Label[]
            {
                LblAnsichtKntktTitel,
                LblAnsichtKntktAnrede,
                LblAnsichtKntktVorname,
                LblAnsichtKntktName,
                LblAnsichtKntktBirthday,
                LblAnsichtKntktGeschlecht,
                LblAnsichtKntktAdr,
                LblAnsichtKntktPLZ,
                LblAnsichtKntktOrt,
                LblAnsichtKntktTelGeschaeft,
                LblAnsichtKntktTelMobile,
                LblAnsichtKntktEmail
            };

            return groupLabelEmployeesAndCustomers;
        }

        // Erstellung Array für Labels der Gruppe Mitarbeiter (ohne Kunde)
        private System.Windows.Forms.Label[] GroupLabelEmployees()
        {
            groupLabelEmployees = new System.Windows.Forms.Label[]
            {
                LblAnsichtKntktMaManr,
                LblAnsichtKntktMaAHVNr,
                LblAnsichtKntktMaNationalitaet,
                LblAnsichtKntktMaKader,
                LblAnsichtKntktMaBeschGrad,
                LblAnsichtKntktMaAbteilung,
                LblAnsichtKntktMaRolle,
                LblAnsichtKntktMaLehrj,
                LblAnsichtKntktMaAktLehrj,
                LblAnsichtKntktMaOfficeNumber,
                LblAnsichtKntktEintrDatum,
                LblAnsichtKntktAustrDatum
            };

            return groupLabelEmployees;
        }

        // Erstellung Array für Eingabefelder der Gruppe Mitarbeiter UND Kunde (alle)
        private Control[] GroupFieldEmployeesAndCustomers()
        {
            groupFieldEmployeesAndCustomers = new Control[]
            {
                TxtAnsichtKntktTitel,
                CmBxAnsichtKntktAnrede,
                TxtAnsichtKntktVorname,
                TxtAnsichtKntktName,
                DateAnsichtKntktBirthday,
                CmBxAnsichtKntktGeschlecht,
                TxtAnsichtKntktAdr,
                TxtAnsichtKntktPLZ,
                TxtAnsichtKntktOrt,
                TxtAnsichtKntktTelGeschaeft,
                TxtAnsichtKntktTelMobile,
                TxtAnsichtKntktEmail
            };

            return groupFieldEmployeesAndCustomers;
        }

        // Erstellung Array für Eingabefelder der Gruppe Mitarbeiter (ohne Kunde)
        private Control[] GroupFieldEmployees()
        {
            groupFieldEmployees = new Control[]
            {
                TxtAnsichtKntktMaManr,
                TxtAnsichtKntktMaAHVNr,
                TxtAnsichtKntktMaNationalitaet,
                TxtAnsichtKntktMaKader,
                NumAnsichtKntktMaBeschGrad,
                TxtAnsichtKntktMaAbteilung,
                TxtAnsichtKntktMaRolle,
                NumAnsichtKntktMaLehrj,
                NumAnsichtKntktMaAktLehrj,
                NumAnsichtKntktMaOfficeNumber,
                DateAnsichtKntktEintrDatum,
                DateAnsichtKntktAustrDatum
            };

            return groupFieldEmployees;
        }

        // Erstellung Array für Notiz-Felder
        private Control[] GroupFieldNotes()
        {
            groupFieldNotes = new Control[]
            {
                LbAnsichtKntktProtokolAusg,
                TxtAnsichtKntktProtokolTitel,
                TxtAnsichtKntktProtokolEing,
                DateAnsichtKntktDateProtokol,
                CmdAnsichtKntktSaveProtokol,
            };

            return groupFieldNotes;
        }



        // Erstellung Array für Buttons
        private Control[] GroupButtons()
        {
            groupButtons = new Control[]
            {
                CmdAnsichtKntktEdit,
                CmdAnsichtKntktDeletAll,
                CmdAnsichtKntktSaveAll,
                CmdAnsichtKntktDashboard,
            };

            return groupButtons;
        }

        // Erstellung Array für KEINE-Pflichtfelder-Prüfung
        private Control[] CheckFieldIgnore()
        {
            checkFieldIgnore = new Control[]
            {
                TxtAnsichtKntktTitel,
                // bei Mitarbeitern bleibt das Feld "Pflicht"
                // (!RdbAnsichtKntktMa.Checked ? TxtAnsichtKntktTelGeschaeft : null),
                 // bei Mitarbeitern mit CH-Nationalität bleibt das Feld "Pflicht"
                // (RdbAnsichtKntktKunde.Checked || (!string.IsNullOrWhiteSpace(TxtAnsichtKntktMaNationalitaet.Text) && TxtAnsichtKntktMaNationalitaet.Text.ToUpper() != "CH") ? TxtAnsichtKntktMaAHVNr : null),
                TxtAnsichtKntktMaKader,
                NumAnsichtKntktMaLehrj,
                NumAnsichtKntktMaAktLehrj,
                DateAnsichtKntktAustrDatum,
            };

            return checkFieldIgnore;
        }

        // Erstellung ToolTip für spezifische Labels (zur besseren Verständlichkeit)
        private void InitializationLabelToolTip()
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip
            {
                AutoPopDelay = 10000, // Standardwert liegt bei 5000ms (Wie lange bleibt Tooltip sichtbar)
                InitialDelay = 100, // Standardwert liegt bei 500ms (Verzögerung bis Tooltip erscheint)
                ReshowDelay = 100, // Standardwert liegt bei 100ms (Verzögerung zwischen mehreren Tooltips hintereinander)
                ShowAlways = true // Standardwert ist FALSE (Tooltip wird auch angezeigt, wenn Formular nicht aktiv)
            };

            SetLabelToolTip(toolTip, LblAnsichtKntktTitel, "Namenstitel (gekürzt)\r\nz.B. Dr., Ing., Prof.");
            SetLabelToolTip(toolTip, LblAnsichtKntktPLZ, "4-/5-stellige Postleitzahl\r\n(Schweiz und Nachbarländer)");
            SetLabelToolTip(toolTip, LblAnsichtKntktMaAHVNr, "Eingabe mit Punkten (CH-Norm)\r\nz.B. 756.1234.5678.90");
            SetLabelToolTip(toolTip, LblAnsichtKntktMaNationalitaet, "2-stelliger Länderkürzel\r\nz.B. CH, DE, FR, IT");
            SetLabelToolTip(toolTip, LblAnsichtKntktMaLehrj, "nur relevant für Lernende");
            SetLabelToolTip(toolTip, LblAnsichtKntktMaAktLehrj, "nur relevant für Lernende");
        }

        // Erzeugung Hover-Effekt bei ToolTip (userfreundlicher)
        private void SetLabelToolTip(System.Windows.Forms.ToolTip tooltip, System.Windows.Forms.Label label, string text)
        {
            tooltip.SetToolTip(label, text);

            // Speicherung Original-Schrift (für keine unerwünschten Nebeneffekte)
            Font originalFont = label.Font;

            label.MouseEnter += (s, e) =>
            {
                label.Font = new Font(originalFont, FontStyle.Bold); // Hover-Effekt mit "fetter" Schrift
            };

            label.MouseLeave += (s, e) =>
            {
                label.Font = originalFont; // Original-Schrift
            };
        }

        // Initialisierung Gruppen und Felder (Befüllung und Sperrung)
        private void InitializationGroupAndField()
        {
            UpdateGroupAndField(false);
        }

        private void UpdateGroupAndField(bool mutable)
        {
            GrpBxDatenAlle.Enabled = mutable;
            GrpBxDatenMA.Enabled = mutable;
            GrpBxAnsichtKntktAktiv.Enabled = mutable;
        }

        private void CmdAnsichtKntktEdit_Click(object sender, EventArgs e)
        {
            UpdateGroupAndField(true);
        }

        private void CmdAnsichtKntktSaveAll_Click(object sender, EventArgs e)
        {
            var validator = new CheckAndValidationFields(this);
            var validationContent = CheckAndValidationFieldsContent();
            bool checkFieldTag = validator.ValidationFields(validationContent);

            if (checkFieldTag)
            {
                UpdateGroupAndField(false);
            }
        }

        // Initialisierung Argumente (Inhalt) für Klasse "CheckAndValidationFields"
        private InitializationFields CheckAndValidationFieldsContent()
        {
            return new InitializationFields
            {
                GroupFieldEmployeesAndCustomers = groupFieldEmployeesAndCustomers,
                GroupFieldEmployees = groupFieldEmployees,
                CheckFieldIgnore = CheckFieldIgnore(),
                IsEmployee = true, // LOGIK IST NOCH ZU DEFINIEREN (nicht analog KontaktErstellen)
                IsClient = false, // LOGIK IST NOCH ZU DEFINIEREN (nicht analog KontaktErstellen)
                Salutation = CmBxAnsichtKntktAnrede,
                Birthday = DateAnsichtKntktBirthday,
                Gender = CmBxAnsichtKntktGeschlecht,
                PLZ = TxtAnsichtKntktPLZ,
                Email = TxtAnsichtKntktEmail,
                AHVNumber = TxtAnsichtKntktMaAHVNr,
                Nationality = TxtAnsichtKntktMaNationalitaet,
                DateOfEntry = DateAnsichtKntktEintrDatum
            };
        }

        private void CmdAnsichtKntktDeletAll_Click(object sender, EventArgs e)
        {
            string message = "Möchtest du den Kontakt unwiderruflich löschen?";
            DialogResult result = MessageBox.Show(message, "Kontakt löschen?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void CmdAnsichtKntktDashboard_Click(object sender, EventArgs e)
        {
            if (GrpBxDatenAlle.Enabled)
            {
                string message = "Die Änderungen wurde noch nicht gespeichert.\r\nBei der Bestätigung gehen diese alle verloren.\r\n\r\nMöchtest du trotzdem zurück zum Dashboard wechseln?";
                DialogResult result = MessageBox.Show(message, "Kontakt schliessen?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
            }
        }

        // Erzeugung MessageBox (Popup) bei fehlenden und/oder fehlerhaften Eingaben (Error)
        private void ShowMessageBox(string message)
        {
            MessageBox.Show(message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        // Notizen in ListBox speichern
        private void CmdAnsichtKntktSaveProtokol_Click(object sender, EventArgs e)
        {
            string title = TxtAnsichtKntktProtokolTitel.Text;
            string text = TxtAnsichtKntktProtokolEing.Text;
            string date = DateAnsichtKntktDateProtokol.Value.ToShortDateString();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(text) || title == "Notiz-Titel" || text == "Notiz")
            {
                MessageBox.Show("Bitte gültigen Titel und Text eingeben");
                return;
            }

            string eintrag = $"{title} - {date}";

            LbAnsichtKntktProtokolAusg.Items.Insert(0, eintrag);

            TxtAnsichtKntktProtokolTitel.Text = "Neuer Notiz-Titel";
            TxtAnsichtKntktProtokolEing.Text = "Neue Notiz";

        }

        
    }
}
