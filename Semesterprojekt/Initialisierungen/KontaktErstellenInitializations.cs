using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semesterprojekt
{
    internal class KontaktErstellenInitializations
    {
        // Initialisierung Argumente (Inhalt) für Klasse "SetToolTip" (auf Basis offenem KontaktErstellen)
        internal static InitializationLabelsToolTip SetToolTip(KontaktErstellen kontaktErstellen)
        {
            return new InitializationLabelsToolTip
            {
                GroupLabelToolTip = kontaktErstellen.groupLabelToolTip,
                Title = kontaktErstellen.LblCreatKntktTitel,
                Birthday = kontaktErstellen.LblCreatKntktBirthday,
                PostalCode = kontaktErstellen.LblCreatKntktPLZ,
                BusinessNumber = kontaktErstellen.LblCreatKntktTelGeschaeft,
                MobileNumber = kontaktErstellen.LblCreatKntktTelMobile,
                AHVNumber = kontaktErstellen.LblCreatKntktMaAHVNr,
                Nationality = kontaktErstellen.LblCreatKntktMaNationalitaet,
                ManagementLevel = kontaktErstellen.LblCreatKntktMaKader,
                AcademicYear = kontaktErstellen.LblCreatKntktMaLehrj,
                CurrentAcademicYear = kontaktErstellen.LblCreatKntktMaAktLehrj,
                PostalCodeOffice = kontaktErstellen.LblCreatKntktPLZOffice,
                DateOfEntry = kontaktErstellen.LblCreatKntktEintrDatum,
                DateOfExit = kontaktErstellen.LblCreatKntktAustrDatum
            };
        }

        // Initialisierung Argumente (Inhalt) für Klasse "ContactData" (auf Basis offenem KontaktErstellen)
        internal static InitializationContactData ContactDataContent(KontaktErstellen kontaktErstellen)
        {
            return new InitializationContactData
            {
                Title = kontaktErstellen.TxtCreatKntktTitel,
                Salutation = kontaktErstellen.CmBxCreatKntktAnrede,
                FirstName = kontaktErstellen.TxtCreatKntktVorname,
                LastName = kontaktErstellen.TxtCreatKntktName,
                Birthday = kontaktErstellen.TxtCreatKntktBirthday,
                Gender = kontaktErstellen.CmBxCreatKntktGeschlecht,
                Address = kontaktErstellen.TxtCreatKntktAdr,
                PostalCode = kontaktErstellen.TxtCreatKntktPLZ,
                City = kontaktErstellen.TxtCreatKntktOrt,
                BusinessNumber = kontaktErstellen.TxtCreatKntktTelGeschaeft,
                MobileNumber = kontaktErstellen.TxtCreatKntktTelMobile,
                Email = kontaktErstellen.TxtCreatKntktEmail,
                EmployeeNumber = kontaktErstellen.TxtCreatKntktMaManr,
                AHVNumber = kontaktErstellen.TxtCreatKntktMaAHVNr,
                Nationality = kontaktErstellen.TxtCreatKntktMaNationalitaet,
                ManagementLevel = kontaktErstellen.NumCreatKntktMaKader,
                LevelOfEmployment = kontaktErstellen.NumCreatKntktMaBeschGrad,
                Department = kontaktErstellen.TxtCreatKntktMaAbteilung,
                Role = kontaktErstellen.TxtCreatKntktMaRolle,
                AcademicYear = kontaktErstellen.NumCreatKntktMaLehrj,
                CurrentAcademicYear = kontaktErstellen.NumCreatKntktMaAktLehrj,
                OfficeNumber = kontaktErstellen.NumCreatKntktMaOfficeNumber,
                AddressOffice = kontaktErstellen.TxtCreatKntktAdrOffice,
                PostalCodeOffice = kontaktErstellen.TxtCreatKntktPLZOffice,
                CityOffice = kontaktErstellen.TxtCreatKntktOrtOffice,
                DateOfEntry = kontaktErstellen.TxtCreatKntktEintrDatum,
                DateOfExit = kontaktErstellen.TxtCreatKntktAustrDatum
            };
        }

        // Initialisierung Argumente (Inhalt) für Klasse "CheckAndValidationFields" (auf Basis offenem KontaktErstellen)
        internal static InitializationCheckAndValidationFields CheckAndValidationFieldsContent(KontaktErstellen kontaktErstellen)
        {
            return new InitializationCheckAndValidationFields
            {
                GroupFieldEmployeesAndCustomers = kontaktErstellen.groupFieldEmployeesAndCustomers,
                GroupFieldEmployees = kontaktErstellen.groupFieldEmployees,
                CheckFieldIgnore = kontaktErstellen.groups.CheckFieldIgnore(kontaktErstellen),
                IsEmployee = kontaktErstellen.RdbCreatKntktMa.Checked,
                IsClient = kontaktErstellen.RdbCreatKntktKunde.Checked,
                Salutation = kontaktErstellen.CmBxCreatKntktAnrede,
                Birthday = kontaktErstellen.TxtCreatKntktBirthday,
                Gender = kontaktErstellen.CmBxCreatKntktGeschlecht,
                PostalCode = kontaktErstellen.TxtCreatKntktPLZ,
                BusinessNumber = kontaktErstellen.TxtCreatKntktTelGeschaeft,
                MobileNumber = kontaktErstellen.TxtCreatKntktTelMobile,
                Email = kontaktErstellen.TxtCreatKntktEmail,
                AHVNumber = kontaktErstellen.TxtCreatKntktMaAHVNr,
                Nationality = kontaktErstellen.TxtCreatKntktMaNationalitaet,
                PostalCodeOffice = kontaktErstellen.TxtCreatKntktPLZOffice,
                DateOfEntry = kontaktErstellen.TxtCreatKntktEintrDatum,
                DateOfExit = kontaktErstellen.TxtCreatKntktAustrDatum
            };
        }
    }
}
