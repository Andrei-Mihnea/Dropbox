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

namespace Dropbox
{
    public partial class DropboxForm : Form
    {
        private readonly DropboxFacade _facade = new DropboxFacade();
        private readonly User _currentUser;
        public DropboxForm(User user)
        {
            InitializeComponent();
            listView1.ContextMenuStrip = contextMenuStrip;
            listView1.Columns.Add("Nume fișier", 200);
            listView1.Columns.Add("Data încărcării", 150);
            _currentUser = user;

            this.AllowDrop = true;
            this.DragEnter += DropboxForm_DragEnter;
            this.DragDrop += DropboxForm_DragDrop;

            initLoad();
            
            //when loading maybe the users have some files uploaded

        }

        private async void initLoad()
        {
            await LoadUserFilesFromServer();
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

            foreach(string file in files)
            {
                await UploadFileToServer(file);
            }

            MessageBox.Show("Fișierele au fost încarcate cu succes");
            await LoadUserFilesFromServer();
        }

        private async Task LoadUserFilesFromServer()
        {
            using (var httpClient = new HttpClient())
            {
                var payload = JsonSerializer.Serialize(new {UserId = _currentUser.Id});
                var content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("http://localhost:8080/list", content);
                var json = await response.Content.ReadAsStringAsync();

                var files = JsonSerializer.Deserialize<List<FileItem>>(json);

                listView1.Items.Clear();

                foreach (var file in files)
                {
                    var item = new ListViewItem(file.FileName);
                    item.SubItems.Add(file.UploadedAt.ToString("g"));
                    item.Tag = file.Id;
                    listView1.Items.Add(item);
                }
            }


        }

        private async Task UploadFileToServer(string filePath)
        {
            using (var client = new HttpClient())
            {
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

        private async void ștergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            var selectedItem = listView1.SelectedItems[0];
            var fileId = (int)selectedItem.Tag;

            var confirm = MessageBox.Show("Sigur dorești să ștergi acest fișier?",
                                          "Confirmare ștergere",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                await DeleteFileFromServer(fileId);
                await LoadUserFilesFromServer();
            }
        }

        private async Task DeleteFileFromServer(int fileId)
        {
            using (var client = new HttpClient())
            {
                var payload = JsonSerializer.Serialize(new { FileId = fileId });
                var content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("http://localhost:8080/delete", content);

                if (!response.IsSuccessStatusCode)
                    MessageBox.Show("Ștergerea a eșuat!");
            }
        }
    }
}
