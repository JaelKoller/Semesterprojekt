using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            this.Size = new Size(800, 600);
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
            System.Windows.Forms.Label[] groupLabelEmployeesAndCustomers = GroupLabelEmployeesAndCustomers();

            // Vorbereitung für Platzierung Eingabefelder der Gruppe Mitarbeitende UND Kunde (alle)
            Control[] groupFieldEmployeesAndCustomers = GroupFieldEmployeesAndCustomers();

            // Platzierung Labels und Eingabefelder der Gruppe Mitarbeitende UND Kunde (alle)
            int startLocationMaKd = 20;
            int labelXAchseMaKd = 10;
            int controlXAchseMaKd = 135;

            for (int i = 0; i < groupFieldEmployeesAndCustomers.Length; i++)
            {
                groupLabelEmployeesAndCustomers[i].Location = new Point(labelXAchseMaKd, startLocationMaKd);
                groupFieldEmployeesAndCustomers[i].Location = new Point(controlXAchseMaKd, startLocationMaKd);

                startLocationMaKd += 30;
            }

            // Vorbereitung für Platzierung Labels der Gruppe Mitarbeitende (ohne Kunde)
            System.Windows.Forms.Label[] groupLabelEmployees = GroupLabelEmployees();

            // Vorbereitung für Platzierung Eingabefelder der Gruppe Mitarbeitende (ohne Kunde)
            Control[] groupFieldEmployees = GroupFieldEmployees();

            // Platzierung Labels und Eingabefelder der Gruppe Mitarbeitende (ohne Kunde)
            int startLocationMa = 20;
            int labelXAchseMa = 10;
            int controlXAchseMa = 135;

            for (int i = 0; i < groupFieldEmployees.Length; i++)
            {
                groupLabelEmployees[i].Location = new Point(labelXAchseMa, startLocationMa);
                groupFieldEmployees[i].Location = new Point(controlXAchseMa, startLocationMa);

                startLocationMa += 30;
            }

            // Platzierung Buttons "Speichern und ..."
            CmdCreateKntktKontaktErstellen.Size = new Size(150, 60);
            CmdCreateKntktKontaktErstellen.Location = new Point(420, 470);
            CmdCreateKntktDashboard.Size = new Size(150, 60);
            CmdCreateKntktDashboard.Location = new Point(575, 470);
        }


        // Erstellung Array für Labels der Gruppe Mitarbeitende UND Kunde (alle)
        private System.Windows.Forms.Label[] GroupLabelEmployeesAndCustomers()
        {
            System.Windows.Forms.Label[] groupLabelEmployeesAndCustomers = new System.Windows.Forms.Label[]
            {
                LblCreatKntktTitel,
                LblCreatKntktAnrede,
                LblCreatKntktName,
                LblCreatKntktVorname,
                LblCreatKntktBirthday,
                LblCreatKntktGeschlecht,
                LblCreatKntktAdr,
                LblCreatKntktPLZ,
                LblCreatKntktOrt,
                LblCreatKntktTelGeschaeft,
                LblCreatKntktTelMobil,
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
                LblCreatKntktMaOfficeAddress,
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
                TxtCreatKntktName,
                TxtCreatKntktVorname,
                DateCreatKntktBirthday,
                CmBxCreatKntktGeschlecht,
                TxtCreatKntktAdr,
                TxtCreatKntktPLZ,
                TxtCreatKntktOrt,
                TxtCreatKntktTelGeschaeft,
                TxtCreatKntktTelMobil,
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
                TxtCreatKntktMaBeschGrad,
                TxtCreattKntktMaAbteilung,
                TxtCreatKntktMaRolle,
                TxtCreatKntktMaLehrj,
                TxtCreatKntktMaAktLehrj,
                TxtCreatKntktMaOfficeAddress,
                TxtCreatKntktEintrDatum,
                TxtCreatKntktAustrDatum
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
            bool checkAHVNumber = false;
            int countEmptyFields = CheckEmptyFields();


            if (countEmptyFields == 0)
            {
                checkAHVNumber = CheckAHVNumber(TxtCreatKntktMaAHVNr.Text);
            }

            if (countEmptyFields == 0 && checkAHVNumber)
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
            this.Close();
        }

        // Prüfung AHV-Nummer auf Korrektheit und Vollständigkeit
        private bool CheckAHVNumber(string ahvNumber)
        {
           if (RdbCreatKntktKunde.Checked)
            {
                return true;
            }

            bool checkAHVNumber = ValidationAHVNumber(ahvNumber);

            if (!checkAHVNumber)
            {
                MessageBox.Show("Die AHV-Nummer ist ungültig.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtCreatKntktMaAHVNr.Focus();
                TxtCreatKntktMaAHVNr.BackColor = Color.LightPink;
            }

            else
            {
                TxtCreatKntktMaAHVNr.BackColor = SystemColors.Window;
            }

            return checkAHVNumber;
        }

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

        private int CheckEmptyFields()
        {
            int countEmptyFields = 0;

            Control[] checkEmptyFieldsAll = GroupFieldEmployeesAndCustomers();

            foreach (Control field in checkEmptyFieldsAll)
            {
                bool checkEmptyFields = ValidationEmptyFields(field);
                countEmptyFields += !checkEmptyFields ? 1 : 0;
            }

            if (RdbCreatKntktMa.Checked)
            {
                Control[] checkEmptyFieldsMA = GroupFieldEmployees();

                foreach (Control field in checkEmptyFieldsMA)
                {
                    bool checkEmptyFields = ValidationEmptyFields(field);
                    countEmptyFields += !checkEmptyFields ? 1 : 0;
                }
            }

            return countEmptyFields;
        }

        private bool ValidationEmptyFields(Control field)
        {
            Control[] checkEmptyFieldsIgnore = CheckEmptyFieldsIgnore();

            if (checkEmptyFieldsIgnore.Contains(field))
            {
                return true;
            }

            else if (string.IsNullOrWhiteSpace(field.Text))
            {
                field.BackColor = Color.LightPink;
                return false;
            }

            else
            {
                field.BackColor = SystemColors.Window;
                return true;
            }
        }

        // Erstellung Array für KEINE-Pflichtfelder-Prüfung
        private Control[] CheckEmptyFieldsIgnore()
        {
            Control[] checkEmptyFieldsIgnore = new Control[]
            {
                TxtCreatKntktTitel,
                TxtCreatKntktTelGeschaeft,
                TxtCreatKntktMaKader,
                TxtCreatKntktMaLehrj,
                TxtCreatKntktMaAktLehrj,
                TxtCreatKntktMaOfficeAddress,
                TxtCreatKntktAustrDatum,
            };

            return checkEmptyFieldsIgnore;
        }
    }
}