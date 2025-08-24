using System;
using System.Drawing;
using System.Windows.Forms;

namespace Semesterprojekt
{
    public partial class KontaktErstellen : Form
    {
        // Initalisierung String "typeOfContact" für "Speichern und neuer Kontakt erstellen"
        private string typeOfContactNew;

        // Initialisierung Klasse "KontaktErstellenLabelAndControlGroups"
        internal KontaktErstellenLabelAndControlGroups groups;

        // Initialisierung mehrfach verwendeter Label-/Control-Gruppen
        private System.Windows.Forms.Label[] groupLabelEmployeesAndCustomers;
        internal Control[] groupFieldEmployeesAndCustomers;
        private System.Windows.Forms.Label[] groupLabelEmployees;
        internal Control[] groupFieldEmployees;
        internal System.Windows.Forms.Label[] groupLabelToolTip;

        // Initialisierung mehrfach verwendeter Index-Counter
        private int tabIndexCounter = 1;

        // Initialisierung verwendeter BackColor (analog separater Klasse)
        private Color backColorOK = SystemColors.Window;

        // Initialisierung verwendetes Tag (analog separater Klasse)
        private string tagOK = "true";

        // Initialisierung Speicherart "save" (Vorbereitung für Ablage in JSON)
        private string saveMode = "save";
        
        // Initialisierung Mitarbeiter-/Kunden-Status mit "active" (Vorbereitung für Ablage in JSON)
        private string contactStatus = "active";
        
        // Initialisierung Mitarbeiter/Kunden Nr. (Vorbereitung für Ablage in JSON)
        private string contactNumberNew;

        public KontaktErstellen(string typeOfContact)
        {
            InitializeComponent();
            this.Size = new Size(775, 760);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;

            // Initialisierung mehrfach verwendeter Label-/Control-Gruppen
            groups = new KontaktErstellenLabelAndControlGroups();
            groupLabelEmployeesAndCustomers = groups.GroupLabelEmployeesAndCustomers(this);
            groupFieldEmployeesAndCustomers = groups.GroupFieldEmployeesAndCustomers(this);
            groupLabelEmployees = groups.GroupLabelEmployees(this);
            groupFieldEmployees = groups.GroupFieldEmployees(this);
            groupLabelToolTip = groups.GroupLabelToolTip(this);

            Design();
            KontaktErstellenInitializations.ContactDataContent(this);
            InitializationLabelToolTip();

            typeOfContactNew = typeOfContact;
            InitializationTypeOfContact();
            UpdateTypeOfContact();

            // Initialisierung (Registrierung) ESC für Rückkehr zu Dashboard (analog Button "Eingaben verwerfen ...")
            this.CancelButton = CmdCreateKntktVerwerfen;
        }

        // Design (Platzierung) der Eingabe-Felder usw.
        private void Design()
        {
            // Platzierung Gruppe Radio-Button (Mitarbeiter vs. Kunde)
            GrpBxCreatKntktMaKunde.Size = new Size(165, 40);
            GrpBxCreatKntktMaKunde.Location = new Point(10, 10);

            // Platzierung Gruppe "Kontaktdaten"
            GrpBxCreatKntktDatenAlle.Size = new Size(365, 400);
            GrpBxCreatKntktDatenAlle.Location = new Point(10, 60);

            // Platzierung Gruppe "Mitarbeiterdaten"
            GrpBxDatenMA.Size = new Size(365, 490);
            GrpBxDatenMA.Location = new Point(385, 60);

            // Platzierung Radio-Buttons (Mitarbeiter vs. Kunde)
            RdbCreatKntktMa.Location = new Point(10, 15);
            RdbCreatKntktKunde.Location = new Point(100, 15);

            // Platzierung Labels und Eingabefelder der Gruppe "Kontaktdaten" (inkl. Start TabIndex bei 1)
            // Erfassung Default-Tag als Vorbereitung für Validierung Eingabefelder mit TRUE (für OK-Fall) 
            PlacementLabelAndField(groupLabelEmployeesAndCustomers, groupFieldEmployeesAndCustomers, ref tabIndexCounter);

            // Platzierung Labels und Eingabefelder der Gruppe "Mitarbeiterdaten" (inkl. Start TabIndex fortführend)
            // Erfassung Default-Tag als Vorbereitung für Validierung Eingabefelder mit TRUE (für OK-Fall) 
            PlacementLabelAndField(groupLabelEmployees, groupFieldEmployees, ref tabIndexCounter);

            // Platzierung Buttons "Speichern und ..."
            CmdCreateKntktKontaktErstellen.Size = new Size(150, 60);
            CmdCreateKntktKontaktErstellen.Location = new Point(445, 580);
            CmdCreateKntktDashboard.Size = new Size(150, 60);
            CmdCreateKntktDashboard.Location = new Point(600, 580);
            CmdCreateKntktVerwerfen.Size = new Size(305, 40);
            CmdCreateKntktVerwerfen.Location = new Point(445, 645);
        }

        // Platzierung Labels und Eingabefelder (dynamisch)
        private void PlacementLabelAndField(System.Windows.Forms.Label[] groupLabel, Control[] groupField, ref int tabIndexCounter)
        {
            int startLocation = 30;
            int labelXAchse = 10;
            int controlXAchse = 150;

            for (int i = 0; i < groupField.Length; i++)
            {
                groupLabel[i].Location = new Point(labelXAchse, startLocation);
                groupField[i].Size = new Size(200, 20);
                groupField[i].Location = new Point(controlXAchse, startLocation);

                startLocation += 30;

                // Label irrelevant für Tab und daher mit TabStop "false"
                groupLabel[i].TabStop = false;
                // Eingabefeld relevant für Tab und daher durchnummeriert (Start bei 1)
                groupField[i].TabIndex = tabIndexCounter++;

                // Default-Tag relevant für Validierung Eingabefelder (Start mit TRUE)
                groupField[i].Tag = tagOK;
            }
        }

        // Erstellung ToolTip für spezifische Labels (zur besseren Verständlichkeit)
        private void InitializationLabelToolTip()
        {
            var initializationSetToolTip = KontaktErstellenInitializations.SetToolTip(this);
            var setToolTip = new SetToolTip();
            setToolTip.SetLabelToolTip(initializationSetToolTip);
        }
      
        // Initalisierung Radio-Button auf Basis "Kontaktart"
        private void InitializationTypeOfContact()
        {
            if (typeOfContactNew == "mitarbeiter")
            {
                RdbCreatKntktMa.Checked = true;
                TxtCreatKntktMaManr.Text = GenerateContactNumber(true);
            }
            
            else if (typeOfContactNew == "kunde")
            {
                RdbCreatKntktKunde.Checked = true;
                GenerateContactNumber(false);
            }
        }

        // Aktualisierung gesperrte Felder auf Basis "Kontaktart"
        private void UpdateTypeOfContact()
        {
            if (RdbCreatKntktMa.Checked)
            {
                typeOfContactNew = "mitarbeiter";
                RdbCreatKntktMa.Checked = true;
                GrpBxDatenMA.Enabled = true;
                TxtCreatKntktMaManr.Text = GenerateContactNumber(true);
            }
            
            else if (RdbCreatKntktKunde.Checked)
            {
                typeOfContactNew = "kunde";
                RdbCreatKntktKunde.Checked = true;
                GrpBxDatenMA.Enabled = false;
                GenerateContactNumber(false);
                CleanGroupFieldEmployees();
            }
        }

        // Automatische Generierung Mitarbeiter-/Kunden Nr. (gemäss JSON)
        private string GenerateContactNumber(bool isEmployee)
        {
            return contactNumberNew = ClientAndEmployeeNumber.GetNumberNext(isEmployee);
        }

        // Bereinigung der Eingabefelder der Gruppe Mitarbeiter (bei Wechsel zu Kunde)
        private void CleanGroupFieldEmployees()
        {
            foreach (Control field in groupFieldEmployees)
            {
                switch (field)
                {
                    case System.Windows.Forms.TextBox txtbxField:
                        txtbxField.Clear();
                        break;
                    case System.Windows.Forms.ComboBox cmbxField:
                        cmbxField.SelectedIndex = -1;
                        break;
                    case NumericUpDown numField:
                        numField.Value = numField.Minimum;
                        break;
                }

                field.BackColor = backColorOK;
                field.Tag = tagOK;
            }
        }

        // Wechsel von Mitarbeiter zu Kunde (und umgekehrt)
        private void RdbCreatKntktMa_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTypeOfContact();
        }

        // Wechsel von Mitarbeiter zu Kunde (und umgekehrt)
        private void RdbCreatKntktKunde_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTypeOfContact();
        }


        // Klick Button "Speichern und neuer Kontakt erstellen"
        private void CmdCreateKntktKontaktErstellen_Click(object sender, EventArgs e)
        {
            var checkAndValidation = new CheckAndValidationFields();
            var checkAndValidationContent = KontaktErstellenInitializations.CheckAndValidationFieldsContent(this);
            bool checkFieldTag = checkAndValidation.ValidationFields(checkAndValidationContent);

            if (checkFieldTag)
            {
                // Speicherung der Daten in JSON "contacts", falls Duplikatencheck erfolgreich
                if (ContactData.SaveContactData(saveMode, contactStatus, typeOfContactNew, contactNumberNew, groupFieldEmployeesAndCustomers, groupFieldEmployees))
                {
                    // Speicherung der Kontakt Nr. in JSON "clientAndEmployeeNumbers"             
                    ClientAndEmployeeNumber.SaveNumberCurrent(typeOfContactNew == "mitarbeiter");

                    this.FormClosed += (s, arg) =>
                    {
                        // Erstellung neues Form "KontaktErstellen"
                        var kontaktErstellenForm = new KontaktErstellen(typeOfContactNew);
                        kontaktErstellenForm.Show();
                    };

                    this.Close();
                }
            }
        }

        // Klick Button "Speichern und zurück zum Dashboard"
        private void CmdCreateKntktDashboard_Click(object sender, EventArgs e)
        {
            var checkAndValidation = new CheckAndValidationFields();
            var checkAndValidationContent = KontaktErstellenInitializations.CheckAndValidationFieldsContent(this);
            bool checkFieldTag = checkAndValidation.ValidationFields(checkAndValidationContent);

            if (checkFieldTag)
            {
                // Speicherung der Daten in JSON "contacts", falls Duplikatencheck erfolgreich
                if (ContactData.SaveContactData(saveMode, contactStatus, typeOfContactNew, contactNumberNew, groupFieldEmployeesAndCustomers, groupFieldEmployees))
                {
                    // Speicherung der Kontakt Nr. in JSON "clientAndEmployeeNumbers"              
                    ClientAndEmployeeNumber.SaveNumberCurrent(typeOfContactNew == "mitarbeiter");
                    this.Close();
                }
            }
        }

        // Klick Button "Eingaben verwerfen und zurück zum Dashboard"
        private void CmdCreateKntktVerwerfen_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Möchtest du die Eingaben verwerfen und zurück zum Dashboard wechseln?", "'Kontakt erstellen' abbrechen?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}