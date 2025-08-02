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
    public partial class Testing_KontaktErstellen : Form
    {
        private KontaktErstellen kontaktErstellenForm;
        
        public Testing_KontaktErstellen()
        {
            InitializeComponent();
        }

        public void TestData(string typeOfContactNew)
        {
            kontaktErstellenForm = new KontaktErstellen(typeOfContactNew);

            // Testdaten für Gruppe Mitarbeiter UND Kunde
            kontaktErstellenForm.TxtCreatKntktTitel.Text = "Dr.";
            kontaktErstellenForm.CmBxCreatKntktAnrede.SelectedItem = "Frau";
            kontaktErstellenForm.TxtCreatKntktVorname.Text = "Jael";
            kontaktErstellenForm.TxtCreatKntktName.Text = "Koller";
            kontaktErstellenForm.DateCreatKntktBirthday.Value = new DateTime(1989, 7, 1);
            kontaktErstellenForm.CmBxCreatKntktGeschlecht.SelectedItem = "weiblich";
            kontaktErstellenForm.TxtCreatKntktTelGeschaeft.Text = "071 123 45 67";
            kontaktErstellenForm.TxtCreatKntktTelMobile.Text = "076 123 45 67";
            kontaktErstellenForm.TxtCreatKntktEmail.Text = "jaelkoller@testmail.ch";
            kontaktErstellenForm.TxtCreatKntktAdr.Text = "Heimstrasse 4";
            kontaktErstellenForm.TxtCreatKntktPLZ.Text = "9014";
            kontaktErstellenForm.TxtCreatKntktOrt.Text = "St. Gallen";

            // Testdaten für Gruppe Mitarbeiter (ohne Kunde)
            if (kontaktErstellenForm.RdbCreatKntktMa.Checked)
            {
                kontaktErstellenForm.TxtCreatKntktMaAHVNr.Text = "756.8800.5641.37";
                kontaktErstellenForm.TxtCreatKntktMaNationalitaet.Text = "CH";
                kontaktErstellenForm.TxtCreatKntktMaKader.Text = "Mitglied der Direktion";
                kontaktErstellenForm.NumCreatKntktMaBeschGrad.Value = 100;
                kontaktErstellenForm.TxtCreatKntktMaAbteilung.Text = "Automatisierung";
                kontaktErstellenForm.TxtCreatKntktMaRolle.Text = "Software Engineer";
                kontaktErstellenForm.NumCreatKntktMaLehrj.Value = 3;
                kontaktErstellenForm.NumCreatKntktMaAktLehrj.Value = 0;
                kontaktErstellenForm.NumCreatKntktMaOfficeNumber.Value = 123;
                kontaktErstellenForm.DateCreatKntktEintrDatum.Value = new DateTime(2005, 8, 1);
                kontaktErstellenForm.DateCreatKntktAustrDatum.Value = new DateTime(2025, 7, 31);
            }

            // Start Form "KontaktErstellen" mit Testdaten
            Application.Run(kontaktErstellenForm);
        }
    }
}
