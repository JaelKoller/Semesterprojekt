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
            string contactStatus = testfallMitarbeiter ? "active" : "inactive";
            string contactNumber = testfallMitarbeiter ? "MA1717" : "KD1717";
            string typeOfContact = testfallMitarbeiter ? "Mitarbeiter" : "Kunde";

            // Erstellung Testdaten            
            var contactData = new InitializationContactData
            {
                ContactStatus = contactStatus,
                ContactNumber = contactNumber,
                TypeOfContact = typeOfContact,
                Fields = new Dictionary<string, string>
                {
                    { "Title", "Dr." },
                    { "Salutation", "Frau" },
                    { "FirstName", "Jael" },
                    { "LastName", "Koller" },
                    { "Birthday", "01.07.1989" },
                    { "Gender", "weiblich" },
                    { "Address", "Heimstrasse 4" },
                    { "PostalCode", "9014" },
                    { "City", "St. Gallen" },
                    { "BusinessNumber", "071 123 45 67" },
                    { "MobileNumber", "076 123 45 67" },
                    { "Email", "jaelkoller@testmail.ch" },
                    { "EmployeeNumber", testfallMitarbeiter ? "MA1717" : "" },
                    { "AHVNumber", testfallMitarbeiter ? "756.8800.5641.37" : "" },
                    { "Nationality", testfallMitarbeiter ? "CH" : "" },
                    { "ManagementLevel", testfallMitarbeiter ? "Mitglied der Direktion" : "" },
                    { "LevelOfEmployment", testfallMitarbeiter ? "100" : "0" },
                    { "Department", testfallMitarbeiter ? "Automatisierung" : "" },
                    { "Role", testfallMitarbeiter ? "Software Engineer" : "" },
                    { "AcademicYear", testfallMitarbeiter ? "3" : "0" },
                    { "CurrentAcademicYear", "0" },
                    { "OfficeNumber", testfallMitarbeiter ? "123" : "0" },
                    { "DateOfEntry", testfallMitarbeiter ? "01.08.2005" : "" },
                    { "DateOfExit", "" }
                }
            };

            ansichtKontaktForm = new AnsichtKontakt(contactData);

            ansichtKontaktForm.LblAnsichtKntktNameAnzeige.Text = $"{typeOfContact}: {ansichtKontaktForm.TxtAnsichtKntktVorname.Text} {ansichtKontaktForm.TxtAnsichtKntktName.Text}";

            // Start Form "KontaktErstellen" mit Testdaten
            Application.Run(ansichtKontaktForm);
        }
    }
}
