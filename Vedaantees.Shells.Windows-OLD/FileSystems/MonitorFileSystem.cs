using System;
using System.IO;
using System.Threading;

namespace Vedaantees.Shells.Windows.FileSystems
{
    public class MonitorFileSystem
    {
        private readonly string _fileName;
        private readonly Action _action;
        private DateTime _lastWriteTime = DateTime.Now;
        
        public MonitorFileSystem(string fileName, Action action)
        {
            _fileName = fileName;
            _action = action;
        }

        public void Start()
        {
            var fileWatcher = new FileSystemWatcher(_fileName);
            fileWatcher.Changed += File_Changed;
            fileWatcher.EnableRaisingEvents = true;
        }

        public void File_Changed(object sender, FileSystemEventArgs e)
        {
            if (DateTime.Now.Subtract(_lastWriteTime).Seconds >= 4)
            {
                _lastWriteTime = DateTime.Now;
                Call();
            }
        }

        public void Call()
        {
            Thread.Sleep(1000);
            _action();
        }
    }
}