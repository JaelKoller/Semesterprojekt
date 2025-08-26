using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Semesterprojekt
{
    internal class SetToolTip
    {
        // Initialisierung Werte für ToolTip
        private readonly System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip
        {
            AutoPopDelay = 20000, // Standardwert liegt bei 5000ms (Wie lange bleibt Tooltip sichtbar)
            InitialDelay = 100, // Standardwert liegt bei 500ms (Verzögerung bis Tooltip erscheint)
            ReshowDelay = 100, // Standardwert liegt bei 100ms (Verzögerung zwischen mehreren Tooltips hintereinander)
            ShowAlways = true // Standardwert ist "false" (Tooltip wird auch angezeigt, wenn Formular nicht aktiv)
        };

        // Initialisierung Label-Texte für ToolTip
        private readonly Dictionary<string, string> labelToolTip = new Dictionary<string, string>
        {
            ["Title"] = "Namenstitel (gekürzt)\r\nz.B. Dr., Ing., Prof.",
            ["PostalCode"] = "4-/5-stellige Postleitzahl\r\n(Schweiz und Nachbarländer)",
            ["PhoneNumber"] = "6-/15-stellige Telefon Nr. mit Vorwahl\r\n(Schweiz und Nachbarländer)\r\nz.B. +41 71 123 44 55",
            ["AHVNumber"] = "Eingabe mit Punkten (CH-Norm)\r\nz.B. 756.1234.5678.90",
            ["Nationality"] = "2-stelliger Länderkürzel\r\nz.B. CH, DE, FR, IT",
            ["ManagementLevel"] = "0 = Fachmitarbeiter/in\r\n1 = Fachspezialist/in\r\n2 = Teamleiter/in\r\n3 = Abteilungsleiter/in\r\n4 = Geschäftsleiter/in\r\n5 = Unternehumgsleiter/in",
            ["AcademicYear"] = "Anzahl abgeschlossene Ausbildungsjahre (EFZ, HF, FH usw.)",
            ["CurrentAcademicYear"] = "nur relevant für Lernende",
            ["PostalCodeOffice"] = "4-stellige Postleitzahl ohne führende 0\r\n(Schweiz)",
            ["Date"] = "Eingabe mit Format 'TT.MM.JJJJ'\r\nz.B. 01.01.2025",
            ["SearchEmployeeContacts"] = "Häkchen für Such-Einschränkung auf 'Mitarbeiter'",
            ["SearchClientContacts"] = "Häkchen für Such-Einschränkung 'Kunde'",
            ["SearchInactiveContacts"] = "Häkchen für Such-Erweiterung"
        };

        // Anzeige ToolTip mit Hover-Effekt
        public void SetLabelToolTip(InitializationLabelsToolTip content)
        {                     
            foreach (System.Windows.Forms.Label label in content.GroupLabelToolTip)
            {
                toolTip.SetToolTip(label, labelToolTip[label.AccessibleName]);

                // Ergänzung Label mit Info-Icon (als Hinweis für ToolTip)
                label.AutoSize = true;
                label.UseCompatibleTextRendering = true;
                label.Text += "ℹ";
                label.Cursor = Cursors.Hand;

                // Speicherung Original-Schrift (für Sicherstellung keine unerwünschten Nebeneffekte)
                Font originalFont = label.Font;
                
                label.MouseEnter += (s, e) => label.Font = new Font(originalFont, FontStyle.Bold); // Hover-Effekt mit "fetter" Schrift
                label.MouseLeave += (s, e) => label.Font = originalFont; // Original-Schrift
            }
        }
    }
}
