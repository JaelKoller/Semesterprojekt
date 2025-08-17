
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Semesterprojekt
{
    public partial class KontaktErstellen : Form
    {
        // Initalisierung String "typeOfContact" für "Speichern und neuer Kontakt erstellen"
        private string typeOfContactNew;

        // Dateipfad für Kontaktdaten-Liste
        // private static readonly string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
        // private readonly string contactDataPath = Path.Combine(projectRoot, "data", "contacts.json");

        // Initialisierung mehrfach verwendeter Label-/Control-Gruppen
        private System.Windows.Forms.Label[] groupLabelEmployeesAndCustomers;
        private Control[] groupFieldEmployeesAndCustomers;
        private System.Windows.Forms.Label[] groupLabelEmployees;
        private Control[] groupFieldEmployees;
        private System.Windows.Forms.Label[] groupLabelToolTip;
        private Control[] checkFieldIgnore;

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
            this.Size = new Size(775, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;

            // Initialisierung mehrfach verwendeter Label-/Control-Gruppen
            groupLabelEmployeesAndCustomers = GroupLabelEmployeesAndCustomers();
            groupFieldEmployeesAndCustomers = GroupFieldEmployeesAndCustomers();
            groupLabelEmployees = GroupLabelEmployees();
            groupFieldEmployees = GroupFieldEmployees();
            groupLabelToolTip = GroupLabelToolTip();

            Design();
            ContactDataContent();
            InitializationLabelToolTip();

            typeOfContactNew = typeOfContact;
            InitializationTypeOfContact();
            UpdateTypeOfContact();
        }

        // Design (Platzierung) der Eingabe-Felder usw.
        private void Design()
        {
            // Platzierung Gruppe Radio-Button (Mitarbeiter vs. Kunde)
            GrpBxCreatKntktMaKunde.Size = new Size(165, 40);
            GrpBxCreatKntktMaKunde.Location = new Point(10, 10);

            // Platzierung Gruppe Mitarbeiter UND Kunde (alle)
            GrpBxCreatKntktDatenAlle.Size = new Size(365, 390);
            GrpBxCreatKntktDatenAlle.Location = new Point(10, 55);

            // Platzierung Gruppe NUR Mitarbeiter (ohne Kunde)
            GrpBxDatenMA.Size = new Size(365, 390);
            GrpBxDatenMA.Location = new Point(385, 55);

            // Platzierung Radio-Buttons (Mitarbeiter vs. Kunde)
            RdbCreatKntktMa.Location = new Point(10, 15);
            RdbCreatKntktKunde.Location = new Point(100, 15);

            // Platzierung Labels und Eingabefelder der Gruppe Mitarbeiter UND Kunde (alle)
            // Zählerstart (Index) für Labels und Eingabefelder der Gruppe Mitarbeiter UND Kunde (alle) mit 1 
            // Erfassung Default-Tag als Vorbereitung für Validierung Eingabefelder der Gruppe Mitarbeiter UND Kunde (alle) mit TRUE (für OK-Fall) 
            PlacementLabelAndField(groupLabelEmployeesAndCustomers, groupFieldEmployeesAndCustomers, ref tabIndexCounter);

            // Platzierung Labels und Eingabefelder der Gruppe Mitarbeiter (ohne Kunde)
            // Zählerstart (Index) für Labels und Eingabefelder der Gruppe Mitarbeiter (ohne Kunde) fortführend
            // Erfassung Default-Tag als Vorbereitung für Validierung Eingabefelder der Gruppe Mitarbeiter (ohne Kunde) mit TRUE (für OK-Fall) 
            PlacementLabelAndField(groupLabelEmployees, groupFieldEmployees, ref tabIndexCounter);

            // Platzierung Buttons "Speichern und ..."
            CmdCreateKntktKontaktErstellen.Size = new Size(150, 60);
            CmdCreateKntktKontaktErstellen.Location = new Point(445, 470);
            CmdCreateKntktDashboard.Size = new Size(150, 60);
            CmdCreateKntktDashboard.Location = new Point(600, 470);
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

        // Erstellung Array für Labels der Gruppe Mitarbeiter UND Kunde (alle)
        private System.Windows.Forms.Label[] GroupLabelEmployeesAndCustomers()
        {
            groupLabelEmployeesAndCustomers = new System.Windows.Forms.Label[]
            {
                LblCreatKntktTitel,
                LblCreatKntktAnrede,
                LblCreatKntktVorname,
                LblCreatKntktName,
                LblCreatKntktBirthday,
                LblCreatKntktGeschlecht,
                LblCreatKntktAdresse,
                LblCreatKntktPLZ,
                LblCreatKntktOrt,
                LblCreatKntktTelGeschaeft,
                LblCreatKntktTelMobile,
                LblCreatKntktEmail
            };

            return groupLabelEmployeesAndCustomers;
        }

        // Erstellung Array für Labels der Gruppe Mitarbeiter (ohne Kunde)
        private System.Windows.Forms.Label[] GroupLabelEmployees()
        {
            groupLabelEmployees = new System.Windows.Forms.Label[]
            {
                LblCreatKntktMaMaNr,
                LblCreatKntktMaAHVNr,
                LblCreatKntktMaNationalitaet,
                LblCreatKntktMaKader,
                LblCreatKntktMaBeschGrad,
                LblCreaKntktMaAbteilung,
                LblCreatKntktMaRolle,
                LblCreatKntktMaLehrj,
                LblCreatKntktMaAktLehrj,
                LblCreatKntktMaOfficeNumber,
                LblCreatKntktEintrDatum,
                LblCreatKntktAustrDatum
            };

            return groupLabelEmployees;
        }

        // Erstellung Array für Labels für ToolTip
        private System.Windows.Forms.Label[] GroupLabelToolTip()
        {
            groupLabelToolTip = new System.Windows.Forms.Label[]
            {
                LblCreatKntktTitel,
                LblCreatKntktBirthday,
                LblCreatKntktPLZ,
                LblCreatKntktMaAHVNr,
                LblCreatKntktMaNationalitaet,
                LblCreatKntktMaLehrj,
                LblCreatKntktMaAktLehrj,
                LblCreatKntktEintrDatum,
                LblCreatKntktAustrDatum
            };

            return groupLabelToolTip;
        }

        // Erstellung Array für Eingabefelder der Gruppe Mitarbeiter UND Kunde (alle)
        private Control[] GroupFieldEmployeesAndCustomers()
        {
            groupFieldEmployeesAndCustomers = new Control[]
            {
                TxtCreatKntktTitel,
                CmBxCreatKntktAnrede,
                TxtCreatKntktVorname,
                TxtCreatKntktName,
                TxtCreatKntktBirthday,
                CmBxCreatKntktGeschlecht,
                TxtCreatKntktAdr,
                TxtCreatKntktPLZ,
                TxtCreatKntktOrt,
                TxtCreatKntktTelGeschaeft,
                TxtCreatKntktTelMobile,
                TxtCreatKntktEmail
            };
            
            return groupFieldEmployeesAndCustomers;
        }

        // Erstellung Array für Eingabefelder der Gruppe Mitarbeiter (ohne Kunde)
        private Control[] GroupFieldEmployees()
        {
            groupFieldEmployees = new Control[]
            {
                TxtCreatKntktMaManr,
                TxtCreatKntktMaAHVNr,
                TxtCreatKntktMaNationalitaet,
                TxtCreatKntktMaKader,
                NumCreatKntktMaBeschGrad,
                TxtCreatKntktMaAbteilung,
                TxtCreatKntktMaRolle,
                NumCreatKntktMaLehrj,
                NumCreatKntktMaAktLehrj,
                NumCreatKntktMaOfficeNumber,
                TxtCreatKntktEintrDatum,
                TxtCreatKntktAustrDatum
            };

            return groupFieldEmployees;
        }

        // Erstellung Array für KEINE-Pflichtfelder-Prüfung
        private Control[] CheckFieldIgnore()
        {
            checkFieldIgnore = new Control[]
            {
                TxtCreatKntktTitel,
                // bei Mitarbeitern bleibt das Feld "Pflicht"
                (!RdbCreatKntktMa.Checked ? TxtCreatKntktTelGeschaeft : null),
                 // bei Mitarbeitern mit CH-Nationalität bleibt das Feld "Pflicht"
                (RdbCreatKntktKunde.Checked || (!string.IsNullOrWhiteSpace(TxtCreatKntktMaNationalitaet.Text) && TxtCreatKntktMaNationalitaet.Text.ToUpper() != "CH") ? TxtCreatKntktMaAHVNr : null),            
                TxtCreatKntktMaKader,
                NumCreatKntktMaLehrj,
                NumCreatKntktMaAktLehrj,
                // bei Mitarbeiter bleibt das Feld "Pflicht"
                (!RdbCreatKntktMa.Checked ? TxtCreatKntktEintrDatum : null),
                // bei enthaltenem Wert wird das Feld validiert
                (string.IsNullOrWhiteSpace(TxtCreatKntktAustrDatum.Text) ? TxtCreatKntktAustrDatum: null),
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
                Title = LblCreatKntktTitel,
                Birthday = LblCreatKntktBirthday,
                PostalCode = LblCreatKntktPLZ,
                AHVNumber = LblCreatKntktMaAHVNr,
                Nationality = LblCreatKntktMaNationalitaet,
                AcademicYear = LblCreatKntktMaLehrj,
                CurrentAcademicYear = LblCreatKntktMaAktLehrj,
                DateOfEntry = LblCreatKntktEintrDatum,
                DateOfExit = LblCreatKntktAustrDatum
            };
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

        private void RdbCreatKntktMa_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTypeOfContact();
        }

        private void RdbCreatKntktKunde_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTypeOfContact();
        }

        private void CmdCreateKntktKontaktErstellen_Click(object sender, EventArgs e)
        {
            var checkAndValidation = new CheckAndValidationFields();
            var checkAndValidationContent = CheckAndValidationFieldsContent();
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

        private void CmdCreateKntktDashboard_Click(object sender, EventArgs e)
        {
            var checkAndValidation = new CheckAndValidationFields();
            var checkAndValidationContent = CheckAndValidationFieldsContent();
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

        // Initialisierung Argumente (Inhalt) für Klasse "ContactData"
        private InitializationContactData ContactDataContent()
        {
            return new InitializationContactData
            {
                Title = TxtCreatKntktTitel,
                Salutation = CmBxCreatKntktAnrede,
                FirstName = TxtCreatKntktVorname,
                LastName = TxtCreatKntktName,
                Birthday = TxtCreatKntktBirthday,
                Gender = CmBxCreatKntktGeschlecht,
                Address = TxtCreatKntktAdr,
                PostalCode = TxtCreatKntktPLZ,
                City = TxtCreatKntktOrt,
                BusinessNumber = TxtCreatKntktTelGeschaeft,
                MobileNumber = TxtCreatKntktTelMobile,
                Email = TxtCreatKntktEmail,
                EmployeeNumber = TxtCreatKntktMaManr,
                AHVNumber = TxtCreatKntktMaAHVNr,
                Nationality = TxtCreatKntktMaNationalitaet,
                ManagementLevel = TxtCreatKntktMaKader,
                LevelOfEmployment = NumCreatKntktMaBeschGrad,
                Department = TxtCreatKntktMaAbteilung,
                Role = TxtCreatKntktMaRolle,
                AcademicYear = NumCreatKntktMaLehrj,
                CurrentAcademicYear = NumCreatKntktMaAktLehrj,
                OfficeNumber = NumCreatKntktMaOfficeNumber,
                DateOfEntry = TxtCreatKntktEintrDatum,
                DateOfExit = TxtCreatKntktAustrDatum
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
                IsEmployee = RdbCreatKntktMa.Checked,
                IsClient = RdbCreatKntktKunde.Checked,
                Salutation = CmBxCreatKntktAnrede,
                Birthday = TxtCreatKntktBirthday,
                Gender = CmBxCreatKntktGeschlecht,
                PostalCode = TxtCreatKntktPLZ,
                Email = TxtCreatKntktEmail,
                AHVNumber = TxtCreatKntktMaAHVNr,
                Nationality = TxtCreatKntktMaNationalitaet,
                DateOfEntry = TxtCreatKntktEintrDatum,
                DateOfExit = TxtCreatKntktAustrDatum
            };
        }
    }
}