namespace Vedaantees.Shells.Windows
{
    partial class GlobalSettingsEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.grpConnections = new System.Windows.Forms.GroupBox();
            this.txtBusPassword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBusUsername = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRavenDbPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRavenDbUsername = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNeo4jPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNeo4jUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSingleSignOnServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBusQueue = new System.Windows.Forms.TextBox();
            this.lblBus = new System.Windows.Forms.Label();
            this.txtNeo4JAddress = new System.Windows.Forms.TextBox();
            this.lblNeo4j = new System.Windows.Forms.Label();
            this.txtRavenDb = new System.Windows.Forms.TextBox();
            this.lblRavenDb = new System.Windows.Forms.Label();
            this.txtPostgreSql = new System.Windows.Forms.TextBox();
            this.lblPostgreSQL = new System.Windows.Forms.Label();
            this.pnlBottom.SuspendLayout();
            this.grpConnections.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.White;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1072, 40);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "    Global Settings Editor";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.Transparent;
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Controls.Add(this.btnSave);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 359);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1072, 59);
            this.pnlBottom.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnClose.Location = new System.Drawing.Point(970, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 34);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSave.Location = new System.Drawing.Point(874, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 34);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSubmit_Click);
            // 
            // grpConnections
            // 
            this.grpConnections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpConnections.Controls.Add(this.txtBusPassword);
            this.grpConnections.Controls.Add(this.label7);
            this.grpConnections.Controls.Add(this.txtBusUsername);
            this.grpConnections.Controls.Add(this.label8);
            this.grpConnections.Controls.Add(this.txtRavenDbPassword);
            this.grpConnections.Controls.Add(this.label5);
            this.grpConnections.Controls.Add(this.txtRavenDbUsername);
            this.grpConnections.Controls.Add(this.label6);
            this.grpConnections.Controls.Add(this.txtNeo4jPassword);
            this.grpConnections.Controls.Add(this.label4);
            this.grpConnections.Controls.Add(this.txtNeo4jUsername);
            this.grpConnections.Controls.Add(this.label3);
            this.grpConnections.Controls.Add(this.txtSingleSignOnServer);
            this.grpConnections.Controls.Add(this.label2);
            this.grpConnections.Controls.Add(this.txtBusQueue);
            this.grpConnections.Controls.Add(this.lblBus);
            this.grpConnections.Controls.Add(this.txtNeo4JAddress);
            this.grpConnections.Controls.Add(this.lblNeo4j);
            this.grpConnections.Controls.Add(this.txtRavenDb);
            this.grpConnections.Controls.Add(this.lblRavenDb);
            this.grpConnections.Controls.Add(this.txtPostgreSql);
            this.grpConnections.Controls.Add(this.lblPostgreSQL);
            this.grpConnections.Location = new System.Drawing.Point(12, 49);
            this.grpConnections.Name = "grpConnections";
            this.grpConnections.Size = new System.Drawing.Size(1048, 298);
            this.grpConnections.TabIndex = 2;
            this.grpConnections.TabStop = false;
            this.grpConnections.Text = "Connections";
            // 
            // txtBusPassword
            // 
            this.txtBusPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBusPassword.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtBusPassword.Location = new System.Drawing.Point(880, 203);
            this.txtBusPassword.Name = "txtBusPassword";
            this.txtBusPassword.Size = new System.Drawing.Size(154, 22);
            this.txtBusPassword.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 7F);
            this.label7.Location = new System.Drawing.Point(878, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 12);
            this.label7.TabIndex = 20;
            this.label7.Text = "Password";
            // 
            // txtBusUsername
            // 
            this.txtBusUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBusUsername.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtBusUsername.Location = new System.Drawing.Point(709, 203);
            this.txtBusUsername.Name = "txtBusUsername";
            this.txtBusUsername.Size = new System.Drawing.Size(168, 22);
            this.txtBusUsername.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 7F);
            this.label8.Location = new System.Drawing.Point(707, 188);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "Username";
            // 
            // txtRavenDbPassword
            // 
            this.txtRavenDbPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRavenDbPassword.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtRavenDbPassword.Location = new System.Drawing.Point(880, 95);
            this.txtRavenDbPassword.Name = "txtRavenDbPassword";
            this.txtRavenDbPassword.Size = new System.Drawing.Size(154, 22);
            this.txtRavenDbPassword.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 7F);
            this.label5.Location = new System.Drawing.Point(878, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "Password";
            // 
            // txtRavenDbUsername
            // 
            this.txtRavenDbUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRavenDbUsername.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtRavenDbUsername.Location = new System.Drawing.Point(709, 95);
            this.txtRavenDbUsername.Name = "txtRavenDbUsername";
            this.txtRavenDbUsername.Size = new System.Drawing.Size(168, 22);
            this.txtRavenDbUsername.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 7F);
            this.label6.Location = new System.Drawing.Point(707, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "Username";
            // 
            // txtNeo4jPassword
            // 
            this.txtNeo4jPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNeo4jPassword.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtNeo4jPassword.Location = new System.Drawing.Point(880, 149);
            this.txtNeo4jPassword.Name = "txtNeo4jPassword";
            this.txtNeo4jPassword.Size = new System.Drawing.Size(154, 22);
            this.txtNeo4jPassword.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 7F);
            this.label4.Location = new System.Drawing.Point(878, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "Password";
            // 
            // txtNeo4jUsername
            // 
            this.txtNeo4jUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNeo4jUsername.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtNeo4jUsername.Location = new System.Drawing.Point(709, 149);
            this.txtNeo4jUsername.Name = "txtNeo4jUsername";
            this.txtNeo4jUsername.Size = new System.Drawing.Size(168, 22);
            this.txtNeo4jUsername.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 7F);
            this.label3.Location = new System.Drawing.Point(707, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "Username";
            // 
            // txtSingleSignOnServer
            // 
            this.txtSingleSignOnServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSingleSignOnServer.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtSingleSignOnServer.Location = new System.Drawing.Point(12, 258);
            this.txtSingleSignOnServer.Name = "txtSingleSignOnServer";
            this.txtSingleSignOnServer.Size = new System.Drawing.Size(1022, 22);
            this.txtSingleSignOnServer.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 7F);
            this.label2.Location = new System.Drawing.Point(10, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Single Sign-On Server";
            // 
            // txtBusQueue
            // 
            this.txtBusQueue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBusQueue.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtBusQueue.Location = new System.Drawing.Point(12, 203);
            this.txtBusQueue.Name = "txtBusQueue";
            this.txtBusQueue.Size = new System.Drawing.Size(694, 22);
            this.txtBusQueue.TabIndex = 7;
            // 
            // lblBus
            // 
            this.lblBus.AutoSize = true;
            this.lblBus.Font = new System.Drawing.Font("Verdana", 7F);
            this.lblBus.Location = new System.Drawing.Point(10, 188);
            this.lblBus.Name = "lblBus";
            this.lblBus.Size = new System.Drawing.Size(67, 12);
            this.lblBus.TabIndex = 6;
            this.lblBus.Text = "Bus-Queue";
            // 
            // txtNeo4JAddress
            // 
            this.txtNeo4JAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNeo4JAddress.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtNeo4JAddress.Location = new System.Drawing.Point(12, 149);
            this.txtNeo4JAddress.Name = "txtNeo4JAddress";
            this.txtNeo4JAddress.Size = new System.Drawing.Size(694, 22);
            this.txtNeo4JAddress.TabIndex = 5;
            // 
            // lblNeo4j
            // 
            this.lblNeo4j.AutoSize = true;
            this.lblNeo4j.Font = new System.Drawing.Font("Verdana", 7F);
            this.lblNeo4j.Location = new System.Drawing.Point(10, 134);
            this.lblNeo4j.Name = "lblNeo4j";
            this.lblNeo4j.Size = new System.Drawing.Size(38, 12);
            this.lblNeo4j.TabIndex = 4;
            this.lblNeo4j.Text = "Neo4J";
            // 
            // txtRavenDb
            // 
            this.txtRavenDb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRavenDb.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtRavenDb.Location = new System.Drawing.Point(12, 95);
            this.txtRavenDb.Name = "txtRavenDb";
            this.txtRavenDb.Size = new System.Drawing.Size(694, 22);
            this.txtRavenDb.TabIndex = 3;
            // 
            // lblRavenDb
            // 
            this.lblRavenDb.AutoSize = true;
            this.lblRavenDb.Font = new System.Drawing.Font("Verdana", 7F);
            this.lblRavenDb.Location = new System.Drawing.Point(10, 80);
            this.lblRavenDb.Name = "lblRavenDb";
            this.lblRavenDb.Size = new System.Drawing.Size(55, 12);
            this.lblRavenDb.TabIndex = 2;
            this.lblRavenDb.Text = "RavenDb";
            // 
            // txtPostgreSql
            // 
            this.txtPostgreSql.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPostgreSql.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtPostgreSql.Location = new System.Drawing.Point(12, 44);
            this.txtPostgreSql.Name = "txtPostgreSql";
            this.txtPostgreSql.Size = new System.Drawing.Size(1022, 22);
            this.txtPostgreSql.TabIndex = 1;
            // 
            // lblPostgreSQL
            // 
            this.lblPostgreSQL.AutoSize = true;
            this.lblPostgreSQL.Font = new System.Drawing.Font("Verdana", 7F);
            this.lblPostgreSQL.Location = new System.Drawing.Point(10, 29);
            this.lblPostgreSQL.Name = "lblPostgreSQL";
            this.lblPostgreSQL.Size = new System.Drawing.Size(69, 12);
            this.lblPostgreSQL.TabIndex = 0;
            this.lblPostgreSQL.Text = "PostgreSQL";
            // 
            // GlobalSettingsEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1072, 418);
            this.Controls.Add(this.grpConnections);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GlobalSettingsEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Global Settings Editor";
            this.Load += new System.EventHandler(this.GlobalSettingsEditor_Load);
            this.pnlBottom.ResumeLayout(false);
            this.grpConnections.ResumeLayout(false);
            this.grpConnections.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox grpConnections;
        private System.Windows.Forms.Label lblPostgreSQL;
        private System.Windows.Forms.TextBox txtBusQueue;
        private System.Windows.Forms.Label lblBus;
        private System.Windows.Forms.TextBox txtNeo4JAddress;
        private System.Windows.Forms.Label lblNeo4j;
        private System.Windows.Forms.TextBox txtRavenDb;
        private System.Windows.Forms.Label lblRavenDb;
        private System.Windows.Forms.TextBox txtPostgreSql;
        private System.Windows.Forms.TextBox txtSingleSignOnServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRavenDbPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRavenDbUsername;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNeo4jPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNeo4jUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBusPassword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBusUsername;
        private System.Windows.Forms.Label label8;
    }
}