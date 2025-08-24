using System.Windows.Forms;

namespace Semesterprojekt
{
    internal class KontaktErstellenLabelAndControlGroups
    {
        // Erstellung Array für Labels der Gruppe "Kontaktdaten" (auf Basis offenem KontaktErstellen)
        internal System.Windows.Forms.Label[] GroupLabelEmployeesAndCustomers(KontaktErstellen kontaktErstellen)
        {
            return new System.Windows.Forms.Label[]
            {
                kontaktErstellen.LblCreatKntktTitel,
                kontaktErstellen.LblCreatKntktAnrede,
                kontaktErstellen.LblCreatKntktVorname,
                kontaktErstellen.LblCreatKntktName,
                kontaktErstellen.LblCreatKntktBirthday,
                kontaktErstellen.LblCreatKntktGeschlecht,
                kontaktErstellen.LblCreatKntktAdresse,
                kontaktErstellen.LblCreatKntktPLZ,
                kontaktErstellen.LblCreatKntktOrt,
                kontaktErstellen.LblCreatKntktTelGeschaeft,
                kontaktErstellen.LblCreatKntktTelMobile,
                kontaktErstellen.LblCreatKntktEmail
            };
        }

        // Erstellung Array für Labels der Gruppe "Mitarbeiterdaten" (auf Basis offenem KontaktErstellen)
        internal System.Windows.Forms.Label[] GroupLabelEmployees(KontaktErstellen kontaktErstellen)
        {
            return new System.Windows.Forms.Label[]
            {
                kontaktErstellen.LblCreatKntktMaMaNr,
                kontaktErstellen.LblCreatKntktMaAHVNr,
                kontaktErstellen.LblCreatKntktMaNationalitaet,
                kontaktErstellen.LblCreatKntktMaKader,
                kontaktErstellen.LblCreatKntktMaBeschGrad,
                kontaktErstellen.LblCreaKntktMaAbteilung,
                kontaktErstellen.LblCreatKntktMaRolle,
                kontaktErstellen.LblCreatKntktMaLehrj,
                kontaktErstellen.LblCreatKntktMaAktLehrj,
                kontaktErstellen.LblCreatKntktMaOfficeNumber,
                kontaktErstellen.LblCreatKntktAdrOffice,
                kontaktErstellen.LblCreatKntktPLZOffice,
                kontaktErstellen.LblCreatKntktOrtOffice,
                kontaktErstellen.LblCreatKntktEintrDatum,
                kontaktErstellen.LblCreatKntktAustrDatum
            };
        }

        // Erstellung Array für Labels für ToolTip (auf Basis offenem KontaktErstellen)
        internal System.Windows.Forms.Label[] GroupLabelToolTip(KontaktErstellen kontaktErstellen)
        {
            return new System.Windows.Forms.Label[]
            {
                kontaktErstellen.LblCreatKntktTitel,
                kontaktErstellen.LblCreatKntktBirthday,
                kontaktErstellen.LblCreatKntktPLZ,
                kontaktErstellen.LblCreatKntktTelGeschaeft,
                kontaktErstellen.LblCreatKntktTelMobile,
                kontaktErstellen.LblCreatKntktMaAHVNr,
                kontaktErstellen.LblCreatKntktMaNationalitaet,
                kontaktErstellen.LblCreatKntktMaKader,
                kontaktErstellen.LblCreatKntktMaLehrj,
                kontaktErstellen.LblCreatKntktMaAktLehrj,
                kontaktErstellen.LblCreatKntktEintrDatum,
                kontaktErstellen.LblCreatKntktAustrDatum,
                kontaktErstellen.LblCreatKntktPLZOffice
            };
        }

        // Erstellung Array für Eingabefelder der Gruppe "Kontaktdaten" (auf Basis offenem KontaktErstellen)
        internal Control[] GroupFieldEmployeesAndCustomers(KontaktErstellen kontaktErstellen)
        {
            return new Control[]
            {
                kontaktErstellen.TxtCreatKntktTitel,
                kontaktErstellen.CmBxCreatKntktAnrede,
                kontaktErstellen.TxtCreatKntktVorname,
                kontaktErstellen.TxtCreatKntktName,
                kontaktErstellen.TxtCreatKntktBirthday,
                kontaktErstellen.CmBxCreatKntktGeschlecht,
                kontaktErstellen.TxtCreatKntktAdr,
                kontaktErstellen.TxtCreatKntktPLZ,
                kontaktErstellen.TxtCreatKntktOrt,
                kontaktErstellen.TxtCreatKntktTelGeschaeft,
                kontaktErstellen.TxtCreatKntktTelMobile,
                kontaktErstellen.TxtCreatKntktEmail
            };
        }

        // Erstellung Array für Eingabefelder der Gruppe "Mitarbeiterdaten" (auf Basis offenem KontaktErstellen)
        internal Control[] GroupFieldEmployees(KontaktErstellen kontaktErstellen)
        {
            return new Control[]
            {
                kontaktErstellen.TxtCreatKntktMaManr,
                kontaktErstellen.TxtCreatKntktMaAHVNr,
                kontaktErstellen.TxtCreatKntktMaNationalitaet,
                kontaktErstellen.NumCreatKntktMaKader,
                kontaktErstellen.NumCreatKntktMaBeschGrad,
                kontaktErstellen.TxtCreatKntktMaAbteilung,
                kontaktErstellen.TxtCreatKntktMaRolle,
                kontaktErstellen.NumCreatKntktMaLehrj,
                kontaktErstellen.NumCreatKntktMaAktLehrj,
                kontaktErstellen.NumCreatKntktMaOfficeNumber,
                kontaktErstellen.TxtCreatKntktAdrOffice,
                kontaktErstellen.TxtCreatKntktPLZOffice,
                kontaktErstellen.TxtCreatKntktOrtOffice,
                kontaktErstellen.TxtCreatKntktEintrDatum,
                kontaktErstellen.TxtCreatKntktAustrDatum
            };
        }

        // Erstellung Array für KEINE-Pflichtfelder-Prüfung (auf Basis offenem KontaktErstellen)
        internal Control[] CheckFieldIgnore(KontaktErstellen kontaktErstellen)
        {
            return new Control[]
            {
                kontaktErstellen.TxtCreatKntktTitel,
                 // bei Mitarbeitern bleibt das Feld "Pflicht"
                // (!RdbCreatKntktMa.Checked ? TxtCreatKntktMaAHVNr : null),            
                kontaktErstellen.NumCreatKntktMaKader,
                kontaktErstellen.NumCreatKntktMaLehrj,
                kontaktErstellen.NumCreatKntktMaAktLehrj,
                // bei Mitarbeiter bleibt das Feld "Pflicht"
                (!kontaktErstellen.RdbCreatKntktMa.Checked ? kontaktErstellen.TxtCreatKntktEintrDatum : null),
                // bei enthaltenem Wert wird das Feld validiert
                (string.IsNullOrWhiteSpace(kontaktErstellen.TxtCreatKntktAustrDatum.Text) ? kontaktErstellen.TxtCreatKntktAustrDatum: null),
            };
        }
    }
}
