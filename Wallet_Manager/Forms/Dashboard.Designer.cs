namespace Wallet_Manager.Forms
{
    partial class Dashboard
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.panel3 = new System.Windows.Forms.Panel();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button_signout = new Guna.UI2.WinForms.Guna2Button();
            this.button_profile = new Guna.UI2.WinForms.Guna2Button();
            this.button_budget = new Guna.UI2.WinForms.Guna2Button();
            this.button_analytics = new Guna.UI2.WinForms.Guna2Button();
            this.button_transaction = new Guna.UI2.WinForms.Guna2Button();
            this.button_wallet = new Guna.UI2.WinForms.Guna2Button();
            this.button_dashboard = new Guna.UI2.WinForms.Guna2Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.displayName = new System.Windows.Forms.Label();
            this.profilePicture = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.pageLabel = new System.Windows.Forms.Label();
            this.display_panel = new System.Windows.Forms.Panel();
            this.guna2AnimateWindow1 = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilePicture)).BeginInit();
            this.display_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(105)))), ((int)(((byte)(233)))));
            this.panel3.Controls.Add(this.guna2Button1);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // guna2Button1
            // 
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            resources.ApplyResources(this.guna2Button1, "guna2Button1");
            this.guna2Button1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(105)))), ((int)(((byte)(233)))));
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button1.Image")));
            this.guna2Button1.ImageSize = new System.Drawing.Size(15, 15);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.button_signout);
            this.panel4.Controls.Add(this.button_profile);
            this.panel4.Controls.Add(this.button_budget);
            this.panel4.Controls.Add(this.button_analytics);
            this.panel4.Controls.Add(this.button_transaction);
            this.panel4.Controls.Add(this.button_wallet);
            this.panel4.Controls.Add(this.button_dashboard);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // button_signout
            // 
            this.button_signout.BorderRadius = 5;
            this.button_signout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_signout.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.button_signout.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.button_signout.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.button_signout.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.button_signout.FillColor = System.Drawing.Color.White;
            resources.ApplyResources(this.button_signout, "button_signout");
            this.button_signout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.button_signout.Image = ((System.Drawing.Image)(resources.GetObject("button_signout.Image")));
            this.button_signout.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.button_signout.ImageOffset = new System.Drawing.Point(20, 0);
            this.button_signout.ImageSize = new System.Drawing.Size(35, 35);
            this.button_signout.Name = "button_signout";
            this.button_signout.PressedColor = System.Drawing.Color.White;
            this.button_signout.Click += new System.EventHandler(this.button_signout_Click);
            // 
            // button_profile
            // 
            this.button_profile.BorderRadius = 5;
            this.button_profile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_profile.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.button_profile.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.button_profile.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.button_profile.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.button_profile.FillColor = System.Drawing.Color.White;
            resources.ApplyResources(this.button_profile, "button_profile");
            this.button_profile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.button_profile.Image = ((System.Drawing.Image)(resources.GetObject("button_profile.Image")));
            this.button_profile.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.button_profile.ImageOffset = new System.Drawing.Point(20, 0);
            this.button_profile.ImageSize = new System.Drawing.Size(35, 35);
            this.button_profile.Name = "button_profile";
            this.button_profile.PressedColor = System.Drawing.Color.White;
            this.button_profile.Click += new System.EventHandler(this.button_profile_Click);
            // 
            // button_budget
            // 
            this.button_budget.BorderRadius = 5;
            this.button_budget.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_budget.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.button_budget.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.button_budget.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.button_budget.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.button_budget.FillColor = System.Drawing.Color.White;
            resources.ApplyResources(this.button_budget, "button_budget");
            this.button_budget.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.button_budget.Image = ((System.Drawing.Image)(resources.GetObject("button_budget.Image")));
            this.button_budget.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.button_budget.ImageOffset = new System.Drawing.Point(20, 0);
            this.button_budget.ImageSize = new System.Drawing.Size(35, 35);
            this.button_budget.Name = "button_budget";
            this.button_budget.PressedColor = System.Drawing.Color.White;
            this.button_budget.Click += new System.EventHandler(this.button_budget_Click);
            // 
            // button_analytics
            // 
            this.button_analytics.BorderRadius = 5;
            this.button_analytics.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_analytics.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.button_analytics.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.button_analytics.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.button_analytics.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.button_analytics.FillColor = System.Drawing.Color.White;
            resources.ApplyResources(this.button_analytics, "button_analytics");
            this.button_analytics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.button_analytics.Image = ((System.Drawing.Image)(resources.GetObject("button_analytics.Image")));
            this.button_analytics.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.button_analytics.ImageOffset = new System.Drawing.Point(20, 0);
            this.button_analytics.ImageSize = new System.Drawing.Size(35, 35);
            this.button_analytics.Name = "button_analytics";
            this.button_analytics.PressedColor = System.Drawing.Color.White;
            this.button_analytics.Click += new System.EventHandler(this.button_insights_Click);
            // 
            // button_transaction
            // 
            this.button_transaction.BorderRadius = 5;
            this.button_transaction.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_transaction.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.button_transaction.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.button_transaction.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.button_transaction.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.button_transaction.FillColor = System.Drawing.Color.White;
            resources.ApplyResources(this.button_transaction, "button_transaction");
            this.button_transaction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.button_transaction.Image = ((System.Drawing.Image)(resources.GetObject("button_transaction.Image")));
            this.button_transaction.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.button_transaction.ImageOffset = new System.Drawing.Point(15, 0);
            this.button_transaction.ImageSize = new System.Drawing.Size(35, 35);
            this.button_transaction.Name = "button_transaction";
            this.button_transaction.PressedColor = System.Drawing.Color.White;
            this.button_transaction.Click += new System.EventHandler(this.button_transaction_Click);
            // 
            // button_wallet
            // 
            this.button_wallet.BorderRadius = 5;
            this.button_wallet.CheckedState.FillColor = System.Drawing.Color.White;
            this.button_wallet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_wallet.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.button_wallet.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.button_wallet.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.button_wallet.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.button_wallet.FillColor = System.Drawing.Color.White;
            resources.ApplyResources(this.button_wallet, "button_wallet");
            this.button_wallet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.button_wallet.Image = ((System.Drawing.Image)(resources.GetObject("button_wallet.Image")));
            this.button_wallet.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.button_wallet.ImageOffset = new System.Drawing.Point(15, 0);
            this.button_wallet.ImageSize = new System.Drawing.Size(35, 35);
            this.button_wallet.Name = "button_wallet";
            this.button_wallet.PressedColor = System.Drawing.Color.White;
            this.button_wallet.Click += new System.EventHandler(this.button_wallet_Click);
            // 
            // button_dashboard
            // 
            this.button_dashboard.BorderRadius = 5;
            this.button_dashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_dashboard.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.button_dashboard.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.button_dashboard.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.button_dashboard.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.button_dashboard.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(105)))), ((int)(((byte)(233)))));
            resources.ApplyResources(this.button_dashboard, "button_dashboard");
            this.button_dashboard.ForeColor = System.Drawing.Color.White;
            this.button_dashboard.Image = ((System.Drawing.Image)(resources.GetObject("button_dashboard.Image")));
            this.button_dashboard.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.button_dashboard.ImageOffset = new System.Drawing.Point(15, 0);
            this.button_dashboard.ImageSize = new System.Drawing.Size(35, 35);
            this.button_dashboard.Name = "button_dashboard";
            this.button_dashboard.PressedColor = System.Drawing.Color.White;
            this.button_dashboard.Click += new System.EventHandler(this.button_dashboard_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.displayName);
            this.panel1.Controls.Add(this.profilePicture);
            this.panel1.Controls.Add(this.pageLabel);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // displayName
            // 
            this.displayName.AutoEllipsis = true;
            resources.ApplyResources(this.displayName, "displayName");
            this.displayName.ForeColor = System.Drawing.Color.Black;
            this.displayName.Name = "displayName";
            this.displayName.Click += new System.EventHandler(this.displayName_Click);
            // 
            // profilePicture
            // 
            resources.ApplyResources(this.profilePicture, "profilePicture");
            this.profilePicture.ImageRotate = 0F;
            this.profilePicture.Name = "profilePicture";
            this.profilePicture.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.profilePicture.TabStop = false;
            this.profilePicture.Click += new System.EventHandler(this.profilePicture_Click);
            // 
            // pageLabel
            // 
            resources.ApplyResources(this.pageLabel, "pageLabel");
            this.pageLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.pageLabel.Name = "pageLabel";
            this.pageLabel.Click += new System.EventHandler(this.pageLabel_Click);
            // 
            // display_panel
            // 
            this.display_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display_panel.Controls.Add(this.panel1);
            this.display_panel.Controls.Add(this.panel4);
            this.display_panel.Controls.Add(this.panel3);
            resources.ApplyResources(this.display_panel, "display_panel");
            this.display_panel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.display_panel.Name = "display_panel";
            this.display_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.display_panel_Paint);
            // 
            // guna2AnimateWindow1
            // 
            this.guna2AnimateWindow1.AnimationType = Guna.UI2.WinForms.Guna2AnimateWindow.AnimateWindowType.AW_VER_POSITIVE;
            this.guna2AnimateWindow1.TargetForm = this;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Name = "label2";
            // 
            // Dashboard
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.display_panel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilePicture)).EndInit();
            this.display_panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel display_panel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label displayName;
        private Guna.UI2.WinForms.Guna2CirclePictureBox profilePicture;
        private System.Windows.Forms.Label pageLabel;
        private System.Windows.Forms.Panel panel4;
        private Guna.UI2.WinForms.Guna2Button button_signout;
        private Guna.UI2.WinForms.Guna2Button button_profile;
        private Guna.UI2.WinForms.Guna2Button button_budget;
        private Guna.UI2.WinForms.Guna2Button button_analytics;
        private Guna.UI2.WinForms.Guna2Button button_transaction;
        private Guna.UI2.WinForms.Guna2Button button_wallet;
        private Guna.UI2.WinForms.Guna2Button button_dashboard;
        private System.Windows.Forms.Panel panel3;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}