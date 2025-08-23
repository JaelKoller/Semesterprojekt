using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
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

        // Initialisierung Speicherart "update" (Vorbereitung für Ablage in JSON)
        string saveMode = "update";

        // Initialisierung mehrfach verwendeter Variablen
        private bool isEmployee;
        private bool isClient;
        private string contactNumber;
        private string typeOfContact;

        // Initialisierung Defaultwert ("Platzhalter") von Notizfelder
        private string defaultNoteTitle;
        private string defaultNoteText;

        public AnsichtKontakt(InitializationContactData contactData)
        {
            InitializeComponent();
            this.Size = new Size(750, 890);
            this.WindowState = FormWindowState.Maximized;
            this.AutoScroll = true;

            // Initialisierung Variablen (Mitarbeiter vs. Kunde)
            isEmployee = contactData.TypeOfContact == "Mitarbeiter";
            isClient = contactData.TypeOfContact == "Kunde";
            contactNumber = contactData.ContactNumber;
            typeOfContact = contactData.TypeOfContact;

            // Initialisierung mehrfach verwendeter Label-/Control-Gruppen
            groupLabelEmployeesAndCustomers = GroupLabelEmployeesAndCustomers();
            groupFieldEmployeesAndCustomers = GroupFieldEmployeesAndCustomers();
            groupLabelEmployees = GroupLabelEmployees();
            groupFieldEmployees = GroupFieldEmployees();
            groupLabelToolTip = GroupLabelToolTip();
            groupFieldNotes = GroupFieldNotes();
            groupButtons = GroupButtons();

            Design();
            ContactDataContent();
            ContactNotesContent();
            InitializationLabelToolTip();
            InitializationContactData(contactData);
            InitializationGroupAndField();

            // Initialisierung Anzeige Titel
            LblAnsichtKntktNameAnzeige.Text = $"{contactData.TypeOfContact}: {contactData.Fields["FirstName"]} {contactData.Fields["LastName"]}\r\n({contactNumber})";

            // Speicherung Defaultwert (inkl. heutiges Datum) von Notizfelder
            defaultNoteTitle = TxtAnsichtKntktProtokolTitel.Text;
            defaultNoteText = TxtAnsichtKntktProtokolEing.Text;
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
            // Zählerstart (Index) Eingabefelder der Gruppe Mitarbeiter UND Kunde (alle) mit 1 
            // Erfassung Default-Tag als Vorbereitung für Validierung Eingabefelder der Gruppe Mitarbeiter UND Kunde (alle) mit TRUE (für OK-Fall) 
            PlacementLabelAndField(groupLabelEmployeesAndCustomers, groupFieldEmployeesAndCustomers, ref tabIndexCounter);

            // Platzierung Gruppe NUR Mitarbeiter (ohne Kunde)
            GrpBxDatenMA.Size = new Size(410, 390);
            GrpBxDatenMA.Location = new Point(10, 450);

            // Platzierung Labels und Eingabefelder der Gruppe Mitarbeiter (ohne Kunde)
            // Zählerstart (Index) Eingabefelder der Gruppe Mitarbeiter (ohne Kunde) fortführend
            // Erfassung Default-Tag als Vorbereitung für Validierung Eingabefelder der Gruppe Mitarbeiter (ohne Kunde) mit TRUE (für OK-Fall) 
            PlacementLabelAndField(groupLabelEmployees, groupFieldEmployees, ref tabIndexCounter);

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
            PlacementFieldNote(groupFieldNotes, ref tabIndexCounter);

            // Platzierung Buttons "Bearbeiten, Löschen, Speichern, Zurück zum Dashboard"
            // Zählerstart (Index) für Buttons "Bearbeiten, Löschen, Speichern, Zurück zum Dashboard" fortführend
            PlacementButton(groupButtons, ref tabIndexCounter);
        }

        // Platzierung Labels und Eingabefelder (dynamisch)
        private void PlacementLabelAndField(System.Windows.Forms.Label[] groupLabel, Control[] groupField, ref int tabIndexCounter)
        {
            int startLocation = 20;
            int labelXAchse = 10;
            int controlXAchse = 150;

            for (int i = 0; i < groupField.Length; i++)
            {
                groupLabel[i].Location = new Point(labelXAchse, startLocation);
                groupField[i].Location = new Point(controlXAchse, startLocation);

                startLocation += 30;

                // Label irrelevant für Tab und daher mit TabStop "false"
                groupLabel[i].TabStop = false;
                // Eingabefeld relevant für Tab und daher durchnummeriert
                groupField[i].TabIndex = tabIndexCounter++;

                // Default-Tag relevant für Validierung Eingabefelder (Start mit TRUE)
                groupField[i].Tag = tagOK;
            }
        }

        // Platzierung Notiz-Felder (fix)
        private void PlacementFieldNote(Control[] groupField, ref int indexCounter)
        {
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
        }

        // Platzierung Buttons (fix)
        private void PlacementButton(Control[] groupField, ref int indexCounter)
        {
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
        }

        // Erstellung Array für Labels der Gruppe Mitarbeiter UND Kunde (alle)
        private System.Windows.Forms.Label[] GroupLabelEmployeesAndCustomers()
        {
            return groupLabelEmployeesAndCustomers = new System.Windows.Forms.Label[]
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
        }

        // Erstellung Array für Labels der Gruppe Mitarbeiter (ohne Kunde)
        private System.Windows.Forms.Label[] GroupLabelEmployees()
        {
            return groupLabelEmployees = new System.Windows.Forms.Label[]
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
        }

        // Erstellung Array für Labels für ToolTip
        private System.Windows.Forms.Label[] GroupLabelToolTip()
        {
            return groupLabelToolTip = new System.Windows.Forms.Label[]
            {
                LblAnsichtKntktTitel,
                LblAnsichtKntktBirthday,
                LblAnsichtKntktPLZ,
                LblAnsichtKntktTelGeschaeft,
                LblAnsichtKntktTelMobile,
                LblAnsichtKntktMaAHVNr,
                LblAnsichtKntktMaNationalitaet,
                LblAnsichtKntktMaLehrj,
                LblAnsichtKntktMaAktLehrj,
                LblAnsichtKntktEintrDatum,
                LblAnsichtKntktAustrDatum
            };
        }

        // Erstellung Array für Eingabefelder der Gruppe Mitarbeiter UND Kunde (alle)
        private Control[] GroupFieldEmployeesAndCustomers()
        {
            return groupFieldEmployeesAndCustomers = new Control[]
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
        }

        // Erstellung Array für Eingabefelder der Gruppe Mitarbeiter (ohne Kunde)
        private Control[] GroupFieldEmployees()
        {
            return groupFieldEmployees = new Control[]
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
        }

        // Erstellung Array für Notiz-Felder
        private Control[] GroupFieldNotes()
        {
            return groupFieldNotes = new Control[]
            {
                LbAnsichtKntktProtokolAusg,
                TxtAnsichtKntktProtokolTitel,
                TxtAnsichtKntktProtokolEing,
                DateAnsichtKntktDateProtokol,
                CmdAnsichtKntktSaveProtokol,
            };
        }

        // Erstellung Array für Buttons
        private Control[] GroupButtons()
        {
            return groupButtons = new Control[]
            {
                CmdAnsichtKntktEdit,
                CmdAnsichtKntktDeletAll,
                CmdAnsichtKntktSaveAll,
                CmdAnsichtKntktDashboard,
            };
        }

        // Erstellung Array für KEINE-Pflichtfelder-Prüfung
        private Control[] CheckFieldIgnore()
        {
            return checkFieldIgnore = new Control[]
            {
                TxtAnsichtKntktTitel,
                // bei Mitarbeitern bleibt das Feld "Pflicht"
                (isClient ? TxtAnsichtKntktTelGeschaeft : null),
                // bei Mitarbeitern mit CH-Nationalität bleibt das Feld "Pflicht"
                ((!string.IsNullOrWhiteSpace(TxtAnsichtKntktMaNationalitaet.Text) && TxtAnsichtKntktMaNationalitaet.Text.ToUpper() != "CH") ? TxtAnsichtKntktMaAHVNr : null),
                TxtAnsichtKntktMaKader,
                NumAnsichtKntktMaLehrj,
                NumAnsichtKntktMaAktLehrj,
                (string.IsNullOrWhiteSpace(TxtAnsichtKntktAustrDatum.Text) ? TxtAnsichtKntktAustrDatum: null)
            };
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
                BusinessNumber = LblAnsichtKntktTelGeschaeft,
                MobileNumber = LblAnsichtKntktTelMobile,
                AHVNumber = LblAnsichtKntktMaAHVNr,
                Nationality = LblAnsichtKntktMaNationalitaet,
                AcademicYear = LblAnsichtKntktMaLehrj,
                CurrentAcademicYear = LblAnsichtKntktMaAktLehrj,
                DateOfEntry = LblAnsichtKntktEintrDatum,
                DateOfExit = LblAnsichtKntktAustrDatum
            };
        }

        // Initialisierung Kontaktdaten (Resultat aus Suche)
        private void InitializationContactData(InitializationContactData contactData)
        {
            RdbAnsichtKntktAktiv.Checked = contactData.ContactStatus == "active";
            RdbAnsichtKntktInaktiv.Checked = contactData.ContactStatus == "inactive";
            TxtAnsichtKntktTitel.Text = Convert.ToString(contactData.Fields["Title"]);
            CmBxAnsichtKntktAnrede.SelectedItem = Convert.ToString(contactData.Fields["Salutation"]);
            TxtAnsichtKntktVorname.Text = Convert.ToString(contactData.Fields["FirstName"]);
            TxtAnsichtKntktName.Text = Convert.ToString(contactData.Fields["LastName"]);
            TxtAnsichtKntktBirthday.Text = Convert.ToString(contactData.Fields["Birthday"]);
            CmBxAnsichtKntktGeschlecht.SelectedItem = Convert.ToString(contactData.Fields["Gender"]);
            TxtAnsichtKntktAdr.Text = Convert.ToString(contactData.Fields["Address"]);
            TxtAnsichtKntktPLZ.Text = Convert.ToString(contactData.Fields["PostalCode"]);
            TxtAnsichtKntktOrt.Text = Convert.ToString(contactData.Fields["City"]);
            TxtAnsichtKntktTelGeschaeft.Text = Convert.ToString(contactData.Fields["BusinessNumber"]);
            TxtAnsichtKntktTelMobile.Text = Convert.ToString(contactData.Fields["MobileNumber"]);
            TxtAnsichtKntktEmail.Text = Convert.ToString(contactData.Fields["Email"]);

            if (isEmployee)
            {
                TxtAnsichtKntktMaManr.Text = Convert.ToString(contactData.Fields["EmployeeNumber"]);
                TxtAnsichtKntktMaAHVNr.Text = Convert.ToString(contactData.Fields["AHVNumber"]);
                TxtAnsichtKntktMaNationalitaet.Text = Convert.ToString(contactData.Fields["Nationality"]);
                TxtAnsichtKntktMaKader.Text = Convert.ToString(contactData.Fields["ManagementLevel"]);
                NumAnsichtKntktMaBeschGrad.Value = Convert.ToDecimal(contactData.Fields["LevelOfEmployment"]);
                TxtAnsichtKntktMaAbteilung.Text = Convert.ToString(contactData.Fields["Department"]);
                TxtAnsichtKntktMaRolle.Text = Convert.ToString(contactData.Fields["Role"]);
                NumAnsichtKntktMaLehrj.Value = Convert.ToDecimal(contactData.Fields["AcademicYear"]);
                NumAnsichtKntktMaAktLehrj.Value = Convert.ToDecimal(contactData.Fields["CurrentAcademicYear"]);
                NumAnsichtKntktMaOfficeNumber.Value = Convert.ToDecimal(contactData.Fields["OfficeNumber"]);
                TxtAnsichtKntktEintrDatum.Text = Convert.ToString(contactData.Fields["DateOfEntry"]);
                TxtAnsichtKntktAustrDatum.Text = Convert.ToString(contactData.Fields["DateOfExit"]);
            }
        }

        // Initialisierung Gruppen und Felder (Befüllung und Sperrung)
        private void InitializationGroupAndField()
        {
            UpdateGroupAndField(false);
        }

        // Aktualisierung Anzeige bezüglich mutierbar vs. gesperrt
        private void UpdateGroupAndField(bool mutable)
        {
            GrpBxDatenAlle.Enabled = mutable;
            GrpBxDatenMA.Enabled = isEmployee ? mutable : false;
            GrpBxAnsichtKntktAktiv.Enabled = mutable;
            CmdAnsichtKntktEdit.Enabled = !mutable;
            CmdAnsichtKntktSaveAll.Enabled = mutable;
        }

        // Klick Button "Bearbeiten"
        private void CmdAnsichtKntktEdit_Click(object sender, EventArgs e)
        {
            UpdateGroupAndField(true);

            // Fokus auf erstes Feld (bei "Bearbeiten")
            this.BeginInvoke((Action)(() => groupFieldEmployeesAndCustomers.First(field => field.TabIndex == 1).Focus()));
        }

        // Klick Button "Speichern"
        private void CmdAnsichtKntktSaveAll_Click(object sender, EventArgs e)
        {
            var validator = new CheckAndValidationFields();
            var validationContent = CheckAndValidationFieldsContent();
            bool checkFieldTag = validator.ValidationFields(validationContent);

            if (checkFieldTag)
            {
                // Ermittlung aktuellster Stand des Kontaktstatus
                string contactStatus = RdbAnsichtKntktAktiv.Checked ? "active" : "inactive";

                // Speicherung der Daten in JSON "contacts", falls Duplikatencheck erfolgreich
                if (ContactData.SaveContactData(saveMode, contactStatus, typeOfContact, contactNumber, groupFieldEmployeesAndCustomers, groupFieldEmployees))
                {
                    UpdateGroupAndField(false);
                }
            }
        }

        // Initialisierung Argumente (Inhalt) für Klasse "ContactData"
        private InitializationContactData ContactDataContent()
        {
            return new InitializationContactData
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

        // Initialisierung Notizen als Listeneinträge
        private void ContactNotesContent()
        {
            // Suche der Notizen auf Basis Kontakt Nr. (falls keine Treffer = NULL)
            ContactNotes contactNotes = Notes.SearchNotesData(contactNumber);

            if (contactNotes != null)
            {
                // Bereinigung der Listeneinträge (Notizen)
                LbAnsichtKntktProtokolAusg.Items.Clear();

                // Hinzufügen jeder Notiz als Listeneintrag in absteigender Reihenfolge (d.h. die neueste Notiz ganz oben)
                foreach (InitializationNotes note in contactNotes.Notes.OrderByDescending(note => DateTime.ParseExact(note.NoteDate, "dd.MM.yyyy", CultureInfo.InvariantCulture)))
                {
                    LbAnsichtKntktProtokolAusg.Items.Add(note);
                }
            }
        }

        // Initialisierung Argumente (Inhalt) für Klasse "CheckAndValidationFields"
        private InitializationCheckAndValidationFields CheckAndValidationFieldsContent()
        {
            return new InitializationCheckAndValidationFields
            {
                GroupFieldEmployeesAndCustomers = groupFieldEmployeesAndCustomers,
                GroupFieldEmployees = groupFieldEmployees,
                CheckFieldIgnore = CheckFieldIgnore(),
                IsEmployee = isEmployee,
                IsClient = isClient,
                Salutation = CmBxAnsichtKntktAnrede,
                Birthday = TxtAnsichtKntktBirthday,
                Gender = CmBxAnsichtKntktGeschlecht,
                PostalCode = TxtAnsichtKntktPLZ,
                BusinessNumber = TxtAnsichtKntktTelGeschaeft,
                MobileNumber = TxtAnsichtKntktTelMobile,
                Email = TxtAnsichtKntktEmail,
                AHVNumber = TxtAnsichtKntktMaAHVNr,
                Nationality = TxtAnsichtKntktMaNationalitaet,
                DateOfEntry = TxtAnsichtKntktEintrDatum,
                DateOfExit = TxtAnsichtKntktAustrDatum
            };
        }

        // Initialisierung Argumente (Inhalt) für Klasse "CheckAndValidationNoteFields"
        private InitializationNotes CheckAndValidationNoteFieldsContent()
        {
            return new InitializationNotes
            {
                ContactNumber = contactNumber,
                DefaultNoteTitle = defaultNoteTitle,
                DefaultNoteText = defaultNoteText,
                NoteTitle = TxtAnsichtKntktProtokolTitel.Text,
                NoteText = TxtAnsichtKntktProtokolEing.Text,
                NoteDate = DateAnsichtKntktDateProtokol.Value.ToString("dd.MM.yyyy")
            };
        }

        // Klick Button "Löschen"
        private void CmdAnsichtKntktDeletAll_Click(object sender, EventArgs e)
        {
            string message = "Möchtest du den Kontakt (inkl. Notizen) unwiderruflich löschen?";
            DialogResult result = MessageBox.Show(message, "Kontakt (inkl. Notizen) löschen?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Löschung der Daten in JSON "contacts"
                if (ContactData.DeleteContactData(contactNumber))
                {
                    // Löschung der Kontakt Nr. in JSON "clientAndEmployeeNumbers"     
                    ClientAndEmployeeNumber.DeleteNumber(contactNumber);

                    // Löschung der Kontakt Nr. in JSON "notes"
                    Notes.DeleteNotesData(contactNumber);
                    
                    this.Close();
                }
            }
        }

        // Klick Button "Zurück zum Dashboard"
        private void CmdAnsichtKntktDashboard_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            string noteTitle = TxtAnsichtKntktProtokolTitel.Text.Trim();
            string noteText = TxtAnsichtKntktProtokolEing.Text.Trim();
            bool noteOpenChanges = noteTitle != defaultNoteTitle || noteText != defaultNoteText;

            if (!GrpBxDatenAlle.Enabled && !noteOpenChanges)
            {
                // Verstecktes Fenster "AlleKontakte" als Owner wird auch geschlossen
                (this.Owner as AlleKontakte)?.Close();
                this.Close();
                return;
            }

            if (GrpBxDatenAlle.Enabled && noteOpenChanges)
                message = "Die Änderungen (inkl. Notiz) wurden noch nicht gespeichert.\r\nBei der Bestätigung gehen diese verloren.";

            else if (GrpBxDatenAlle.Enabled)
                message = "Die Änderungen wurden noch nicht gespeichert.\r\nBei der Bestätigung gehen diese verloren.";

            else
                message = "Die neue Notiz wurde noch nicht gespeichert.\r\nBei der Bestätigung geht diese verloren.";

            message += "\r\n\r\nMöchtest du trotzdem zurück zum Dashboard wechseln?";
            DialogResult result = MessageBox.Show(message, "Kontakt schliessen?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Verstecktes Fenster "AlleKontakte" als Owner wird auch geschlossen
                (this.Owner as AlleKontakte)?.Close();
                this.Close();
                return;
            }
        }

        // Klick Button "Speichern" (neue Notiz)
        private void CmdAnsichtKntktSaveProtokol_Click(object sender, EventArgs e)
        {
            string errorMessage = string.Empty;
            var noteContent = CheckAndValidationNoteFieldsContent();
            bool note = CheckAndValidationNoteFields.CheckNoteFields(noteContent, TxtAnsichtKntktProtokolTitel, TxtAnsichtKntktProtokolEing, out errorMessage);

            if (!note)
            {
                // Erzeugung MessageBox (Popup) bei fehlerhaften Eingaben
                MessageBox.Show(errorMessage, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Speicherung der Daten in JSON "notes"
            if (Notes.SaveNotesData(noteContent))
            {
                // Speicherung der neuen Notiz (inkl. Sortierung)
                ContactNotesContent();

                // Bereinigung der Eingabefelder (für nächste Notiz)
                TxtAnsichtKntktProtokolTitel.Text = noteContent.DefaultNoteTitle;
                TxtAnsichtKntktProtokolEing.Text = noteContent.DefaultNoteText;
                DateAnsichtKntktDateProtokol.Value = DateTime.Today;
            }
        }

        // Doppelklick auf Listeneintrag bei Notizen (für Anzeige via MessageBox)
        private void LbAnsichtKntktProtokolAusg_DoubleClick(object sender, EventArgs e)
        {
            if (LbAnsichtKntktProtokolAusg.SelectedItem is InitializationNotes noteData)
            {
                string message = $"{noteData.NoteText}\r\n\r\n{noteData.NoteDate}";
                MessageBox.Show(message, noteData.NoteTitle, MessageBoxButtons.OK);
            }
        }
    }
}