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
namespace Kitaplik_Proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=localhost;Initial Catalog=Kitaplik;Integrated Security=True");
        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Kitaplar",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            durum = "0";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele();
        }
        string durum = "";
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Insert into Kitaplar (KitapAd,Yazar,Tur,Sayfa,Durum) values (@p1,@p2,@p3,@p4,@p5)",baglanti);
            komut.Parameters.AddWithValue("@p1",txt_kitapAd.Text);
            komut.Parameters.AddWithValue("@p2", txt_kitapYazar.Text);
            komut.Parameters.AddWithValue("@p3", cmb_tur.Text);
            komut.Parameters.AddWithValue("@p4", txt_sayfa.Text);
            komut.Parameters.AddWithValue("@p5", durum);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap sisteme kaydedilmiştir.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Listele();

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            durum = "1";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txt_kitapid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txt_kitapAd.Text=dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txt_kitapYazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmb_tur.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txt_sayfa.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString()=="True")
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete  from Kitaplar where KitapID=@p1",baglanti);
            komut.Parameters.AddWithValue("@p1", txt_kitapid.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap listeden silindi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            Listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update Kitaplar set KitapAd=@p1,Yazar=@p2,Tur=@p3,Sayfa=@p4,Durum=@p5 where KitapID=@p6", baglanti);
            komut.Parameters.AddWithValue("@p1", txt_kitapAd.Text);
            komut.Parameters.AddWithValue("@p2", txt_kitapYazar.Text);
            komut.Parameters.AddWithValue("@p3", cmb_tur.Text);
            komut.Parameters.AddWithValue("@p4", txt_sayfa.Text);
            if (radioButton1.Checked==true)
            {
                komut.Parameters.AddWithValue("@p5", durum);
            }
            if (radioButton2.Checked==true)
            {
                komut.Parameters.AddWithValue("@p5", durum);
            }
            komut.Parameters.AddWithValue("@p6", txt_kitapid.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt güncellendi.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Listele();
        }

        private void btnKitapBul_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * from Kitaplar where KitapAd=@p1",baglanti);
            komut.Parameters.AddWithValue("@p1", txt_kitapbul.Text);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            Ara(txt_kitapbul.Text);
        }   

        private void Ara(string kitapadi)
        {
            SqlCommand komut = new SqlCommand("Select * from Kitaplar where KitapAd like '%" + kitapadi + "%'", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void txt_kitapbul_TextChanged(object sender, EventArgs e)
        {
            string uzunluk=txt_kitapbul.Text;
            if (uzunluk.Length>1)
            {
                Ara(txt_kitapbul.Text);
            }
            else
            {
                Listele();
            }

        }
    }
}

