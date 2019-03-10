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
using System.IO;

namespace sql_komutları_gp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //local veritabanındaki bilgileri select ile cagırıp listbox1 e ekleyen program
            string bag = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='C:\Users\reela\onedrive\documents\visual studio 2013\Projects\sql komutları gp1\sql komutları gp1\deneme1.mdf';Integrated Security=True";
            SqlConnection con = new SqlConnection(bag);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from ogrenci order by ders_id desc", con);
            SqlDataReader dr = cmd.ExecuteReader();
            listBox1.Items.Clear();
            while (dr.Read())
            {
                listBox1.Items.Add(dr["ders_id"].ToString() + "\t" + dr["ders_name"].ToString());
            }

            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //local veritabanındaki stunlara kayıt ekleyen program(İNSERT İNTO)
            string bag = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='C:\Users\reela\onedrive\documents\visual studio 2013\Projects\sql komutları gp1\sql komutları gp1\deneme1.mdf';Integrated Security=True";
            SqlConnection con = new SqlConnection(bag);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into ogrenci(ders_name) values ('ingilizce')", con);
            int adet = cmd.ExecuteNonQuery();
            MessageBox.Show(adet.ToString() + "kayıt eklendi");
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //textbox1 de yazan metni local veritabanındaki stuna kayıt ekleyen program
            string bag = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='C:\Users\reela\onedrive\documents\visual studio 2013\Projects\sql komutları gp1\sql komutları gp1\deneme1.mdf';Integrated Security=True";
            SqlConnection con = new SqlConnection(bag);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into ogrenci(ders_name) values ('"+textBox1.Text+"')", con);
            int adet = cmd.ExecuteNonQuery();
            MessageBox.Show(adet.ToString() + "kayıt eklendi");
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //textbox1de yazan metni parametre olarak local veritabanında stuna kayıt  ekleyen program
            string bag = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='C:\Users\reela\onedrive\documents\visual studio 2013\Projects\sql komutları gp1\sql komutları gp1\deneme1.mdf';Integrated Security=True";
            SqlConnection con = new SqlConnection(bag);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into ogrenci(ders_name) values(@ders_name)", con);
            cmd.Parameters.AddWithValue("@ders_name", textBox1.Text);
            int adet = cmd.ExecuteNonQuery();
            MessageBox.Show(adet.ToString() + "kayıt eklendi");
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //local veritabanından kayıt silen program
            string bag = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='C:\Users\reela\onedrive\documents\visual studio 2013\Projects\sql komutları gp1\sql komutları gp1\deneme1.mdf';Integrated Security=True";
            SqlConnection con = new SqlConnection(bag);
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from ogrenci where ders_id=8", con);
            int adet = cmd.ExecuteNonQuery();
            MessageBox.Show(adet.ToString() + "kayıt silindi");
            con.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //listbox1de secili olan bilgiyi local veri tabanından silen program
            string bag = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='C:\Users\reela\onedrive\documents\visual studio 2013\Projects\sql komutları gp1\sql komutları gp1\deneme1.mdf';Integrated Security=True";
            SqlConnection con = new SqlConnection(bag);
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from ogrenci where ders_id=@id", con);
            int numara = Convert.ToInt16(listBox1.SelectedItem.ToString().Split('\t')[0]);
            cmd.Parameters.AddWithValue("@id", numara);
            int adet = cmd.ExecuteNonQuery();
            MessageBox.Show(adet.ToString() + " kayıt silindi");
            con.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //listbox1 de secılı olan metni veri tabanında buyuk harve ceviren program
            string bag = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='C:\Users\reela\onedrive\documents\visual studio 2013\Projects\sql komutları gp1\sql komutları gp1\deneme1.mdf';Integrated Security=True";
            SqlConnection con = new SqlConnection(bag);
            con.Open();
            SqlCommand cmd = new SqlCommand("update ogrenci  set ders_name=Upper(ders_name) where ders_id=@id", con);
            int numara = Convert.ToInt16(listBox1.SelectedItem.ToString().Split('\t')[0]);
            cmd.Parameters.AddWithValue("@id", numara);
            int adet = cmd.ExecuteNonQuery();
            MessageBox.Show(adet.ToString() + " kayıt guncellendi");
            con.Close();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //deneme txt adlı dosyanın ıcındekı metni kayıt bılgısı olarak locak veritabanına ekleyen program
            string bag = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='C:\Users\reela\onedrive\documents\visual studio 2013\Projects\sql komutları gp1\sql komutları gp1\deneme1.mdf';Integrated Security=True";
            SqlConnection con = new SqlConnection(bag);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into ogrenci(ders_name) values(@ders_ismi)", con);
            cmd.Parameters.Add("@ders_ismi", SqlDbType.NVarChar);
            StreamReader sr = new StreamReader("kategoriler.txt");
            while (!sr.EndOfStream)
            {
                string satir = sr.ReadLine();
                cmd.Parameters["@ders_ismi"].Value = satir;
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("aktarım tamam");
            sr.Close();
            con.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // local DB de bulunan verileri txt dosyasına aktaran program
            string bag = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='C:\Users\reela\onedrive\documents\visual studio 2013\Projects\sql komutları gp1\sql komutları gp1\deneme1.mdf';Integrated Security=True";
            SqlConnection con = new SqlConnection(bag);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from ogrenci", con);
            StreamWriter sw = new StreamWriter("databse.txt");
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string satir = (dr["ders_id"].ToString() + "\t" + dr["ders_name"].ToString());
                sw.WriteLine(satir);
            }
            MessageBox.Show("aktarım tamamlandı");
            sw.Close();
            con.Close(); ;
        }
    }
}
