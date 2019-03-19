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
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = true;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            String connsql = "server=.;database=test;integrated security=SSPI"; // 数据库连接字符串,database设置为自己的数据库名，以Windows身份验证
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = connsql;
                    conn.Open(); // 打开数据库连接
                    String sql = "select * from student"; // 查询语句
                    SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器
                    DataTable dt = new DataTable(); // 实例化数据表
                    myda.Fill(dt); // 保存数据 
                    dataGridView1.DataSource = dt;
                    conn.Close(); // 关闭数据库连接
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息：数据库异常");
            }
        }

        private void addtoolStripButton1_Click(object sender, EventArgs e)
        {
            AddStudent As = new AddStudent();
            As.ShowDialog();

            toolStripButton4_Click(sender, e);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void searchtoolStripButton5_Click(object sender, EventArgs e)
        {
            String connsql = "server=.;database=test;integrated security=SSPI"; // 数据库连接字符串,database设置为自己的数据库名，以Windows身份验证
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = connsql;
                    conn.Open(); // 打开数据库连接
                    String sql = "select * from student where ssn='"+ toolStripTextBox1.Text+"'"; // 查询语句

                    SqlDataAdapter myda = new SqlDataAdapter(sql, conn); // 实例化适配器

                    DataTable dt = new DataTable(); // 实例化数据表
                    myda.Fill(dt); // 保存数据 
                    dataGridView1.DataSource = dt;
                    conn.Close(); // 关闭数据库连接
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息：数据库异常");
            }
        }

        private void deletetoolStripButton3_Click(object sender, EventArgs e)
        {
            String connsql = "server=.;database=test;integrated security=SSPI"; // 数据库连接字符串,database设置为自己的数据库名，以Windows身份验证
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = connsql;
                    conn.Open(); // 打开数据库连接 

                    int i = dataGridView1.CurrentRow.Index;
                    string sql = "delete from student where ssn = '" + this.dataGridView1.Rows[i].Cells[0].Value.ToString()+ "'"; 
                    SqlCommand com = conn.CreateCommand();
                    com.CommandText = sql;
                    com.ExecuteNonQuery();
                    MessageBox.Show("删除成功！");
                    conn.Close(); // 关闭数据库连接

                    toolStripButton4_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息：删除失败！");
            }
        }

        private void updatetoolStripButton2_Click(object sender, EventArgs e)
        {
            List<String> info = new List<String>();

            for(int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                info.Add(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value.ToString().Trim().ToString());
            }
            update u = new update(info);
            u.ShowDialog();
            toolStripButton4_Click(sender, e);
        }
    }
}
