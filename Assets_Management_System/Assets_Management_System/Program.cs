using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assets_Management_System
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

            // Seed database automatically if empty
            try
            {
                new Services.DataSeeder().Seed();
            }
            catch { /* Ignore seeding errors here */ }

            Application.Run(new LoginForm());
        }
    }
}
