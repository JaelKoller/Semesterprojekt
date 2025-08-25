using Semesterprojekt.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Semesterprojekt
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Dashboard());

            //var testingKontaktErstellen = new Testing_KontaktErstellen();
            //testingKontaktErstellen.TestData("mitarbeiter", "daisy");
            //testingKontaktErstellen.TestData("kunde", "daisy");
            //testingKontaktErstellen.TestData("mitarbeiter", "donald");
            //testingKontaktErstellen.TestData("kunde", "donald");
            //testingKontaktErstellen.TestData("mitarbeiter", "olga");
            //testingKontaktErstellen.TestData("kunde", "olga");
            //testingKontaktErstellen.TestData("mitarbeiter", "error");
            //testingKontaktErstellen.TestData("kunde", "error");
            //testingKontaktErstellen.TestData("mitarbeiter", "errorSpecial");
            //testingKontaktErstellen.TestData("kunde", "errorSpecial");

            //var testingAlleKontakte = new Testing_AlleKontakte();
            //testingAlleKontakte.TestData();

            //var testingAnsichtKontakt = new Testing_AnsichtKontakt();
            //testingAnsichtKontakt.TestData(true);
            //testingAnsichtKontakt.TestData(false);

            //Application.Run(new Testing_ClientAndEmployeeNumber());
        }
    }
}
