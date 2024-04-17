using System.Windows.Forms;

namespace SBM_POWER_BI_LAUNCHER.Utilities.Helpers
{
    public interface IProtectorHelper
    {
        bool Start(Form form);
        bool StartWithProcess(Form form, string processName);
        bool Stop(Form form);
    }
}