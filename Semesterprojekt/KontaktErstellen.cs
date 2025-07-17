using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            this.StartPosition = FormStartPosition.CenterScreen;

            typeOfContactNew = typeOfContact;
            InitalizationTypeOfContact();
            UpdateTypeOfContact();
        }

        // Initalisierung Radio-Button auf Basis "Kontaktart"
        private void InitalizationTypeOfContact()
        {
            if (typeOfContactNew == "mitarbeitende")
            {
                RdbCreatKntktMa.Checked = true;
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
                TxtCreatKntktMaKader.Enabled = true;
                TxtCreatKntktMaBeschGrad.Enabled = true;
                TxtCreattKntktMaAbteilung.Enabled = true;
                TxtCreatKntktMaRolle.Enabled = true;
                TxtCreatKntktMaLehrj.Enabled = true;
                TxtCreatKntktMaAktLehrj.Enabled = true;
            }
            else if (RdbCreatKntktKunde.Checked)
            {
                typeOfContactNew = "kunde";
                RdbCreatKntktKunde.Checked = true;
                TxtCreatKntktMaKader.Enabled = false;
                TxtCreatKntktMaBeschGrad.Enabled = false;
                TxtCreattKntktMaAbteilung.Enabled = false;
                TxtCreatKntktMaRolle.Enabled = false;
                TxtCreatKntktMaLehrj.Enabled = false;
                TxtCreatKntktMaAktLehrj.Enabled = false;
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
            CheckAHVNumber(TxtCreatKntktMaAHVNr.Text);


            this.FormClosed += (s, arg) =>
            {
                // Erstellung neues Form "KontaktErstellen"
                var kontaktErstellenForm = new KontaktErstellen(typeOfContactNew);
                kontaktErstellenForm.Show();
            };
            this.Close();
        }

        private void CmdCreateKntktDashboard_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Prüfung AHV-Nummer auf Korrektheit und Vollständigkeit
        private void CheckAHVNumber(string ahvNumber)
        {
            bool checkAHVNumber = ValidationAHVNumber(ahvNumber);
            if (!checkAHVNumber)
            {
                MessageBox.Show("Die AHV-Nummer ist ungültig.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
    }
}
