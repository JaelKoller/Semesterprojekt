using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
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

        // Initialisierung mehrfach verwendete Control-Gruppen
        private System.Windows.Forms.Label[] groupLabelEmployeesAndCustomers;
        private Control[] groupFieldEmployeesAndCustomers;
        private System.Windows.Forms.Label[] groupLabelEmployees;
        private Control[] groupFieldEmployees;
        private Control[] checkFieldIgnore;

        // Initialisierung mehrfach verwendeter Index-Counter
        private int tabIndexCounter = 1;

        // Initialisierung mehrfach verwendeter BackColor
        private Color backColorOK = SystemColors.Window;
        private Color backColorNOK = Color.LightPink;

        // Initialisierung mehrfach verwendetes Tag
        private string tagOK = "true";
        private string tagNOK = "false";

        public KontaktErstellen(string typeOfContact)
        {
            InitializeComponent();
            this.Size = new Size(750, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Initialisierung mehrfach verwendete Control-Gruppen
            groupLabelEmployeesAndCustomers = GroupLabelEmployeesAndCustomers();
            groupFieldEmployeesAndCustomers = GroupFieldEmployeesAndCustomers();
            groupLabelEmployees = GroupLabelEmployees();
            groupFieldEmployees = GroupFieldEmployees();

            Design();
            InitializationLabelToolTip();

            typeOfContactNew = typeOfContact;
            InitializationTypeOfContact();
            UpdateTypeOfContact();
        }

        // Design (Platzierung) der Eingabe-Felder usw.
        private void Design()
        {
            // Platzierung Gruppe Radio-Button (Mitarbeitende vs. Kunde)
            GrpBxCreatKntktMaKunde.Size = new Size(185, 40);
            GrpBxCreatKntktMaKunde.Location = new Point(10, 10);

            // Platzierung Gruppe Mitarbeitende UND Kunde (alle)
            GrpBxCreatKntktDatenAlle.Size = new Size(350, 390);
            GrpBxCreatKntktDatenAlle.Location = new Point(10, 55);

            // Platzierung Gruppe NUR Mitarbeitende (ohne Kunde)
            GrpBxDatenMA.Size = new Size(350, 390);
            GrpBxDatenMA.Location = new Point(375, 55);

            // Platzierung Radio-Buttons (Mitarbeitende vs. Kunde)
            RdbCreatKntktMa.Location = new Point(10, 15);
            RdbCreatKntktKunde.Location = new Point(120, 15);

            // Platzierung Labels und Eingabefelder der Gruppe Mitarbeitende UND Kunde (alle)
            // Zählerstart (Index) für Labels und Eingabefelder der Gruppe Mitarbeitende UND Kunde (alle) mit 1 
            // Erfassung Default-Tag als Vorbereitung für Validierung Eingabefelder der Gruppe Mitarbeitende UND Kunde (alle) mit TRUE (für OK-Fall) 
            tabIndexCounter = PlacementLabelAndField(groupLabelEmployeesAndCustomers, groupFieldEmployeesAndCustomers, tabIndexCounter);

            // Platzierung Labels und Eingabefelder der Gruppe Mitarbeitende (ohne Kunde)
            // Zählerstart (Index) für Labels und Eingabefelder der Gruppe Mitarbeitende (ohne Kunde) fortführend
            // Erfassung Default-Tag als Vorbereitung für Validierung Eingabefelder der Gruppe Mitarbeitende (ohne Kunde) mit TRUE (für OK-Fall) 
            PlacementLabelAndField(groupLabelEmployees, groupFieldEmployees, tabIndexCounter);

            // Platzierung Buttons "Speichern und ..."
            CmdCreateKntktKontaktErstellen.Size = new Size(150, 60);
            CmdCreateKntktKontaktErstellen.Location = new Point(420, 470);
            CmdCreateKntktDashboard.Size = new Size(150, 60);
            CmdCreateKntktDashboard.Location = new Point(575, 470);
        }

        // Platzierung Labels und Eingabefelder (dynamisch)
        private int PlacementLabelAndField(System.Windows.Forms.Label[] groupLabel, Control[] groupField, int indexCounter)
        {
            int startLocation = 20;
            int labelXAchse = 10;
            int controlXAchse = 135;
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

                // Default-Tag relevant für Validierung Eingabefelder (Start mit TRUE)
                groupField[i].Tag = tagOK;
            }

            return tabIndexCounter;
        }

        // Erstellung Array für Labels der Gruppe Mitarbeitende UND Kunde (alle)
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

        // Erstellung Array für Labels der Gruppe Mitarbeitende (ohne Kunde)
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

        // Erstellung Array für Eingabefelder der Gruppe Mitarbeitende UND Kunde (alle)
        private Control[] GroupFieldEmployeesAndCustomers()
        {
            groupFieldEmployeesAndCustomers = new Control[]
            {
                TxtCreatKntktTitel,
                CmBxCreatKntktAnrede,
                TxtCreatKntktVorname,
                TxtCreatKntktName,
                DateCreatKntktBirthday,
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

        // Erstellung Array für Eingabefelder der Gruppe Mitarbeitende (ohne Kunde)
        private Control[] GroupFieldEmployees()
        {
            groupFieldEmployees = new Control[]
            {
                TxtCreatKntktMaManr,
                TxtCreatKntktMaAHVNr,
                TxtCreatKntktMaNationalitaet,
                TxtCreatKntktMaKader,
                NumCreatKntktMaBeschGrad,
                TxtCreattKntktMaAbteilung,
                TxtCreatKntktMaRolle,
                NumCreatKntktMaLehrj,
                NumCreatKntktMaAktLehrj,
                NumCreatKntktMaOfficeNumber,
                DateCreatKntktEintrDatum,
                DateCreatKntktAustrDatum
            };

            return groupFieldEmployees;
        }

        // Erstellung Array für KEINE-Pflichtfelder-Prüfung
        private Control[] CheckFieldIgnore()
        {
            checkFieldIgnore = new Control[]
            {
                TxtCreatKntktTitel,
                (!RdbCreatKntktMa.Checked ? TxtCreatKntktTelGeschaeft : null), // bei Mitarbeitenden bleibt das Feld "Pflicht"
                (!string.IsNullOrWhiteSpace(TxtCreatKntktMaNationalitaet.Text) && TxtCreatKntktMaNationalitaet.Text.ToUpper() != "CH" ? TxtCreatKntktMaAHVNr : null), // bei ausländischer Nationalität ist das Feld "nicht Pflicht"
                TxtCreatKntktMaKader,
                NumCreatKntktMaLehrj,
                NumCreatKntktMaAktLehrj,
                DateCreatKntktAustrDatum,
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

            SetLabelToolTip(toolTip, LblCreatKntktTitel, "Namenstitel (gekürzt)\r\nz.B. Dr., Ing., Prof.");
            SetLabelToolTip(toolTip, LblCreatKntktPLZ, "4-/5-stellige Postleitzahl\r\n(Schweiz und Nachbarländer)");
            SetLabelToolTip(toolTip,LblCreatKntktMaAHVNr, "Eingabe mit Punkten (CH-Norm)\r\nz.B. 756.1234.5678.90");
            SetLabelToolTip(toolTip,LblCreatKntktMaNationalitaet, "2-stelliger Länderkürzel\r\nz.B. CH, DE, FR, IT");
            SetLabelToolTip(toolTip, LblCreatKntktMaLehrj, "nur relevant für Lernende");
            SetLabelToolTip(toolTip, LblCreatKntktMaAktLehrj, "nur relevant für Lernende");
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

        // Initalisierung Radio-Button auf Basis "Kontaktart"
        private void InitializationTypeOfContact()
        {
            if (typeOfContactNew == "mitarbeitende")
            {
                RdbCreatKntktMa.Checked = true;
                TxtCreatKntktMaManr.Text = "TEST"; // AUTOMATISCHE GENERIERUNG IST EINZUBAUEN !!!
            }
            
            else if (typeOfContactNew == "kunde")
            {
                RdbCreatKntktKunde.Checked = true;
            }
        }

        // Aktualisierung gesperrte Felder auf Basis "Kontaktart"
        private void UpdateTypeOfContact()
        {
            if (RdbCreatKntktMa.Checked)
            {
                typeOfContactNew = "mitarbeitende";
                RdbCreatKntktMa.Checked = true;
                GrpBxDatenMA.Enabled = true;
            }
            
            else if (RdbCreatKntktKunde.Checked)
            {
                typeOfContactNew = "kunde";
                RdbCreatKntktKunde.Checked = true;
                GrpBxDatenMA.Enabled = false;
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
            bool checkFieldTag = ValidationFields();

            if (checkFieldTag)
            {
                this.FormClosed += (s, arg) =>
                {
                    // Erstellung neues Form "KontaktErstellen"
                    var kontaktErstellenForm = new KontaktErstellen(typeOfContactNew);
                    kontaktErstellenForm.Show();
                };
                this.Close();
            }
        }

        private void CmdCreateKntktDashboard_Click(object sender, EventArgs e)
        {
            bool checkFieldTag = ValidationFields();

            if (checkFieldTag)
            {
                this.Close();
            }
        }

        // Prüfung Felder gemäss Erwartungen (leere Felder, Defaultwerte usw.)
        private bool ValidationFields()
        {
            // Prüfung (Grundlagen)
            foreach (Control field in groupFieldEmployeesAndCustomers)
            {
                CheckFields(field);
            }

            if (RdbCreatKntktMa.Checked)
            {
                foreach (Control field in groupFieldEmployees)
                {
                    CheckFields(field);
                }
            }

            // Prüfung (vertieft)
            List<Control> groupFieldAll = new List<Control>();
            groupFieldAll.AddRange(groupFieldEmployeesAndCustomers);
            groupFieldAll.AddRange(groupFieldEmployees);

            ValidationFieldsExtension(groupFieldAll);

            // Ausgabe Validierungsstatus (für Speichervorgang)
            bool checkFieldTag = true;

            foreach (Control field in groupFieldAll)
            {
                checkFieldTag = field.Tag == tagNOK ? false : checkFieldTag;
            }

            return checkFieldTag;
        }

        // Prüfung einzelner Felder gemäss Erwartungen (leere Felder, Defaultwerte usw.)
        private void CheckFields(Control field)
        {
            Control[] checkFieldIgnore = CheckFieldIgnore();

            if (checkFieldIgnore.Contains(field))
            {
                field.BackColor = backColorOK;
                field.Tag = tagOK;
                return;
            }

            if (field is System.Windows.Forms.ComboBox cbxField && string.IsNullOrWhiteSpace(field.Text))
            {
                // Einfärbung technisch nicht möglich (diverse Versuche gescheitert)
                field.Tag = tagNOK;
                return;
            }

            if (field is DateTimePicker dateField && dateField.Value.Date == new DateTime(1900, 1, 1))
            {
                // Einfärbung technisch nicht möglich (diverse Versuche gescheitert)
                field.Tag = tagNOK;
                return;
            }

            if (field is NumericUpDown numField && (numField.Value == numField.Minimum || numField.Value == numField.Maximum))
            {
                numField.BackColor = backColorNOK;
                field.Tag = tagNOK;
                return;
            }

            if (string.IsNullOrWhiteSpace(field.Text))
            {
                field.BackColor = backColorNOK;
                field.Tag = tagNOK;
                return;
            }

            field.BackColor = backColorOK;
            field.Tag = tagOK;
        }

        // Prüfung einzelner Spezifalfelder gemäss Erwartungen inkl. Popup
        private void ValidationFieldsExtension(List<Control> groupFieldAll)
        {
            foreach (Control field in groupFieldAll)
            {
                if (field.BackColor == backColorNOK)
                {
                    ShowMessageBox("Bitte fülle alle Pflichtfelder korrekt aus!");
                    return;
                }
            }

            if (CmBxCreatKntktAnrede.Tag == tagNOK)
            {
                ShowMessageBox("Anrede fehlt");
                return;
            }

            if (DateCreatKntktBirthday.Tag == tagNOK)
            {
                ShowMessageBox(@"Geburtsdatum ""01.01.1900"" entspricht Defaultwert und ist ungültig");
                DateCreatKntktBirthday.Focus();
                return;
            }
            
            if (CmBxCreatKntktGeschlecht.Tag == tagNOK)
            {
                ShowMessageBox("Geschlecht fehlt");
                return;
            }

            CheckPLZNumber();
            if (TxtCreatKntktPLZ.Tag == tagNOK)
                return;

            CheckEMail();
            if (TxtCreatKntktEmail.Tag == tagNOK)
                return;

            CheckAHVNumber();
            if (TxtCreatKntktMaAHVNr.Tag == tagNOK)
                return;
            
            if (DateCreatKntktEintrDatum.Tag == tagNOK)
            {
                ShowMessageBox(@"Eintrittsdatum ""01.01.1900"" entspricht Defaultwert und ist ungültig");
                DateCreatKntktEintrDatum.Focus();
                return;
            }
        }

        // Prüfung Format auf 4 bis 5 Zahlen (Standard für Schweiz und umliegende Länder)
        private void CheckPLZNumber()
        {
            if (Regex.IsMatch(TxtCreatKntktPLZ.Text, @"^\d{4,5}$"))
            {
                TxtCreatKntktPLZ.BackColor = backColorOK;
                TxtCreatKntktPLZ.Tag = tagOK;
            }

            else
            {
                TxtCreatKntktPLZ.BackColor = backColorNOK;
                TxtCreatKntktPLZ.Tag = tagNOK;
                ShowMessageBox(string.Format(@"PLZ ""{0}"" ist ungültig", TxtCreatKntktPLZ.Text));
                TxtCreatKntktPLZ.Focus();
            }
        }

        private void CheckEMail()
        {           
            if (Regex.IsMatch(TxtCreatKntktEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                TxtCreatKntktEmail.BackColor = backColorOK;
                TxtCreatKntktEmail.Tag = tagOK;
            }

            else
            {
                TxtCreatKntktEmail.BackColor = backColorNOK;
                TxtCreatKntktEmail.Tag = tagNOK;
                ShowMessageBox(string.Format(@"E-Mail ""{0}"" ist ungültig", TxtCreatKntktEmail.Text));
                TxtCreatKntktEmail.Focus();
            }
        }

        // Prüfung AHV-Nummer auf Korrektheit und Vollständigkeit (1. Schritt)
        private void CheckAHVNumber()
        {
            if (RdbCreatKntktKunde.Checked || string.IsNullOrWhiteSpace(TxtCreatKntktMaNationalitaet.Text) || TxtCreatKntktMaNationalitaet.Text.ToUpper() != "CH")
            {
                TxtCreatKntktMaAHVNr.BackColor = backColorOK;
                TxtCreatKntktMaAHVNr.Tag = tagOK;
            }

            if (!ValidationAHVNumber(TxtCreatKntktMaAHVNr.Text))
            {
                TxtCreatKntktMaAHVNr.BackColor = backColorNOK;
                ShowMessageBox(string.Format(@"AHV-Nummer ""{0}"" ist ungültig", TxtCreatKntktMaAHVNr.Text));
                TxtCreatKntktMaAHVNr.Focus();

            }

            else
            {
                TxtCreatKntktMaAHVNr.BackColor = backColorOK;
            }
        }

        // Prüfung AHV-Nummer auf Korrektheit und vollständigkeit (2. Schritt)
        private bool ValidationAHVNumber(string ahvNumber)
        {
            // Prüfung Format gemäss CH-Norm (BSV): 756.xxxx.xxxx.xx
            string pattern = @"^756\.\d{4}\.\d{4}\.\d{2}$";

            if (!Regex.IsMatch(ahvNumber, pattern))
                return false;

            // Entfernung Punkte
            string ahvNumberNoPoints = ahvNumber.Replace(".", "");

            // Prüfung AHV-Nummer ohne Punkte
            if (ahvNumberNoPoints.Length != 13 || !ahvNumberNoPoints.All(char.IsDigit))
                return false;

            // Extraktion Ziffern (als Vorbereitung für Prüfziffer)
            int total = 0;

            for (int i = 0; i < 12; i++)
            {
                int digit = int.Parse(ahvNumberNoPoints[i].ToString());
                int weight = (i % 2 == 0) ? 1 : 3;
                total += digit * weight;
            }

            // Prüfung Prüfziffer gemäss Norm EAN-13 (BSV)
            int checkDigit = int.Parse(ahvNumberNoPoints[12].ToString());
            int expectation = (10 - (total % 10)) % 10;

            return checkDigit == expectation;
        }

        // Erzeugung MessageBox (Popup) bei fehlenden und/oder fehlerhaften Eingaben gemäss Erwartungen
        private void ShowMessageBox (string message)
        {
            MessageBox.Show(message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}