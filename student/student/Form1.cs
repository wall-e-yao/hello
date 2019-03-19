using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace student
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fudaoyuan ff = new fudaoyuan();
            ff.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            stu dlg = new stu();
            dlg.ShowDialog();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("你确定要退出学生管理系统吗！", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                e.Cancel = false;  //点击OK   
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            teacher teach = new teacher();
            teach.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            admin ad = new admin();
            ad.ShowDialog();
        }
    }
}
