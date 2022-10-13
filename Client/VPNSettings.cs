using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class VPNSettings : Form
    {
        public string address = null;
        public string port = null;
        public string type = null;
        public string username = null;
        public string password = null;

        public VPNSettings()
        {
            InitializeComponent();
            Prox_Type.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Address_Value.Text.CompareTo("") == 0 || Port_Value.Text.CompareTo("") == 0)
            {
                DialogResult test = MessageBox.Show("Please Enter a Value or Exit Settings", "Settings Error", MessageBoxButtons.OKCancel);

                if (test == DialogResult.Cancel)
                    this.Close();
            }
            else
            {
                type = Prox_Type.Text;
                address = Address_Value.Text;
                port = Port_Value.Text;

                if(LoginDetailsCheck.Checked)
                {
                    username = UsernameText.Text;
                    password = PasswordText.Text;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
