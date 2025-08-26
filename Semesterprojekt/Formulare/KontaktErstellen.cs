using System;
using System.Drawing;
using System.Windows.Forms;

namespace Semesterprojekt
{
    public partial class KontaktErstellen : Form
    {
        // Initalisierung String "typeOfContact" für "Speichern und neuer Kontakt erstellen"
        private string typeOfContactNew;
        private string typeOfContactEmployee = "mitarbeiter";
        private string typeOfContactCustomer = "kunde";

        // Initialisierung Klasse "KontaktErstellenLabelAndControlGroups"
        internal KontaktErstellenLabelAndControlGroups groups;

        // Initialisierung mehrfach verwendeter Label-/Control-Gruppen
        internal System.Windows.Forms.Label[] groupLabelEmployeesAndCustomers;
        internal Control[] groupFieldEmployeesAndCustomers;
        internal System.Windows.Forms.Label[] groupLabelEmployees;
        internal Control[] groupFieldEmployees;
        internal System.Windows.Forms.Label[] groupLabelToolTip;

        // Initialisierung verwendeter BackColor (analog separater Klasse)
        private Color backColorOK = SystemColors.Window;

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

            KontaktErstellenDesign.Design(this);
            KontaktErstellenInitializations.ContactDataContent(this);
            KontaktErstellenDesign.InitializationLabelToolTip(this);

            typeOfContactNew = typeOfContact;
            InitializationTypeOfContact();
            UpdateTypeOfContact();

            // Initialisierung (Registrierung) ESC für Rückkehr zu Dashboard (analog Button "Eingaben verwerfen ...")
            this.CancelButton = CmdCreateKntktVerwerfen;
        }
      
        // Initalisierung Radio-Button auf Basis "Kontaktart" (Generierung Kontakt Nr. mit Update)
        private void InitializationTypeOfContact()
        {
            if (typeOfContactNew == typeOfContactEmployee)
            {
                RdbCreatKntktMa.Checked = true;
            }
            
            else if (typeOfContactNew == typeOfContactCustomer)
            {
                RdbCreatKntktKunde.Checked = true;
            }
        }

        // Aktualisierung gesperrte Felder auf Basis "Kontaktart"
        private void UpdateTypeOfContact()
        {
            if (RdbCreatKntktMa.Checked)
            {
                typeOfContactNew = typeOfContactEmployee;
                RdbCreatKntktMa.Checked = true;
                GrpBxDatenMA.Enabled = true;
                TxtCreatKntktMaManr.Text = GenerateContactNumber(true);
            }
            
            else if (RdbCreatKntktKunde.Checked)
            {
                typeOfContactNew = typeOfContactCustomer;
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
                field.Tag = "true";
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
                    ClientAndEmployeeNumber.SaveNumberCurrent(typeOfContactNew == typeOfContactEmployee);

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
                    ClientAndEmployeeNumber.SaveNumberCurrent(typeOfContactNew == typeOfContactEmployee);
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