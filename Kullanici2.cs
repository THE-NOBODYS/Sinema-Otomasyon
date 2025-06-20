using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Sinema_Otomasyon.Anasayfa;

namespace Sinema_Otomasyon
{
    public partial class Kullanici : Form
    {
        public Kullanici()
        {
            InitializeComponent();
            KullaniciBilgileriniGetir();
        }


        private void Kullanici_Load(object sender, EventArgs e)
        {
            MessageBox.Show(Giris.girilenEmail);
           
        }


        private void KullaniciBilgileriniGetir()
        {
            try
            {
                // Bağlantıyı Giris formundaki static değişkenden alıyoruz
                using (OleDbConnection baglanti = new OleDbConnection(Giris.veribaglanti))
                {
                    baglanti.Open();
                    // Access veritabanında parametre için @ yerine ? kullanılır
                    string sorgu = "SELECT isim, soyisim, mail, telno FROM kisiler WHERE mail = ?";
                    using (OleDbCommand komut = new OleDbCommand(sorgu, baglanti))
                    {
                        // Access için parametre eklerken ? işareti için değer ekliyoruz
                        komut.Parameters.AddWithValue("?", Giris.girilenEmail);

                        // ExecuteReader ile verileri okuyoruz
                        using (OleDbDataReader reader = komut.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // TextBox'lara bilgileri yaz
                                AdTextBox.Text = reader["isim"].ToString();
                                SoyadTextBox.Text = reader["soyisim"].ToString();
                                MailTextBox.Text = reader["mail"].ToString();
                                TelNoTextBox.Text = reader["telno"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanıcı bilgileri yüklenirken hata oluştu: " + ex.Message);
            }
        }

      
    
    }
}
