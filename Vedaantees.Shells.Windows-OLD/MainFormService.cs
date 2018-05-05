﻿using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Vedaantees.Shells.Windows.Configurations;
using Vedaantees.Shells.Windows.FileSystems;

namespace Vedaantees.Shells.Windows
{
    public class MainFormService
    {
        private ShellConfiguration _shellConfiguration;
        
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

        public void StopServers()
        {
            Kill("dotnet");
        }

        private void Kill(string name)
        {
            var process = Process.GetProcessesByName(name);
            foreach (var p in process)
                p.Kill();
        }

        public async void LoadFileMonitor()
        {
            await Task.Run(() => new ShellConfiguration
                                        {
                                            ModulesFolder = ConfigurationManager.AppSettings.Get("modules-folder"),
                                            RuntimeDeploymentFolder = ConfigurationManager.AppSettings.Get("runtime-deployment-folder"),
                                            HostApi = ConfigurationManager.AppSettings.Get("host-api-file"),
                                            SsoApi = ConfigurationManager.AppSettings.Get("sso-api-file"),
                                            FunctionsApi = ConfigurationManager.AppSettings.Get("functions-api-file"),
                                            TasksApi = ConfigurationManager.AppSettings.Get("tasks-api-file")
                                        })
                                        .ContinueWith(taskConfigurationResult =>
                                        {
                                            _shellConfiguration = taskConfigurationResult.Result;
                                            var directoryMonitor = new MonitorFileSystem(_shellConfiguration.RuntimeDeploymentFolder, StartOrRestartServer);
                                            directoryMonitor.Start();
                                        })
                                        .ContinueWith(result => StartOrRestartServer());
        }
    }
}