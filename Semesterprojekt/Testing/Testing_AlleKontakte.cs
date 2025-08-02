using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Semesterprojekt.Testing
{
    public partial class Testing_AlleKontakte : Form
    {
        private AlleKontakte alleKontakteForm;

        public Testing_AlleKontakte()
        {
            InitializeComponent();
        }
        public void TestData()
        {
            alleKontakteForm = new AlleKontakte();

            // Testdaten
            alleKontakteForm.TxtAllKntktVorname.Text = "Jael";
            alleKontakteForm.TxtAllKntktName.Text = "Koller";
            alleKontakteForm.TxtAllKntktBirthday.Text = "01.07.1989";
            alleKontakteForm.ChkBAllKntktMa.Checked = false;
            alleKontakteForm.ChkBAllKntktKunde.Checked = false;
            alleKontakteForm.ChkBAllKntktInaktiv.Checked = true;

            // Start Form "AlleKontakte" mit Testdaten
            Application.Run(alleKontakteForm);
        }
    }
}
