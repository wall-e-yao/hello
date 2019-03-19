using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace newword
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void boldtoolStripButton1_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripButton btn = sender as ToolStripButton;
            bool isChecked = btn.Checked;

            Font oldFont = myrichTextBox1.SelectionFont, newFont;
            if (isChecked)
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Bold);
            else
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Bold);

            myrichTextBox1.SelectionFont = newFont;

            boldToolStripMenuItem.Checked = isChecked;

            boldtoolStripStatusLabel1.Enabled = isChecked;
        }
        private void italictoolStripButton1_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripButton btn = sender as ToolStripButton;
            bool isChecked = btn.Checked;
            Font oldFont = myrichTextBox1.SelectionFont, newFont;
            if (isChecked)
            {
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Italic);
            }
            else
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Italic);

            myrichTextBox1.SelectionFont = newFont;

            italicToolStripMenuItem.Checked = isChecked;

            italictoolStripStatusLabel1.Enabled = isChecked;
        }
        private void undertoolStripButton1_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripButton btn = sender as ToolStripButton;
            bool isChecked = btn.Checked;
            Font oldFont = myrichTextBox1.SelectionFont, newFont;
            if (isChecked)
            {
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Underline);
            }
            else
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Underline);

            myrichTextBox1.SelectionFont = newFont;

            underlineToolStripMenuItem.Checked = isChecked;

            undertoolStripStatusLabel1.Enabled = isChecked;
        }

        private void boldToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem =
               sender as ToolStripMenuItem;

            bool isChecked = menuItem.Checked;

            Font oldFont = myrichTextBox1.SelectionFont, newFont;
            if (isChecked)
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Bold);
            else
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Bold);

            myrichTextBox1.SelectionFont = newFont;

            boldtoolStripButton1.Checked = isChecked;
        }

        private void italicToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem =
              sender as ToolStripMenuItem;

            bool isChecked = menuItem.Checked;

            Font oldFont = myrichTextBox1.SelectionFont, newFont;
            if (isChecked)
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Italic);
            else
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Italic);

            myrichTextBox1.SelectionFont = newFont;

            italictoolStripButton1.Checked = isChecked;
        }

        private void underlineToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            bool isChecked = menuItem.Checked;

            Font oldFont = myrichTextBox1.SelectionFont, newFont;
            if (isChecked)
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Underline);
            else
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Underline);

            myrichTextBox1.SelectionFont = newFont;

            undertoolStripButton1.Checked = isChecked;
        }

        private void myrichTextBox1_TextChanged(object sender, EventArgs e)
        {
            numbertoolStripStatusLabel2.Text = myrichTextBox1.TextLength.ToString();
        }

        private void myrichTextBox1_MouseCaptureChanged(object sender, EventArgs e)
        {
            if(myrichTextBox1.SelectionFont.Style == FontStyle.Bold)
            {
                boldtoolStripButton1.Checked = true;
                boldToolStripMenuItem.Checked = true;
                boldtoolStripStatusLabel1.Enabled = true;

            }
            if(myrichTextBox1.SelectionFont.Style == FontStyle.Italic)
            {
                italictoolStripButton1.Checked = true;
                italicToolStripMenuItem.Checked = true;
                italictoolStripStatusLabel1.Enabled = true;
            }
            if(myrichTextBox1.SelectionFont.Style == FontStyle.Underline)
            {
                undertoolStripButton1.Checked = true;
                underlineToolStripMenuItem.Checked = true;
                undertoolStripStatusLabel1.Enabled = true;
            }

            snumbertoolStripStatusLabel1.Text = myrichTextBox1.SelectedText.Length.ToString();
        }

        private void heiToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            myrichTextBox1.SelectionFont = new Font("黑体", myrichTextBox1.SelectionFont.Size);
        }

        private void kaiToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            myrichTextBox1.SelectionFont = new Font("楷体", myrichTextBox1.SelectionFont.Size);
        }

        private void songToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            myrichTextBox1.SelectionFont = new Font("宋体", myrichTextBox1.SelectionFont.Size);
        }

        private void liToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            myrichTextBox1.SelectionFont = new Font("隶书", myrichTextBox1.SelectionFont.Size);
        }

        private void fonttoolStripButton1_Click(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();

            DialogResult result = dlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                myrichTextBox1.SelectionFont = dlg.Font;
            }
        }

        private void colortoolStripButton1_Click(object sender, EventArgs e)
        {
            ColorDialog cdg = new ColorDialog();
            DialogResult result = cdg.ShowDialog();
            if (result == DialogResult.OK)
            {
                myrichTextBox1.SelectionColor = cdg.Color;
            }

        }

        private void saveSToolStripButton_Click(object sender, EventArgs e)
        {
            string add = @"C:\Users\yaohao\Desktop\C#实验\saveaddress\";
            string name = "test";
            int i = 1;
            DirectoryInfo dif = new DirectoryInfo(add);
     
            FileInfo[] file = dif.GetFiles();
            foreach(FileInfo f in file)
            {
                if (f.Name == name + i + ".doc")
                {
                    i++;
                }
            }

            myrichTextBox1.SaveFile(add+name+i+".doc");
            myrichTextBox1.Text += name + i + ".doc";
            MessageBox.Show("存储于"+ add + name + i + ".doc");

        }

        private void saveasAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "word文档|*.doc";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                myrichTextBox1.SaveFile(dlg.FileName);
            }
        }

        private void saveSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveSToolStripButton_Click(sender, e);
        }

        private void openOToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "word文档|*.doc|所有文件|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                myrichTextBox1.LoadFile(dlg.FileName);
            }
        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openOToolStripButton_Click(sender,e);
        }

        private void newNToolStripButton_Click(object sender, EventArgs e)
        {
            saveSToolStripButton_Click(sender, e);
            myrichTextBox1.Text = "";
        }

        private void 新建NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newNToolStripButton_Click( sender, e);
        }

        private void 退出XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 全选AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myrichTextBox1.SelectAll();
        }

        private void 剪切TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myrichTextBox1.Cut();
        }

        private void 撤消UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myrichTextBox1.Undo();
        }

        private void 重复RToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 复制CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myrichTextBox1.Copy();
        }

        private void 粘贴PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myrichTextBox1.Paste();
        }

        private void printPToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("正在为您打印。。。");
        }

        private void 打印PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPToolStripButton_Click(sender, e);
        }

        private void viewVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //预览

        }
    }
}
