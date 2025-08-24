using System.Windows.Forms;

namespace Semesterprojekt
{
    internal class AnsichtKontaktLabelAndControlGroups
    {
        // Erstellung Array für Labels der Gruppe "Kontaktdaten" (auf Basis offener AnsichtKontakt)
        internal System.Windows.Forms.Label[] GroupLabelEmployeesAndCustomers(AnsichtKontakt ansichtKontakt)
        {
            return new System.Windows.Forms.Label[]
            {
                ansichtKontakt.LblAnsichtKntktTitel,
                ansichtKontakt.LblAnsichtKntktAnrede,
                ansichtKontakt.LblAnsichtKntktVorname,
                ansichtKontakt.LblAnsichtKntktName,
                ansichtKontakt.LblAnsichtKntktBirthday,
                ansichtKontakt.LblAnsichtKntktGeschlecht,
                ansichtKontakt.LblAnsichtKntktAdr,
                ansichtKontakt.LblAnsichtKntktPLZ,
                ansichtKontakt.LblAnsichtKntktOrt,
                ansichtKontakt.LblAnsichtKntktTelGeschaeft,
                ansichtKontakt.LblAnsichtKntktTelMobile,
                ansichtKontakt.LblAnsichtKntktEmail
            };
        }

        // Erstellung Array für Labels der Gruppe "Mitarbeiterdaten" (auf Basis offener AnsichtKontakt)
        internal System.Windows.Forms.Label[] GroupLabelEmployees(AnsichtKontakt ansichtKontakt)
        {
            return new System.Windows.Forms.Label[]
            {
                ansichtKontakt.LblAnsichtKntktMaManr,
                ansichtKontakt.LblAnsichtKntktMaAHVNr,
                ansichtKontakt.LblAnsichtKntktMaNationalitaet,
                ansichtKontakt.LblAnsichtKntktMaKader,
                ansichtKontakt.LblAnsichtKntktMaBeschGrad,
                ansichtKontakt.LblAnsichtKntktMaAbteilung,
                ansichtKontakt.LblAnsichtKntktMaRolle,
                ansichtKontakt.LblAnsichtKntktMaLehrj,
                ansichtKontakt.LblAnsichtKntktMaAktLehrj,
                ansichtKontakt.LblAnsichtKntktMaOfficeNumber,
                ansichtKontakt.LblAnsichtKntktAdrOffice,
                ansichtKontakt.LblAnsichtKntktPLZOffice,
                ansichtKontakt.LblAnsichtKntktOrtOffice,
                ansichtKontakt.LblAnsichtKntktEintrDatum,
                ansichtKontakt.LblAnsichtKntktAustrDatum
            };
        }

        // Erstellung Array für Labels für ToolTip (auf Basis offener AnsichtKontakt)
        internal System.Windows.Forms.Label[] GroupLabelToolTip(AnsichtKontakt ansichtKontakt)
        {
            return new System.Windows.Forms.Label[]
            {
                ansichtKontakt.LblAnsichtKntktTitel,
                ansichtKontakt.LblAnsichtKntktBirthday,
                ansichtKontakt.LblAnsichtKntktPLZ,
                ansichtKontakt.LblAnsichtKntktTelGeschaeft,
                ansichtKontakt.LblAnsichtKntktTelMobile,
                ansichtKontakt.LblAnsichtKntktMaAHVNr,
                ansichtKontakt.LblAnsichtKntktMaNationalitaet,
                ansichtKontakt.LblAnsichtKntktMaKader,
                ansichtKontakt.LblAnsichtKntktMaLehrj,
                ansichtKontakt.LblAnsichtKntktMaAktLehrj,
                ansichtKontakt.LblAnsichtKntktPLZOffice,
                ansichtKontakt.LblAnsichtKntktEintrDatum,
                ansichtKontakt.LblAnsichtKntktAustrDatum
            };
        }

        // Erstellung Array für Eingabefelder der Gruppe "Kontaktdaten" (auf Basis offener AnsichtKontakt)
        internal Control[] GroupFieldEmployeesAndCustomers(AnsichtKontakt ansichtKontakt)
        {
            return new Control[]
            {
                ansichtKontakt.TxtAnsichtKntktTitel,
                ansichtKontakt.CmBxAnsichtKntktAnrede,
                ansichtKontakt.TxtAnsichtKntktVorname,
                ansichtKontakt.TxtAnsichtKntktName,
                ansichtKontakt.TxtAnsichtKntktBirthday,
                ansichtKontakt.CmBxAnsichtKntktGeschlecht,
                ansichtKontakt.TxtAnsichtKntktAdr,
                ansichtKontakt.TxtAnsichtKntktPLZ,
                ansichtKontakt.TxtAnsichtKntktOrt,
                ansichtKontakt.TxtAnsichtKntktTelGeschaeft,
                ansichtKontakt.TxtAnsichtKntktTelMobile,
                ansichtKontakt.TxtAnsichtKntktEmail
            };
        }

        // Erstellung Array für Eingabefelder der Gruppe "Mitarbeiterdaten" (auf Basis offener AnsichtKontakt)
        internal Control[] GroupFieldEmployees(AnsichtKontakt ansichtKontakt)
        {
            return new Control[]
            {
                ansichtKontakt.TxtAnsichtKntktMaManr,
                ansichtKontakt.TxtAnsichtKntktMaAHVNr,
                ansichtKontakt.TxtAnsichtKntktMaNationalitaet,
                ansichtKontakt.NumAnsichtKntktMaKader,
                ansichtKontakt.NumAnsichtKntktMaBeschGrad,
                ansichtKontakt.TxtAnsichtKntktMaAbteilung,
                ansichtKontakt.TxtAnsichtKntktMaRolle,
                ansichtKontakt.NumAnsichtKntktMaLehrj,
                ansichtKontakt.NumAnsichtKntktMaAktLehrj,
                ansichtKontakt.NumAnsichtKntktMaOfficeNumber,
                ansichtKontakt.TxtAnsichtKntktAdrOffice,
                ansichtKontakt.TxtAnsichtKntktPLZOffice,
                ansichtKontakt.TxtAnsichtKntktOrtOffice,
                ansichtKontakt.TxtAnsichtKntktEintrDatum,
                ansichtKontakt.TxtAnsichtKntktAustrDatum
            };
        }

        // Erstellung Array für Notiz-Felder (auf Basis offener AnsichtKontakt)
        internal Control[] GroupFieldNotes(AnsichtKontakt ansichtKontakt)
        {
            return new Control[]
            {
                ansichtKontakt.LbAnsichtKntktProtokolAusg,
                ansichtKontakt.TxtAnsichtKntktProtokolTitel,
                ansichtKontakt.TxtAnsichtKntktProtokolEing,
                ansichtKontakt.DateAnsichtKntktDateProtokol,
                ansichtKontakt.CmdAnsichtKntktSaveProtokol
            };
        }

        // Erstellung Array für Buttons (auf Basis offener AnsichtKontakt)
        internal Control[] GroupButtons(AnsichtKontakt ansichtKontakt)
        {
            return new Control[]
            {
                ansichtKontakt.CmdAnsichtKntktEdit,
                ansichtKontakt.CmdAnsichtKntktSaveAll,
                ansichtKontakt.CmdAnsichtKntktDeletAll,
                ansichtKontakt.CmdAnsichtKntktAlleKontakte,
                ansichtKontakt.CmdAnsichtKntktDashboard
            };
        }

        // Erstellung Array für KEINE-Pflichtfelder-Prüfung (auf Basis offener AnsichtKontakt)
        internal Control[] CheckFieldIgnore(AnsichtKontakt ansichtKontakt)
        {
            return new Control[]
            {
                ansichtKontakt.TxtAnsichtKntktTitel,
                ansichtKontakt.NumAnsichtKntktMaKader,
                ansichtKontakt.NumAnsichtKntktMaLehrj,
                ansichtKontakt.NumAnsichtKntktMaAktLehrj,
                (string.IsNullOrWhiteSpace(ansichtKontakt.TxtAnsichtKntktAustrDatum.Text) ? ansichtKontakt.TxtAnsichtKntktAustrDatum: null)
            };
        }
    }
}
