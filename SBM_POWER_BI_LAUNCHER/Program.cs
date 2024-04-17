using System;
using System.Windows.Forms;
using SBM_POWER_BI_LAUNCHER.Utilities.DependencyInjection;
using SBM_POWER_BI_LAUNCHER.Utilities.Helpers;

namespace SBM_POWER_BI_LAUNCHER
{
    internal static class Program
    {
        [STAThread]
        //public static void Main(string[] args)
        public static void Main(string[] args)
        {
            var container = new DependencyInjectionContainer();
            container.Register<IProtectorHelper>(new ProtectorHelper());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainScreen("Urls", container.Resolve<IProtectorHelper>()));
            //if (args.Length == 0)
            //{
            //    return;
            //}
            //string Urls = args[0].ToString();
            //if (Urls.Contains("callpbi"))
            //{
            //    Urls = Urls.Replace("callpbi:", "");
            //    Application.Run(new MainScreen(Urls, container.Resolve<IProtectorHelper>()));
            //}
            //else
            //{
            //    Application.Run(new MainScreen(Urls, container.Resolve<IProtectorHelper>()));
            //}
        }
    }
}