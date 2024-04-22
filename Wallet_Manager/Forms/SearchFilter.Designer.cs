namespace Wallet_Manager.Forms
{
    partial class SearchFilter
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.endDatePicker = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.startDatePicker = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.walletCategoryComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            this.walletComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            this.categoryComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            this.transactionTypeComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(139, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 28);
            this.label1.TabIndex = 6;
            this.label1.Text = "Select Transaction Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(139, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 28);
            this.label2.TabIndex = 7;
            this.label2.Text = "Select Category";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(139, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 28);
            this.label3.TabIndex = 8;
            this.label3.Text = "Select Wallet";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(139, 259);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(216, 28);
            this.label4.TabIndex = 9;
            this.label4.Text = "Select Wallet Category";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(139, 340);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 28);
            this.label5.TabIndex = 10;
            this.label5.Text = "Select Start Date";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(139, 406);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(154, 28);
            this.label6.TabIndex = 11;
            this.label6.Text = "Select End Date";
            // 
            // endDatePicker
            // 
            this.endDatePicker.Checked = true;
            this.endDatePicker.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.endDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.endDatePicker.Location = new System.Drawing.Point(402, 406);
            this.endDatePicker.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.endDatePicker.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(200, 36);
            this.endDatePicker.TabIndex = 5;
            this.endDatePicker.Value = new System.DateTime(2024, 4, 22, 13, 47, 37, 813);
            // 
            // startDatePicker
            // 
            this.startDatePicker.Checked = true;
            this.startDatePicker.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.startDatePicker.Location = new System.Drawing.Point(402, 340);
            this.startDatePicker.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.startDatePicker.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(200, 36);
            this.startDatePicker.TabIndex = 4;
            this.startDatePicker.Value = new System.DateTime(2024, 4, 22, 13, 47, 28, 846);
            // 
            // walletCategoryComboBox
            // 
            this.walletCategoryComboBox.BackColor = System.Drawing.Color.Transparent;
            this.walletCategoryComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.walletCategoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.walletCategoryComboBox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.walletCategoryComboBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.walletCategoryComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.walletCategoryComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.walletCategoryComboBox.ItemHeight = 30;
            this.walletCategoryComboBox.Location = new System.Drawing.Point(402, 259);
            this.walletCategoryComboBox.Name = "walletCategoryComboBox";
            this.walletCategoryComboBox.Size = new System.Drawing.Size(200, 36);
            this.walletCategoryComboBox.TabIndex = 2;
            this.walletCategoryComboBox.SelectedIndexChanged += new System.EventHandler(this.walletTypeComboBox_SelectedIndexChanged);
            // 
            // walletComboBox
            // 
            this.walletComboBox.BackColor = System.Drawing.Color.Transparent;
            this.walletComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.walletComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.walletComboBox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.walletComboBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.walletComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.walletComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.walletComboBox.ItemHeight = 30;
            this.walletComboBox.Location = new System.Drawing.Point(402, 188);
            this.walletComboBox.Name = "walletComboBox";
            this.walletComboBox.Size = new System.Drawing.Size(200, 36);
            this.walletComboBox.TabIndex = 1;
            this.walletComboBox.SelectedIndexChanged += new System.EventHandler(this.walletComboBox_SelectedIndexChanged);
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.BackColor = System.Drawing.Color.Transparent;
            this.categoryComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.categoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryComboBox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.categoryComboBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.categoryComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.categoryComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.categoryComboBox.ItemHeight = 30;
            this.categoryComboBox.Location = new System.Drawing.Point(402, 114);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(200, 36);
            this.categoryComboBox.TabIndex = 0;
            // 
            // transactionTypeComboBox
            // 
            this.transactionTypeComboBox.BackColor = System.Drawing.Color.Transparent;
            this.transactionTypeComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.transactionTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.transactionTypeComboBox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.transactionTypeComboBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.transactionTypeComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.transactionTypeComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.transactionTypeComboBox.ItemHeight = 30;
            this.transactionTypeComboBox.Location = new System.Drawing.Point(402, 45);
            this.transactionTypeComboBox.Name = "transactionTypeComboBox";
            this.transactionTypeComboBox.Size = new System.Drawing.Size(200, 36);
            this.transactionTypeComboBox.TabIndex = 3;
            this.transactionTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.transactionTypeComboBox_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(448, 466);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 45);
            this.button1.TabIndex = 12;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(243, 466);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(154, 45);
            this.button2.TabIndex = 13;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SearchFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 536);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.endDatePicker);
            this.Controls.Add(this.startDatePicker);
            this.Controls.Add(this.transactionTypeComboBox);
            this.Controls.Add(this.walletCategoryComboBox);
            this.Controls.Add(this.walletComboBox);
            this.Controls.Add(this.categoryComboBox);
            this.Name = "SearchFilter";
            this.Text = "SearchFilter";
            this.Load += new System.EventHandler(this.SearchFilter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2DateTimePicker endDatePicker;
        private Guna.UI2.WinForms.Guna2DateTimePicker startDatePicker;
        private Guna.UI2.WinForms.Guna2ComboBox walletCategoryComboBox;
        private Guna.UI2.WinForms.Guna2ComboBox walletComboBox;
        private Guna.UI2.WinForms.Guna2ComboBox categoryComboBox;
        private Guna.UI2.WinForms.Guna2ComboBox transactionTypeComboBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}