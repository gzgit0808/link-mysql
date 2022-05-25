using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 数据库连接
{
    public partial class Form1 : Form
    {
        MySqlConnection conn; //连接数据库对象
        MySqlDataAdapter mda; //适配器变量
        DataSet ds;  //临时数据集

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string M_str_sqlcon = "server=localhost;user id=root;password=gz20020808;database=sql";                                                                                              //创建数据库连接对象
            conn = new MySqlConnection(M_str_sqlcon);
            try
            {
                //打开数据库连接
                conn.Open();
                MessageBox.Show("数据库已经连接了！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "select * from course";
            mda = new MySqlDataAdapter(sql, conn);
            ds = new DataSet();
            mda.Fill(ds, "course");
            //显示数据
            dataGridView1.DataSource = ds.Tables["course"];
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (mda == null || ds == null)
            {
                MessageBox.Show("请先导入数据");
                return;
            }
            try
            {
                string msg = "确实要修改吗？";
                if (1 == (int)MessageBox.Show(msg, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
                {
                    MySqlCommandBuilder builder = new MySqlCommandBuilder(mda); //命令生成器。
                    //适配器会自动更新用户在表上的操作到数据库中
                    mda.Update(ds, "course");
                    MessageBox.Show("修改成功", "提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (mda == null || ds == null)
            {
                MessageBox.Show("请先导入数据");
                return;
            }
            try
            {
                string msg = "确实要添加此条数据吗？";
                if (1 == (int)MessageBox.Show(msg, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
                {
                    MySqlCommandBuilder builder = new MySqlCommandBuilder(mda);
                    mda.Update(ds, "course");
                    MessageBox.Show("添加成功", "提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            int id = (int)dataGridView1.Rows[index].Cells[0].Value;
            string sql = "delete from course where id=" + id + "";
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            int i = cmd.ExecuteNonQuery();
            if (i < 0)
            {
                conn.Close();
                MessageBox.Show("删除失败");
                return;
            }
            conn.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
