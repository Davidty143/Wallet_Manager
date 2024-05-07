using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wallet_Manager.Forms
{
    public partial class SettingsUC : UserControl
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        public SettingsUC()
        {
            InitializeComponent();
            PopulateComboBox();
        }

        public void PopulateComboBox()
        {
            SelectSettingComboBox.Items.Clear();

            SelectSettingComboBox.Items.Add("Edit Profile");
            SelectSettingComboBox.Items.Add("Change Password");

            SelectSettingComboBox.SelectedIndex = 0; 
        }





        private void guna2CustomGradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectSettingComboBox.SelectedItem.ToString() == "Edit Profile")
            {
                editProfile1.BringToFront();
            }
            else if (SelectSettingComboBox.SelectedItem.ToString() == "Change Password")
            {
                editPassword1.BringToFront();
            }
        }

        private void editProfile1_Load(object sender, EventArgs e)
        {

        }
    }
}
