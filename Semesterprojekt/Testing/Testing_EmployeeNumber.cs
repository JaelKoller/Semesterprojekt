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
    public partial class Testing_EmployeeNumber : Form
    {
        public Testing_EmployeeNumber()
        {
            InitializeComponent();
            string employeeNumberNew = EmployeeNumber.GetEmployeeNumberNext();
            TxtBxTest.Text = employeeNumberNew;
        }
    }
}
