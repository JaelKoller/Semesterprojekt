using System.Windows.Forms;

namespace Semesterprojekt
{
    internal class ArrowKeyFunction
    {
        // Festlegung Pfeiltasten-Funktionen
        public static void ArrowFunction(object sender, KeyEventArgs e)
        {
            Form form = (Form)sender;

            bool excludedArrowKeys = ExcludedArrowKeys(form.ActiveControl);
            bool isForwardKey = (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right);
            bool isBackwardKey = (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left);

            // Verwendung Pfeiltasten links (für zurück) und rechts (für weiter) bei ausgeschlossenen Eingabefelder
            if (excludedArrowKeys)
            {
                if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
                {
                    // Fokussierung nächstes Eingabefeld gemäss Tab-Reihenfolge
                    form.SelectNextControl(form.ActiveControl ?? form, isForwardKey, tabStopOnly: true, nested: true, wrap: true);
                    // Markierung "verbrauchter Event" (Verhinderung Standard-/Doppelverhalten)
                    e.Handled = true;
                }

                return;
            }

            // Verwendung Pfeiltasten oben/links (für zurück) und unten/rechts (für weiter) bei allen anderen Eingabefelder
            if (isForwardKey || isBackwardKey)
            {
                // Fokussierung nächstes Eingabefeld gemäss Tab-Reihenfolge
                form.SelectNextControl(form.ActiveControl ?? form, isForwardKey, tabStopOnly: true, nested: true, wrap: true);
                // Markierung "verbrauchter Event" (Verhinderung Standard-/Doppelverhalten)
                e.Handled = true;
            }
        }

        // Prüfung der ausgeschlossenen Eingabefelder (Beibehaltung der "Eigen-Funktion" für Navigation)
        private static bool ExcludedArrowKeys(Control field)
        {
            while (field != null)
            {
                if
                    (field is ComboBox ||
                    field is NumericUpDown ||
                    field is DateTimePicker ||
                    field is ListBox ||
                    field is TrackBar ||
                    field is DataGridView ||
                    (field is TextBox textBox && textBox.Multiline))
                    return true;

                // Verschiebung in einen Container nach oben
                field = field.Parent;
            }

            return false;
        }
    }
}
