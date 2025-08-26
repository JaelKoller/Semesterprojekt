using System.Drawing;
using System.Windows.Forms;

namespace Semesterprojekt
{
    internal class DashboardDesign
    {
        // Platzierung Buttons (dynamisch)
        internal static void PlacementButton(Dashboard dashboard, Control[] groupField)
        {
            int tabIndexCounter = 1;
            int width = 150;
            int height = 100;
            int location = 80;

            for (int i = 0; i < groupField.Length; i++)
            {
                groupField[i].Size = new Size(width, height);

                // Buttons relevant für Tab und daher durchnummeriert
                groupField[i].TabIndex = tabIndexCounter++;
            }

            // Erhöhung Länge und Breite mit Gap von 10
            width += 10;
            height += 10;

            dashboard.BtnDashMaNew.Location = new Point(location, location);
            dashboard.BtnDashKndNew.Location = new Point(location, location + height);
            dashboard.BtnDashAllKntkt.Location = new Point(location + width, location);
            dashboard.BtnDashClose.Location = new Point(location + width, location + height);
        }
    }
}
