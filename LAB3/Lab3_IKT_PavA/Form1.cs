using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace Lab3_IKT_PavA
{
    public struct DataText
    {
        public string text;
    }
    public partial class CopyForm : Form
    {       
        static public DataText Dt;
        public string n; 
        public int c;

        public CopyForm()
        {
            InitializeComponent();
            
        }    

        private void copyDirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                string filename = @"C:\Users\admin\Desktop\test";
                int i = 0;
                string path;
                for (int k = 0; k < listView1.Items.Count; k++)
                {
                    string file = listView1.Items[k].Text;
                    pictureBox1.Image = Image.FromFile(file);
                    path = filename + i.ToString() + ".png";
                    string logo = "(c)PavA";
                    Bitmap bmp = new Bitmap(pictureBox1.Image);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        using (Font myFont = new Font("Times New Roman", 32))
                            g.DrawString(logo, myFont, Brushes.Gold, 1, 1);
                    }
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = bmp;
                    Bitmap bmpSave = new Bitmap(pictureBox1.DisplayRectangle.Width, pictureBox1.DisplayRectangle.Height);
                    pictureBox1.DrawToBitmap(bmpSave, pictureBox1.DisplayRectangle);
                    bmpSave.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);
                    i++;
                }
                pictureBox1.Image = null;
                string caption = "Batch Mode";
                string message = "Operations completed";
                MessageBox.Show(message, caption);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {          
            OpenFileDialog open_dialog = new OpenFileDialog();
           // open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            open_dialog.Multiselect = true;
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                imageList1.Images.Clear();
                listView1.Clear();
                pictureBox1.Image = null;
                int num = open_dialog.FileNames.Length;
                string[] path = new string[num];
                foreach (string f in open_dialog.FileNames)
                {
                    path[c] = f;
                    imageList1.Images.Add(Image.FromFile(f));
                    c++;
                }
                listView1.LargeImageList = imageList1;
                for (int i = 0; i < c; i++)
                    listView1.Items.Add(path[i], i);
            }

        }

        private void openDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            DirectoryInfo dir;
            FileInfo[] fil;
            if (folder.ShowDialog() == DialogResult.OK)
            {
                imageList1.Images.Clear();
                listView1.Clear();
                pictureBox1.Image = null;
                dir = new DirectoryInfo(folder.SelectedPath);
                fil = dir.GetFiles();
                string p = folder.SelectedPath;
                string[] s = new string[fil.Length];
                for (int i = 0; i < fil.Length; i++)
                {
                    s[i] = p + "\\" + fil[i].Name;
                    imageList1.Images.Add(Image.FromFile(s[i]));
                    c++;
                }
                listView1.LargeImageList = imageList1;
                for (int i = 0; i < c; i++)
                    listView1.Items.Add(s[i], i);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    pictureBox1.Image = imageList1.Images[listView1.SelectedItems[i].ImageIndex];
                    this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    string file = listView1.SelectedItems[i].Text;
                    n = listView1.SelectedItems[i].Text;
                    pictureBox1.Image = Image.FromFile(file);
                }
            }
            catch 
            {
                string caption = "Error";
                string message = "This image has already been modified";
                MessageBoxButtons button = MessageBoxButtons.OK;
                MessageBox.Show( message, caption, button); 
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string caption = "Directory Form"; 
            string message = "Do you want close this form?"; 
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.Yes)
                this.Close();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string caption = "Directory Form";
            string message = "Если не работает перезапусти." + "\n Если работает не ломай!";
            MessageBox.Show(message, caption);
        }

        private void copyTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddText addLogo = new FormAddText();
            addLogo.ShowDialog();
            string logo = Dt.text;
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                using (Font myFont = new Font("Times New Roman", 32))
                    g.DrawString(logo, myFont, Brushes.Gold, 1, 1);
            }
            pictureBox1.Image.Dispose();
            pictureBox1.Image = bmp;

            for (int k = 0; k < c; k++)
            {
                if (listView1.Items[k].Text == n)
                {
                    listView1.Items[k].Text = "Copy";
                    listView1.Items[k].ForeColor = Color.Gold;
                    listView1.Items[k].Font = new Font("Times New Roman", 10, FontStyle.Bold);
                }
            }

            string name = Path.GetFileName(n);
            int width = bmp.Width;
            int height = bmp.Height;
            int i;
            for (i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = name;
                dataGridView1.Rows[i].Cells[1].Value = width;
                dataGridView1.Rows[i].Cells[2].Value = height;
                dataGridView1.Rows[i].Cells[3].Value = logo;
                i++;
            }
        }

        private void addCopyrightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string logo = "(c)PavA";
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                using (Font myFont = new Font("Times New Roman", 32))
                    g.DrawString(logo, myFont, Brushes.Gold, 1, 1);
            }
            pictureBox1.Image.Dispose();
            pictureBox1.Image = bmp;

            for (int k = 0; k < c; k++)
            {
                if (listView1.Items[k].Text == n)
                {
                    listView1.Items[k].Text = "Copy";
                    listView1.Items[k].ForeColor = Color.Gold;
                    listView1.Items[k].Font = new Font("Times New Roman", 10, FontStyle.Bold);
                }
            }
            string name = Path.GetFileName(n);
            int width = bmp.Width;
            int height = bmp.Height;
            int i;
            for (i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = name;
                dataGridView1.Rows[i].Cells[1].Value = width;
                dataGridView1.Rows[i].Cells[2].Value = height;
                dataGridView1.Rows[i].Cells[3].Value = logo;
                i++;
            }     
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imageSave = new Bitmap(pictureBox1.DisplayRectangle.Width, pictureBox1.DisplayRectangle.Height);
            if (pictureBox1.Image != null) 
            {
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить картинку как...";
                savedialog.OverwritePrompt = true;
                savedialog.CheckPathExists = true;
                //savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        imageSave.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    catch
                    {
                        string caption = "Error";
                        string message = "Cannot be saved";
                        MessageBox.Show(message, caption);
                    }
                }
            }
        }

        private void batchModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = @"C:\Users\admin\Desktop\test";
            int i = 0;
            string path;
            for (int k = 0; k < listView1.Items.Count; k++)
            {
                string file = listView1.Items[k].Text;
                pictureBox1.Image = Image.FromFile(file);
                path = filename + i.ToString() + ".png";
                string logo = "(c)PavA";
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    using (Font myFont = new Font("Times New Roman", 32))
                        g.DrawString(logo, myFont, Brushes.Gold, 1, 1);
                }
                pictureBox1.Image.Dispose();
                pictureBox1.Image = bmp;
                Bitmap bmpSave = new Bitmap(pictureBox1.DisplayRectangle.Width, pictureBox1.DisplayRectangle.Height);
                pictureBox1.DrawToBitmap(bmpSave, pictureBox1.DisplayRectangle);
                bmpSave.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);
                i++;
            }
            pictureBox1.Image = null;
            string caption = "Batch Mode";
            string message = "Operations completed";
            MessageBox.Show(message, caption);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }
    }
}
