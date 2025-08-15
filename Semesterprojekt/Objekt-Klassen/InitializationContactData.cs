using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Semesterprojekt
{
    public class InitializationContactData
    {
        public string ContactStatus { get; set; }
        public string ContactNumber { get; set; }
        public string TypeOfContact { get; set; }
        
        // Dictionary für alle Eingabefelder mit Schlüssel "AccessibleName" (für JSON)
        public Dictionary<string, string> Fields { get; set; } = new Dictionary<string, string>();

        // Setzen des AccessibleName für Speicherung in JSON (dynamisch für alle Forms nutzbar)
        // Nutzung Funktion "nameof(...)" für Rückgabe Bezeichnung (Name) von Property als String
        // Nutzung "[JsonIgnore]" für Unterbindung Mitgabe NULL-Einträge ans JSON
        private TextBox title;
        [JsonIgnore] public TextBox Title { get => title; set { title = value; if (value != null) value.AccessibleName = nameof(Title); } }

        private ComboBox salutation;
        [JsonIgnore] public ComboBox Salutation { get => salutation; set { salutation = value; if (value != null) value.AccessibleName = nameof(Salutation); } }

        private TextBox firstName;
        [JsonIgnore] public TextBox FirstName { get => firstName; set { firstName = value; if (value != null) value.AccessibleName = nameof(FirstName); } }

        private TextBox lastName;
        [JsonIgnore] public TextBox LastName { get => lastName; set { lastName = value; if (value != null) value.AccessibleName = nameof(LastName); } }

        private TextBox birthday;
        [JsonIgnore] public TextBox Birthday { get => birthday; set { birthday = value; if (value != null) value.AccessibleName = nameof(Birthday); } }

        private ComboBox gender;
        [JsonIgnore] public ComboBox Gender { get => gender; set { gender = value; if (value != null) value.AccessibleName = nameof(Gender); } }

        private TextBox address;
        [JsonIgnore] public TextBox Address { get => address; set { address = value; if (value != null) value.AccessibleName = nameof(Address); } }

        private TextBox postalCode;
        [JsonIgnore] public TextBox PostalCode { get => postalCode; set { postalCode = value; if (value != null) value.AccessibleName = nameof(PostalCode); } }

        private TextBox city;
        [JsonIgnore] public TextBox City { get => city; set { city = value; if (value != null) value.AccessibleName = nameof(City); } }

        private TextBox businessNumber;
        [JsonIgnore] public TextBox BusinessNumber { get => businessNumber; set { businessNumber = value; if (value != null) value.AccessibleName = nameof(BusinessNumber); } }

        private TextBox mobileNumber;
        [JsonIgnore] public TextBox MobileNumber { get => mobileNumber; set { mobileNumber = value; if (value != null) value.AccessibleName = nameof(MobileNumber); } }

        private TextBox email;
        [JsonIgnore] public TextBox Email { get => email; set { email = value; if (value != null) value.AccessibleName = nameof(Email); } }

        private TextBox employeeNumber;
        [JsonIgnore] public TextBox EmployeeNumber { get => employeeNumber; set { employeeNumber = value; if (value != null) value.AccessibleName = nameof(EmployeeNumber); } }

        private TextBox ahvNumber;
        [JsonIgnore] public TextBox AHVNumber { get => ahvNumber; set { ahvNumber = value; if (value != null) value.AccessibleName = nameof(AHVNumber); } }

        private TextBox nationality;
        [JsonIgnore] public TextBox Nationality { get => nationality; set { nationality = value; if (value != null) value.AccessibleName = nameof(Nationality); } }

        private TextBox managementLevel;
        [JsonIgnore] public TextBox ManagementLevel { get => managementLevel; set { managementLevel = value; if (value != null) value.AccessibleName = nameof(ManagementLevel); } }

        private NumericUpDown levelOfEmployment;
        [JsonIgnore] public NumericUpDown LevelOfEmployment { get => levelOfEmployment; set { levelOfEmployment = value; if (value != null) value.AccessibleName = nameof(LevelOfEmployment); } }

        private TextBox department;
        [JsonIgnore] public TextBox Department { get => department; set { department = value; if (value != null) value.AccessibleName = nameof(Department); } }

        private TextBox role;
        [JsonIgnore] public TextBox Role { get => role; set { role = value; if (value != null) value.AccessibleName = nameof(Role); } }

        private NumericUpDown academicYear;
        [JsonIgnore] public NumericUpDown AcademicYear { get => academicYear; set { academicYear = value; if (value != null) value.AccessibleName = nameof(AcademicYear); } }

        private NumericUpDown currentAcademicYear;
        [JsonIgnore] public NumericUpDown CurrentAcademicYear { get => currentAcademicYear; set { currentAcademicYear = value; if (value != null) value.AccessibleName = nameof(CurrentAcademicYear); } }

        private NumericUpDown officeNumber;
        [JsonIgnore] public NumericUpDown OfficeNumber { get => officeNumber; set { officeNumber = value; if (value != null) value.AccessibleName = nameof(OfficeNumber); } }

        private TextBox dateOfEntry;
        [JsonIgnore] public TextBox DateOfEntry { get => dateOfEntry; set { dateOfEntry = value; if (value != null) value.AccessibleName = nameof(DateOfEntry); } }

        private TextBox dateOfExit;
        [JsonIgnore] public TextBox DateOfExit { get => dateOfExit; set { dateOfExit = value; if (value != null) value.AccessibleName = nameof(DateOfExit); } }
    }
}
