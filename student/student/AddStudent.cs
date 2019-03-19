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
    public partial class AddStudent : Form
    {
        public AddStudent()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String connsql = "server=.;database=test;integrated security=SSPI"; // 数据库连接字符串,database设置为自己的数据库名，以Windows身份验证
            try
            {
                string newssn = ssn.Text;
                string newname = name.Text;
                string newsex = sex.Text;
                string newbirthday = birthday.Text;
                string newaddress = address.Text;
                string newroom = room.Text;
                string newtelephone = telephone.Text;
                string newgrade = grade.Text;
                string newclass = myclass.Text;
                string newcssn = cnum.Text;
                string newclassleader = classleader.Text;

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = connsql;
                    conn.Open(); // 打开数据库连接 

                    string sql = "insert into student values" +
                        "('" + newssn + "','" + newname + "','" + newsex + "','" + newbirthday + "','" + newaddress +
                        "','" + newroom + "','" + newtelephone + "','" + newgrade + "','" + newclass + "','"
                        + newcssn + "','" + newclassleader + "')";
                    //string sql = "insert into student values('20160009','张三','男','19970621','台湾','C25-304','18845674569','2016','5','001','否')";

                    SqlCommand com = conn.CreateCommand();

                    com.CommandText = sql;

                    com.ExecuteNonQuery();

                    MessageBox.Show("添加成功！");

                    conn.Close(); // 关闭数据库连接
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息：添加失败！" + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
