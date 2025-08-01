using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Semesterprojekt
{
    public partial class AlleKontakte : Form
    {
        // Initialisierung verwendete Label-/Control-Gruppen
        private System.Windows.Forms.Label[] groupLabel;
        private Control[] groupField;

        // Initialisierung verwendeter Index-Counter
        private int tabIndexCounter = 1;

        // Initialisierung verwendeter BackColor
        private Color backColorOK = SystemColors.Window;
        private Color backColorNOK = Color.LightPink;

        public AlleKontakte()
        {
            InitializeComponent();
            this.Size = new Size(470, 410);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Initialisierung verwendete Label-/Control-Gruppen
            groupLabel = GroupLabel();
            groupField = GroupField();

            Design();
            InitializationLabelToolTip();
        }

        // Design (Platzierung) der Eingabe-Felder usw.
        private void Design()
        {
            // Platzierung Titel "Suchen nach:"
            LblAllKntktSuchen.Location = new Point(50, 50);

            // Platzierung Labels und Eingabefelder
            // Zählerstart (Index) für Labels und Eingabefelder bei 1
            PlacementLabelAndField(groupLabel, groupField, tabIndexCounter);

            // Platzierung Buttons "Suchen" und "zurück zu Dashboard"
            BtnAllKntktSuchen.Size = new Size(355, 30);
            BtnAllKntktSuchen.Location = new Point(50, 300);
            BtnAllKntktHome.Size = new Size(90, 40);
            BtnAllKntktHome.Location = new Point(350, 10);
        }

        // Platzierung Labels und Eingabefelder (dynamisch)
        private void PlacementLabelAndField(System.Windows.Forms.Label[] groupLabel, Control[] groupField, int indexCounter)
        {
            int startLocation = 100;
            int labelXAchse = 50;
            int controlXAchse = 220;
            int tabIndexCounter = indexCounter;

            for (int i = 0; i < groupField.Length; i++)
            {
                groupLabel[i].Location = new Point(labelXAchse, startLocation);
                groupField[i].Location = new Point(controlXAchse, startLocation);

                startLocation += 30;

                // Label irrelevant für Tab und daher fix mit 0
                groupLabel[i].TabIndex = 0;
                // Eingabefeld relevant für Tab und daher durchnummeriert (Start bei 1)
                groupField[i].TabIndex = tabIndexCounter++;
            }
        }

        // Erstellung Array für Labels
        private System.Windows.Forms.Label[] GroupLabel()
        {
            groupLabel = new System.Windows.Forms.Label[]
            {
                LblAllKntktVorname,
                LblAllKntktName,
                LblAllKntktBirthday,
                LblAllKntktMa,
                LblAllKntktKunde,
                LblAllKntktDeaktiviert
            };

            return groupLabel;
        }

        // Erstellung Array für Eingabefelder
        private Control[] GroupField()
        {
            groupField = new Control[]
            {
                TxtAllKntktVorname,
                TxtAllKntktName,
                TxtAllKntktBirthday,
                ChkBAllKntktMa,
                ChkBAllKntktKunde,
                ChkBAllKntktDeaktiviert
            };

            return groupField;
        }

        // Erstellung ToolTip für spezifische Labels (zur besseren Verständlichkeit)
        private void InitializationLabelToolTip()
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip
            {
                AutoPopDelay = 10000, // Standardwert liegt bei 5000ms (Wie lange bleibt Tooltip sichtbar)
                InitialDelay = 100, // Standardwert liegt bei 500ms (Verzögerung bis Tooltip erscheint)
                ReshowDelay = 100, // Standardwert liegt bei 100ms (Verzögerung zwischen mehreren Tooltips hintereinander)
                ShowAlways = true // Standardwert ist FALSE (Tooltip wird auch angezeigt, wenn Formular nicht aktiv)
            };

            SetLabelToolTip(toolTip, LblAllKntktBirthday, "Eingabe mit Nullen und Punkten\r\nz.B. 01.01.1900");
            SetLabelToolTip(toolTip, LblAllKntktMa, "Häkchen für Such-Einschränkung auf 'Mitarbeiter'");
            SetLabelToolTip(toolTip, LblAllKntktKunde, "Häkchen für Such-Einschränkung 'Kunde'");
            SetLabelToolTip(toolTip, LblAllKntktDeaktiviert, "Häkchen für Such-Erweiterung");
        }

        // Erzeugung Hover-Effekt bei ToolTip (userfreundlicher)
        private void SetLabelToolTip(System.Windows.Forms.ToolTip tooltip, System.Windows.Forms.Label label, string text)
        {
            tooltip.SetToolTip(label, text);

            // Speicherung Original-Schrift (für keine unerwünschten Nebeneffekte)
            Font originalFont = label.Font;

            label.MouseEnter += (s, e) =>
            {
                label.Font = new Font(originalFont, FontStyle.Bold); // Hover-Effekt mit "fetter" Schrift
            };

            label.MouseLeave += (s, e) =>
            {
                label.Font = originalFont; // Original-Schrift
            };
        }

        private void BtnAllKntktSuchen_Click(object sender, EventArgs e)
        {
            // Initialisierung "AnsichtKontakt" für Absprung via Button "Suchen"
            var ansichtKontaktForm = new AnsichtKontakt();
            ansichtKontaktForm.FormClosed += (s, arg) => this.Show();
            ansichtKontaktForm.Show();
            this.Hide();
        }

        private void BtnAllKntktHome_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
