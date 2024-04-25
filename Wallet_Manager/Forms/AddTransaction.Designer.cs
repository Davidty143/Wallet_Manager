namespace Wallet_Manager.Forms
{
    partial class AddTransaction
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.guna2GradientButton3 = new Guna.UI2.WinForms.Guna2GradientButton();
            this.guna2GradientButton2 = new Guna.UI2.WinForms.Guna2GradientButton();
            this.guna2GradientButton1 = new Guna.UI2.WinForms.Guna2GradientButton();
            this.addIncomeUC1 = new Wallet_Manager.Forms.AddIncomeUC();
            this.addExpenseUC1 = new Wallet_Manager.Forms.AddExpenseUC();
            this.addTransferUC1 = new Wallet_Manager.Forms.AddTransferUC();
            this.panel1.SuspendLayout();
            this.guna2CustomGradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.addIncomeUC1);
            this.panel1.Controls.Add(this.addExpenseUC1);
            this.panel1.Controls.Add(this.addTransferUC1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 92);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(601, 534);
            this.panel1.TabIndex = 7;
            // 
            // guna2CustomGradientPanel1
            // 
            this.guna2CustomGradientPanel1.Controls.Add(this.guna2GradientButton3);
            this.guna2CustomGradientPanel1.Controls.Add(this.guna2GradientButton2);
            this.guna2CustomGradientPanel1.Controls.Add(this.guna2GradientButton1);
            this.guna2CustomGradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2CustomGradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            this.guna2CustomGradientPanel1.Size = new System.Drawing.Size(601, 92);
            this.guna2CustomGradientPanel1.TabIndex = 8;
            this.guna2CustomGradientPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2CustomGradientPanel1_Paint_1);
            // 
            // guna2GradientButton3
            // 
            this.guna2GradientButton3.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2GradientButton3.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2GradientButton3.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2GradientButton3.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2GradientButton3.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2GradientButton3.FillColor = System.Drawing.Color.MediumPurple;
            this.guna2GradientButton3.FillColor2 = System.Drawing.Color.MediumOrchid;
            this.guna2GradientButton3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2GradientButton3.ForeColor = System.Drawing.Color.White;
            this.guna2GradientButton3.Location = new System.Drawing.Point(412, 40);
            this.guna2GradientButton3.Name = "guna2GradientButton3";
            this.guna2GradientButton3.Size = new System.Drawing.Size(180, 45);
            this.guna2GradientButton3.TabIndex = 9;
            this.guna2GradientButton3.Text = "Transfer";
            this.guna2GradientButton3.Click += new System.EventHandler(this.guna2GradientButton3_Click_1);
            // 
            // guna2GradientButton2
            // 
            this.guna2GradientButton2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2GradientButton2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2GradientButton2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2GradientButton2.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2GradientButton2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2GradientButton2.FillColor = System.Drawing.Color.MediumPurple;
            this.guna2GradientButton2.FillColor2 = System.Drawing.Color.MediumOrchid;
            this.guna2GradientButton2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2GradientButton2.ForeColor = System.Drawing.Color.White;
            this.guna2GradientButton2.Location = new System.Drawing.Point(214, 41);
            this.guna2GradientButton2.Name = "guna2GradientButton2";
            this.guna2GradientButton2.Size = new System.Drawing.Size(180, 45);
            this.guna2GradientButton2.TabIndex = 8;
            this.guna2GradientButton2.Text = "Expense";
            this.guna2GradientButton2.Click += new System.EventHandler(this.guna2GradientButton2_Click_1);
            // 
            // guna2GradientButton1
            // 
            this.guna2GradientButton1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2GradientButton1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2GradientButton1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2GradientButton1.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2GradientButton1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2GradientButton1.FillColor = System.Drawing.Color.MediumPurple;
            this.guna2GradientButton1.FillColor2 = System.Drawing.Color.MediumOrchid;
            this.guna2GradientButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2GradientButton1.ForeColor = System.Drawing.Color.White;
            this.guna2GradientButton1.Location = new System.Drawing.Point(15, 41);
            this.guna2GradientButton1.Name = "guna2GradientButton1";
            this.guna2GradientButton1.Size = new System.Drawing.Size(180, 45);
            this.guna2GradientButton1.TabIndex = 7;
            this.guna2GradientButton1.Text = "Income";
            this.guna2GradientButton1.Click += new System.EventHandler(this.guna2GradientButton1_Click_1);
            // 
            // addIncomeUC1
            // 
            this.addIncomeUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addIncomeUC1.Location = new System.Drawing.Point(0, 0);
            this.addIncomeUC1.Name = "addIncomeUC1";
            this.addIncomeUC1.Size = new System.Drawing.Size(601, 534);
            this.addIncomeUC1.TabIndex = 2;
            this.addIncomeUC1.Load += new System.EventHandler(this.addIncomeUC1_Load_2);
            // 
            // addExpenseUC1
            // 
            this.addExpenseUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addExpenseUC1.Location = new System.Drawing.Point(0, 0);
            this.addExpenseUC1.Name = "addExpenseUC1";
            this.addExpenseUC1.Size = new System.Drawing.Size(601, 534);
            this.addExpenseUC1.TabIndex = 1;
            // 
            // addTransferUC1
            // 
            this.addTransferUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addTransferUC1.Location = new System.Drawing.Point(0, 0);
            this.addTransferUC1.Name = "addTransferUC1";
            this.addTransferUC1.Size = new System.Drawing.Size(601, 534);
            this.addTransferUC1.TabIndex = 0;
            // 
            // AddTransaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(601, 626);
            this.Controls.Add(this.guna2CustomGradientPanel1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddTransaction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddTransaction";
            this.panel1.ResumeLayout(false);
            this.guna2CustomGradientPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private AddIncomeUC addIncomeUC1;
        private AddExpenseUC addExpenseUC1;
        private AddTransferUC addTransferUC1;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private Guna.UI2.WinForms.Guna2GradientButton guna2GradientButton3;
        private Guna.UI2.WinForms.Guna2GradientButton guna2GradientButton2;
        private Guna.UI2.WinForms.Guna2GradientButton guna2GradientButton1;
    }
}