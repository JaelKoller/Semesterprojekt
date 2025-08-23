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

        public void TestData(string typeOfContactNew, string caseName)
        {
            kontaktErstellenForm = new KontaktErstellen(typeOfContactNew);

            switch (caseName)
            {
                case "daisy":
                    // Testdaten für Gruppe Mitarbeiter UND Kunde
                    kontaktErstellenForm.TxtCreatKntktTitel.Text = "Dr.";
                    kontaktErstellenForm.CmBxCreatKntktAnrede.SelectedItem = "Frau";
                    kontaktErstellenForm.TxtCreatKntktVorname.Text = "Daisy";
                    kontaktErstellenForm.TxtCreatKntktName.Text = "Duck";
                    kontaktErstellenForm.TxtCreatKntktBirthday.Text = "01.07.1989";
                    kontaktErstellenForm.CmBxCreatKntktGeschlecht.SelectedItem = "weiblich";
                    kontaktErstellenForm.TxtCreatKntktTelGeschaeft.Text = "+41 71 123 44 55";
                    kontaktErstellenForm.TxtCreatKntktTelMobile.Text = "+41 79 123 44 55";
                    kontaktErstellenForm.TxtCreatKntktEmail.Text = "daisyduck@testmail.ch";
                    kontaktErstellenForm.TxtCreatKntktAdr.Text = "Duckstrasse 17";
                    kontaktErstellenForm.TxtCreatKntktPLZ.Text = "9000";
                    kontaktErstellenForm.TxtCreatKntktOrt.Text = "St. Gallen";

                    // Testdaten für Gruppe Mitarbeiter (ohne Kunde)
                    if (kontaktErstellenForm.RdbCreatKntktMa.Checked)
                    {
                        kontaktErstellenForm.TxtCreatKntktMaAHVNr.Text = "756.8800.5641.37";
                        kontaktErstellenForm.TxtCreatKntktMaNationalitaet.Text = "CH";
                        kontaktErstellenForm.NumCreatKntktMaKader.Value = 5;
                        kontaktErstellenForm.NumCreatKntktMaBeschGrad.Value = 100;
                        kontaktErstellenForm.TxtCreatKntktMaAbteilung.Text = "Automatisierung";
                        kontaktErstellenForm.TxtCreatKntktMaRolle.Text = "Software Engineer";
                        kontaktErstellenForm.NumCreatKntktMaLehrj.Value = 3;
                        kontaktErstellenForm.NumCreatKntktMaAktLehrj.Value = 0;
                        kontaktErstellenForm.NumCreatKntktMaOfficeNumber.Value = 123;
                        kontaktErstellenForm.TxtCreatKntktEintrDatum.Text = "01.08.2005";
                        kontaktErstellenForm.TxtCreatKntktAustrDatum.Text = string.Empty;
                    }
                    break;

                case "donald":
                    // Testdaten für Gruppe Mitarbeiter UND Kunde
                    kontaktErstellenForm.TxtCreatKntktTitel.Text = "";
                    kontaktErstellenForm.CmBxCreatKntktAnrede.SelectedItem = "Herr";
                    kontaktErstellenForm.TxtCreatKntktVorname.Text = "Donald";
                    kontaktErstellenForm.TxtCreatKntktName.Text = "Duck";
                    kontaktErstellenForm.TxtCreatKntktBirthday.Text = "13.12.1998";
                    kontaktErstellenForm.CmBxCreatKntktGeschlecht.SelectedItem = "männlich";
                    kontaktErstellenForm.TxtCreatKntktTelGeschaeft.Text = "+41 (0)71 987 66 55";
                    kontaktErstellenForm.TxtCreatKntktTelMobile.Text = "+41 (0)79 987 66 55";
                    kontaktErstellenForm.TxtCreatKntktEmail.Text = "donald.duck@testmail.ch";
                    kontaktErstellenForm.TxtCreatKntktAdr.Text = "Duckstrasse 17";
                    kontaktErstellenForm.TxtCreatKntktPLZ.Text = "9000";
                    kontaktErstellenForm.TxtCreatKntktOrt.Text = "St. Gallen";

                    // Testdaten für Gruppe Mitarbeiter (ohne Kunde)
                    if (kontaktErstellenForm.RdbCreatKntktMa.Checked)
                    {
                        kontaktErstellenForm.TxtCreatKntktMaAHVNr.Text = "756.8800.5641.37";
                        kontaktErstellenForm.TxtCreatKntktMaNationalitaet.Text = "CH";
                        kontaktErstellenForm.NumCreatKntktMaKader.Value = 3;
                        kontaktErstellenForm.NumCreatKntktMaBeschGrad.Value = 80;
                        kontaktErstellenForm.TxtCreatKntktMaAbteilung.Text = "Projektmanagement";
                        kontaktErstellenForm.TxtCreatKntktMaRolle.Text = "Projektleiter";
                        kontaktErstellenForm.NumCreatKntktMaLehrj.Value = 0;
                        kontaktErstellenForm.NumCreatKntktMaAktLehrj.Value = 0;
                        kontaktErstellenForm.NumCreatKntktMaOfficeNumber.Value = 456;
                        kontaktErstellenForm.TxtCreatKntktEintrDatum.Text = "13.10.2024";
                        kontaktErstellenForm.TxtCreatKntktAustrDatum.Text = string.Empty;
                    }
                    break;

                case "olga":
                    // Testdaten für Gruppe Mitarbeiter UND Kunde
                    kontaktErstellenForm.TxtCreatKntktTitel.Text = "";
                    kontaktErstellenForm.CmBxCreatKntktAnrede.SelectedItem = "keine";
                    kontaktErstellenForm.TxtCreatKntktVorname.Text = "Olga";
                    kontaktErstellenForm.TxtCreatKntktName.Text = "Muster";
                    kontaktErstellenForm.TxtCreatKntktBirthday.Text = "31.12.1997";
                    kontaktErstellenForm.CmBxCreatKntktGeschlecht.SelectedItem = "divers";
                    kontaktErstellenForm.TxtCreatKntktTelGeschaeft.Text = "+49-123-12345678";
                    kontaktErstellenForm.TxtCreatKntktTelMobile.Text = "+49 123.98765.44";
                    kontaktErstellenForm.TxtCreatKntktEmail.Text = "olga@testmail.test.com";
                    kontaktErstellenForm.TxtCreatKntktAdr.Text = "Musterstrasse 25a";
                    kontaktErstellenForm.TxtCreatKntktPLZ.Text = "8000";
                    kontaktErstellenForm.TxtCreatKntktOrt.Text = "Zürich";

                    // Testdaten für Gruppe Mitarbeiter (ohne Kunde)
                    if (kontaktErstellenForm.RdbCreatKntktMa.Checked)
                    {
                        kontaktErstellenForm.TxtCreatKntktMaAHVNr.Text = "756.8800.5641.37";
                        kontaktErstellenForm.TxtCreatKntktMaNationalitaet.Text = "DE";
                        kontaktErstellenForm.NumCreatKntktMaKader.Value = 1;
                        kontaktErstellenForm.NumCreatKntktMaBeschGrad.Value = 50;
                        kontaktErstellenForm.TxtCreatKntktMaAbteilung.Text = "Kreditverarbeitung";
                        kontaktErstellenForm.TxtCreatKntktMaRolle.Text = "Fachspezialistin Kredite";
                        kontaktErstellenForm.NumCreatKntktMaLehrj.Value = 0;
                        kontaktErstellenForm.NumCreatKntktMaAktLehrj.Value = 0;
                        kontaktErstellenForm.NumCreatKntktMaOfficeNumber.Value = 456;
                        kontaktErstellenForm.TxtCreatKntktEintrDatum.Text = "01.01.2020";
                        kontaktErstellenForm.TxtCreatKntktAustrDatum.Text = string.Empty;
                    }
                    break;

                case "error":
                    // Testdaten für Gruppe Mitarbeiter UND Kunde
                    kontaktErstellenForm.TxtCreatKntktTitel.Text = "";
                    kontaktErstellenForm.CmBxCreatKntktAnrede.SelectedItem = "";
                    kontaktErstellenForm.TxtCreatKntktVorname.Text = "Error";
                    kontaktErstellenForm.TxtCreatKntktName.Text = "Muster";
                    kontaktErstellenForm.TxtCreatKntktBirthday.Text = "15.15.2000";
                    kontaktErstellenForm.CmBxCreatKntktGeschlecht.SelectedItem = "";
                    kontaktErstellenForm.TxtCreatKntktTelGeschaeft.Text = "071 123 44 55";
                    kontaktErstellenForm.TxtCreatKntktTelMobile.Text = "079 123 44 55";
                    kontaktErstellenForm.TxtCreatKntktEmail.Text = "error@testmail";
                    kontaktErstellenForm.TxtCreatKntktAdr.Text = "Errorweg";
                    kontaktErstellenForm.TxtCreatKntktPLZ.Text = "690";
                    kontaktErstellenForm.TxtCreatKntktOrt.Text = "Bregenz AT";

                    // Testdaten für Gruppe Mitarbeiter (ohne Kunde)
                    if (kontaktErstellenForm.RdbCreatKntktMa.Checked)
                    {
                        kontaktErstellenForm.TxtCreatKntktMaAHVNr.Text = "756.8800.5641.30";
                        kontaktErstellenForm.TxtCreatKntktMaNationalitaet.Text = "CH";
                        kontaktErstellenForm.NumCreatKntktMaKader.Value = 0;
                        kontaktErstellenForm.NumCreatKntktMaBeschGrad.Value = 100;
                        kontaktErstellenForm.TxtCreatKntktMaAbteilung.Text = "Revision";
                        kontaktErstellenForm.TxtCreatKntktMaRolle.Text = "Revisor";
                        kontaktErstellenForm.NumCreatKntktMaLehrj.Value = 5;
                        kontaktErstellenForm.NumCreatKntktMaAktLehrj.Value = 4;
                        kontaktErstellenForm.NumCreatKntktMaOfficeNumber.Value = 0;
                        kontaktErstellenForm.TxtCreatKntktEintrDatum.Text = "01.01.3025";
                        kontaktErstellenForm.TxtCreatKntktAustrDatum.Text = string.Empty;
                    }
                    break;
            }

            // Start Form "KontaktErstellen" mit Testdaten
            Application.Run(kontaktErstellenForm);
        }
    }
}
