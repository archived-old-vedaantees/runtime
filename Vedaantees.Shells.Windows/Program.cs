using System;
using System.Windows.Forms;
using Vedaantees.Shells.Windows.Messaging;

namespace Vedaantees.Shells.Windows
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            MessagingEngine.Init();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
