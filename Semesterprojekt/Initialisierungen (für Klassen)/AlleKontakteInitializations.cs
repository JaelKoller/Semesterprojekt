using System.Collections.Generic;

namespace Semesterprojekt
{
    internal class AlleKontakteInitializations
    {
        // Initialisierung Argumente (Inhalt) für Klasse "SetToolTip"
        internal static InitializationLabelsToolTip SetToolTip(AlleKontakte alleKontakte)
        {
            return new InitializationLabelsToolTip
            {
                GroupLabelToolTip = alleKontakte.groupLabelToolTip,
                Birthday = alleKontakte.LblAllKntktBirthday,
                SearchEmployeeContacts = alleKontakte.LblAllKntktMa,
                SearchClientContacts = alleKontakte.LblAllKntktKunde,
                SearchInactiveContacts = alleKontakte.LblAllKntktInaktiv
            };
        }

        // Erstellung Dictonary für Suche
        internal static Dictionary<string, object> GroupSeach(AlleKontakte alleKontakte)
        {
            return new Dictionary<string, object>
            {
                { "FirstName", alleKontakte.TxtAllKntktVorname.Text.Trim() },
                { "LastName", alleKontakte.TxtAllKntktName.Text.Trim() },
                { "Birthday", alleKontakte.TxtAllKntktBirthday.Text.Trim() },
                { "CheckEmployee", alleKontakte.ChkBAllKntktMa.Checked },
                { "CheckClient", alleKontakte.ChkBAllKntktKunde.Checked },
                { "CheckInactive", alleKontakte.ChkBAllKntktInaktiv.Checked }
            };
        }
    }
}
