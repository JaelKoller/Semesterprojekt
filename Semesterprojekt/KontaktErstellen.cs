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

namespace Semesterprojekt
{
    public partial class KontaktErstellen : Form
    {
        // Initalisierung String "typeOfContact" für "Speichern und neuer Kontakt erstellen"
        string typeOfContactNew;
        
        public KontaktErstellen(string typeOfContact)
        {
            InitializeComponent();
            this.Size = new Size(750, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            Design();

            typeOfContactNew = typeOfContact;
            InitalizationTypeOfContact();
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

            // Vorbereitung für Platzierung Labels der Gruppe Mitarbeitende UND Kunde (alle)
            // Vorbereitung für Platzierung Eingabefelder der Gruppe Mitarbeitende UND Kunde (alle)
            // Platzierung Labels und Eingabefelder der Gruppe Mitarbeitende UND Kunde (alle)
            // Zählerstart (Index) für Labels und Eingabefelder der Gruppe Mitarbeitende UND Kunde (alle) mit 1 
            // Erfassung Default-Tag als Vorbereitung für Validierung Eingabefelder der Gruppe Mitarbeitende UND Kunde (alle) mit TRUE (für OK-Fall) 
            System.Windows.Forms.Label[] groupLabelEmployeesAndCustomers = GroupLabelEmployeesAndCustomers();
            Control[] groupFieldEmployeesAndCustomers = GroupFieldEmployeesAndCustomers();
            int tabIndexCounter = PlacementLabelAndField(groupLabelEmployeesAndCustomers, groupFieldEmployeesAndCustomers, 1);

            // Vorbereitung für Platzierung Labels der Gruppe Mitarbeitende (ohne Kunde)
            // Vorbereitung für Platzierung Eingabefelder der Gruppe Mitarbeitende (ohne Kunde)
            // Platzierung Labels und Eingabefelder der Gruppe Mitarbeitende (ohne Kunde)
            // Zählerstart (Index) für Labels und Eingabefelder der Gruppe Mitarbeitende (ohne Kunde) fortführend
            // Erfassung Default-Tag als Vorbereitung für Validierung Eingabefelder der Gruppe Mitarbeitende (ohne Kunde) mit TRUE (für OK-Fall) 
            System.Windows.Forms.Label[] groupLabelEmployees = GroupLabelEmployees();
            Control[] groupFieldEmployees = GroupFieldEmployees();
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
                groupField[i].Tag = "true";
            }

            return tabIndexCounter;
        }

        // Erstellung Array für Labels der Gruppe Mitarbeitende UND Kunde (alle)
        private System.Windows.Forms.Label[] GroupLabelEmployeesAndCustomers()
        {
            System.Windows.Forms.Label[] groupLabelEmployeesAndCustomers = new System.Windows.Forms.Label[]
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
            System.Windows.Forms.Label[] groupLabelEmployees = new System.Windows.Forms.Label[]
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
            Control[] groupFieldEmployeesAndCustomers = new Control[]
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
            Control[] groupFieldEmployees = new Control[]
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

        // Initalisierung Radio-Button auf Basis "Kontaktart"
        private void InitalizationTypeOfContact()
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
            ValidationFields();

            List<Control> groupFieldAll = new List<Control>();
            groupFieldAll.AddRange(GroupFieldEmployeesAndCustomers());
            groupFieldAll.AddRange(GroupFieldEmployees());

            bool checkFieldTag = true;

            foreach (Control field in groupFieldAll)
            {
                checkFieldTag = field.Tag == "false" ? false : checkFieldTag;
            }

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
            this.Close(); // AUSARBEITUNG bzgl. SPEICHERUNG OFFEN
        }

        // Prüfung Felder gemäss Erwartungen (leere Felder, Defaultwerte usw.)
        private void ValidationFields()
        {
            Control[] groupFieldEmployeesAndCustomers = GroupFieldEmployeesAndCustomers();
            Control[] groupFieldEmployees = GroupFieldEmployees();

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

            List<Control> groupFieldAll = new List<Control>();
            groupFieldAll.AddRange(groupFieldEmployeesAndCustomers);
            groupFieldAll.AddRange(groupFieldEmployees);

            ValidationFieldsExtension(groupFieldAll);           
        }

        // Prüfung einzelner Felder gemäss Erwartungen (leere Felder, Defaultwerte usw.)
        private void CheckFields(Control field)
        {
            Control[] checkFieldIgnore = CheckFieldIgnore();

            if (checkFieldIgnore.Contains(field))
            {
                return;
            }

            else if (field is ComboBox cbxField && string.IsNullOrWhiteSpace(field.Text))
            {
                // Einfärbung technisch nicht möglich (diverse Versuche gescheitert)
                field.Tag = "false";
            }

            else if (field is DateTimePicker dateField && dateField.Value.Date == new DateTime(1900, 1, 1))
            {
                // Einfärbung technisch nicht möglich (diverse Versuche gescheitert)
                field.Tag = "false";
            }

            else if (field is NumericUpDown numField && (numField.Value == numField.Minimum || numField.Value == numField.Maximum))
            {
                numField.BackColor = Color.LightPink;
                field.Tag = "false";
            }

            else if (string.IsNullOrWhiteSpace(field.Text))
            {
                field.BackColor = Color.LightPink;
                field.Tag = "false";
            }

            else
            {
                field.BackColor = SystemColors.Window;
                field.Tag = "true";
            }
        }

        // Prüfung einzelner Spezifalfelder gemäss Erwartungen inkl. Popup
        private void ValidationFieldsExtension(List<Control> groupFieldAll)
        {
            bool checkBackColor = false;
            
            foreach (Control field in groupFieldAll)
            {
                checkBackColor = field.BackColor == Color.LightPink ? true : checkBackColor;
            }

            foreach (Control field in groupFieldAll)
            {
                if (checkBackColor)
                {
                    ShowMessageBox("Bitte fülle alle Pflichtfelder korrekt aus!");
                    return;
                }
                
                else if (CmBxCreatKntktAnrede.Tag == "false")
                {
                    ShowMessageBox("Anrede fehlt");
                    return;
                }

                else if (DateCreatKntktBirthday.Tag == "false")
                {
                    ShowMessageBox(@"Geburtsdatum ""01.01.1900"" entspricht Defaultwert und ist ungültig");
                    DateCreatKntktBirthday.Focus();
                    return;
                }

                else if (CmBxCreatKntktGeschlecht.Tag == "false")
                {
                    ShowMessageBox("Geschlecht fehlt");
                    return;
                }

                else if (TxtCreatKntktPLZ.Tag == "true" || TxtCreatKntktPLZ.Tag == "false")
                {
                    CheckPLZNumber();
                    return;
                }

                else if (TxtCreatKntktMaAHVNr.Tag == "true" || TxtCreatKntktMaAHVNr.Tag == "false")
                {
                    CheckAHVNumber();
                    return;
                }

                else if (DateCreatKntktEintrDatum.Tag == "false")
                {
                    ShowMessageBox(@"Eintrittsdatum ""01.01.1900"" entspricht Defaultwert und ist ungültig");
                    DateCreatKntktEintrDatum.Focus();
                    return;
                }
            }
        }

        // Prüfung Format auf 4 bis 5 Zahlen (Standard für Schweiz und umliegende Länder)
        private void CheckPLZNumber()
        {
            bool checkPLZNumber = Regex.IsMatch(TxtCreatKntktPLZ.Text, @"^\d{4,5}$");

            if (Regex.IsMatch(TxtCreatKntktPLZ.Text, @"^\d{4,5}$"))
            {
                TxtCreatKntktPLZ.Tag = "true";
            }

            else
            {
                TxtCreatKntktPLZ.Tag = "false";
                ShowMessageBox(string.Format(@"PLZ ""{0}"" ist ungültig", TxtCreatKntktPLZ.Text));
                TxtCreatKntktPLZ.Focus();
            }
        }

        // Prüfung AHV-Nummer auf Korrektheit und Vollständigkeit (1. Schritt)
        private void CheckAHVNumber()
        {
            if (RdbCreatKntktKunde.Checked || TxtCreatKntktMaNationalitaet.Text.ToUpper() != "CH")
            {
                TxtCreatKntktMaAHVNr.BackColor = SystemColors.Window;
                TxtCreatKntktMaAHVNr.Tag = "true";
            }

            bool checkAHVNumber = ValidationAHVNumber(TxtCreatKntktMaAHVNr.Text);

            if (!checkAHVNumber)
            {
                ShowMessageBox(string.Format(@"AHV-Nummer ""{0}"" ist ungültig", TxtCreatKntktMaAHVNr.Text));
                TxtCreatKntktMaAHVNr.Focus();
                TxtCreatKntktMaAHVNr.BackColor = Color.LightPink;
            }

            else
            {
                TxtCreatKntktMaAHVNr.BackColor = SystemColors.Window;
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
            MessageBox.Show(message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Erstellung Array für KEINE-Pflichtfelder-Prüfung
        private Control[] CheckFieldIgnore()
        {
            Control[] checkFieldIgnore = new Control[]
            {
                TxtCreatKntktTitel,
                (!RdbCreatKntktMa.Checked ? TxtCreatKntktTelGeschaeft : null), // bei Mitarbeitenden bleibt das Feld "Pflicht"
                (TxtCreatKntktMaNationalitaet.Text.ToUpper() != "CH" ? TxtCreatKntktMaAHVNr : null), // bei ausländischer Nationalität ist das Feld "nicht Pflicht"
                TxtCreatKntktMaKader,
                NumCreatKntktMaLehrj,
                NumCreatKntktMaAktLehrj,
                DateCreatKntktAustrDatum,
            };

            return checkFieldIgnore;
        }
    }
}