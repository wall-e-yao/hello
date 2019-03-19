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
    public partial class stu : Form
    {
        bool loginStatus;

        public stu()
        {
            InitializeComponent();
            loginStatus = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //String connsql = "server=.;database=test;integrated security=SSPI"; // 数据库连接字符串,database设置为自己的数据库名，以Windows身份验证
            try
            {
                //using (SqlConnection conn = new SqlConnection())
                //{
                //    conn.ConnectionString = connsql;
                //    conn.Open(); // 打开数据库连接

                //    String sql = "select * from student where ssn= '" + textBox1.Text + "'"; // 查询语句
                //    String sql1 = "select distinct name,course_name,chengji from student, course, study where ssn = stu_ssn and course_num = course_number and ssn= '" + textBox1.Text + "'";
                //    SqlDataAdapter myda = new SqlDataAdapter(sql1, conn); // 实例化适配器

                //    DataTable dt = new DataTable(); // 实例化数据表

                //    myda.Fill(dt); // 保存数据 

                //    DataRow student = dt.Rows[0];

                //    dataGridView1.DataSource = dt; // 设置到DataGridView中

                //    if (student.Field<string>("name") != null)
                //        label2.Text = student.Field<string>("name").Trim(' ') + " 同学您好!";


                //    conn.Close(); // 关闭数据库连接
                //this.courseTableAdapter.Fill(this.testDataSet.course);

                //this.studentTableAdapter.Fill(this.testDataSet.student);

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息：您输入的学号出现错误");
            }
        }

        private void stu_FormClosing_1(object sender, FormClosingEventArgs e)
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

                    String sql = "select * from student where ssn= '" + toolStripTextBox1.Text + "'"; // 查询语句
                    //String sql1 = "select distinct name,course_name,chengji from student, course, study where ssn = stu_ssn and course_num = course_number and ssn= '" + textBox1.Text + "'";
                    SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器

                    DataTable dt = new DataTable(); // 实例化数据表

                    myda.Fill(dt); // 保存数据 

                    DataRow student = dt.Rows[0];

                    if (student.Field<string>("ssn").Trim() == toolStripTextBox1.Text && toolStripTextBox2.Text == toolStripTextBox1.Text)
                    {
                        loginStatus = true;
                        MessageBox.Show("登录成功");
                    }
                    else
                    {
                        //MessageBox.Show(student.Field<string>("ssn").Trim());
                        MessageBox.Show("登录失败");
                    }

                    conn.Close(); // 关闭数据库连接
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息：您输入的学号出现错误");
            }
        }


        private void allcourseByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (loginStatus)
                {
                    String connsql = "server=.;database=test;integrated security=SSPI"; // 数据库连接字符串,database设置为自己的数据库名，以Windows身份验证
                    try
                    {
                        using (SqlConnection conn = new SqlConnection())
                        {
                            conn.ConnectionString = connsql;
                            conn.Open(); // 打开数据库连接
                            String sql = "select * from course"; // 查询语句
                            SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                            DataTable dt = new DataTable(); // 实例化数据表
                            myda.Fill(dt); // 保存数据 
                            dataGridView1.DataSource = dt;
                            conn.Close(); // 关闭数据库连接
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("错误信息：toolStripLabel2_Click 我的课程");
                    }
                }
                else
                    MessageBox.Show("请您输入账号密码！");
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void mycoursetoolStripButton2_Click_1(object sender, EventArgs e)
        {
            if (loginStatus)
            {
                String connsql = "server=.;database=test;integrated security=SSPI"; // 数据库连接字符串,database设置为自己的数据库名，以Windows身份验证
                try
                {
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = connsql;
                        conn.Open(); // 打开数据库连接
                        String sql = "select * from course where course_num in " +
                            "(select course_number from study where stu_ssn ='" + toolStripTextBox1.Text + "')"; // 查询语句
                        SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                        DataTable dt = new DataTable(); // 实例化数据表
                        myda.Fill(dt); // 保存数据 
                        dataGridView1.DataSource = dt;
                        conn.Close(); // 关闭数据库连接
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("错误信息：没有此人");
                }
            }
            else
                MessageBox.Show("请您输入账号密码！");
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            String connsql = "server=.;database=test;integrated security=SSPI"; // 数据库连接字符串,database设置为自己的数据库名，以Windows身份验证
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = connsql;
                    conn.Open(); // 打开数据库连接 
                    int i = dataGridView1.CurrentRow.Index;
                    string sql = "insert into study(course_number,stu_ssn,chengji) values('" + this.dataGridView1.Rows[i].Cells[1].Value.ToString().Trim().ToString() + "','" +
           this.toolStripTextBox1.Text.Trim().ToString() + "'," + 0 + ")";

                    SqlCommand com = conn.CreateCommand();
                    com.CommandText = sql;
                    com.ExecuteNonQuery();
                    MessageBox.Show("选课成功！");
                    conn.Close(); // 关闭数据库连接
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息：已经修过！");
            }
        }

        private void deltoolStripButton2_Click(object sender, EventArgs e)
        {
            String connsql = "server=.;database=test;integrated security=SSPI"; // 数据库连接字符串,database设置为自己的数据库名，以Windows身份验证
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = connsql;
                    conn.Open(); // 打开数据库连接 
                    int i = dataGridView1.CurrentRow.Index;
                    string sql = "delete from study where course_number = '" + this.dataGridView1.Rows[i].Cells[1].Value.ToString().Trim().ToString() + "' and stu_ssn = '" +
           this.toolStripTextBox1.Text.Trim().ToString() + "'";

                    SqlCommand com = conn.CreateCommand();
                    com.CommandText = sql;
                    com.ExecuteNonQuery();
                    MessageBox.Show("退课成功！");
                    conn.Close(); // 关闭数据库连接
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息：没有该课程！");
            }
        }

        private void gradetoolStripButton2_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripButton btn = sender as ToolStripButton;
            bool isChecked = btn.Checked;
            if (isChecked)
            {
                if (loginStatus)
                {
                    String connsql = "server=.;database=test;integrated security=SSPI"; // 数据库连接字符串,database设置为自己的数据库名，以Windows身份验证
                    try
                    {
                        using (SqlConnection conn = new SqlConnection())
                        {
                            conn.ConnectionString = connsql;
                            conn.Open(); // 打开数据库连接
                            String sql = "select course_name, chengji from study, course where course_num = course_number and stu_ssn = '" + toolStripTextBox1.Text + "'";
                            SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                            DataTable dt = new DataTable(); // 实例化数据表
                            myda.Fill(dt); // 保存数据 
                            dataGridView2.Visible = true;
                            dataGridView2.DataSource = dt;
                            conn.Close(); // 关闭数据库连接
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("错误信息：没有此人");
                    }
                }
                else
                    MessageBox.Show("请您输入账号密码！");
            }
            else
            {
                dataGridView2.Visible = false;
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }
    }

}
