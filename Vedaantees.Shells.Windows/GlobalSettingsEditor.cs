using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Vedaantees.Framework.Configurations;

namespace Vedaantees.Shells.Windows
{
    public partial class GlobalSettingsEditor : Form
    {
        public GlobalSettingsEditor()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            var globalSettingsFilePath = ConfigurationManager.AppSettings["global-settings"];
            var settingsFile = File.ReadAllText(globalSettingsFilePath);
            var configuration = JsonConvert.DeserializeObject<GlobalConfiguration>(settingsFile).Vedaantees;

            configuration.SqlStore.ConnectionString = txtPostgreSql.Text;

            configuration.GraphStore.Url = txtNeo4JAddress.Text;
            configuration.GraphStore.Username = txtNeo4jUsername.Text;
            configuration.GraphStore.Password = txtNeo4jPassword.Text;

            configuration.DocumentStore.Url = txtRavenDb.Text;
            configuration.DocumentStore.Username = txtRavenDbUsername.Text;
            configuration.DocumentStore.Password = txtRavenDbPassword.Text;

            configuration.Bus.Endpoint = txtBusQueue.Text;
            configuration.Bus.Username = txtBusUsername.Text;
            configuration.Bus.Password = txtBusPassword.Text;
            
            configuration.SingleSignOnServer =  txtSingleSignOnServer.Text;

            File.WriteAllText(globalSettingsFilePath,JsonConvert.SerializeObject(new GlobalConfiguration { Vedaantees = configuration }, Formatting.Indented));
        }

        private async void GlobalSettingsEditor_Load(object sender, EventArgs e)
        {
            var configuration = await Task.Run(() =>
            {
                var settingsFile = File.ReadAllText(ConfigurationManager.AppSettings["global-settings"]);
                return JsonConvert.DeserializeObject<GlobalConfiguration>(settingsFile).Vedaantees;
            });

            txtPostgreSql.Text = configuration.SqlStore.ConnectionString;

            txtNeo4JAddress.Text = configuration.GraphStore.Url;
            txtNeo4jUsername.Text = configuration.GraphStore.Username;
            txtNeo4jPassword.Text = configuration.GraphStore.Password;

            txtRavenDb.Text = configuration.DocumentStore.Url;
            txtRavenDbUsername.Text = configuration.DocumentStore.Username;
            txtRavenDbPassword.Text = configuration.DocumentStore.Password;

            txtBusQueue.Text = configuration.Bus.Endpoint;
            txtBusUsername.Text = configuration.Bus.Username;
            txtBusPassword.Text = configuration.Bus.Password;

            txtSingleSignOnServer.Text = configuration.SingleSignOnServer;
        }
    }

    public class GlobalConfiguration
    {
        public ProviderConfiguration Vedaantees { get; set; }
    }
}
