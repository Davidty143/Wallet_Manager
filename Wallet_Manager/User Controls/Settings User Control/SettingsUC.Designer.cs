namespace Wallet_Manager.Forms
{
    partial class SettingsUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.editProfileLabel = new Guna.UI2.WinForms.Guna2Button();
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.editProfile1 = new Wallet_Manager.Forms.EditProfile();
            this.editPassword1 = new Wallet_Manager.Forms.EditPassword();
            this.guna2CustomGradientPanel2 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.SelectSettingComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            this.guna2CustomGradientPanel1.SuspendLayout();
            this.guna2CustomGradientPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // editProfileLabel
            // 
            this.editProfileLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.editProfileLabel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.editProfileLabel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.editProfileLabel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.editProfileLabel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.editProfileLabel.FillColor = System.Drawing.Color.White;
            this.editProfileLabel.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editProfileLabel.ForeColor = System.Drawing.Color.Black;
            this.editProfileLabel.Location = new System.Drawing.Point(81, 317);
            this.editProfileLabel.Name = "editProfileLabel";
            this.editProfileLabel.Size = new System.Drawing.Size(238, 43);
            this.editProfileLabel.TabIndex = 2;
            // 
            // guna2CustomGradientPanel1
            // 
            this.guna2CustomGradientPanel1.Controls.Add(this.editProfile1);
            this.guna2CustomGradientPanel1.Controls.Add(this.editPassword1);
            this.guna2CustomGradientPanel1.Location = new System.Drawing.Point(401, 29);
            this.guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            this.guna2CustomGradientPanel1.Size = new System.Drawing.Size(709, 717);
            this.guna2CustomGradientPanel1.TabIndex = 4;
            // 
            // editProfile1
            // 
            this.editProfile1.BackColor = System.Drawing.Color.White;
            this.editProfile1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editProfile1.Location = new System.Drawing.Point(0, 0);
            this.editProfile1.Name = "editProfile1";
            this.editProfile1.Size = new System.Drawing.Size(709, 717);
            this.editProfile1.TabIndex = 1;
            this.editProfile1.Load += new System.EventHandler(this.editProfile1_Load);
            // 
            // editPassword1
            // 
            this.editPassword1.BackColor = System.Drawing.Color.White;
            this.editPassword1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editPassword1.Location = new System.Drawing.Point(0, 0);
            this.editPassword1.Name = "editPassword1";
            this.editPassword1.Size = new System.Drawing.Size(709, 717);
            this.editPassword1.TabIndex = 0;
            // 
            // guna2CustomGradientPanel2
            // 
            this.guna2CustomGradientPanel2.BorderRadius = 5;
            this.guna2CustomGradientPanel2.BorderThickness = 1;
            this.guna2CustomGradientPanel2.Controls.Add(this.guna2CustomGradientPanel1);
            this.guna2CustomGradientPanel2.Controls.Add(this.SelectSettingComboBox);
            this.guna2CustomGradientPanel2.Controls.Add(this.editProfileLabel);
            this.guna2CustomGradientPanel2.Location = new System.Drawing.Point(51, 42);
            this.guna2CustomGradientPanel2.Name = "guna2CustomGradientPanel2";
            this.guna2CustomGradientPanel2.Size = new System.Drawing.Size(1402, 796);
            this.guna2CustomGradientPanel2.TabIndex = 5;
            this.guna2CustomGradientPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2CustomGradientPanel2_Paint);
            // 
            // SelectSettingComboBox
            // 
            this.SelectSettingComboBox.BackColor = System.Drawing.Color.Transparent;
            this.SelectSettingComboBox.BorderRadius = 5;
            this.SelectSettingComboBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.SelectSettingComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.SelectSettingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectSettingComboBox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.SelectSettingComboBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.SelectSettingComboBox.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectSettingComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.SelectSettingComboBox.ItemHeight = 30;
            this.SelectSettingComboBox.Location = new System.Drawing.Point(109, 67);
            this.SelectSettingComboBox.Name = "SelectSettingComboBox";
            this.SelectSettingComboBox.Size = new System.Drawing.Size(228, 36);
            this.SelectSettingComboBox.TabIndex = 4;
            this.SelectSettingComboBox.SelectedIndexChanged += new System.EventHandler(this.guna2ComboBox1_SelectedIndexChanged);
            // 
            // SettingsUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.guna2CustomGradientPanel2);
            this.Name = "SettingsUC";
            this.Size = new System.Drawing.Size(1509, 950);
            this.guna2CustomGradientPanel1.ResumeLayout(false);
            this.guna2CustomGradientPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button editProfileLabel;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private EditProfile editProfile1;
        private EditPassword editPassword1;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel2;
        private Guna.UI2.WinForms.Guna2ComboBox SelectSettingComboBox;
    }
}
