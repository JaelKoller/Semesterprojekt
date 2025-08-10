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
        private System.Windows.Forms.Label[] groupLabelToolTip;
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

        // default-Text aus TextBoxen in Variabel speichern zur überprüfung
        private string defaultTitle;
        private string defaultNote;

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
            groupLabelToolTip = GroupLabelToolTip();
            groupFieldNotes = GroupFieldNotes();
            groupButtons = GroupButtons();

            Design();
            InitializationContactDataContent();
            InitializationLabelToolTip();
            InitializationGroupAndField();

            // Initialisierung Notizfelder (Defaultwert inkl. heutiges Datum)
            defaultTitle = TxtAnsichtKntktProtokolTitel.Text;
            defaultNote = TxtAnsichtKntktProtokolEing.Text;
            DateAnsichtKntktDateProtokol.Value = DateTime.Today;
        }

        private void Design()
        {
            // Platzierung Titel "Mitarbeiter / Kunde: ..." (dynamisch)
            LblAnsichtKntktNameAnzeige.Size = new Size(200, 40);
            LblAnsichtKntktNameAnzeige.Location = new Point(10, 20);

            // Platzierung Gruppe Mitarbeiter UND Kunde (alle)
            GrpBxDatenAlle.Size = new Size(410, 390);
            GrpBxDatenAlle.Location = new Point(10, 55);

            // Platzierung Labels und Eingabefelder der Gruppe Mitarbeiter UND Kunde (alle)
            // Zählerstart (Index) für Labels und Eingabefelder der Gruppe Mitarbeiter UND Kunde (alle) mit 1 
            // Erfassung Default-Tag als Vorbereitung für Validierung Eingabefelder der Gruppe Mitarbeiter UND Kunde (alle) mit TRUE (für OK-Fall) 
            tabIndexCounter = PlacementLabelAndField(groupLabelEmployeesAndCustomers, groupFieldEmployeesAndCustomers, tabIndexCounter);

            // Platzierung Gruppe NUR Mitarbeiter (ohne Kunde)
            GrpBxDatenMA.Size = new Size(410, 390);
            GrpBxDatenMA.Location = new Point(10, 450);

            // Platzierung Labels und Eingabefelder der Gruppe Mitarbeiter (ohne Kunde)
            // Zählerstart (Index) für Labels und Eingabefelder der Gruppe Mitarbeiter (ohne Kunde) fortführend
            // Erfassung Default-Tag als Vorbereitung für Validierung Eingabefelder der Gruppe Mitarbeiter (ohne Kunde) mit TRUE (für OK-Fall) 
            PlacementLabelAndField(groupLabelEmployees, groupFieldEmployees, tabIndexCounter);

            // Platzierung Gruppe Radio-Button (Aktiv vs. Inaktiv)
            GrpBxAnsichtKntktAktiv.Size = new Size(150, 40);
            GrpBxAnsichtKntktAktiv.Location = new Point(430, 10);

            // Platzierung Radio-Buttons (Aktiv vs. Inaktiv)
            // Zählerstart (Index) für Radio-Buttons (Aktiv vs. Inaktiv) fortführend (nur auf ersten Radio-Button möglich)
            RdbAnsichtKntktAktiv.Location = new Point(15, 15);
            RdbAnsichtKntktAktiv.TabIndex = tabIndexCounter++;
            RdbAnsichtKntktInaktiv.Location = new Point(75, 15);
            RdbAnsichtKntktInaktiv.TabStop = false;

            // Platzierung Gruppe "Notizen zu Person"
            GrpBxAnsichtKntktNotiz.Size = new Size(410, 535);
            GrpBxAnsichtKntktNotiz.Location = new Point(430, 55);

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
            int controlXAchse = 150;

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
            int width = 380;
            int height = 250;
            int locationX = 15;
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
            int locationX = 480;
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

        // Erstellung Array für Labels für ToolTip
        private System.Windows.Forms.Label[] GroupLabelToolTip()
        {
            groupLabelToolTip = new System.Windows.Forms.Label[]
            {
                LblAnsichtKntktTitel,
                LblAnsichtKntktBirthday,
                LblAnsichtKntktPLZ,
                LblAnsichtKntktMaAHVNr,
                LblAnsichtKntktMaNationalitaet,
                LblAnsichtKntktMaLehrj,
                LblAnsichtKntktMaAktLehrj,
                LblAnsichtKntktEintrDatum,
                LblAnsichtKntktAustrDatum
            };

            return groupLabelToolTip;
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
                TxtAnsichtKntktBirthday,
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
                TxtAnsichtKntktEintrDatum,
                TxtAnsichtKntktAustrDatum
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
                (string.IsNullOrWhiteSpace(TxtAnsichtKntktAustrDatum.Text) ? TxtAnsichtKntktAustrDatum: null)
            };

            return checkFieldIgnore;
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
                Title = LblAnsichtKntktTitel,
                Birthday = LblAnsichtKntktBirthday,
                PostalCode = LblAnsichtKntktPLZ,
                AHVNumber = LblAnsichtKntktMaAHVNr,
                Nationality = LblAnsichtKntktMaNationalitaet,
                AcademicYear = LblAnsichtKntktMaLehrj,
                CurrentAcademicYear = LblAnsichtKntktMaAktLehrj,
                DateOfEntry = LblAnsichtKntktEintrDatum,
                DateOfExit = LblAnsichtKntktAustrDatum
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
            GrpBxDatenMA.Enabled = mutable; // LOGIK IST NOCH ZU DEFINIEREN (NUR BEI MA)
            GrpBxAnsichtKntktAktiv.Enabled = mutable;
        }

        private void CmdAnsichtKntktEdit_Click(object sender, EventArgs e)
        {
            UpdateGroupAndField(true);
        }

        private void CmdAnsichtKntktSaveAll_Click(object sender, EventArgs e)
        {
            var validator = new CheckAndValidationFields();
            var validationContent = CheckAndValidationFieldsContent();
            bool checkFieldTag = validator.ValidationFields(validationContent);

            if (checkFieldTag)
            {
                UpdateGroupAndField(false);
            }
        }

        // Initialisierung Argumente (Inhalt) für Klasse "ContactData"
        private ContactData InitializationContactDataContent()
        {
            return new ContactData
            {
                Title = TxtAnsichtKntktTitel,
                Salutation = CmBxAnsichtKntktAnrede,
                FirstName = TxtAnsichtKntktVorname,
                LastName = TxtAnsichtKntktName,
                Birthday = TxtAnsichtKntktBirthday,
                Gender = CmBxAnsichtKntktGeschlecht,
                Address = TxtAnsichtKntktAdr,
                PostalCode = TxtAnsichtKntktPLZ,
                City = TxtAnsichtKntktOrt,
                BusinessNumber = TxtAnsichtKntktTelGeschaeft,
                MobileNumber = TxtAnsichtKntktTelMobile,
                Email = TxtAnsichtKntktEmail,
                EmployeeNumber = TxtAnsichtKntktMaManr,
                AHVNumber = TxtAnsichtKntktMaAHVNr,
                Nationality = TxtAnsichtKntktMaNationalitaet,
                ManagementLevel = TxtAnsichtKntktMaKader,
                LevelOfEmployment = NumAnsichtKntktMaBeschGrad,
                Department = TxtAnsichtKntktMaAbteilung,
                Role = TxtAnsichtKntktMaRolle,
                AcademicYear = NumAnsichtKntktMaLehrj,
                CurrentAcademicYear = NumAnsichtKntktMaAktLehrj,
                OfficeNumber = NumAnsichtKntktMaOfficeNumber,
                DateOfEntry = TxtAnsichtKntktEintrDatum,
                DateOfExit = TxtAnsichtKntktAustrDatum
            };
        }

        // Initialisierung Argumente (Inhalt) für Klasse "CheckAndValidationFields"
        private InitializationCheckAndValidationFields CheckAndValidationFieldsContent()
        {
            return new InitializationCheckAndValidationFields
            {
                GroupFieldEmployeesAndCustomers = groupFieldEmployeesAndCustomers,
                GroupFieldEmployees = groupFieldEmployees,
                CheckFieldIgnore = CheckFieldIgnore(),
                IsEmployee = true, // LOGIK IST NOCH ZU DEFINIEREN (nicht analog KontaktErstellen)
                IsClient = false, // LOGIK IST NOCH ZU DEFINIEREN (nicht analog KontaktErstellen)
                Salutation = CmBxAnsichtKntktAnrede,
                Birthday = TxtAnsichtKntktBirthday,
                Gender = CmBxAnsichtKntktGeschlecht,
                PostalCode = TxtAnsichtKntktPLZ,
                Email = TxtAnsichtKntktEmail,
                AHVNumber = TxtAnsichtKntktMaAHVNr,
                Nationality = TxtAnsichtKntktMaNationalitaet,
                DateOfEntry = TxtAnsichtKntktEintrDatum,
                DateOfExit = TxtAnsichtKntktAustrDatum
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

        // Notizen in ListBox speichern
        private void CmdAnsichtKntktSaveProtokol_Click(object sender, EventArgs e)
        {

            // Speichert Text und Datum in Variabel für Klasse Notes
            string title = TxtAnsichtKntktProtokolTitel.Text;
            string text = TxtAnsichtKntktProtokolEing.Text;
            string date = DateAnsichtKntktDateProtokol.Value.ToShortDateString();

            // Übergibt Variabeln an Klasse Notes
            Notes newNote = new Notes
            {
                Title = title,
                Date = date,
                Text = text
            };

            // Prüft ob Titel und Notiz gültig sind
            bool titleOk = !string.IsNullOrWhiteSpace(title) && title != defaultTitle;
            bool noteOk = !string.IsNullOrWhiteSpace(text) && text != defaultNote;

            // Prüft, dass nicht der default-Wert gespeichert wird
            // Gibt bei bedarf Fehlermeldung

            // Titel und Notiz gut
            if (titleOk && noteOk)
            {
                // Tag und Normalfarbe setzten  bei Titel
                TxtAnsichtKntktProtokolTitel.BackColor = backColorOK;
                TxtAnsichtKntktProtokolTitel.Tag = tagOK;

                // Tag und Normalfarbe setzten  bei Notiz
                TxtAnsichtKntktProtokolEing.BackColor = backColorOK;
                TxtAnsichtKntktProtokolEing.Tag = tagOK;

                // Setzt neue Notiz an oberste Stelle in ListBox
                LbAnsichtKntktProtokolAusg.Items.Insert(0, newNote);

                // Setzt default-Wert wieder in TextBox von Titel und Notiz
                TxtAnsichtKntktProtokolTitel.Text = defaultTitle;
                TxtAnsichtKntktProtokolEing.Text = defaultNote;

                return;
            }

            // Titel: Setzt Tag und Farbe wieder auf OK
            TxtAnsichtKntktProtokolTitel.BackColor = backColorOK;
            TxtAnsichtKntktProtokolTitel.Tag = tagOK;

            // Notiz: Setzt Tag und Farbe wieder auf OK
            TxtAnsichtKntktProtokolEing.BackColor = backColorOK;
            TxtAnsichtKntktProtokolEing.Tag = tagOK;

            // Titel und Notiz nicht OK
            if (!titleOk && !noteOk)
            {
                // Tag setzten und rotfärben bei Titel
                TxtAnsichtKntktProtokolTitel.BackColor = backColorNOK;
                TxtAnsichtKntktProtokolTitel.Tag = tagNOK;
                TxtAnsichtKntktProtokolTitel.Focus();

                // Tag setzten und rotfärben bei Notiz
                TxtAnsichtKntktProtokolEing.BackColor = backColorNOK;
                TxtAnsichtKntktProtokolEing.Tag = tagNOK;

                ShowMessageBox("Bitte gültigen Titel und Text eingeben");
                return;
            }

            // Titel nicht OK
            if (!titleOk)
            {
                // Tag setzten und rotfärben bei Titel
                TxtAnsichtKntktProtokolTitel.BackColor = backColorNOK;
                TxtAnsichtKntktProtokolTitel.Tag = tagNOK;
                TxtAnsichtKntktProtokolTitel.Focus();

                ShowMessageBox("Bitte gültigen Titel eingeben");
                return;
            }

            // Notiz nicht OK
            if (!noteOk)
            {
                // Tag setzten und rotfärben bei Notiz
                TxtAnsichtKntktProtokolEing.BackColor = backColorNOK;
                TxtAnsichtKntktProtokolEing.Tag = tagNOK;
                TxtAnsichtKntktProtokolEing.Focus();

                ShowMessageBox("Bitte gültige Notiz eingeben");
                return;
            }
        }


        // Notizen in MessageBox anzeigen
        private void LbAnsichtKntktProtokolAusg_DoubleClick(object sender, EventArgs e)
        {
            if (LbAnsichtKntktProtokolAusg.SelectedItem is Notes choosenNote)
            {
                string message = $"{choosenNote.Title}\n" +
                                 $"{choosenNote.Date}\n\n" +
                                 $"{choosenNote.Text}";

                MessageBox.Show(message, "Notiz anzeigen", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Erzeugung MessageBox (Popup) bei fehlenden und/oder fehlerhaften Eingaben (Error)
        private void ShowMessageBox(string message)
        {
            MessageBox.Show(message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

       
    }
}
