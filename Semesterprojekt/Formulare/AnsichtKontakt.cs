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
        // Initialisierung Klasse "AnsichtKontaktLabelAndControlGroups"
        internal AnsichtKontaktLabelAndControlGroups groups;

        // Initialisierung mehrfach verwendeter Label-/Control-Gruppen
        internal System.Windows.Forms.Label[] groupLabelEmployeesAndCustomers;
        internal Control[] groupFieldEmployeesAndCustomers;
        internal System.Windows.Forms.Label[] groupLabelEmployees;
        internal Control[] groupFieldEmployees;
        internal System.Windows.Forms.Label[] groupLabelToolTip;
        internal Control[] groupFieldNotes;
        internal Control[] groupButtons;

        // Initialisierung Speicherart "update" (Vorbereitung für Ablage in JSON)
        string saveMode = "update";

        // Initialisierung mehrfach verwendeter Variablen
        internal bool isEmployee;
        internal bool isClient;
        internal string contactNumber;
        private string typeOfContact;

        // Initialisierung Defaultwert ("Platzhalter") von Notizfelder
        internal string defaultNoteTitle;
        internal string defaultNoteText;

        public AnsichtKontakt(InitializationContactData contactData)
        {
            InitializeComponent();
            this.Size = new Size(750, 990);
            this.WindowState = FormWindowState.Maximized;
            this.AutoScroll = true;

            // Initialisierung Variablen (Mitarbeiter vs. Kunde)
            isEmployee = contactData.TypeOfContact == "Mitarbeiter";
            isClient = contactData.TypeOfContact == "Kunde";
            contactNumber = contactData.ContactNumber;
            typeOfContact = contactData.TypeOfContact;

            // Initialisierung mehrfach verwendeter Label-/Control-Gruppen
            groups = new AnsichtKontaktLabelAndControlGroups();
            groupLabelEmployeesAndCustomers = groups.GroupLabelEmployeesAndCustomers(this);
            groupFieldEmployeesAndCustomers = groups.GroupFieldEmployeesAndCustomers(this);
            groupLabelEmployees = groups.GroupLabelEmployees(this);
            groupFieldEmployees = groups.GroupFieldEmployees(this);
            groupLabelToolTip = groups.GroupLabelToolTip(this);
            groupFieldNotes = groups.GroupFieldNotes(this);
            groupButtons = groups.GroupButtons(this);

            AnsichtKontaktDesign.Design(this);
            AnsichtKontaktInitializations.ContactDataContent(this);
            ContactNotesContent();
            AnsichtKontaktDesign.InitializationLabelToolTip(this);
            AnsichtKontaktInitializations.InitializationContactData(this, contactData);

            // Initialisierung Gruppen und Felder (Befüllung und Sperrung)
            UpdateGroupAndField(false);

            // Initialisierung (Registrierung) ESC für Rückkehr zu AlleKontakte (analog Button)
            this.CancelButton = CmdAnsichtKntktAlleKontakte;

            // Initialisierung Anzeige Titel
            LblAnsichtKntktNameAnzeige.Text = $"{contactData.TypeOfContact}: {contactData.Fields["FirstName"]} {contactData.Fields["LastName"]}\r\n({contactNumber})";

            // Speicherung Defaultwert (inkl. heutiges Datum) von Notizfelder (als Vorbereitung für Abgleich)
            defaultNoteTitle = TxtAnsichtKntktProtokolTitel.Text;
            defaultNoteText = TxtAnsichtKntktProtokolEing.Text;
            DateAnsichtKntktDateProtokol.Value = DateTime.Today;
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
            var validationContent = AnsichtKontaktInitializations.CheckAndValidationFieldsContent(this);
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

        // Klick Button "Zurück zur Kontaktsuche"
        private void CmdAnsichtKntktAlleKontakte_Click(object sender, EventArgs e)
        {
            if (CloseViewContact(true))
                this.Close();
        }

        // Klick Button "Zurück zum Dashboard"
        private void CmdAnsichtKntktDashboard_Click(object sender, EventArgs e)
        {
            if (CloseViewContact(false))
            {
                // Verstecktes Fenster "AlleKontakte" als Owner wird auch geschlossen
                (this.Owner as AlleKontakte)?.Close();
                this.Close();
            }
        }

        // Prüfung offene Änderungen (inkl. neue Notiz)
        private bool CloseViewContact(bool backToSearch)
        {
            string noteTitle = TxtAnsichtKntktProtokolTitel.Text.Trim();
            string noteText = TxtAnsichtKntktProtokolEing.Text.Trim();
            bool noteOpenChanges = noteTitle != defaultNoteTitle || noteText != defaultNoteText;

            if (!GrpBxDatenAlle.Enabled && !noteOpenChanges)
                return true;

            string message = string.Empty;
            string backToMessage = backToSearch ? "zur Kontaktsuche" : "zum Dashboard";

            if (GrpBxDatenAlle.Enabled && noteOpenChanges)
                message = "Die Änderungen (inkl. Notiz) wurden noch nicht gespeichert.\r\nBei der Bestätigung gehen diese verloren.";

            else if (GrpBxDatenAlle.Enabled)
                message = "Die Änderungen wurden noch nicht gespeichert.\r\nBei der Bestätigung gehen diese verloren.";

            else
                message = "Die neue Notiz wurde noch nicht gespeichert.\r\nBei der Bestätigung geht diese verloren.";

            message += $"\r\n\r\nMöchtest du trotzdem zurück {backToMessage} wechseln?";
            DialogResult result = MessageBox.Show(message, "Kontakt schliessen?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                return true;

            return false;
        }

        // Klick Button "Speichern" (neue Notiz)
        private void CmdAnsichtKntktSaveProtokol_Click(object sender, EventArgs e)
        {
            string errorMessage = string.Empty;
            var noteContent = AnsichtKontaktInitializations.CheckAndValidationNoteFieldsContent(this);
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