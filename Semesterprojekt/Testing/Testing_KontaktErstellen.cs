using System.Windows.Forms;

namespace Semesterprojekt.Testing
{
    internal partial class Testing_KontaktErstellen : Form
    {
        private KontaktErstellen kontaktErstellenForm;

        internal Testing_KontaktErstellen()
        {
            InitializeComponent();
        }

        internal void TestData(string typeOfContactNew, string caseName)
        {
            kontaktErstellenForm = new KontaktErstellen(typeOfContactNew);

            switch (caseName)
            {
                case "daisy":
                    // Testdaten für Gruppe "Kontaktdaten"
                    kontaktErstellenForm.TxtCreatKntktTitel.Text = "Dr.";
                    kontaktErstellenForm.CmBxCreatKntktAnrede.SelectedItem = "Frau";
                    kontaktErstellenForm.TxtCreatKntktVorname.Text = "Daisy";
                    kontaktErstellenForm.TxtCreatKntktName.Text = "Duck";
                    kontaktErstellenForm.TxtCreatKntktBirthday.Text = "01.07.1989";
                    kontaktErstellenForm.CmBxCreatKntktGeschlecht.SelectedItem = "weiblich";
                    kontaktErstellenForm.TxtCreatKntktAdr.Text = "Duckstrasse 17";
                    kontaktErstellenForm.TxtCreatKntktPLZ.Text = "9000";
                    kontaktErstellenForm.TxtCreatKntktOrt.Text = "St. Gallen";
                    kontaktErstellenForm.TxtCreatKntktTelGeschaeft.Text = "+41 71 123 44 55";
                    kontaktErstellenForm.TxtCreatKntktTelMobile.Text = "+41 79 123 44 55";
                    kontaktErstellenForm.TxtCreatKntktEmail.Text = "daisyduck@testmail.ch";

                    // Testdaten für Gruppe "Mitarbeiterdaten"
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
                        kontaktErstellenForm.TxtCreatKntktAdrOffice.Text = "Bürostrasse 100";
                        kontaktErstellenForm.TxtCreatKntktPLZOffice.Text = "9001";
                        kontaktErstellenForm.TxtCreatKntktOrtOffice.Text = "St. Gallen";
                        kontaktErstellenForm.TxtCreatKntktEintrDatum.Text = "01.08.2005";
                        kontaktErstellenForm.TxtCreatKntktAustrDatum.Text = string.Empty;
                    }
                    break;

                case "donald":
                    // Testdaten für Gruppe "Kontaktdaten"
                    kontaktErstellenForm.TxtCreatKntktTitel.Text = "";
                    kontaktErstellenForm.CmBxCreatKntktAnrede.SelectedItem = "Herr";
                    kontaktErstellenForm.TxtCreatKntktVorname.Text = "Donald";
                    kontaktErstellenForm.TxtCreatKntktName.Text = "Duck";
                    kontaktErstellenForm.TxtCreatKntktBirthday.Text = "13.12.1998";
                    kontaktErstellenForm.CmBxCreatKntktGeschlecht.SelectedItem = "männlich";
                    kontaktErstellenForm.TxtCreatKntktAdr.Text = "Duckstrasse 17";
                    kontaktErstellenForm.TxtCreatKntktPLZ.Text = "9000";
                    kontaktErstellenForm.TxtCreatKntktOrt.Text = "St. Gallen";
                    kontaktErstellenForm.TxtCreatKntktTelGeschaeft.Text = "+41719876655";
                    kontaktErstellenForm.TxtCreatKntktTelMobile.Text = "+41799876655";
                    kontaktErstellenForm.TxtCreatKntktEmail.Text = "donald.duck@testmail.ch";

                    // Testdaten für Gruppe "Mitarbeiterdaten"
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
                        kontaktErstellenForm.TxtCreatKntktAdrOffice.Text = "Bürostrasse 100";
                        kontaktErstellenForm.TxtCreatKntktPLZOffice.Text = "9001";
                        kontaktErstellenForm.TxtCreatKntktOrtOffice.Text = "St. Gallen";
                        kontaktErstellenForm.TxtCreatKntktEintrDatum.Text = "13.10.2024";
                        kontaktErstellenForm.TxtCreatKntktAustrDatum.Text = string.Empty;
                    }
                    break;

                case "olga":
                    // Testdaten für Gruppe "Kontaktdaten"
                    kontaktErstellenForm.TxtCreatKntktTitel.Text = "Prof., Dr.";
                    kontaktErstellenForm.CmBxCreatKntktAnrede.SelectedItem = "keine";
                    kontaktErstellenForm.TxtCreatKntktVorname.Text = "Olga Döröthéà";
                    kontaktErstellenForm.TxtCreatKntktName.Text = "Spinnenbein-D'Agosta";
                    kontaktErstellenForm.TxtCreatKntktBirthday.Text = "31.12.1997";
                    kontaktErstellenForm.CmBxCreatKntktGeschlecht.SelectedItem = "divers";
                    kontaktErstellenForm.TxtCreatKntktAdr.Text = "Musterstrasse 25a";
                    kontaktErstellenForm.TxtCreatKntktPLZ.Text = "8045";
                    kontaktErstellenForm.TxtCreatKntktOrt.Text = "Zürich-Wiedikon";
                    kontaktErstellenForm.TxtCreatKntktTelGeschaeft.Text = "+49 123 12345678";
                    kontaktErstellenForm.TxtCreatKntktTelMobile.Text = "+49 123 98765 44";
                    kontaktErstellenForm.TxtCreatKntktEmail.Text = "olga_hauser55@testmail.test.com";

                    // Testdaten für Gruppe "Mitarbeiterdaten"
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
                        kontaktErstellenForm.TxtCreatKntktAdrOffice.Text = "Weiher 96";
                        kontaktErstellenForm.TxtCreatKntktPLZOffice.Text = "9103";
                        kontaktErstellenForm.TxtCreatKntktOrtOffice.Text = "Schwellbrunn";
                        kontaktErstellenForm.TxtCreatKntktEintrDatum.Text = "01.01.2020";
                        kontaktErstellenForm.TxtCreatKntktAustrDatum.Text = string.Empty;
                    }
                    break;

                case "error":
                    // Testdaten für Gruppe "Kontaktdaten"
                    kontaktErstellenForm.TxtCreatKntktTitel.Text = "";
                    kontaktErstellenForm.CmBxCreatKntktAnrede.SelectedItem = "";
                    kontaktErstellenForm.TxtCreatKntktVorname.Text = "Error";
                    kontaktErstellenForm.TxtCreatKntktName.Text = "Muster";
                    kontaktErstellenForm.TxtCreatKntktBirthday.Text = "15.15.2000";
                    kontaktErstellenForm.CmBxCreatKntktGeschlecht.SelectedItem = "";
                    kontaktErstellenForm.TxtCreatKntktAdr.Text = "Errorweg";
                    kontaktErstellenForm.TxtCreatKntktPLZ.Text = "690";
                    kontaktErstellenForm.TxtCreatKntktOrt.Text = "Bregenz AT";
                    kontaktErstellenForm.TxtCreatKntktTelGeschaeft.Text = "071 123 44 55";
                    kontaktErstellenForm.TxtCreatKntktTelMobile.Text = "+41 (0)79 123 44 55";
                    kontaktErstellenForm.TxtCreatKntktEmail.Text = "error@testmail";

                    // Testdaten für Gruppe "Mitarbeiterdaten"
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
                        kontaktErstellenForm.TxtCreatKntktAdrOffice.Text = "Error-Business-Weg";
                        kontaktErstellenForm.TxtCreatKntktPLZOffice.Text = "920";
                        kontaktErstellenForm.TxtCreatKntktOrtOffice.Text = "Gossau SG";
                        kontaktErstellenForm.TxtCreatKntktEintrDatum.Text = "01.01.3025";
                        kontaktErstellenForm.TxtCreatKntktAustrDatum.Text = string.Empty;
                    }
                    break;

                case "errorSpecial":
                    // Testdaten für Gruppe "Kontaktdaten"
                    kontaktErstellenForm.TxtCreatKntktTitel.Text = "Prof@";
                    kontaktErstellenForm.CmBxCreatKntktAnrede.SelectedItem = "keine";
                    kontaktErstellenForm.TxtCreatKntktVorname.Text = "Error#";
                    kontaktErstellenForm.TxtCreatKntktName.Text = "[Muster]";
                    kontaktErstellenForm.TxtCreatKntktBirthday.Text = "05.04.1997";
                    kontaktErstellenForm.CmBxCreatKntktGeschlecht.SelectedItem = "divers";
                    kontaktErstellenForm.TxtCreatKntktAdr.Text = "Errorweg/12";
                    kontaktErstellenForm.TxtCreatKntktPLZ.Text = "76530";
                    kontaktErstellenForm.TxtCreatKntktOrt.Text = "Baden-Baden DE";
                    kontaktErstellenForm.TxtCreatKntktTelGeschaeft.Text = "+071 123 44 55";
                    kontaktErstellenForm.TxtCreatKntktTelMobile.Text = "+41-079 123 44 55";
                    kontaktErstellenForm.TxtCreatKntktEmail.Text = "error$error@testmail.ch";

                    // Testdaten für Gruppe "Mitarbeiterdaten"
                    if (kontaktErstellenForm.RdbCreatKntktMa.Checked)
                    {
                        kontaktErstellenForm.TxtCreatKntktMaAHVNr.Text = "756.8800.5641.37";
                        kontaktErstellenForm.TxtCreatKntktMaNationalitaet.Text = "";
                        kontaktErstellenForm.NumCreatKntktMaKader.Value = 0;
                        kontaktErstellenForm.NumCreatKntktMaBeschGrad.Value = 100;
                        kontaktErstellenForm.TxtCreatKntktMaAbteilung.Text = "Ausbildung (Lernende)";
                        kontaktErstellenForm.TxtCreatKntktMaRolle.Text = "Ausbildner & Betreuer";
                        kontaktErstellenForm.NumCreatKntktMaLehrj.Value = 5;
                        kontaktErstellenForm.NumCreatKntktMaAktLehrj.Value = 4;
                        kontaktErstellenForm.NumCreatKntktMaOfficeNumber.Value = 999;
                        kontaktErstellenForm.TxtCreatKntktAdrOffice.Text = "Error%Business%Weg";
                        kontaktErstellenForm.TxtCreatKntktPLZOffice.Text = "ABCD";
                        kontaktErstellenForm.TxtCreatKntktOrtOffice.Text = "Gossau;";
                        kontaktErstellenForm.TxtCreatKntktEintrDatum.Text = "1.1.2025";
                        kontaktErstellenForm.TxtCreatKntktAustrDatum.Text = string.Empty;
                    }
                    break;
            }

            // Start Form "KontaktErstellen" mit Testdaten
            Application.Run(kontaktErstellenForm);
        }
    }
}
