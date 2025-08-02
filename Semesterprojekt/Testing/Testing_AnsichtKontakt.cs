using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Semesterprojekt.Testing
{
    public partial class Testing_AnsichtKontakt : Form
    {
        private AnsichtKontakt ansichtKontaktForm;

        public Testing_AnsichtKontakt()
        {
            InitializeComponent();
        }
        public void TestData(bool testfallMitarbeiter)
        {
            ansichtKontaktForm = new AnsichtKontakt();

            // Testdaten für Gruppe Mitarbeiter UND Kunde
            ansichtKontaktForm.TxtAnsichtKntktTitel.Text = "Dr.";
            ansichtKontaktForm.CmBxAnsichtKntktAnrede.SelectedItem = "Frau";
            ansichtKontaktForm.TxtAnsichtKntktVorname.Text = "Jael";
            ansichtKontaktForm.TxtAnsichtKntktName.Text = "Koller";
            ansichtKontaktForm.DateAnsichtKntktBirthday.Value = new DateTime(1989, 7, 1);
            ansichtKontaktForm.CmBxAnsichtKntktGeschlecht.SelectedItem = "weiblich";
            ansichtKontaktForm.TxtAnsichtKntktTelGeschaeft.Text = "071 123 45 67";
            ansichtKontaktForm.TxtAnsichtKntktTelMobile.Text = "076 123 45 67";
            ansichtKontaktForm.TxtAnsichtKntktEmail.Text = "jaelkoller@testmail.ch";
            ansichtKontaktForm.TxtAnsichtKntktAdr.Text = "Heimstrasse 4";
            ansichtKontaktForm.TxtAnsichtKntktPLZ.Text = "9014";
            ansichtKontaktForm.TxtAnsichtKntktOrt.Text = "St. Gallen";
            ansichtKontaktForm.RdbAnsichtKntktAktiv.Checked = true;

            // Testdaten für Gruppe Mitarbeiter (ohne Kunde)
            if (testfallMitarbeiter)
            {
                ansichtKontaktForm.TxtAnsichtKntktMaAHVNr.Text = "756.8800.5641.37";
                ansichtKontaktForm.TxtAnsichtKntktMaNationalitaet.Text = "CH";
                ansichtKontaktForm.TxtAnsichtKntktMaKader.Text = "Mitglied der Direktion";
                ansichtKontaktForm.NumAnsichtKntktMaBeschGrad.Value = 100;
                ansichtKontaktForm.TxtAnsichtKntktMaAbteilung.Text = "Automatisierung";
                ansichtKontaktForm.TxtAnsichtKntktMaRolle.Text = "Software Engineer";
                ansichtKontaktForm.NumAnsichtKntktMaLehrj.Value = 3;
                ansichtKontaktForm.NumAnsichtKntktMaAktLehrj.Value = 0;
                ansichtKontaktForm.NumAnsichtKntktMaOfficeNumber.Value = 123;
                ansichtKontaktForm.DateAnsichtKntktEintrDatum.Value = new DateTime(2005, 8, 1);
                ansichtKontaktForm.DateAnsichtKntktAustrDatum.Value = new DateTime(2025, 7, 31);
            }

            // Start Form "KontaktErstellen" mit Testdaten
            Application.Run(ansichtKontaktForm);
        }
    }
}
