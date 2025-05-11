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

            if (_facade.GetUserFiles(user.Id) != null)
            {
                LoadUserFiles();
            }
            //when loading maybe the users have some files uploaded

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

        private void DropboxForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach(string file in files)
            {
                _facade.UploadFile(_currentUser, file);
            }

            MessageBox.Show("Fișierele au fost încarcate cu succes");
            LoadUserFiles();
        }

        private void LoadUserFiles()
        {
            var files = _facade.GetUserFiles(_currentUser.Id);
            listView1.Items.Clear();

            foreach(var file in files)
            {
                var item = new ListViewItem(file.FileName);
                item.SubItems.Add(file.UploadedAt.ToString("g"));
                item.Tag = file.Id;
                listView1.Items.Add(item);
            }
            
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {

        }

        private void ștergeToolStripMenuItem_Click(object sender, EventArgs e)
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
                _facade.DeleteFile(fileId); 
                LoadUserFiles();             
            }
        }
    }
}
