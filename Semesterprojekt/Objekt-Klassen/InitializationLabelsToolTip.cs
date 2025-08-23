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

        private System.Windows.Forms.Label postalCode;
        public System.Windows.Forms.Label PostalCode { get => postalCode; set { postalCode = value; if (value != null) value.AccessibleName = nameof(PostalCode); } }
        
        private System.Windows.Forms.Label businessNumber;
        public System.Windows.Forms.Label BusinessNumber { get => businessNumber; set { businessNumber = value; if (value != null) value.AccessibleName = "PhoneNumber"; } }

        private System.Windows.Forms.Label mobileNumber;
        public System.Windows.Forms.Label MobileNumber { get => mobileNumber; set { mobileNumber = value; if (value != null) value.AccessibleName = "PhoneNumber"; } }

        private System.Windows.Forms.Label ahvNumber;
        public System.Windows.Forms.Label AHVNumber { get => ahvNumber; set { ahvNumber = value; if (value != null) value.AccessibleName = nameof(AHVNumber); } }

        private System.Windows.Forms.Label nationality;
        public System.Windows.Forms.Label Nationality { get => nationality; set { nationality = value; if (value != null) value.AccessibleName = nameof(Nationality); } }

        private System.Windows.Forms.Label managementLevel;
        public System.Windows.Forms.Label ManagementLevel { get => managementLevel; set { managementLevel = value; if (value != null) value.AccessibleName = nameof(ManagementLevel); } }

        private System.Windows.Forms.Label academicYear;
        public System.Windows.Forms.Label AcademicYear { get => academicYear; set { academicYear = value; if (value != null) value.AccessibleName = nameof(AcademicYear); } }

        private System.Windows.Forms.Label currentAcademicYear;
        public System.Windows.Forms.Label CurrentAcademicYear { get => currentAcademicYear; set { currentAcademicYear = value; if (value != null) value.AccessibleName = nameof(CurrentAcademicYear); } }

        private System.Windows.Forms.Label postalCodeOffice;
        public System.Windows.Forms.Label PostalCodeOffice { get => postalCodeOffice; set { postalCodeOffice = value; if (value != null) value.AccessibleName = nameof(PostalCodeOffice); } }

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
}
