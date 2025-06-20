using Guna.UI2.WinForms; // Guna UI2 kontrollerini kullanmak için
using System; // Temel .NET sınıfları
using System.Collections.Generic; // Koleksiyon sınıfları
using System.ComponentModel; // Bileşen desteği
using System.Data; // Veri işlemleri için
using System.Data.OleDb; // Access veri tabanı bağlantısı için
using System.Drawing; // Grafiksel işlemler için
using System.Linq; // LINQ işlemleri için
using System.Text; // Metin işlemleri için
using System.Threading.Tasks; // Asenkron işlemler için
using System.Windows.Forms; // Form ve kontroller için

namespace Sinema_Otomasyon // Projenin namespace'i
{
    public partial class Bilgilerim : Form // Bilgilerim formu
    {
        public Bilgilerim() // Yapıcı metot
        {
            InitializeComponent(); // Form bileşenlerini başlat
        }

        private void Bilgilerim_Load(object sender, EventArgs e) // Form yüklendiğinde çalışır
        {
            Guna2TextBoxlariSaltGosterimYap(); // Textbox'ları salt okunur yap
            KullaniciBilgileriniGetir(); // Kullanıcı bilgilerini veritabanından getir
            Kisayol.ProfilFotografGuncelle(profilfotograf); // Profil fotoğrafını güncelle
        }

        private void GuncelleBtn_Click(object sender, EventArgs e) // Güncelle butonuna basıldığında
        {
            try // Hata yakalama bloğu
            {
                OleDbConnection baglanti = new OleDbConnection(Giris.veribaglanti); // Veritabanı bağlantısı oluştur
                baglanti.Open(); // Bağlantıyı aç
                string sorgu = "UPDATE kisiler SET profilfotografi=?  WHERE mail=?"; // SQL sorgusu
                using (OleDbCommand komut = new OleDbCommand(sorgu, baglanti)) // Komut oluştur
                {
                    komut.Parameters.AddWithValue("?", Giris.ProfilFoto); // Profil fotoğrafı parametresi
                    komut.Parameters.AddWithValue("?", Giris.girilenEmail); // Email parametresi
                    int kontrol = komut.ExecuteNonQuery(); // Sorguyu çalıştır
                    if (kontrol > 0) // Güncelleme başarılıysa
                    {
                        MessageBox.Show("Güncelleme Başarılı!  " + Giris.girilenEmail, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information); // Bilgi mesajı göster
                    }
                }
            }
            catch (Exception ex) // Hata oluşursa
            {
                MessageBox.Show("Güncelleme Hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); // Hata mesajı göster
            }
        }

        private void KullaniciBilgileriniGetir() // Veritabanından kullanıcı bilgilerini getirir
        {
            OleDbConnection baglanti = new OleDbConnection(Giris.veribaglanti); // Veritabanı bağlantısı oluştur
            string sorgu = "SELECT isim,soyisim,mail,telno FROM kisiler WHERE mail = ?"; // SQL sorgusu
            using (OleDbCommand komut = new OleDbCommand(sorgu, baglanti)) // Komut oluştur
            {
                komut.Parameters.AddWithValue("?", Giris.girilenEmail); // Email parametresi

                baglanti.Open(); // Bağlantıyı aç
                using (OleDbDataReader dr = komut.ExecuteReader()) // Okuyucu ile verileri oku
                    if (dr.Read()) // Veri varsa
                    {
                        AdTextBox.Text = dr["isim"].ToString(); // Ad kutusuna veriyi yaz
                        SoyadTextBox.Text = dr["soyisim"].ToString(); // Soyad kutusuna veriyi yaz
                        MailTextBox.Text = dr["mail"].ToString(); // Mail kutusuna veriyi yaz
                        TelTextBox.Text = dr["telno"].ToString(); // Telefon kutusuna veriyi yaz
                    }

                baglanti.Close(); // Bağlantıyı kapat
            }
        }

        private void GeriDonBtn_Click(object sender, EventArgs e) // Geri dön butonuna basıldığında
        {
            Kisayol.ProfilFotografGuncelle(Kullanici.bilgilerprofil); // Profil fotoğrafını güncelle
            this.Close(); // Formu kapat
        }

        private void profilfotograf_Click(object sender, EventArgs e) // Profil fotoğrafına tıklanınca
        {
            OpenFileDialog dosyasec = new OpenFileDialog(); // Dosya seçme penceresi oluştur
            dosyasec.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp"; // Sadece resim dosyaları
            dosyasec.Title = "Film Broşürü Seç"; // Pencere başlığı
            if (dosyasec.ShowDialog() == DialogResult.OK) // Eğer dosya seçildiyse
            {
                Giris.ProfilFoto = dosyasec.FileName; // Seçilen dosya yolunu kaydet
                profilfotograf.Image = Image.FromFile(Giris.ProfilFoto); // Resmi yükle
                profilfotograf.SizeMode = PictureBoxSizeMode.StretchImage; // Resmi boyuta uydur
            }
        }

        private void Guna2TextBoxlariSaltGosterimYap() // Textbox'ları salt (okunur) hale getir
        {
            Guna.UI2.WinForms.Guna2TextBox[] kutular = // Salt yapılacak textbox dizisi
            {
                AdTextBox,
                SoyadTextBox,
                MailTextBox,
                TelTextBox
            };

            foreach (var tBox in kutular) // Her kutu için
            {
                tBox.ReadOnly = true; // Yazılamaz yap
                tBox.TabStop = false; // Sekme ile geçilemez
                tBox.Cursor = Cursors.Default; // İmleç normal

                // Gri arka plan olmaması için manuel ayarlar
                tBox.FillColor = Color.White; // Arka plan beyaz
                tBox.ForeColor = Color.Black; // Yazı rengi siyah
                tBox.BorderColor = Color.LightGray; // Kenar rengi açık gri

                // Gölgelendirme efekti vs. yoksa daha sade görünüm için
                tBox.DisabledState.FillColor = Color.White; // Devre dışı arka plan
                tBox.DisabledState.ForeColor = Color.Black; // Devre dışı yazı rengi
                tBox.DisabledState.BorderColor = Color.LightGray; // Devre dışı kenar rengi
                tBox.DisabledState.PlaceholderForeColor = Color.Black; // Devre dışı placeholder rengi
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e) // Hakkımızda butonu
        {
            MessageBox.Show("Programı yapanlar ;\nArda GÜLAY,\n Cansın İÇİM,\n Ege Yağız KAYA,\n Mert OKUMUŞ."); // Bilgi mesajı
        }
    }
}
