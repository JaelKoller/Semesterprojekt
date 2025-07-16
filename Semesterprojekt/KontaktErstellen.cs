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
    public partial class KontaktErstellen : Form
    {
        public KontaktErstellen()
        {
            InitializeComponent();

        }

        private void CmdCreateKntktKontaktErstellen_Click(object sender, EventArgs e)
        {
            // Erstellung neues Form "KontaktErstellen"
            var kontaktErstellenForm = new KontaktErstellen();
            kontaktErstellenForm.FormClosed += (s, arg) => this.Show();
            kontaktErstellenForm.Show();
            this.Hide();
        }

        private void CmdCreateKntktDashboard_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
