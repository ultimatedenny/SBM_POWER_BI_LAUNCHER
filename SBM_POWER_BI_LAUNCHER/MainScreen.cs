using System;
using SBM_POWER_BI_LAUNCHER.Utilities.Helpers;
using ReaLTaiizor.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace SBM_POWER_BI_LAUNCHER
{
    public partial class MainScreen : MetroForm
    {
        private readonly IProtectorHelper _protectorHelper;
        string Statuss = "";
        public Module _module = new Module();
        string DLL_HIDE = "";
        string DLL_UNHIDE = "";
        string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;

        List<string> browserExecutables = new List<string>();
        List<string> officeExecutables = new List<string>();
        List<string> codeExecutables = new List<string>();

        public MainScreen(string Status, IProtectorHelper protectorHelper)
        {
            InitializeComponent();
            Statuss = Status;
            DLL_HIDE = Path.Combine(exeDirectory, "Hide.dll");
            DLL_UNHIDE = Path.Combine(exeDirectory, "UnHide.dll");
            browserExecutables = new List<string>
            {
                "chrome.exe",
                "firefox.exe",
                "msedge.exe",
                "Safari.exe",
                "opera.exe",
                "brave.exe"
            };
            officeExecutables = new List<string>
            {
                "winword.exe",       // Microsoft Word
                "excel.exe",         // Microsoft Excel
                "powerpnt.exe",      // Microsoft PowerPoint
                "outlook.exe",       // Microsoft Outlook
                "msaccess.exe",      // Microsoft Access
                "onenote.exe",       // Microsoft OneNote
                "mspub.exe",         // Microsoft Publisher
                "teams.exe",         // Microsoft Teams
                "visio.exe",         // Microsoft Visio
                "winproj.exe",       // Microsoft Project
                "pbidesktop.exe"
            };
            codeExecutables = new List<string>
            {
                "devenv.exe",
                "notepad++.exe",
                "notepad.exe"
            };
        }

        public void MainScreen_Load(object sender, EventArgs e)
        {
            StatusLabel.Text = "Not Running";
            metroButton1.Text = "Start";
            metroButton2.Enabled = false;
            metroButton3.Enabled = false;
            metroButton4.Enabled = false;
            metroButton5.Enabled = false;
            metroButton6.Enabled = false;
            metroButton7.Enabled = false;
            metroButton8.Enabled = false;
            metroButton9.Enabled = false;
            DLL_HIDE = Path.Combine(exeDirectory, "Hide.dll");
            DLL_UNHIDE = Path.Combine(exeDirectory, "UnHide.dll");

            //if (Statuss == "0")
            //{
            //    Start();
            //    Hide_Browser();
            //    this.Close();
            //}
            //else if (Statuss == "1")
            //{
            //    Start();
            //    unHide_Browser();
            //    this.Close();
            //}
            //else
            //{
            //    Start();
            //    unHide_Browser();
            //    this.Close();
            //}
        }

        private void UpdateUIState()
        {
            if (StatusLabel.Text == "Not Running")
            {
                StatusLabel.Text = "Running";
                metroButton1.Text = "Stop";
            }
            else
            {
                StatusLabel.Text = "Not Running";
                metroButton1.Text = "Start";
            }
        }

        public void FillProcessBg()
        {
            Process[] processes = Process.GetProcesses();
            comboBoxProcesses.Items.Clear();
            foreach (var process in processes)
            {
                if (process.MainWindowHandle == IntPtr.Zero)
                {
                    comboBoxProcesses.Items.Add(process.ProcessName + ".exe");
                }
            }
        }

        public void FillProcessAc()
        {
            Process[] processes = Process.GetProcesses();
            comboBoxProcesses.Items.Clear();
            foreach (var process in processes)
            {
                if (process.MainWindowHandle != IntPtr.Zero)
                {
                    comboBoxProcesses.Items.Add(process.ProcessName + ".exe");
                }
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            FillProcessAc();
        }

        private void metroButton3_Click_1(object sender, EventArgs e)
        {
            FillProcessBg();
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            Inject();
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            UnInject();
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            Hide_Browser();
        }

        private void metroButton8_Click(object sender, EventArgs e)
        {
            unHide_Browser();
        }

        private void metroButton9_Click(object sender, EventArgs e)
        {
            Hide_Office();
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            UnHide_Office();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcesses();
            try
            {
                if (StatusLabel.Text != "Not Running")
                {
                    Stop();
                }
                else
                {
                    Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Start()
        {
            UpdateUIState();
            metroButton2.Enabled = true;
            metroButton3.Enabled = true;
            metroButton4.Enabled = true;
            metroButton5.Enabled = true;
            metroButton6.Enabled = true;
            metroButton7.Enabled = true;
            metroButton8.Enabled = true;
            metroButton9.Enabled = true;
        }

        public void Stop()
        {
            UpdateUIState();
            metroButton2.Enabled = false;
            metroButton3.Enabled = false;
            metroButton4.Enabled = false;
            metroButton5.Enabled = false;
            metroButton6.Enabled = false;
            metroButton7.Enabled = false;
            metroButton8.Enabled = false;
            metroButton9.Enabled = false;
        }

        public void Hide_Office()
        {
            Process[] processes = Process.GetProcesses();
            var distinctProcessNames = processes.Select(p => p.ProcessName).Distinct();
            foreach (var processName in distinctProcessNames)
            {
                if (officeExecutables.Contains((processName + ".exe"), StringComparer.OrdinalIgnoreCase))
                {
                    var bsname = (processName + ".exe");
                    _module.InjectDLL(processName, DLL_HIDE);
                }
            }
        }

        public void UnHide_Office()
        {
            Process[] processes = Process.GetProcesses();
            var distinctProcessNames = processes.Select(p => p.ProcessName).Distinct();
            foreach (var processName in distinctProcessNames)
            {
                if (officeExecutables.Contains((processName + ".exe"), StringComparer.OrdinalIgnoreCase))
                {
                    var bsname = (processName + ".exe");
                    _module.InjectDLL(processName, DLL_UNHIDE);
                }
            }
        }

        public void Hide_Browser()
        {
            Process[] processes = Process.GetProcesses();
            var distinctProcessNames = processes.Select(p => p.ProcessName).Distinct();
            foreach (var processName in distinctProcessNames)
            {
                if (browserExecutables.Contains((processName + ".exe"), StringComparer.OrdinalIgnoreCase))
                {
                    var bsname = (processName + ".exe");
                    _module.InjectDLL(processName, DLL_HIDE);
                }
            }
            //foreach (var process in processes)
            //{
            //    if (process.MainWindowHandle != IntPtr.Zero)
            //    {
            //        var distinctProcessNames = processes.Select(p => p.ProcessName).Distinct();
            //        foreach (var processName in distinctProcessNames)
            //        {
            //            if (browserExecutables.Contains((processName + ".exe"), StringComparer.OrdinalIgnoreCase))
            //            {
            //                var bsname = (processName + ".exe");
            //                _module.InjectDLL(processName, DLL_HIDE);
            //            }
            //        }
            //    }
            //}
        }

        public void unHide_Browser()
        {
            Process[] processes = Process.GetProcesses();
            var distinctProcessNames = processes.Select(p => p.ProcessName).Distinct();
            foreach (var processName in distinctProcessNames)
            {
                if (browserExecutables.Contains((processName + ".exe"), StringComparer.OrdinalIgnoreCase))
                {
                    var bsname = (processName + ".exe");
                    _module.InjectDLL(processName, DLL_UNHIDE);
                }
            }
        }

        public void Hide_SourceCode()
        {
            Process[] processes = Process.GetProcesses();
            var distinctProcessNames = processes.Select(p => p.ProcessName).Distinct();
            foreach (var processName in distinctProcessNames)
            {
                if (codeExecutables.Contains((processName + ".exe"), StringComparer.OrdinalIgnoreCase))
                {
                    var bsname = (processName + ".exe");
                    _module.InjectDLL(processName, DLL_UNHIDE);
                }
            }
        }
        public void UnHide_SourceCode()
        {
            Process[] processes = Process.GetProcesses();
            var distinctProcessNames = processes.Select(p => p.ProcessName).Distinct();
            foreach (var processName in distinctProcessNames)
            {
                if (codeExecutables.Contains((processName + ".exe"), StringComparer.OrdinalIgnoreCase))
                {
                    var bsname = (processName + ".exe");
                    _module.InjectDLL(processName, DLL_UNHIDE);
                }
            }
        }

        public void Inject()
        {
            try
            {
                if (comboBoxProcesses.GetItemText(comboBoxProcesses.SelectedItem) != "")
                {
                    if (DLL_HIDE != null)
                    {
                        string _new = comboBoxProcesses.SelectedItem.ToString().Substring(0, comboBoxProcesses.SelectedItem.ToString().Length - 4);
                        _module.InjectDLL(_new, DLL_HIDE);
                    }
                    else
                    {
                        MessageBox.Show("Please Select A Dll", "Error: No Dll Selected", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else
                {
                    MessageBox.Show("Please Select A Process From The List", "Error: No Process Selected", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UnInject()
        {
            try
            {
                if (comboBoxProcesses.GetItemText(comboBoxProcesses.SelectedItem) != "")
                {
                    if (DLL_UNHIDE != null)
                    {
                        string _new = comboBoxProcesses.SelectedItem.ToString().Substring(0, comboBoxProcesses.SelectedItem.ToString().Length - 4);
                        _module.InjectDLL(_new, DLL_UNHIDE);
                    }
                    else
                    {
                        MessageBox.Show("Please Select A Dll", "Error: No Dll Selected", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else
                {
                    MessageBox.Show("Please Select A Process From The List", "Error: No Process Selected", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Some error rip", "lol", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RestartApplication()
        {
            string executablePath = Application.ExecutablePath;
            Process.Start(executablePath);
            Application.Exit();
        }

        private void metroButton11_Click(object sender, EventArgs e)
        {
            Hide_SourceCode();
        }

        private void metroButton10_Click(object sender, EventArgs e)
        {
            UnHide_SourceCode();
        }
    }
}