using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vedaantees.Shells.Windows
{
    public partial class Main : Form
    {
        private MonitorFileSystem _directoryMonitor;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadFileMonitor();
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void LoadFileMonitor()
        {
            TaskProgress.MarqueeAnimationSpeed = 30;

            await Task.Run(() => new ShellConfiguration
                           {
                                ModulesFolder           = ConfigurationManager.AppSettings.Get("modules-folder"),
                                RuntimeDeploymentFolder = ConfigurationManager.AppSettings.Get("runtime-deployment-folder"),
                                HostApi                 = ConfigurationManager.AppSettings.Get("host-api-file"),
                                SsoApi                  = ConfigurationManager.AppSettings.Get("sso-api-file"),
                                FunctionsApi            = ConfigurationManager.AppSettings.Get("functions-api-file"),
                                TasksApi                = ConfigurationManager.AppSettings.Get("tasks-api-file")
                           })
                      .ContinueWith(taskConfigurationResult => 
                            {
                                var shellConfiguration = taskConfigurationResult.Result;
                                _directoryMonitor = new MonitorFileSystem(shellConfiguration.RuntimeDeploymentFolder, StartOrRestartServer);
                            });

            TaskProgress.MarqueeAnimationSpeed = 0;
        }

        public void StartOrRestartServer()
        {
            Kill("dotnet");
            Thread.Sleep(1000);

            foreach (var file in Directory.GetFiles(DeploymentFolder))
                File.Copy(file, Path.Combine(ModulesFolder, Path.GetFileName(file)), true);
        }

        private static void Kill(string name)
        {
            var process = Process.GetProcessesByName(name);

            foreach (var p in process)
                p.Kill();
        }
    }
}
