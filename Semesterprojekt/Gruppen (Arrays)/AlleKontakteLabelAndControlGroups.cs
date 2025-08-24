using System.Windows.Forms;

namespace Semesterprojekt
{
    internal class AlleKontakteLabelAndControlGroups
    {
        // Erstellung Array für Labels (auf Basis offenem AlleKontakte)
        internal System.Windows.Forms.Label[] GroupLabel(AlleKontakte alleKontakte)
        {
            return new System.Windows.Forms.Label[]
            {
                alleKontakte.LblAllKntktVorname,
                alleKontakte.LblAllKntktName,
                alleKontakte.LblAllKntktBirthday,
                alleKontakte.LblAllKntktMa,
                alleKontakte.LblAllKntktKunde,
                alleKontakte.LblAllKntktInaktiv
            };
        }

        // Erstellung Array für Labels für ToolTip (auf Basis offenem AlleKontakte)
        internal System.Windows.Forms.Label[] GroupLabelToolTip(AlleKontakte alleKontakte)
        {
            return new System.Windows.Forms.Label[]
            {
                alleKontakte.LblAllKntktBirthday,
                alleKontakte.LblAllKntktMa,
                alleKontakte.LblAllKntktKunde,
                alleKontakte.LblAllKntktInaktiv
            };
        }

        // Erstellung Array für Eingabefelder (auf Basis offenem AlleKontakte)
        internal Control[] GroupField(AlleKontakte alleKontakte)
        {
            return new Control[]
            {
                alleKontakte.TxtAllKntktVorname,
                alleKontakte.TxtAllKntktName,
                alleKontakte.TxtAllKntktBirthday,
                alleKontakte.ChkBAllKntktMa,
                alleKontakte.ChkBAllKntktKunde,
                alleKontakte.ChkBAllKntktInaktiv
            };
        }
    }
}
