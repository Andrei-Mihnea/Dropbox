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
using CommonModels;

namespace Dropbox
{
    public partial class LoginForm : Form
    {
        DropboxFacade _facade = new DropboxFacade();
        public User LoggedInUser {  get; private set; }
        public LoginForm()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, EventArgs e)
        {
            try
            {
                var username = numeUtilizator.Text;
                var password = parola.Text;
               
                var user = _facade.Login(username, password);
                LoggedInUser = user;
                this.DialogResult = DialogResult.OK;

                MessageBox.Show("Logarea a fost realizata cu succes");

                //DropboxForm dropboxForm = new DropboxForm(user);
                //dropboxForm.Show();
                //this.Hide();
                
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
