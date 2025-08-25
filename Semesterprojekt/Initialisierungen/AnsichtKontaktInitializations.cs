//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Semesterprojekt
//{
//    //// Basis offenes Form "AnsichtKontakt"
//    internal class AnsichtKontaktInitializations
//    {
//        // Initialisierung Argumente (Inhalt) für Klasse "SetToolTip"
//        private InitializationLabelsToolTip SetToolTip()
//        {
//            return new InitializationLabelsToolTip
//            {
//                GroupLabelToolTip = groupLabelToolTip,
//                Title = LblAnsichtKntktTitel,
//                Birthday = LblAnsichtKntktBirthday,
//                PostalCode = LblAnsichtKntktPLZ,
//                BusinessNumber = LblAnsichtKntktTelGeschaeft,
//                MobileNumber = LblAnsichtKntktTelMobile,
//                AHVNumber = LblAnsichtKntktMaAHVNr,
//                Nationality = LblAnsichtKntktMaNationalitaet,
//                ManagementLevel = LblAnsichtKntktMaKader,
//                AcademicYear = LblAnsichtKntktMaLehrj,
//                CurrentAcademicYear = LblAnsichtKntktMaAktLehrj,
//                PostalCodeOffice = LblAnsichtKntktPLZOffice,
//                DateOfEntry = LblAnsichtKntktEintrDatum,
//                DateOfExit = LblAnsichtKntktAustrDatum
//            };
//        }

//        // Initialisierung Kontaktdaten (Resultat aus Suche)
//        private void InitializationContactData(InitializationContactData contactData)
//        {
//            RdbAnsichtKntktAktiv.Checked = contactData.ContactStatus == "active";
//            RdbAnsichtKntktInaktiv.Checked = contactData.ContactStatus == "inactive";
//            TxtAnsichtKntktTitel.Text = Convert.ToString(contactData.Fields["Title"]);
//            CmBxAnsichtKntktAnrede.SelectedItem = Convert.ToString(contactData.Fields["Salutation"]);
//            TxtAnsichtKntktVorname.Text = Convert.ToString(contactData.Fields["FirstName"]);
//            TxtAnsichtKntktName.Text = Convert.ToString(contactData.Fields["LastName"]);
//            TxtAnsichtKntktBirthday.Text = Convert.ToString(contactData.Fields["Birthday"]);
//            CmBxAnsichtKntktGeschlecht.SelectedItem = Convert.ToString(contactData.Fields["Gender"]);
//            TxtAnsichtKntktAdr.Text = Convert.ToString(contactData.Fields["Address"]);
//            TxtAnsichtKntktPLZ.Text = Convert.ToString(contactData.Fields["PostalCode"]);
//            TxtAnsichtKntktOrt.Text = Convert.ToString(contactData.Fields["City"]);
//            TxtAnsichtKntktTelGeschaeft.Text = Convert.ToString(contactData.Fields["BusinessNumber"]);
//            TxtAnsichtKntktTelMobile.Text = Convert.ToString(contactData.Fields["MobileNumber"]);
//            TxtAnsichtKntktEmail.Text = Convert.ToString(contactData.Fields["Email"]);

//            if (isEmployee)
//            {
//                TxtAnsichtKntktMaManr.Text = Convert.ToString(contactData.Fields["EmployeeNumber"]);
//                TxtAnsichtKntktMaAHVNr.Text = Convert.ToString(contactData.Fields["AHVNumber"]);
//                TxtAnsichtKntktMaNationalitaet.Text = Convert.ToString(contactData.Fields["Nationality"]);
//                NumAnsichtKntktMaKader.Text = Convert.ToString(contactData.Fields["ManagementLevel"]);
//                NumAnsichtKntktMaBeschGrad.Value = Convert.ToDecimal(contactData.Fields["LevelOfEmployment"]);
//                TxtAnsichtKntktMaAbteilung.Text = Convert.ToString(contactData.Fields["Department"]);
//                TxtAnsichtKntktMaRolle.Text = Convert.ToString(contactData.Fields["Role"]);
//                NumAnsichtKntktMaLehrj.Value = Convert.ToDecimal(contactData.Fields["AcademicYear"]);
//                NumAnsichtKntktMaAktLehrj.Value = Convert.ToDecimal(contactData.Fields["CurrentAcademicYear"]);
//                NumAnsichtKntktMaOfficeNumber.Value = Convert.ToDecimal(contactData.Fields["OfficeNumber"]);
//                TxtAnsichtKntktAdrOffice.Text = Convert.ToString(contactData.Fields["AddressOffice"]);
//                TxtAnsichtKntktPLZOffice.Text = Convert.ToString(contactData.Fields["PostalCodeOffice"]);
//                TxtAnsichtKntktOrtOffice.Text = Convert.ToString(contactData.Fields["CityOffice"]);
//                TxtAnsichtKntktEintrDatum.Text = Convert.ToString(contactData.Fields["DateOfEntry"]);
//                TxtAnsichtKntktAustrDatum.Text = Convert.ToString(contactData.Fields["DateOfExit"]);
//            }
//        }

//        // Initialisierung Argumente (Inhalt) für Klasse "ContactData"
//        private InitializationContactData ContactDataContent()
//        {
//            return new InitializationContactData
//            {
//                Title = TxtAnsichtKntktTitel,
//                Salutation = CmBxAnsichtKntktAnrede,
//                FirstName = TxtAnsichtKntktVorname,
//                LastName = TxtAnsichtKntktName,
//                Birthday = TxtAnsichtKntktBirthday,
//                Gender = CmBxAnsichtKntktGeschlecht,
//                Address = TxtAnsichtKntktAdr,
//                PostalCode = TxtAnsichtKntktPLZ,
//                City = TxtAnsichtKntktOrt,
//                BusinessNumber = TxtAnsichtKntktTelGeschaeft,
//                MobileNumber = TxtAnsichtKntktTelMobile,
//                Email = TxtAnsichtKntktEmail,
//                EmployeeNumber = TxtAnsichtKntktMaManr,
//                AHVNumber = TxtAnsichtKntktMaAHVNr,
//                Nationality = TxtAnsichtKntktMaNationalitaet,
//                ManagementLevel = NumAnsichtKntktMaKader,
//                LevelOfEmployment = NumAnsichtKntktMaBeschGrad,
//                Department = TxtAnsichtKntktMaAbteilung,
//                Role = TxtAnsichtKntktMaRolle,
//                AcademicYear = NumAnsichtKntktMaLehrj,
//                CurrentAcademicYear = NumAnsichtKntktMaAktLehrj,
//                OfficeNumber = NumAnsichtKntktMaOfficeNumber,
//                AddressOffice = TxtAnsichtKntktAdrOffice,
//                PostalCodeOffice = TxtAnsichtKntktPLZOffice,
//                CityOffice = TxtAnsichtKntktOrtOffice,
//                DateOfEntry = TxtAnsichtKntktEintrDatum,
//                DateOfExit = TxtAnsichtKntktAustrDatum
//            };
//        }

//        // Initialisierung Argumente (Inhalt) für Klasse "CheckAndValidationFields"
//        private InitializationCheckAndValidationFields CheckAndValidationFieldsContent()
//        {
//            return new InitializationCheckAndValidationFields
//            {
//                GroupFieldEmployeesAndCustomers = groupFieldEmployeesAndCustomers,
//                GroupFieldEmployees = groupFieldEmployees,
//                CheckFieldIgnore = groups.CheckFieldIgnore(this),
//                CheckFieldSpecialCharactersWithoutNumbers = groups.CheckFieldSpecialCharactersWithoutNumbers(this),
//                CheckFieldSpecialCharactersWithNumbers = groups.CheckFieldSpecialCharactersWithNumbers(this),
//                IsEmployee = isEmployee,
//                IsClient = isClient,
//                Salutation = CmBxAnsichtKntktAnrede,
//                Birthday = TxtAnsichtKntktBirthday,
//                Gender = CmBxAnsichtKntktGeschlecht,
//                PostalCode = TxtAnsichtKntktPLZ,
//                BusinessNumber = TxtAnsichtKntktTelGeschaeft,
//                MobileNumber = TxtAnsichtKntktTelMobile,
//                Email = TxtAnsichtKntktEmail,
//                AHVNumber = TxtAnsichtKntktMaAHVNr,
//                Nationality = TxtAnsichtKntktMaNationalitaet,
//                PostalCodeOffice = TxtAnsichtKntktPLZOffice,
//                DateOfEntry = TxtAnsichtKntktEintrDatum,
//                DateOfExit = TxtAnsichtKntktAustrDatum
//            };
//        }

//        // Initialisierung Argumente (Inhalt) für Klasse "CheckAndValidationNoteFields"
//        private InitializationNotes CheckAndValidationNoteFieldsContent()
//        {
//            return new InitializationNotes
//            {
//                ContactNumber = contactNumber,
//                DefaultNoteTitle = defaultNoteTitle,
//                DefaultNoteText = defaultNoteText,
//                NoteTitle = TxtAnsichtKntktProtokolTitel.Text,
//                NoteText = TxtAnsichtKntktProtokolEing.Text,
//                NoteDate = DateAnsichtKntktDateProtokol.Value.ToString("dd.MM.yyyy")
//            };
//        }
//    }
//}
