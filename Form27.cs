﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHMS
{
    public partial class Form27 : Form
    {
        public Form27()
        {
            InitializeComponent();
        }

        void BindData()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-40HQ0BH;Initial Catalog=username;Persist Security Info=True;User ID=sa;Password=AsDf13@2");
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM docinfo", con);
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM docuser", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            da.Fill(dt);
            da1.Fill(dt1);
            dataGridView1.DataSource = dt;
            dataGridView2.DataSource = dt1;
        }
        private void Form27_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-40HQ0BH;Initial Catalog=username;Persist Security Info=True;User ID=sa;Password=AsDf13@2");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM docinfo order by ID", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.RowTemplate.Height = 75;
            dataGridView1.DataSource = dt;
            DataGridViewImageColumn pic1 = new DataGridViewImageColumn();
            pic1 = (DataGridViewImageColumn)dataGridView1.Columns[1];
            pic1.ImageLayout = DataGridViewImageCellLayout.Stretch;

            //con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM docuser order by ID", con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            dt1.Clear();
            da1.Fill(dt1);
            //dataGridView2.RowTemplate.Height = 75;
            dataGridView2.DataSource = dt1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-40HQ0BH;Initial Catalog=username;Persist Security Info=True;User ID=sa;Password=AsDf13@2");
            con.Open();

            if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter a doctor id for remove.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                SqlCommand check_User_Name = new SqlCommand("SELECT ID FROM docinfo WHERE (ID = @ID)", con);
                check_User_Name.Parameters.AddWithValue("@ID", textBox1.Text);
                SqlDataReader reader = check_User_Name.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    reader.Dispose();

                    SqlCommand cmd = new SqlCommand("DELETE docinfo WHERE ID=@ID", con);
                    SqlCommand cmd1 = new SqlCommand("DELETE docuser WHERE ID=@ID", con);
                    cmd.Parameters.AddWithValue("@ID", int.Parse(textBox1.Text));
                    cmd1.Parameters.AddWithValue("@ID", int.Parse(textBox1.Text));
                    /*cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Age", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Password", textBox4.Text);*/
                    cmd.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    textBox1.Text = "";
                    /*textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";*/
                    MessageBox.Show("Doctor removed successfully!");
                    BindData();
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form22 a = new Form22();
            a.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
