using System;
using System.Windows.Forms;
using Car_accounting.carAccounting.CMD;

namespace Car_accounting
{
    class CarAccounting
    {
        [STAThread]
        static void Main()
        {
            MainForm mainForm = new MainForm("Car accounting");
            Application.Run(mainForm);
        }
    }
}