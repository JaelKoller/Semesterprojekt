using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Semesterprojekt
{
    internal class InitializationLabelsToolTip
    {
        public System.Windows.Forms.Label[] GroupLabelToolTip { get; set; }

        // Setzen des AccessibleName für Auslesung Label-ToolTip-Text aus Dictionary (dynamisch für alle Forms nutzbar)
        // Nutzung Funktion "nameof(...)" für Rückgabe Bezeichnung (Name) von Property als String (keine Anwendung bei Doppelverwendung von Label-ToolTip-Text)
        private System.Windows.Forms.Label title;
        public System.Windows.Forms.Label Title { get => title; set { title = value; if (value != null) value.AccessibleName = nameof(Title); } }

        private System.Windows.Forms.Label birthday;
        public System.Windows.Forms.Label Birthday { get => birthday; set { birthday = value; if (value != null) value.AccessibleName = "Date"; } }

        private System.Windows.Forms.Label plz;
        public System.Windows.Forms.Label PLZ { get => plz; set { plz = value; if (value != null) value.AccessibleName = nameof(PLZ); } }

        private System.Windows.Forms.Label ahvNumber;
        public System.Windows.Forms.Label AHVNumber { get => ahvNumber; set { ahvNumber = value; if (value != null) value.AccessibleName = nameof(AHVNumber); } }

        private System.Windows.Forms.Label nationality;
        public System.Windows.Forms.Label Nationality { get => nationality; set { nationality = value; if (value != null) value.AccessibleName = nameof(Nationality); } }

        private System.Windows.Forms.Label academicYear;
        public System.Windows.Forms.Label AcademicYear { get => academicYear; set { academicYear = value; if (value != null) value.AccessibleName = "Academic"; } }

        private System.Windows.Forms.Label currentAcademicYear;
        public System.Windows.Forms.Label CurrentAcademicYear { get => currentAcademicYear; set { currentAcademicYear = value; if (value != null) value.AccessibleName = "Academic"; } }

        private System.Windows.Forms.Label dateOfEntry;
        public System.Windows.Forms.Label DateOfEntry { get => dateOfEntry; set { dateOfEntry = value; if (value != null) value.AccessibleName = "Date"; } }

        private System.Windows.Forms.Label dateOfExit;
        public System.Windows.Forms.Label DateOfExit { get => dateOfExit; set { dateOfExit = value; if (value != null) value.AccessibleName = "Date"; } }

        private System.Windows.Forms.Label searchEmployeeContacts;
        public System.Windows.Forms.Label SearchEmployeeContacts { get => searchEmployeeContacts; set { searchEmployeeContacts = value; if (value != null) value.AccessibleName = nameof(SearchEmployeeContacts); } }

        private System.Windows.Forms.Label searchClientContacts;
        public System.Windows.Forms.Label SearchClientContacts { get => searchClientContacts; set { searchClientContacts = value; if (value != null) value.AccessibleName = nameof(SearchClientContacts); } }

        private System.Windows.Forms.Label searchInactiveContacts;
        public System.Windows.Forms.Label SearchInactiveContacts { get => searchInactiveContacts; set { searchInactiveContacts = value; if (value != null) value.AccessibleName = nameof(SearchInactiveContacts); } }
    }

    internal class SetToolTip
    {
        // Initialisierung Werte für ToolTip
        private readonly System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip
        {
            AutoPopDelay = 10000, // Standardwert liegt bei 5000ms (Wie lange bleibt Tooltip sichtbar)
            InitialDelay = 100, // Standardwert liegt bei 500ms (Verzögerung bis Tooltip erscheint)
            ReshowDelay = 100, // Standardwert liegt bei 100ms (Verzögerung zwischen mehreren Tooltips hintereinander)
            ShowAlways = true // Standardwert ist FALSE (Tooltip wird auch angezeigt, wenn Formular nicht aktiv)
        };

        // Initialisierung Label-Texte für ToolTip
        private readonly Dictionary<string, string> labelToolTip = new Dictionary<string, string>
        {
            ["Title"] = "Namenstitel (gekürzt)\r\nz.B. Dr., Ing., Prof.",
            ["PLZ"] = "4-/5-stellige Postleitzahl\r\n(Schweiz und Nachbarländer)",
            ["AHVNumber"] = "Eingabe mit Punkten (CH-Norm)\r\nz.B. 756.1234.5678.90",
            ["Nationality"] = "2-stelliger Länderkürzel\r\nz.B. CH, DE, FR, IT",
            ["Academic"] = "nur relevant für Lernende",
            ["Date"] = "Eingabe mit Format 'TT.MM.JJJJ'\r\nz.B. 01.01.1900",
            ["SearchEmployeeContacts"] = "Häkchen für Such-Einschränkung auf 'Mitarbeiter'",
            ["SearchClientContacts"] = "Häkchen für Such-Einschränkung 'Kunde'",
            ["SearchInactiveContacts"] = "Häkchen für Such-Erweiterung"
        };

        // Erzeugung Hover-Effekt bei ToolTip (userfreundlicher)
        public void SetLabelToolTip(InitializationLabelsToolTip content)
        {                     
            foreach (System.Windows.Forms.Label label in content.GroupLabelToolTip)
            {
                toolTip.SetToolTip(label, labelToolTip[label.AccessibleName]);

                // Speicherung Original-Schrift (für keine unerwünschten Nebeneffekte)
                Font originalFont = label.Font;
                
                label.MouseEnter += (s, e) => label.Font = new Font(originalFont, FontStyle.Bold); // Hover-Effekt mit "fetter" Schrift
                label.MouseLeave += (s, e) => label.Font = originalFont; // Original-Schrift

            }
        }
    }
}
