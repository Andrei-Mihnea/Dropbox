using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogic;
using CommonModels;


//Autor implementare interfata Irimescu Dragos Andrei
namespace Dropbox
{
    public partial class DropboxForm : Form
    {
        private readonly DropboxFacade _facade = new DropboxFacade();
        private readonly User _currentUser;
        public DropboxForm(User user)
        {
            InitializeComponent();

            //initializare listView pentru un display mai organizat
            listView1.ContextMenuStrip = contextMenuStrip;
            listView1.Columns.Add("Nume fișier", 200);
            listView1.Columns.Add("Data încărcării", 150);
            _currentUser = user;


            //permiterea drag&drop-ului
            this.AllowDrop = true;
            this.DragEnter += DropboxForm_DragEnter;
            this.DragDrop += DropboxForm_DragDrop;

            initLoad();
            
            //when loading maybe the users have some files uploaded

        }

        private async void initLoad()
        {
            await LoadUserFilesAsync();
        }
        private void DropboxForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private async void DropboxForm_DragDrop(object sender, DragEventArgs e)
        {

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            //uploadare fisier (parcuge fiecare fisier care trebuie uploadat)
            foreach(string file in files)
            {
                await UploadFileToServer(file);
            }

            MessageBox.Show("Fișierele au fost încarcate cu succes");
            //afisare fisiere noi
            await LoadUserFilesAsync();
        }

        private async Task LoadUserFilesAsync()
        {
            try
            {
                //obtinere fisiere utilizatr
                var files = await _facade.GetUserFiles(_currentUser.Id);

                //curatare a listei pentru afisarea noua
                listView1.Items.Clear();


                //iteratie pentru afisarea fisierelor
                foreach(var file in files)
                {
                    var item = new ListViewItem(file.FileName);
                    item.SubItems.Add(file.UploadedAt.ToString("g"));
                    item.Tag = file.Id;
                    listView1.Items.Add(item);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Eroare la incarcarea fisierelor ${ex.Message}");
            }
        }

        private async Task UploadFileToServer(string filePath)
        {
            using (var client = new HttpClient())
            {
                //transformare in fisier json pentru a putea transmite la server
                var payload = JsonSerializer.Serialize(new
                {
                    UserId = _currentUser.Id,
                    FilePath = filePath
                });

                var content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("http://localhost:8080/upload", content);

                if (!response.IsSuccessStatusCode)
                    MessageBox.Show("Upload failed!");
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {

        }

        private async void stergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            var selectedItem = listView1.SelectedItems[0];
            string fileName = selectedItem.Text;

            var confirm = MessageBox.Show("Sigur dorești să ștergi acest fișier?",
                                          "Confirmare ștergere",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Warning);
            
            if (confirm == DialogResult.Yes)
            {
                await _facade.DeleteFile(_currentUser.Id,fileName);
                await LoadUserFilesAsync();
            }
        }


        private async void descarcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;

            var selectedItem = listView1.SelectedItems[0];
            string fileName = selectedItem.Text;
            //afisare locatie descarcare a fisierlui
            using (SaveFileDialog sfd = new SaveFileDialog { FileName = fileName })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    await _facade.DownloadFile(_currentUser.Id, fileName, sfd.FileName);
                    MessageBox.Show("Descarcarea a fost realizata cu succes");
                }
            }
        }
    }
}
