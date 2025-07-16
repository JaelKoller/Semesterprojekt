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

            // AutoSize und AutoSizeMode für Fenster
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }
    }
}
