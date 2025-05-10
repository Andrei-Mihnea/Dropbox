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

        private void Register_Click(object sender, EventArgs e)
        {
            try
            {
                if (password.Text == textBox4.Text) //texbox4 este Retype Password
                {
                    var usrname = username.Text;
                    var pwdname = password.Text;

                    DropboxFacade dropbox = new DropboxFacade();
                    dropbox.Register(usrname, pwdname);

                    MessageBox.Show("Înregistrare efecutată cu succes");
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
