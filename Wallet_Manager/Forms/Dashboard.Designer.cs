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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.display_panel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button_goals = new Guna.UI2.WinForms.Guna2Button();
            this.button_budget = new Guna.UI2.WinForms.Guna2Button();
            this.button_analytics = new Guna.UI2.WinForms.Guna2Button();
            this.button_transaction = new Guna.UI2.WinForms.Guna2Button();
            this.button_wallet = new Guna.UI2.WinForms.Guna2Button();
            this.button_dashboard = new Guna.UI2.WinForms.Guna2Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.transactionHistory1 = new Wallet_Manager.Forms.TransactionHistory();
            this.display_panel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // display_panel
            // 
            this.display_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.display_panel.Controls.Add(this.transactionHistory1);
            this.display_panel.Controls.Add(this.panel1);
            this.display_panel.Controls.Add(this.panel4);
            this.display_panel.Controls.Add(this.panel3);
            this.display_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.display_panel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.display_panel.Location = new System.Drawing.Point(0, 0);
            this.display_panel.Margin = new System.Windows.Forms.Padding(4);
            this.display_panel.Name = "display_panel";
            this.display_panel.Size = new System.Drawing.Size(1924, 1020);
            this.display_panel.TabIndex = 5;
            this.display_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.display_panel_Paint);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label10);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(413, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1509, 135);
            this.panel1.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Lucida Sans", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.label10.Location = new System.Drawing.Point(26, 70);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(279, 44);
            this.label10.TabIndex = 0;
            this.label10.Text = "Dashboard";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.button_goals);
            this.panel4.Controls.Add(this.button_budget);
            this.panel4.Controls.Add(this.button_analytics);
            this.panel4.Controls.Add(this.button_transaction);
            this.panel4.Controls.Add(this.button_wallet);
            this.panel4.Controls.Add(this.button_dashboard);
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Controls.Add(this.pictureBox2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 27);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(413, 991);
            this.panel4.TabIndex = 6;
            // 
            // button_goals
            // 
            this.button_goals.BorderRadius = 5;
            this.button_goals.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_goals.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.button_goals.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.button_goals.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.button_goals.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.button_goals.FillColor = System.Drawing.Color.White;
            this.button_goals.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_goals.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.button_goals.Image = global::Wallet_Manager.Properties.Resources.button_goals_inactive;
            this.button_goals.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.button_goals.ImageOffset = new System.Drawing.Point(20, 0);
            this.button_goals.ImageSize = new System.Drawing.Size(40, 40);
            this.button_goals.Location = new System.Drawing.Point(77, 604);
            this.button_goals.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_goals.Name = "button_goals";
            this.button_goals.PressedColor = System.Drawing.Color.White;
            this.button_goals.Size = new System.Drawing.Size(261, 57);
            this.button_goals.TabIndex = 29;
            this.button_goals.Text = "    Goals   ";
            this.button_goals.Click += new System.EventHandler(this.button_goals_Click);
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
            this.button_budget.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_budget.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.button_budget.Image = global::Wallet_Manager.Properties.Resources.button_budget_inactive;
            this.button_budget.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.button_budget.ImageOffset = new System.Drawing.Point(20, 0);
            this.button_budget.ImageSize = new System.Drawing.Size(35, 35);
            this.button_budget.Location = new System.Drawing.Point(77, 534);
            this.button_budget.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_budget.Name = "button_budget";
            this.button_budget.PressedColor = System.Drawing.Color.White;
            this.button_budget.Size = new System.Drawing.Size(261, 57);
            this.button_budget.TabIndex = 27;
            this.button_budget.Text = "     Budget  ";
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
            this.button_analytics.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_analytics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.button_analytics.Image = global::Wallet_Manager.Properties.Resources.button_analytics_inactive;
            this.button_analytics.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.button_analytics.ImageOffset = new System.Drawing.Point(20, 0);
            this.button_analytics.ImageSize = new System.Drawing.Size(35, 35);
            this.button_analytics.Location = new System.Drawing.Point(77, 461);
            this.button_analytics.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_analytics.Name = "button_analytics";
            this.button_analytics.PressedColor = System.Drawing.Color.White;
            this.button_analytics.Size = new System.Drawing.Size(261, 57);
            this.button_analytics.TabIndex = 25;
            this.button_analytics.Text = "      Insights  ";
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
            this.button_transaction.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_transaction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.button_transaction.Image = global::Wallet_Manager.Properties.Resources.button_transaction_inactive;
            this.button_transaction.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.button_transaction.ImageOffset = new System.Drawing.Point(15, 0);
            this.button_transaction.ImageSize = new System.Drawing.Size(35, 35);
            this.button_transaction.Location = new System.Drawing.Point(77, 390);
            this.button_transaction.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_transaction.Name = "button_transaction";
            this.button_transaction.PressedColor = System.Drawing.Color.White;
            this.button_transaction.Size = new System.Drawing.Size(261, 57);
            this.button_transaction.TabIndex = 23;
            this.button_transaction.Text = "          Transaction  ";
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
            this.button_wallet.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_wallet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.button_wallet.Image = global::Wallet_Manager.Properties.Resources.button_wallet_inactive;
            this.button_wallet.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.button_wallet.ImageOffset = new System.Drawing.Point(15, 0);
            this.button_wallet.ImageSize = new System.Drawing.Size(35, 35);
            this.button_wallet.Location = new System.Drawing.Point(77, 319);
            this.button_wallet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_wallet.Name = "button_wallet";
            this.button_wallet.PressedColor = System.Drawing.Color.White;
            this.button_wallet.Size = new System.Drawing.Size(261, 57);
            this.button_wallet.TabIndex = 21;
            this.button_wallet.Text = "  Wallet  ";
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
            this.button_dashboard.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_dashboard.ForeColor = System.Drawing.Color.White;
            this.button_dashboard.Image = global::Wallet_Manager.Properties.Resources.button_dashboard_active;
            this.button_dashboard.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.button_dashboard.ImageOffset = new System.Drawing.Point(15, 0);
            this.button_dashboard.ImageSize = new System.Drawing.Size(35, 35);
            this.button_dashboard.Location = new System.Drawing.Point(77, 248);
            this.button_dashboard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_dashboard.Name = "button_dashboard";
            this.button_dashboard.PressedColor = System.Drawing.Color.White;
            this.button_dashboard.Size = new System.Drawing.Size(261, 57);
            this.button_dashboard.TabIndex = 19;
            this.button_dashboard.Text = "      Dashboard";
            this.button_dashboard.Click += new System.EventHandler(this.button_dashboard_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(248, 49);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(112, 108);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(36, 70);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(205, 44);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(105)))), ((int)(((byte)(233)))));
            this.panel3.Controls.Add(this.guna2Button1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1922, 27);
            this.panel3.TabIndex = 5;
            // 
            // guna2Button1
            // 
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.guna2Button1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(105)))), ((int)(((byte)(233)))));
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Image = global::Wallet_Manager.Properties.Resources.exit_3;
            this.guna2Button1.ImageSize = new System.Drawing.Size(15, 15);
            this.guna2Button1.Location = new System.Drawing.Point(1872, 0);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(50, 27);
            this.guna2Button1.TabIndex = 0;
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // transactionHistory1
            // 
            this.transactionHistory1.BackColor = System.Drawing.Color.White;
            this.transactionHistory1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transactionHistory1.Location = new System.Drawing.Point(413, 162);
            this.transactionHistory1.Name = "transactionHistory1";
            this.transactionHistory1.Size = new System.Drawing.Size(1509, 856);
            this.transactionHistory1.TabIndex = 8;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1020);
            this.Controls.Add(this.display_panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wallet Manager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.display_panel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel display_panel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2Button button_goals;
        private Guna.UI2.WinForms.Guna2Button button_budget;
        private Guna.UI2.WinForms.Guna2Button button_analytics;
        private Guna.UI2.WinForms.Guna2Button button_transaction;
        private Guna.UI2.WinForms.Guna2Button button_wallet;
        private Guna.UI2.WinForms.Guna2Button button_dashboard;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private TransactionHistory transactionHistory1;
    }
}