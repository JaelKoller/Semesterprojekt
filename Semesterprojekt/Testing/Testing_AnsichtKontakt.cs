using System;
using System.Collections.Generic;
using System.Globalization;
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
        
        // Erstellung von 25 Notizen für Test-Kontakt
        private void CreateTestNotes (string contactNumber)
        {
            // Bereinigung allfälliger bestehender Notizen zu Test-Kontakt
            Notes.DeleteNotesData(contactNumber);

            int countNotes = 25;
            int daySpacing = 400;
            DateTime startDate = DateTime.Today.AddDays(-(countNotes - 1) * daySpacing);

            for (int note = 0; note < countNotes; note++)
            {
                InitializationNotes noteData = new InitializationNotes
                {
                    ContactNumber = contactNumber,
                    NoteTitle = $"Das ist der Titel zu einer Testnotiz! #{countNotes - note}",
                    NoteText = $"Das ist der Text zu der Testnotiz #{countNotes - note}. Hier werden nun ein paar Punkte festgehalten:\r\n\r\n" +
                    $"- Erfassung von Mitarbeitern und Kunden funktioniert\r\n" +
                    $"- Mutieren von Mitarbeitern und Kunden funktioniert\r\n" +
                    $"- Aktivieren und Deaktivieren von Mitarbeitern und Kunden funktioniert\r\n" +
                    $"- Löschen von Mitarbeitern und Kunden funktioniert\r\n" +
                    $"- Automatische Vergabe von Mitarbeiternummern (ebenso Kundennummern) funktioniert\r\n" +
                    $"- Protokollieren von Notizen in Kundenkontakten inkl. Historie funktioniert\r\n" +
                    $"- Suchmöglichkeiten über gespeicherten Informationen (Name, Vorname, Geburtsdatum, Mitarbeiter/Kunde) funktioniert\r\n" +
                    $"- Automatisches Speichern und Laden des Datenstamms auf Festplatte funktioniert\r\n\r\n" +
                    $"Es gibt noch diverse optionale Anforderungen, wie z.B. Validierungslogiken usw., welche den Benutzer unterstützen.",
                    NoteDate = startDate.AddDays(note * daySpacing).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture)
                };

                Notes.SaveNotesData(noteData);
            }
        }
        
        public void TestData(bool testfallMitarbeiter)
        {
            string contactStatus = testfallMitarbeiter ? "active" : "inactive";
            string contactNumber = testfallMitarbeiter ? "MA-Test" : "KD-Test";
            string typeOfContact = testfallMitarbeiter ? "Mitarbeiter" : "Kunde";

            // Aufruf Erstellung Notizen
            CreateTestNotes(contactNumber);
            
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
                    { "EmployeeNumber", testfallMitarbeiter ? "MA9999" : "" },
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
