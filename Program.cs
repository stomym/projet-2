using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maisha_Launcher
{
    static class Program
    {
     
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Login f = new Login();
            f.Show();
            Application.Run();
           // Application.Run(new Login());
        }
    }
}
