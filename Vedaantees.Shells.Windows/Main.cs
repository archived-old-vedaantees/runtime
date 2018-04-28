using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vedaantees.Shells.Windows.Editors;
using Vedaantees.Shells.Windows.Extensions;
using Vedaantees.Shells.Windows.Messages;
using Vedaantees.Shells.Windows.Messaging;

namespace Vedaantees.Shells.Windows
{   
    public partial class Main : Form, 
                                ISubscriber<AppBusyStart>,
                                ISubscriber<AppBusyEnd>
    {
        private readonly MainFormService _mainFormService;
        
        public Main()
        {
            _marker = 0;
            _mainFormService = new MainFormService();
            InitializeComponent();
            var mdiClient = Controls.OfType<MdiClient>().First();
            mdiClient.BackColor = Color.WhiteSmoke;
            this.SetBevel(false);
            
        }
        
        private async void Main_Load(object sender, EventArgs e)
        {
            await Task.Run(() => { MessagingEngine.Subscribe(this); })
                      .ContinueWith(result => { _mainFormService.LoadFileMonitor(); });
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void BtnRestart_Click(object sender, EventArgs e)
        {
            _mainFormService.StartOrRestartServer();
        }

        private void GlobalSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var editor = new GlobalSettingsEditor();
            editor.ShowDialog();
        }


        private int _marker;
        public void Execute(AppBusyStart message)
        {
            _marker++;
            Invoke((Action) (() => TaskProgress.MarqueeAnimationSpeed = 30));
            
        }

        public void Execute(AppBusyEnd message)
        {
            _marker--;

            if (_marker==0)
                Invoke((Action)(() => TaskProgress.MarqueeAnimationSpeed = 0));
        }

        private void BtnKill_Click(object sender, EventArgs e)
        {
            _mainFormService.StopServers();
        }
    }
}
