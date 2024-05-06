namespace Wallet_Manager.Forms
{
    partial class EditPassword
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
            this.updatePass = new Guna.UI2.WinForms.Guna2Button();
            this.label7 = new System.Windows.Forms.Label();
            this.confirmPass = new Guna.UI2.WinForms.Guna2TextBox();
            this.newPass = new Guna.UI2.WinForms.Guna2TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.currPass = new Guna.UI2.WinForms.Guna2TextBox();
            this.showPassCheckBox = new Guna.UI2.WinForms.Guna2CheckBox();
            this.showPassCheckBox2 = new Guna.UI2.WinForms.Guna2CheckBox();
            this.SuspendLayout();
            // 
            // updatePass
            // 
            this.updatePass.BackColor = System.Drawing.Color.Transparent;
            this.updatePass.BorderRadius = 5;
            this.updatePass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.updatePass.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.updatePass.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.updatePass.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.updatePass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.updatePass.FillColor = System.Drawing.Color.MediumSlateBlue;
            this.updatePass.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updatePass.ForeColor = System.Drawing.Color.White;
            this.updatePass.Location = new System.Drawing.Point(117, 483);
            this.updatePass.Name = "updatePass";
            this.updatePass.Size = new System.Drawing.Size(477, 57);
            this.updatePass.TabIndex = 81;
            this.updatePass.Text = "Change Password";
            this.updatePass.Click += new System.EventHandler(this.updatePass_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(112, 317);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(176, 28);
            this.label7.TabIndex = 80;
            this.label7.Text = "Confirm Password";
            // 
            // confirmPass
            // 
            this.confirmPass.AutoSize = true;
            this.confirmPass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.confirmPass.BorderRadius = 5;
            this.confirmPass.BorderThickness = 2;
            this.confirmPass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.confirmPass.DefaultText = "";
            this.confirmPass.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.confirmPass.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.confirmPass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.confirmPass.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.confirmPass.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.confirmPass.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmPass.ForeColor = System.Drawing.Color.Black;
            this.confirmPass.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.confirmPass.Location = new System.Drawing.Point(117, 360);
            this.confirmPass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.confirmPass.Name = "confirmPass";
            this.confirmPass.PasswordChar = '\0';
            this.confirmPass.PlaceholderText = "";
            this.confirmPass.SelectedText = "";
            this.confirmPass.Size = new System.Drawing.Size(477, 57);
            this.confirmPass.TabIndex = 79;
            this.confirmPass.TextChanged += new System.EventHandler(this.confirmPass_TextChanged);
            // 
            // newPass
            // 
            this.newPass.AutoSize = true;
            this.newPass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.newPass.BorderRadius = 5;
            this.newPass.BorderThickness = 2;
            this.newPass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.newPass.DefaultText = "";
            this.newPass.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.newPass.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.newPass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.newPass.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.newPass.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.newPass.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newPass.ForeColor = System.Drawing.Color.Black;
            this.newPass.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.newPass.Location = new System.Drawing.Point(117, 230);
            this.newPass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.newPass.Name = "newPass";
            this.newPass.PasswordChar = '\0';
            this.newPass.PlaceholderText = "";
            this.newPass.SelectedText = "";
            this.newPass.Size = new System.Drawing.Size(477, 57);
            this.newPass.TabIndex = 78;
            this.newPass.TextChanged += new System.EventHandler(this.newPass_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(112, 183);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 28);
            this.label2.TabIndex = 77;
            this.label2.Text = "New Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(112, 37);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 28);
            this.label3.TabIndex = 76;
            this.label3.Text = "Current Password";
            // 
            // currPass
            // 
            this.currPass.AutoSize = true;
            this.currPass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.currPass.BorderRadius = 5;
            this.currPass.BorderThickness = 2;
            this.currPass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.currPass.DefaultText = "";
            this.currPass.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.currPass.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.currPass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.currPass.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.currPass.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.currPass.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currPass.ForeColor = System.Drawing.Color.Black;
            this.currPass.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.currPass.Location = new System.Drawing.Point(117, 85);
            this.currPass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.currPass.Name = "currPass";
            this.currPass.PasswordChar = '\0';
            this.currPass.PlaceholderText = "";
            this.currPass.SelectedText = "";
            this.currPass.Size = new System.Drawing.Size(477, 57);
            this.currPass.TabIndex = 75;
            this.currPass.TextChanged += new System.EventHandler(this.currPass_TextChanged);
            // 
            // showPassCheckBox
            // 
            this.showPassCheckBox.AutoSize = true;
            this.showPassCheckBox.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.showPassCheckBox.CheckedState.BorderRadius = 0;
            this.showPassCheckBox.CheckedState.BorderThickness = 0;
            this.showPassCheckBox.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.showPassCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.showPassCheckBox.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showPassCheckBox.ForeColor = System.Drawing.Color.DimGray;
            this.showPassCheckBox.Location = new System.Drawing.Point(460, 435);
            this.showPassCheckBox.Name = "showPassCheckBox";
            this.showPassCheckBox.Size = new System.Drawing.Size(125, 21);
            this.showPassCheckBox.TabIndex = 82;
            this.showPassCheckBox.Text = "Show Password";
            this.showPassCheckBox.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.showPassCheckBox.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.showPassCheckBox.UncheckedState.BorderRadius = 0;
            this.showPassCheckBox.UncheckedState.BorderThickness = 0;
            this.showPassCheckBox.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.showPassCheckBox.CheckedChanged += new System.EventHandler(this.showPassCheckBox_CheckedChanged);
            // 
            // showPassCheckBox2
            // 
            this.showPassCheckBox2.AutoSize = true;
            this.showPassCheckBox2.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.showPassCheckBox2.CheckedState.BorderRadius = 0;
            this.showPassCheckBox2.CheckedState.BorderThickness = 0;
            this.showPassCheckBox2.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.showPassCheckBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.showPassCheckBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showPassCheckBox2.ForeColor = System.Drawing.Color.DimGray;
            this.showPassCheckBox2.Location = new System.Drawing.Point(460, 159);
            this.showPassCheckBox2.Name = "showPassCheckBox2";
            this.showPassCheckBox2.Size = new System.Drawing.Size(125, 21);
            this.showPassCheckBox2.TabIndex = 83;
            this.showPassCheckBox2.Text = "Show Password";
            this.showPassCheckBox2.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.showPassCheckBox2.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.showPassCheckBox2.UncheckedState.BorderRadius = 0;
            this.showPassCheckBox2.UncheckedState.BorderThickness = 0;
            this.showPassCheckBox2.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.showPassCheckBox2.CheckedChanged += new System.EventHandler(this.showPassCheckBox2_CheckedChanged);
            // 
            // EditPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.showPassCheckBox2);
            this.Controls.Add(this.showPassCheckBox);
            this.Controls.Add(this.updatePass);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.confirmPass);
            this.Controls.Add(this.newPass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.currPass);
            this.Name = "EditPassword";
            this.Size = new System.Drawing.Size(709, 717);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button updatePass;
        private System.Windows.Forms.Label label7;
        private Guna.UI2.WinForms.Guna2TextBox confirmPass;
        private Guna.UI2.WinForms.Guna2TextBox newPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2TextBox currPass;
        private Guna.UI2.WinForms.Guna2CheckBox showPassCheckBox;
        private Guna.UI2.WinForms.Guna2CheckBox showPassCheckBox2;
    }
}
