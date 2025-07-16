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
        public AlleKontakte()
        {
            InitializeComponent();
        }

        private void LblAllKntktName_Click(object sender, EventArgs e)
        {

        }

        private void LblAllKntktSuchen_Click(object sender, EventArgs e)
        {

        }

        private void DateAllKntktBirthday_ValueChanged(object sender, EventArgs e)
        {

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
