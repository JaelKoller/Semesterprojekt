﻿using Semesterprojekt.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Semesterprojekt
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new Dashboard());

            var testingKontaktErstellen = new Testing_KontaktErstellen();
            testingKontaktErstellen.TestData("mitarbeitende");
            testingKontaktErstellen.TestData("kunde");
        }
    }
}
