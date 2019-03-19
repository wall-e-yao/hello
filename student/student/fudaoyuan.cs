using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace student
{
    public partial class fudaoyuan : Form
    {
        public fudaoyuan()
        {
            InitializeComponent();
        }
        private void fudaoyuan_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("你确定要关闭吗！", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                e.Cancel = false;  //点击OK   
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            String connsql = "server=.;database=test;integrated security=SSPI"; // 数据库连接字符串,database设置为自己的数据库名，以Windows身份验证
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = connsql;
                    conn.Open(); // 打开数据库连接

                    string co = collegetoolStripTextBox1.Text;
                    string gr = gradetoolStripTextBox3.Text;
                    string cl = classtoolStripTextBox2.Text;
                    string sql = null;

                    if (co.ToString() == "")
                    {
                        MessageBox.Show("请输入学院！");
                        return;
                    }
                    if(co!="" && gr!= "" && cl != "")
                    {
                        sql = "select stu_ssn, name,course_name, chengji from study, student, course where ssn = stu_ssn and course_num = course_number and cssn = " +
                        "'" + co + "' and grade " +
                        "= '" + gr + "'and class " +
                        "= '" + cl + "'";
                    }
                    else if(co != "" && gr != "" && cl == "")
                    {
                        sql = "select stu_ssn, name, course_name, chengji from study, student, course where ssn = stu_ssn and course_num = course_number and cssn = " +
                       "'" + co + "' and grade " +
                       "= '" + gr + "'";
                    }
                    else if(co != "" && gr == "" && cl == "")
                    {
                        sql = "select stu_ssn, name, course_name, chengji from study, student, course where ssn = stu_ssn and course_num = course_number and cssn = " +
                        "'" + co + "'";
                    }

                    SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器

                    DataTable dt = new DataTable(); // 实例化数据表

                    myda.Fill(dt); // 保存数据 

                    dataGridView1.DataSource = dt; // 设置到DataGridView中

                    conn.Close(); // 关闭数据库连接
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息：" + ex.Message, "出现错误");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            label3.Text = dataGridView1.Rows[i].Cells[2].Value.ToString().Trim().ToString();

            //string sql = "select avg(chengji) from study, course where course_num = course_number and course_name = '" + dataGridView1.Rows[i].Cells[2].Value.ToString().Trim().ToString() + "'";

            string co = collegetoolStripTextBox1.Text;
            string gr = gradetoolStripTextBox3.Text;
            string cl = classtoolStripTextBox2.Text;
            string sql = null;

            if (co.ToString() == "")
            {
                MessageBox.Show("请输入学院！");
                return;
            }
            if (co != "" && gr != "" && cl != "")
            {
                sql = "select name,chengji from study, student, course where ssn = stu_ssn and course_num = course_number and course_name = '"+ dataGridView1.Rows[i].Cells[2].Value.ToString().Trim().ToString() + "' and cssn = " +
                "'" + co + "' and grade " +
                "= '" + gr + "'and class " +
                "= '" + cl + "'";
            }
            else if (co != "" && gr != "" && cl == "")
            {
                sql = "select name,chengji from study, student, course where ssn = stu_ssn and course_num = course_number and course_name = '" + dataGridView1.Rows[i].Cells[2].Value.ToString().Trim().ToString() + "' and cssn = " +
                "'" + co + "' and grade " +
                "= '" + gr + "'";
            }
            else if (co != "" && gr == "" && cl == "")
            {
                sql = "select name,chengji from study, student, course where ssn = stu_ssn and course_num = course_number and course_name = '" + dataGridView1.Rows[i].Cells[2].Value.ToString().Trim().ToString() + "' and cssn = " +
                "'" + co + "'";
            }

            String connsql = "server=.;database=test;integrated security=SSPI"; // 数据库连接字符串,database设置为自己的数据库名，以Windows身份验证
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = connsql;
                    conn.Open(); // 打开数据库连接

                    SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器

                    DataTable dt = new DataTable(); // 实例化数据表

                    myda.Fill(dt); // 保存数据 

                    dataGridView2.DataSource = dt; // 设置到DataGridView中

                    conn.Close(); // 关闭数据库连接
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息：" + ex.Message, "出现错误");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            label3.Text = dataGridView1.Rows[i].Cells[2].Value.ToString().Trim().ToString();

            //string sql = "select avg(chengji) from study, course where course_num = course_number and course_name = '" + dataGridView1.Rows[i].Cells[2].Value.ToString().Trim().ToString() + "'";

            string co = collegetoolStripTextBox1.Text;
            string gr = gradetoolStripTextBox3.Text;
            string cl = classtoolStripTextBox2.Text;
            string sql = null;

            if (co.ToString() == "")
            {
                MessageBox.Show("请输入学院！");
                return;
            }
            if (co != "" && gr != "" && cl != "")
            {
                sql = "select avg(chengji) as '"+ dataGridView1.Rows[i].Cells[2].Value.ToString().Trim().ToString() + "的平均分' from study, student, course where ssn = stu_ssn and course_num = course_number and course_name = '" + dataGridView1.Rows[i].Cells[2].Value.ToString().Trim().ToString() + "' and cssn = " +
                "'" + co + "' and grade " +
                "= '" + gr + "'and class " +
                "= '" + cl + "'";
            }
            else if (co != "" && gr != "" && cl == "")
            {
                sql = "select avg(chengji) as '" + dataGridView1.Rows[i].Cells[2].Value.ToString().Trim().ToString() + "的平均分'  from study, student, course where ssn = stu_ssn and course_num = course_number and course_name = '" + dataGridView1.Rows[i].Cells[2].Value.ToString().Trim().ToString() + "' and cssn = " +
                "'" + co + "' and grade " +
                "= '" + gr + "'";
            }
            else if (co != "" && gr == "" && cl == "")
            {
                sql = "select avg(chengji) as  '" + dataGridView1.Rows[i].Cells[2].Value.ToString().Trim().ToString() + "的平均分'  from study, student, course where ssn = stu_ssn and course_num = course_number and course_name = '" + dataGridView1.Rows[i].Cells[2].Value.ToString().Trim().ToString() + "' and cssn = " +
                "'" + co + "'";
            }

            String connsql = "server=.;database=test;integrated security=SSPI"; // 数据库连接字符串,database设置为自己的数据库名，以Windows身份验证
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = connsql;
                    conn.Open(); // 打开数据库连接

                    SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器

                    DataTable dt = new DataTable(); // 实例化数据表

                    myda.Fill(dt); // 保存数据 

                    dataGridView3.DataSource = dt; // 设置到DataGridView中
                    conn.Close(); // 关闭数据库连接
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息：" + ex.Message, "出现错误");
            }
        }

        private void collegetoolStripTextBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
