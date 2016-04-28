using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Alfresco;

namespace TWAINCapture
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCapture mediacapture = new frmCapture(txtusername.Text, txtpassword.Text);
            mediacapture.ShowDialog();
            //bool valid =  AuthenticationUtils.startSession(txtusername.Text, txtpassword.Text);

            //if (valid == true)
            //{
            //    AuthenticationUtils.endSession();
            //    this.Hide();
            //    frmCapture mediacapture = new frmCapture(txtusername.Text, txtpassword.Text);
            //    mediacapture.ShowDialog();
            //   Application.Exit();
            //}
            //else
            //{
            //    MessageBox.Show("Invalid User/Password");
            //}
        }
        public static bool doLogin()
        {
            bool result = false;

            try
            {
                DialogResult dialogResult = new login().ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                // For now do nothing with this ...
                result = false;
            }

            return result;
        }

    }
}
