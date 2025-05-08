using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogic;

namespace Dropbox
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void Register_Click(object sender, EventArgs e)
        {
            try
            {
                if (password.Text == textBox4.Text) //texbox4 este Retype Password
                {
                    var usrname = username.Text;
                    var pwdname = password.Text;

                    DropboxFacade facade = new DropboxFacade();
                    facade.Register(usrname, pwdname);

                    MessageBox.Show($"Înregistrare efecutată cu succes");
                }
                else
                {
                    throw new Exception("Parolele nu coincid");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la înregistrare: {ex.Message}");
            }
        }
    }
}
