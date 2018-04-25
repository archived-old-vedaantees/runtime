using System;
using System.Collections.Generic;
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
        private ShellConfiguration _shellConfiguration;
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
                            _shellConfiguration = taskConfigurationResult.Result;
                            _directoryMonitor = new MonitorFileSystem(_shellConfiguration.RuntimeDeploymentFolder, StartOrRestartServer);
                       })
                      .ContinueWith(result=> StartOrRestartServer());

            TaskProgress.MarqueeAnimationSpeed = 0;
        }

        public void StartOrRestartServer()
        {
            Kill("dotnet");

            foreach (var file in Directory.GetFiles(_shellConfiguration.RuntimeDeploymentFolder))
                File.Copy(file, Path.Combine(_shellConfiguration.ModulesFolder, Path.GetFileName(file)), true);
            
            var servers = new List<string>
                            {
                                _shellConfiguration.SsoApi,
                                _shellConfiguration.FunctionsApi,
                                _shellConfiguration.TasksApi,
                                _shellConfiguration.HostApi
                            };

            foreach (var server in servers)
            {
                if (string.IsNullOrEmpty(server)) continue;

                Thread.Sleep(700);
                var p = new Process
                {
                    StartInfo =
                    {
                        FileName = "cmd.exe",
                        Arguments = $@"/C dotnet {server} -v",
                        Verb = "runas",
                        LoadUserProfile = true,
                        UseShellExecute = true,
                        WindowStyle = ProcessWindowStyle.Minimized,
                        WorkingDirectory = new FileInfo(server).DirectoryName
                    }
                };
                p.Start();
                
            }
        }

        private static void Kill(string name)
        {
            var process = Process.GetProcessesByName(name);

            foreach (var p in process)
                p.Kill();
        }
    }
}
