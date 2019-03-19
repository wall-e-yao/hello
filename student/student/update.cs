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
    public partial class update : Form
    {
        public update(List<String> info)
        {
            InitializeComponent();
            ssn.Text = info[0];
            name.Text = info[1];
            sex.Text = info[2];
            birthday.Text = info[3];
            address.Text = info[4];
            room.Text = info[5];
            telephone.Text = info[6];
            grade.Text = info[7];
            myclass.Text = info[8];
            cnum.Text = info[9];
            classleader.Text = info[10];

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String connsql = "server=.;database=test;integrated security=SSPI"; // 数据库连接字符串,database设置为自己的数据库名，以Windows身份验证
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = connsql;
                    conn.Open(); // 打开数据库连接 
                    string sql = "update student set name = '"+name.Text+"',sex = '"+sex.Text+ "',birthday = '" + birthday.Text + "', address = '" + address.Text + "',room = '" + room.Text + "',telephone = '" + telephone.Text + "', grade = '" + grade.Text + "',class = '" + myclass.Text + "', cssn = '" + cnum.Text + "',classleader = '" + classleader.Text + "'where ssn = '"+ssn.Text+"'";
                    SqlCommand com = conn.CreateCommand();
                    com.CommandText = sql;
                    com.ExecuteNonQuery();
                    MessageBox.Show("更新成功！");
                    conn.Close(); // 关闭数据库连接
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息：更新错误！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
