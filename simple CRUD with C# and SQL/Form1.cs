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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace simple_CRUD_with_C__and_SQL
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-ECM8IVK\\SQLEXPRESS;Initial Catalog=CRUD;Integrated Security=True;");
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string username = textBox2.Text;
            string password = textBox3.Text;

            if(id != "" && username != "" && password != "")
            {
                conn.Open();
                string checkquery = "SELECT COUNT(1) from tbl_crud WHERE id = @id";
                SqlCommand checkcmd = new SqlCommand(checkquery, conn);
                checkcmd.Parameters.AddWithValue("@id", int.Parse(id));
                int check = (int)checkcmd.ExecuteScalar();
                conn.Close();
                if(check == 0)
                {
                    conn.Open();
                    string query = "INSERT into tbl_crud values(@id,@username,@password)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", int.Parse(id));
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Successfully created.");
                }
                else
                {
                    MessageBox.Show("ID already existed.");
                }
            }
            else
            {
                MessageBox.Show("Please enter your id, username, and password");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            string query = "SELECT * from tbl_crud";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string username = textBox2.Text;
            string password = textBox3.Text;

            if (id != "" && username != "" && password != "")
            {
                conn.Open();
                string checkquery = "SELECT COUNT(1) from tbl_crud WHERE id = @id";
                SqlCommand checkcmd = new SqlCommand(checkquery, conn);
                checkcmd.Parameters.AddWithValue("@id", int.Parse(id));
                int check = (int)checkcmd.ExecuteScalar();
                conn.Close();
                if (check > 0)
                {
                    conn.Open();
                    string query = "UPDATE tbl_crud SET username = @username, password = @password WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", int.Parse(id));
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Successfully updated.");
                }
                else
                {
                    MessageBox.Show("ID not found.");
                }
            }
            else
            {
                MessageBox.Show("Please enter your id, username, and password");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;

            if(id != "")
            {
                conn.Open();
                string checkquery = "SELECT COUNT(1) from tbl_crud WHERE id = @id";
                SqlCommand checkcmd = new SqlCommand(checkquery, conn);
                checkcmd.Parameters.AddWithValue("@id", int.Parse(id));
                int checkrow = (int)checkcmd.ExecuteScalar();
                conn.Close();
                if (checkrow > 0)
                {
                    string query = "DELETE from tbl_crud where id = @id";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Successfully deleted.");
                }
                else
                {
                    MessageBox.Show("ID not found.");
                }
            }
            else
            {
                MessageBox.Show("Please enter ID.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }
    }
}
