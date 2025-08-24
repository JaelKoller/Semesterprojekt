using System.Windows.Forms;

namespace Semesterprojekt
{
    internal class DashboardLabelAndControlGroups
    {
        // Erstellung Array für Buttons (auf Basis offenem Dashboard)
        internal Control[] GroupButtons(Dashboard dashboard)
        {
            return new Control[]
            {
                dashboard.BtnDashMaNew,
                dashboard.BtnDashKndNew,
                dashboard.BtnDashAllKntkt,
                dashboard.BtnDashClose
            };
        }
    }
}