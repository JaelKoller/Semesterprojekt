using System.Drawing;
using System.Windows.Forms;

namespace Semesterprojekt
{
    internal class KontaktErstellenDesign
    {
        // Initialisierung Index-Counter für TapStop
        private static int tabIndexCounter = 1;

        // Design (Platzierung) der Labels, Eingabefelder, Buttons usw.
        internal static void Design(KontaktErstellen kontaktErstellen)
        {
            // Platzierung Gruppe Radio-Buttons (Mitarbeiter vs. Kunde)
            kontaktErstellen.GrpBxCreatKntktMaKunde.Size = new Size(165, 40);
            kontaktErstellen.GrpBxCreatKntktMaKunde.Location = new Point(10, 10);

            // Platzierung Gruppe "Kontaktdaten"
            kontaktErstellen.GrpBxCreatKntktDatenAlle.Size = new Size(365, 400);
            kontaktErstellen.GrpBxCreatKntktDatenAlle.Location = new Point(10, 60);

            // Platzierung Gruppe "Mitarbeiterdaten"
            kontaktErstellen.GrpBxDatenMA.Size = new Size(365, 490);
            kontaktErstellen.GrpBxDatenMA.Location = new Point(385, 60);

            // Platzierung Radio-Buttons (Mitarbeiter vs. Kunde)
            kontaktErstellen.RdbCreatKntktMa.Location = new Point(10, 15);
            kontaktErstellen.RdbCreatKntktKunde.Location = new Point(100, 15);

            // Platzierung Labels und Eingabefelder der Gruppe "Kontaktdaten" (inkl. Start TabIndex bei 1)
            // Erfassung Default-Tag als Vorbereitung für Validierung Eingabefelder mit "true" (für OK-Fall) 
            PlacementLabelAndField(kontaktErstellen.groupLabelEmployeesAndCustomers, kontaktErstellen.groupFieldEmployeesAndCustomers, ref tabIndexCounter);

            // Platzierung Labels und Eingabefelder der Gruppe "Mitarbeiterdaten" (inkl. Start TabIndex fortführend)
            // Erfassung Default-Tag als Vorbereitung für Validierung Eingabefelder mit "true" (für OK-Fall) 
            PlacementLabelAndField(kontaktErstellen.groupLabelEmployees, kontaktErstellen.groupFieldEmployees, ref tabIndexCounter);

            // Platzierung Buttons "Speichern und ..."
            kontaktErstellen.CmdCreateKntktKontaktErstellen.Size = new Size(150, 60);
            kontaktErstellen.CmdCreateKntktKontaktErstellen.Location = new Point(445, 580);

            kontaktErstellen.CmdCreateKntktDashboard.Size = new Size(150, 60);
            kontaktErstellen.CmdCreateKntktDashboard.Location = new Point(600, 580);

            kontaktErstellen.CmdCreateKntktVerwerfen.Size = new Size(305, 40);
            kontaktErstellen.CmdCreateKntktVerwerfen.Location = new Point(445, 645);
        }

        // Platzierung Labels und Eingabefelder (dynamisch)
        private static void PlacementLabelAndField(System.Windows.Forms.Label[] groupLabel, Control[] groupField, ref int tabIndexCounter)
        {
            int startLocation = 30;
            int labelXAchse = 10;
            int controlXAchse = 150;

            for (int i = 0; i < groupField.Length; i++)
            {
                groupLabel[i].Location = new Point(labelXAchse, startLocation);
                groupField[i].Size = new Size(200, 20);
                groupField[i].Location = new Point(controlXAchse, startLocation);

                startLocation += 30;

                // Label irrelevant für Tab und daher mit TabStop "false"
                groupLabel[i].TabStop = false;
                // Eingabefeld relevant für Tab und daher durchnummeriert (Start bei 1)
                groupField[i].TabIndex = tabIndexCounter++;
                // Default-Tag relevant für Validierung Eingabefelder (Start mit "true")
                groupField[i].Tag = "true";
            }
        }

        // Erstellung ToolTip für spezifische Labels (zur besseren Verständlichkeit)
        internal static void InitializationLabelToolTip(KontaktErstellen kontaktErstellen)
        {
            var initializationSetToolTip = KontaktErstellenInitializations.SetToolTip(kontaktErstellen);
            var setToolTip = new SetToolTip();
            setToolTip.SetLabelToolTip(initializationSetToolTip);
        }
    }
}
