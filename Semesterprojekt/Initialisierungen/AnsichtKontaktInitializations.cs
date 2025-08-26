using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semesterprojekt
{
    //// Basis offenes Form "AnsichtKontakt"
    internal class AnsichtKontaktInitializations
    {
        // Initialisierung Argumente (Inhalt) für Klasse "SetToolTip"
        internal static InitializationLabelsToolTip SetToolTip(AnsichtKontakt ansichtKontakt)
        {
            return new InitializationLabelsToolTip
            {
                GroupLabelToolTip = ansichtKontakt.groupLabelToolTip,
                Title = ansichtKontakt.LblAnsichtKntktTitel,
                Birthday = ansichtKontakt.LblAnsichtKntktBirthday,
                PostalCode = ansichtKontakt.LblAnsichtKntktPLZ,
                BusinessNumber = ansichtKontakt.LblAnsichtKntktTelGeschaeft,
                MobileNumber = ansichtKontakt.LblAnsichtKntktTelMobile,
                AHVNumber = ansichtKontakt.LblAnsichtKntktMaAHVNr,
                Nationality = ansichtKontakt.LblAnsichtKntktMaNationalitaet,
                ManagementLevel = ansichtKontakt.LblAnsichtKntktMaKader,
                AcademicYear = ansichtKontakt.LblAnsichtKntktMaLehrj,
                CurrentAcademicYear = ansichtKontakt.LblAnsichtKntktMaAktLehrj,
                PostalCodeOffice = ansichtKontakt.LblAnsichtKntktPLZOffice,
                DateOfEntry = ansichtKontakt.LblAnsichtKntktEintrDatum,
                DateOfExit = ansichtKontakt.LblAnsichtKntktAustrDatum
            };
        }

        // Initialisierung Kontaktdaten (Resultat aus Suche)
        internal static void InitializationContactData(AnsichtKontakt ansichtKontakt, InitializationContactData contactData)
        {
            ansichtKontakt.RdbAnsichtKntktAktiv.Checked = contactData.ContactStatus == "active";
            ansichtKontakt.RdbAnsichtKntktInaktiv.Checked = contactData.ContactStatus == "inactive";
            ansichtKontakt.TxtAnsichtKntktTitel.Text = Convert.ToString(contactData.Fields["Title"]);
            ansichtKontakt.CmBxAnsichtKntktAnrede.SelectedItem = Convert.ToString(contactData.Fields["Salutation"]);
            ansichtKontakt.TxtAnsichtKntktVorname.Text = Convert.ToString(contactData.Fields["FirstName"]);
            ansichtKontakt.TxtAnsichtKntktName.Text = Convert.ToString(contactData.Fields["LastName"]);
            ansichtKontakt.TxtAnsichtKntktBirthday.Text = Convert.ToString(contactData.Fields["Birthday"]);
            ansichtKontakt.CmBxAnsichtKntktGeschlecht.SelectedItem = Convert.ToString(contactData.Fields["Gender"]);
            ansichtKontakt.TxtAnsichtKntktAdr.Text = Convert.ToString(contactData.Fields["Address"]);
            ansichtKontakt.TxtAnsichtKntktPLZ.Text = Convert.ToString(contactData.Fields["PostalCode"]);
            ansichtKontakt.TxtAnsichtKntktOrt.Text = Convert.ToString(contactData.Fields["City"]);
            ansichtKontakt.TxtAnsichtKntktTelGeschaeft.Text = Convert.ToString(contactData.Fields["BusinessNumber"]);
            ansichtKontakt.TxtAnsichtKntktTelMobile.Text = Convert.ToString(contactData.Fields["MobileNumber"]);
            ansichtKontakt.TxtAnsichtKntktEmail.Text = Convert.ToString(contactData.Fields["Email"]);

            if (ansichtKontakt.isEmployee)
            {
                ansichtKontakt.TxtAnsichtKntktMaManr.Text = Convert.ToString(contactData.Fields["EmployeeNumber"]);
                ansichtKontakt.TxtAnsichtKntktMaAHVNr.Text = Convert.ToString(contactData.Fields["AHVNumber"]);
                ansichtKontakt.TxtAnsichtKntktMaNationalitaet.Text = Convert.ToString(contactData.Fields["Nationality"]);
                ansichtKontakt.NumAnsichtKntktMaKader.Text = Convert.ToString(contactData.Fields["ManagementLevel"]);
                ansichtKontakt.NumAnsichtKntktMaBeschGrad.Value = Convert.ToDecimal(contactData.Fields["LevelOfEmployment"]);
                ansichtKontakt.TxtAnsichtKntktMaAbteilung.Text = Convert.ToString(contactData.Fields["Department"]);
                ansichtKontakt.TxtAnsichtKntktMaRolle.Text = Convert.ToString(contactData.Fields["Role"]);
                ansichtKontakt.NumAnsichtKntktMaLehrj.Value = Convert.ToDecimal(contactData.Fields["AcademicYear"]);
                ansichtKontakt.NumAnsichtKntktMaAktLehrj.Value = Convert.ToDecimal(contactData.Fields["CurrentAcademicYear"]);
                ansichtKontakt.NumAnsichtKntktMaOfficeNumber.Value = Convert.ToDecimal(contactData.Fields["OfficeNumber"]);
                ansichtKontakt.TxtAnsichtKntktAdrOffice.Text = Convert.ToString(contactData.Fields["AddressOffice"]);
                ansichtKontakt.TxtAnsichtKntktPLZOffice.Text = Convert.ToString(contactData.Fields["PostalCodeOffice"]);
                ansichtKontakt.TxtAnsichtKntktOrtOffice.Text = Convert.ToString(contactData.Fields["CityOffice"]);
                ansichtKontakt.TxtAnsichtKntktEintrDatum.Text = Convert.ToString(contactData.Fields["DateOfEntry"]);
                ansichtKontakt.TxtAnsichtKntktAustrDatum.Text = Convert.ToString(contactData.Fields["DateOfExit"]);
            }
        }

        // Initialisierung Argumente (Inhalt) für Klasse "ContactData"
        internal static InitializationContactData ContactDataContent(AnsichtKontakt ansichtKontakt)
        {
            return new InitializationContactData
            {
                Title = ansichtKontakt.TxtAnsichtKntktTitel,
                Salutation = ansichtKontakt.CmBxAnsichtKntktAnrede,
                FirstName = ansichtKontakt.TxtAnsichtKntktVorname,
                LastName = ansichtKontakt.TxtAnsichtKntktName,
                Birthday = ansichtKontakt.TxtAnsichtKntktBirthday,
                Gender = ansichtKontakt.CmBxAnsichtKntktGeschlecht,
                Address = ansichtKontakt.TxtAnsichtKntktAdr,
                PostalCode = ansichtKontakt.TxtAnsichtKntktPLZ,
                City = ansichtKontakt.TxtAnsichtKntktOrt,
                BusinessNumber = ansichtKontakt.TxtAnsichtKntktTelGeschaeft,
                MobileNumber = ansichtKontakt.TxtAnsichtKntktTelMobile,
                Email = ansichtKontakt.TxtAnsichtKntktEmail,
                EmployeeNumber = ansichtKontakt.TxtAnsichtKntktMaManr,
                AHVNumber = ansichtKontakt.TxtAnsichtKntktMaAHVNr,
                Nationality = ansichtKontakt.TxtAnsichtKntktMaNationalitaet,
                ManagementLevel = ansichtKontakt.NumAnsichtKntktMaKader,
                LevelOfEmployment = ansichtKontakt.NumAnsichtKntktMaBeschGrad,
                Department = ansichtKontakt.TxtAnsichtKntktMaAbteilung,
                Role = ansichtKontakt.TxtAnsichtKntktMaRolle,
                AcademicYear = ansichtKontakt.NumAnsichtKntktMaLehrj,
                CurrentAcademicYear = ansichtKontakt.NumAnsichtKntktMaAktLehrj,
                OfficeNumber = ansichtKontakt.NumAnsichtKntktMaOfficeNumber,
                AddressOffice = ansichtKontakt.TxtAnsichtKntktAdrOffice,
                PostalCodeOffice = ansichtKontakt.TxtAnsichtKntktPLZOffice,
                CityOffice = ansichtKontakt.TxtAnsichtKntktOrtOffice,
                DateOfEntry = ansichtKontakt.TxtAnsichtKntktEintrDatum,
                DateOfExit = ansichtKontakt.TxtAnsichtKntktAustrDatum
            };
        }

        // Initialisierung Argumente (Inhalt) für Klasse "CheckAndValidationFields"
        internal static InitializationCheckAndValidationFields CheckAndValidationFieldsContent(AnsichtKontakt ansichtKontakt)
        {
            return new InitializationCheckAndValidationFields
            {
                GroupFieldEmployeesAndCustomers = ansichtKontakt.groupFieldEmployeesAndCustomers,
                GroupFieldEmployees = ansichtKontakt.groupFieldEmployees,
                CheckFieldIgnore = ansichtKontakt.groups.CheckFieldIgnore(ansichtKontakt),
                CheckFieldSpecialCharactersWithoutNumbers = ansichtKontakt.groups.CheckFieldSpecialCharactersWithoutNumbers(ansichtKontakt),
                CheckFieldSpecialCharactersWithNumbers = ansichtKontakt.groups.CheckFieldSpecialCharactersWithNumbers(ansichtKontakt),
                IsEmployee = ansichtKontakt.isEmployee,
                IsClient = ansichtKontakt.isClient,
                Salutation = ansichtKontakt.CmBxAnsichtKntktAnrede,
                Birthday = ansichtKontakt.TxtAnsichtKntktBirthday,
                Gender = ansichtKontakt.CmBxAnsichtKntktGeschlecht,
                PostalCode = ansichtKontakt.TxtAnsichtKntktPLZ,
                BusinessNumber = ansichtKontakt.TxtAnsichtKntktTelGeschaeft,
                MobileNumber = ansichtKontakt.TxtAnsichtKntktTelMobile,
                Email = ansichtKontakt.TxtAnsichtKntktEmail,
                AHVNumber = ansichtKontakt.TxtAnsichtKntktMaAHVNr,
                Nationality = ansichtKontakt.TxtAnsichtKntktMaNationalitaet,
                PostalCodeOffice = ansichtKontakt.TxtAnsichtKntktPLZOffice,
                DateOfEntry = ansichtKontakt.TxtAnsichtKntktEintrDatum,
                DateOfExit = ansichtKontakt.TxtAnsichtKntktAustrDatum
            };
        }

        // Initialisierung Argumente (Inhalt) für Klasse "CheckAndValidationNoteFields"
        internal static InitializationNotes CheckAndValidationNoteFieldsContent(AnsichtKontakt ansichtKontakt)
        {
            return new InitializationNotes
            {
                ContactNumber = ansichtKontakt.contactNumber,
                DefaultNoteTitle = ansichtKontakt.defaultNoteTitle,
                DefaultNoteText = ansichtKontakt.defaultNoteText,
                NoteTitle = ansichtKontakt.TxtAnsichtKntktProtokolTitel.Text,
                NoteText = ansichtKontakt.TxtAnsichtKntktProtokolEing.Text,
                NoteDate = ansichtKontakt.DateAnsichtKntktDateProtokol.Value.ToString("dd.MM.yyyy")
            };
        }
    }
}
