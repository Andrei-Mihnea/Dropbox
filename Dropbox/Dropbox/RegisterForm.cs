using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

        private async void Register_Click(object sender, EventArgs e)
        {
            try
            {
                if (password.Text == textBox4.Text) //texbox4 este Retype Password
                {
                    var usrname = username.Text;
                    var pwdname = password.Text;

                    using (var httpClient = new HttpClient())
                    {
                        var request = new
                        {
                            Username = usrname,
                            Password = pwdname,
                        };

                        string json = JsonSerializer.Serialize(request);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        var response = await httpClient.PostAsync("http://localhost:8080/register", content);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Înregistrare efecutată cu succes");
                            this.Close();
                        }
                        else
                        {
                            string error = await response.Content.ReadAsStringAsync();
                            throw new Exception(error);
                        }

                    }
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
