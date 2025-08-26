using System.Drawing;
using System.Windows.Forms;

namespace Semesterprojekt
{
    internal class AlleKontakteDesign
    {
        // Initialisierung Index-Counter für TapStop
        private static int tabIndexCounter = 1;

        // Design (Platzierung) der Labels, Eingabefelder, Buttons usw.
        internal static void Design(AlleKontakte alleKontakte)
        {
            // Platzierung Titel "Suchen nach:"
            alleKontakte.LblAllKntktSuchen.Location = new Point(50, 50);

            // Platzierung Labels und Eingabefelder (inkl. Start TabIndex bei 1)
            PlacementLabelAndField(alleKontakte.groupLabel, alleKontakte.groupField, ref tabIndexCounter);

            // Platzierung Buttons "Suchen", "Suche zurücksetzen" und "zurück zu Dashboard" (inkl. Start TabIndex fortführend)
            alleKontakte.BtnAllKntktSuchen.Size = new Size(340, 30);
            alleKontakte.BtnAllKntktSuchen.Location = new Point(50, 290);
            alleKontakte.BtnAllKntktSuchen.TabIndex = tabIndexCounter++;

            alleKontakte.BtnAllKntktSucheReset.Size = new Size(340, 30);
            alleKontakte.BtnAllKntktSucheReset.Location = new Point(50, 585);
            alleKontakte.BtnAllKntktSucheReset.TabIndex = tabIndexCounter++;

            alleKontakte.BtnAllKntktHome.Size = new Size(90, 50);
            alleKontakte.BtnAllKntktHome.Location = new Point(320, 20);
            alleKontakte.BtnAllKntktHome.TabIndex = tabIndexCounter++;

            // Platzierung Suchausgabe und Anzahl Treffer (inkl.TabStop "false")
            alleKontakte.LbAllKntktSuchAusg.Size = new Size(340, 200);
            alleKontakte.LbAllKntktSuchAusg.Location = new Point(50, 340);
            alleKontakte.LbAllKntktSuchAusg.TabStop = false;

            alleKontakte.LblAllKntktAnzSuchAusg.Size = new Size(50, 20);
            alleKontakte.LblAllKntktAnzSuchAusg.Location = new Point(50, 550);
            alleKontakte.LblAllKntktAnzSuchAusg.TabStop = false;
        }

        // Platzierung Labels und Eingabefelder (dynamisch)
        private static void PlacementLabelAndField(System.Windows.Forms.Label[] groupLabel, Control[] groupField, ref int tabIndexCounter)
        {
            int startLocation = 100;
            int labelXAchse = 50;
            int controlXAchse = 200;

            for (int i = 0; i < groupField.Length; i++)
            {
                groupLabel[i].Location = new Point(labelXAchse, startLocation);
                groupField[i].Location = new Point(controlXAchse, startLocation);

                startLocation += 30;

                // Label irrelevant für Tab und daher mit TabStop "false"
                groupLabel[i].TabStop = false;
                // Eingabefeld relevant für Tab und daher durchnummeriert (Start bei 1)
                groupField[i].TabIndex = tabIndexCounter++;
            }
        }

        // Erstellung ToolTip für spezifische Labels (zur besseren Verständlichkeit)
        internal static void InitializationLabelToolTip(AlleKontakte alleKontakte)
        {
            var initializationSetToolTip = AlleKontakteInitializations.SetToolTip(alleKontakte);
            var setToolTip = new SetToolTip();
            setToolTip.SetLabelToolTip(initializationSetToolTip);
        }
    }
}