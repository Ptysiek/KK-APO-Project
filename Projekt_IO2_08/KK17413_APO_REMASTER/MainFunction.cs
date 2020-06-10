using System;
using System.Windows.Forms;
using KK17413_APO_REMASTER.BackEnd;


namespace KK17413_APO_REMASTER
{
    static class MainFunction
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Program goodRun = new Program();
        }
    }
}
