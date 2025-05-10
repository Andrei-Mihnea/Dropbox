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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, EventArgs e)
        {
            try
            {
                DropboxFacade dropbox = new DropboxFacade();

                var username = numeUtilizator.Text;
                var password = parola.Text;

                dropbox.Login(username, password);

                MessageBox.Show("Logarea a fost realizata cu succes");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o problema la logare {ex.Message}");
            }
        }

        private void register_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();

            registerForm.Show();
                      
        }
    }
}
