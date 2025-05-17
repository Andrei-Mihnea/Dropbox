using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

        private async void login_Click(object sender, EventArgs e)
        {
            try
            {
                var username = numeUtilizator.Text;
                var password = parola.Text;

                using (var client = new HttpClient())
                {
                    var request = new
                    {
                        Username = username,
                        Password = password,
                    };

                    string json = JsonSerializer.Serialize(request);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("http://localhost:8080/login", content);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        var user = JsonSerializer.Deserialize<User>(result);
                        LoggedInUser = user;
                        this.DialogResult = DialogResult.OK;

                        MessageBox.Show("Logarea a fost realizata cu succes");
                    }
                    else
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Login esuat: " + error.ToString());
                    }
                }   
                
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
