using System.Windows.Forms;

namespace Semesterprojekt.Testing
{
    internal partial class Testing_AlleKontakte : Form
    {
        private AlleKontakte alleKontakteForm;

        internal Testing_AlleKontakte()
        {
            InitializeComponent();
        }
        internal void TestData()
        {
            alleKontakteForm = new AlleKontakte();

            // Testdaten
            alleKontakteForm.TxtAllKntktVorname.Text = "Daisy";
            alleKontakteForm.TxtAllKntktName.Text = "Duck";
            alleKontakteForm.TxtAllKntktBirthday.Text = "01.07.1989";
            alleKontakteForm.ChkBAllKntktMa.Checked = false;
            alleKontakteForm.ChkBAllKntktKunde.Checked = false;
            alleKontakteForm.ChkBAllKntktInaktiv.Checked = true;

            // Start Form "AlleKontakte" mit Testdaten
            Application.Run(alleKontakteForm);
        }
    }
}
