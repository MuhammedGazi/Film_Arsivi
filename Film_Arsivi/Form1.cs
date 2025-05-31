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
using System.Data.Common;
using Microsoft.Web.WebView2.WinForms;

namespace Film_Arsivi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //Data Source=DESKTOP-GLCB4AC;Initial Catalog=csharpEgitimEFTravelDB;Integrated Security=True

        SqlConnection conn=new SqlConnection();

        void Flimler()
        {
            conn.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from TBLFLIMLER",conn);
            DataTable dt=new DataTable();
            dataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Flimler();
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into TBLFLIMLER (AD,KATEGORI,LINK) values (@p1,@p2,@p3)", conn);
            cmd.Parameters.AddWithValue("@p1", txtFilmad.Text);
            cmd.Parameters.AddWithValue("@p2",txtKategori.Text);
            cmd.Parameters.AddWithValue("@p3",txtLink.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            Flimler();
        }

        private async void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            string link=dataGridView1.Rows[secilen].Cells[3].Value.ToString();

            await webView21.EnsureCoreWebView2Async(null);
            webView21.Source=new Uri(link);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("eleştiri ve önerilerinizi lütfen bildiriniz","hakkımızda",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        Color[] renkler = { Color.Gray, Color.LightBlue, Color.LightGreen, Color.LightPink, Color.LightYellow };
        int renkIndex = 0; 

        private void button4_Click(object sender, EventArgs e)
        {
            renkIndex = (renkIndex + 1) % renkler.Length;
            this.BackColor = renkler[renkIndex];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
