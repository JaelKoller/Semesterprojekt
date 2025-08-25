using System.Windows.Forms;

namespace Semesterprojekt
{
    internal class InitializationCheckAndValidationFields
    {
        public Control[] GroupFieldEmployeesAndCustomers { get; set; }
        public Control[] GroupFieldEmployees { get; set; }
        public Control[] CheckFieldIgnore { get; set; }
        public Control[] CheckFieldSpecialCharactersWithoutNumbers { get; set; }
        public Control[] CheckFieldSpecialCharactersWithNumbers { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsClient { get; set; }
        public ComboBox Salutation { get; set; }
        public TextBox Birthday { get; set; }
        public ComboBox Gender { get; set; }
        public TextBox PostalCode { get; set; }
        public TextBox BusinessNumber { get; set; }
        public TextBox MobileNumber { get; set; }
        public TextBox Email { get; set; }
        public TextBox AHVNumber { get; set; }
        public TextBox Nationality { get; set; }
        public TextBox PostalCodeOffice { get; set; }
        public TextBox DateOfEntry { get; set; }
        public TextBox DateOfExit { get; set; }
    }
}
