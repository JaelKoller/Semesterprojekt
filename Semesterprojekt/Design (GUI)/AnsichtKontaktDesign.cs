using System.Drawing;
using System.Windows.Forms;

namespace Semesterprojekt
{
    internal class AnsichtKontaktDesign
    {
        // Initialisierung verwendeter Index-Counter
        private static int tabIndexCounter = 1;

        // Design (Platzierung) der Eingabe-Felder usw.
        internal static void Design(AnsichtKontakt ansichtKontakt)
        {
            // Platzierung Titel "Mitarbeiter / Kunde: ..."
            ansichtKontakt.LblAnsichtKntktNameAnzeige.Size = new Size(200, 40);
            ansichtKontakt.LblAnsichtKntktNameAnzeige.Location = new Point(10, 20);

            // Platzierung Gruppe "Kontaktdaten"
            ansichtKontakt.GrpBxDatenAlle.Size = new Size(410, 400);
            ansichtKontakt.GrpBxDatenAlle.Location = new Point(10, 60);

            // Platzierung Labels und Eingabefelder der Gruppe "Kontaktdaten" (inkl. Start TabIndex bei 1)
            // Erfassung Default-Tag als Vorbereitung für Validierung Eingabefelder mit TRUE (für OK-Fall) 
            PlacementLabelAndField(ansichtKontakt.groupLabelEmployeesAndCustomers, ansichtKontakt.groupFieldEmployeesAndCustomers, ref tabIndexCounter);

            // Platzierung Gruppe "Mitarbeiterdaten"
            ansichtKontakt.GrpBxDatenMA.Size = new Size(410, 490);
            ansichtKontakt.GrpBxDatenMA.Location = new Point(10, 470);

            // Platzierung Labels und Eingabefelder der Gruppe "Mitarbeiterdaten" (inkl. Start TabIndex fortführend)
            // Erfassung Default-Tag als Vorbereitung für Validierung Eingabefelder mit TRUE (für OK-Fall) 
            PlacementLabelAndField(ansichtKontakt.groupLabelEmployees, ansichtKontakt.groupFieldEmployees, ref tabIndexCounter);

            // Platzierung Gruppe Radio-Button (Aktiv vs. Inaktiv)
            ansichtKontakt.GrpBxAnsichtKntktAktiv.Size = new Size(150, 40);
            ansichtKontakt.GrpBxAnsichtKntktAktiv.Location = new Point(435, 10);

            // Platzierung Radio-Buttons (Aktiv vs. Inaktiv) 
            // Zählerstart TabIndex fortführend (nur auf ersten Radio-Button möglich)
            ansichtKontakt.RdbAnsichtKntktAktiv.Location = new Point(15, 15);
            ansichtKontakt.RdbAnsichtKntktAktiv.TabIndex = tabIndexCounter++;
            ansichtKontakt.RdbAnsichtKntktInaktiv.Location = new Point(75, 15);
            ansichtKontakt.RdbAnsichtKntktInaktiv.TabStop = false;

            // Platzierung Gruppe "Notizen zu Person"
            ansichtKontakt.GrpBxAnsichtKntktNotiz.Size = new Size(410, 545);
            ansichtKontakt.GrpBxAnsichtKntktNotiz.Location = new Point(435, 60);

            // Platzierung Felder der Gruppe "Notizen zu Person" (inkl. Start TabIndex fortführend)
            PlacementFieldNote(ansichtKontakt, ansichtKontakt.groupFieldNotes, ref tabIndexCounter);

            // Platzierung Buttons "Bearbeiten, Löschen, Speichern, Zurück zum Dashboard" (inkl. Start TabIndex fortführend)
            PlacementButton(ansichtKontakt, ansichtKontakt.groupButtons, ref tabIndexCounter);
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
                groupField[i].Location = new Point(controlXAchse, startLocation);

                startLocation += 30;

                // Label irrelevant für Tab und daher mit TabStop "false"
                groupLabel[i].TabStop = false;
                // Eingabefeld relevant für Tab und daher durchnummeriert
                groupField[i].TabIndex = tabIndexCounter++;

                // Default-Tag relevant für Validierung Eingabefelder (Start mit TRUE)
                groupField[i].Tag = "true";
            }
        }

        // Platzierung Notiz-Felder (fix)
        private static void PlacementFieldNote(AnsichtKontakt ansichtKontakt, Control[] groupField, ref int indexCounter)
        {
            int width = 380;
            int height = 250;
            int locationX = 15;
            int locationY = 30;

            for (int i = 0; i < groupField.Length; i++)
            {
                // Notiz-Felder relevant für Tab und daher durchnummeriert
                groupField[i].TabIndex = tabIndexCounter++;
            }

            ansichtKontakt.LbAnsichtKntktProtokolAusg.Size = new Size(width, height);
            ansichtKontakt.LbAnsichtKntktProtokolAusg.Location = new Point(locationX, locationY);
            ansichtKontakt.TxtAnsichtKntktProtokolTitel.Size = new Size(width, 20);
            ansichtKontakt.TxtAnsichtKntktProtokolTitel.Location = new Point(locationX, height = height + 30);
            ansichtKontakt.TxtAnsichtKntktProtokolEing.Size = new Size(width, 150);
            ansichtKontakt.TxtAnsichtKntktProtokolEing.Location = new Point(locationX, height = height + 30);
            ansichtKontakt.DateAnsichtKntktDateProtokol.Size = new Size(width, 40);
            ansichtKontakt.DateAnsichtKntktDateProtokol.Location = new Point(locationX, height = height + 160);
            ansichtKontakt.CmdAnsichtKntktSaveProtokol.Size = new Size(width, 30);
            ansichtKontakt.CmdAnsichtKntktSaveProtokol.Location = new Point(locationX, height + 30);
        }

        // Platzierung Buttons (dynamisch)
        private static void PlacementButton(AnsichtKontakt ansichtKontakt, Control[] groupField, ref int indexCounter)
        {
            int width = 150;
            int height = 60;
            int locationX = 485;
            int locationY = 640;

            for (int i = 0; i < groupField.Length; i++)
            {
                groupField[i].Size = new Size(width, height);

                // Buttons relevant für Tab und daher durchnummeriert
                groupField[i].TabIndex = tabIndexCounter++;
            }

            // Erhöhung Länge und Breite mit Gap von 10
            width += 10;
            height += 10;

            ansichtKontakt.CmdAnsichtKntktEdit.Location = new Point(locationX, locationY);
            ansichtKontakt.CmdAnsichtKntktSaveAll.Location = new Point(locationX, locationY + height);
            ansichtKontakt.CmdAnsichtKntktDeletAll.Location = new Point(locationX, locationY + height + height);
            ansichtKontakt.CmdAnsichtKntktAlleKontakte.Location = new Point(locationX + width, locationY);
            ansichtKontakt.CmdAnsichtKntktDashboard.Location = new Point(locationX + width, locationY + height);
        }

        // Erstellung ToolTip für spezifische Labels (zur besseren Verständlichkeit)
        internal static void InitializationLabelToolTip(AnsichtKontakt ansichtKontakt)
        {
            var initializationSetToolTip = AnsichtKontaktInitializations.SetToolTip(ansichtKontakt);
            var setToolTip = new SetToolTip();
            setToolTip.SetLabelToolTip(initializationSetToolTip);
        }
    }
}