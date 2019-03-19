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
    public partial class teacher : Form
    {
        public teacher()
        {
            InitializeComponent();
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
                    int i = dataGridView1.CurrentRow.Index;
                    string sql = "update study set chengji = '" + this.toolStripTextBox2.Text.Trim().ToString() + "' where stu_ssn =" +
                        " '" + this.dataGridView1.Rows[i].Cells[0].Value.ToString().Trim().ToString()+ "'and course_number = "+
                        "'"+ toolStripTextBox1.Text.Trim().ToString()+"'";
                    SqlCommand com = conn.CreateCommand();
                    com.CommandText = sql;
                    com.ExecuteNonQuery();
                    this.dataGridView1.Rows[i].Cells[3].Value = toolStripTextBox2.Text;
                    conn.Close(); // 关闭数据库连接
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息：成绩录入错误啦！");
            }
        }

        private void findtoolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBox1!=null)
                {
                    String connsql = "server=.;database=test;integrated security=SSPI"; // 数据库连接字符串,database设置为自己的数据库名，以Windows身份验证
                    try
                    {
                        using (SqlConnection conn = new SqlConnection())
                        {
                            conn.ConnectionString = connsql;
                            conn.Open(); // 打开数据库连接
                            String sql = "select stu_ssn, name, course_name, chengji from study, course, student where course_num = course_number and ssn = stu_ssn and course_num = '"+ toolStripTextBox1 .Text.Trim().ToString()+ "'"; // 查询语句
                            SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                            DataTable dt = new DataTable(); // 实例化数据表
                            myda.Fill(dt); // 保存数据 
                            dataGridView1.DataSource = dt;
                            conn.Close(); // 关闭数据库连接
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("错误信息：没有找到该课程！");
                    }
                }
                else
                    MessageBox.Show("请您输入您教授的课程号！");
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        double max = 0, min = 100;
        private void label1_Click(object sender, EventArgs e)
        {
            int num = dataGridView1.RowCount;
            if (num == 0)
                return;
            double sum = 0;
            max = 0; min = 100;
            for (int i = 0; i < num; i++)
            {
                double chengji = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                if (chengji > max)
                    max = chengji;
                if (chengji < min)
                    min = chengji;
                sum += chengji;
            }
            textBox1.Text = (sum / num).ToString();
            textBox2.Text = max.ToString();
            textBox3.Text = min.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            textBox3.Text = min.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try{
                if (toolStripTextBox1 != null)
                {
                    String connsql = "server=.;database=test;integrated security=SSPI"; // 数据库连接字符串,database设置为自己的数据库名，以Windows身份验证
                    try
                    {
                        using (SqlConnection conn = new SqlConnection())
                        {
                            conn.ConnectionString = connsql;
                            conn.Open(); // 打开数据库连接
                            string sql = "select stu_ssn, name, course_name, " +
                              "chengji from study, student, course where ssn = stu_ssn " +
                             "and course_num = course_number and course_number" +
                            " = '" + toolStripTextBox1.Text.Trim().ToString() + "' " +
                           "and chengji > " + Convert.ToDouble(textBox5.Text.Trim().ToString());
                            SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                            DataTable dt = new DataTable(); // 实例化数据表
                            myda.Fill(dt); // 保存数据 
                            dataGridView1.DataSource = dt;
                            textBox6.Text = dataGridView1.RowCount.ToString();
                            conn.Close(); // 关闭数据库连接
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("错误信息：没有找到该课程！");
                    }
                }
                else
                    MessageBox.Show("请您输入您教授的课程号！");
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBox1 != null)
                {
                    String connsql = "server=.;database=test;integrated security=SSPI"; // 数据库连接字符串,database设置为自己的数据库名，以Windows身份验证
                    try
                    {
                        using (SqlConnection conn = new SqlConnection())
                        {
                            conn.ConnectionString = connsql;
                            conn.Open(); // 打开数据库连接
                            string sql = "select stu_ssn, name, course_name, " +
                              "chengji from study, student, course where ssn = stu_ssn " +
                             "and course_num = course_number and course_number" +
                            " = '" + toolStripTextBox1.Text.Trim().ToString() + "' " +
                           "and chengji < " + Convert.ToDouble(textBox4.Text.Trim().ToString());
                            SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                            DataTable dt = new DataTable(); // 实例化数据表
                            myda.Fill(dt); // 保存数据 
                            dataGridView1.DataSource = dt;
                            textBox7.Text = dataGridView1.RowCount.ToString();
                            conn.Close(); // 关闭数据库连接
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("错误信息：没有找到该课程！");
                    }
                }
                else
                    MessageBox.Show("请您输入您教授的课程号！");
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int num = dataGridView1.RowCount;
            if (num == 0)
                return;
            double sum = 0;
            max = 0;min = 100;
            for (int i = 0; i < num; i++)
            {
                double chengji = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                if (chengji > max)
                    max = chengji;
                if (chengji < min)
                    min = chengji;
                sum += chengji;
            }
            textBox1.Text = (sum / num).ToString();
            textBox2.Text = max.ToString();
            textBox3.Text = min.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            textBox2.Text = max.ToString();
        }
    }
}
