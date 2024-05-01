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
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        public SettingsUC()
        {
            InitializeComponent();
        }

        private void SettingsUC_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            editProfile1.BringToFront();
            editProfileLabel.Font = new Font(editProfileLabel.Font, FontStyle.Bold);
            changePassLabel.Font = new Font(changePassLabel.Font, FontStyle.Regular);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            editPassword1.BringToFront();
            editProfileLabel.Font = new Font(editProfileLabel.Font, FontStyle.Regular);
            changePassLabel.Font = new Font(changePassLabel.Font, FontStyle.Bold);
        }

        private void editProfile1_Load(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel2_Paint(object sender, PaintEventArgs e)
        {
                
        }
    }
}
