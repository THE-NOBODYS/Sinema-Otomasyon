using System; // Temel sistem bileşenleri
using System.Collections.Generic; // Koleksiyonlar için
using System.ComponentModel; // Bileşen model desteği
using System.Data; // Veri sınıfları
using System.Data.OleDb; // Access veritabanı bağlantısı için
using System.Drawing; // Grafik işlemleri için
using System.Linq; // LINQ işlemleri için
using System.Text; // Metin işlemleri için
using System.Threading.Tasks; // Asenkron işlemler için
using System.Windows.Forms; // Windows form bileşenleri için

namespace Sinema_Otomasyon // Proje namespace'i
{
    public partial class Giris : Form // Giriş formu sınıfı
    {
        public static string veribaglanti = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Lenovo User\Desktop\Sinema Otomasyon V13 SON\Veri Tabanı\sinema.accdb;Persist Security Info=False;"; // Veritabanı bağlantı cümlesi
        public static string girilenEmail = ""; // Girilen e-posta adresi
        public static string Kullaniciadi = ""; // Kullanıcının adı
        public static string KullaniciSoyadi = ""; // Kullanıcının soyadı
        public static string KullaniciTel = ""; // Kullanıcının telefon numarası
        public static string ProfilFoto = ""; // Kullanıcının profil fotoğrafı
        public static double ToplamUcret = 0; // Toplam ücret
        public static double FilmUcret = 0; // Film ücreti
        private Admin admin; // Admin formu nesnesi

        public Giris() // Yapıcı metot
        {
            InitializeComponent(); // Form bileşenlerini başlat
            admin = new Admin(); // Admin formunu oluştur
        }

        private void Giris_Load(object sender, EventArgs e) // Form yüklendiğinde çalışır
        {
        }

        public static long Randomİd() // Rastgele ID üretir
        {
            long min = 1000000000L; // Minimum değer
            long max = 9999999999L; // Maksimum değer

            long sayi;
            do
            {
                byte[] bytes = Guid.NewGuid().ToByteArray(); // Yeni GUID oluştur
                sayi = BitConverter.ToInt64(bytes, 0) & long.MaxValue; // Pozitif long değere çevir
            } while (sayi < 0); // Negatifse tekrar üret

            return sayi % (max - min + 1) + min; // Belirtilen aralıkta ID döndür
        }

        private void MisafirButton_Click_1(object sender, EventArgs e) // Misafir girişi butonuna basıldığında
        {
            ((Kisayol)this.Parent).FormGecis(new Anasayfa()); // Anasayfaya geç
        }

        public static void BaglantiKapat(OleDbConnection baglanti) // Veritabanı bağlantısını güvenli şekilde kapatır
        {
            if (baglanti != null && baglanti.State == System.Data.ConnectionState.Open) // Bağlantı açıksa
            {
                try
                {
                    baglanti.Close(); // Bağlantıyı kapat
                }
                catch (Exception ex) // Hata olursa
                {
                    MessageBox.Show("Bağlantı kapatma hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); // Hata mesajı göster
                }
            }
        }

        private void KayitOlButton_Click_1(object sender, EventArgs e) // Kayıt ol butonuna basıldığında
        {
            Kayit kayit = new Kayit(); // Yeni kayıt formu oluştur
            kayit.ShowDialog(); // Kayıt formunu göster
        }

        private void buttonGirisYap_Click_1(object sender, EventArgs e) // Giriş yap butonuna basıldığında
        {
            girilenEmail = TextboxEmail.Text.Trim(); // E-posta girişini al ve boşlukları temizle
            string girilenSifre = TextboxSifre.Text.Trim(); // Şifre girişini al ve boşlukları temizle
            if (TextboxEmail.Text != "" && TextboxSifre.Text != "") // Alanlar boş değilse
            {
                if (girilenEmail != "admin" && girilenSifre != "admin") // Admin değilse
                {
                    using (OleDbConnection connection = new OleDbConnection(veribaglanti)) // Veritabanı bağlantısı oluştur
                    {
                        try
                        {
                            connection.Open(); // Bağlantıyı aç
                            string sorgu = "SELECT mail,sifre,isim,soyisim,telno,profilfotografi FROM kisiler WHERE mail = ? AND sifre = ?"; // SQL sorgusu
                            using (OleDbCommand komut = new OleDbCommand(sorgu, connection)) // Komut oluştur
                            {
                                komut.Parameters.AddWithValue("?", girilenEmail); // E-posta parametresi
                                komut.Parameters.AddWithValue("?", girilenSifre); // Şifre parametresi
                                OleDbDataReader sonuc = komut.ExecuteReader(); // Sorguyu çalıştır

                                if (sonuc.HasRows) // Kullanıcı bulunduysa
                                {
                                    while (sonuc.Read()) // Verileri oku
                                    {
                                        Kullaniciadi = sonuc["isim"].ToString(); // İsim al
                                        KullaniciSoyadi = sonuc["soyisim"].ToString(); // Soyisim al
                                        KullaniciTel = sonuc["telno"].ToString(); // Telefon al
                                        ProfilFoto = sonuc["profilfotografi"].ToString(); // Profil fotoğrafı al
                                    }
                                    ((Kisayol)this.Parent).FormGecis(new Anasayfa()); // Anasayfaya geç
                                }
                                else // Kullanıcı bulunamadıysa
                                {
                                    MessageBox.Show("Hatalı E-posta veya Şifre", "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error); // Hata mesajı
                                    TextboxSifre.Focus(); // Şifre kutusuna odaklan
                                }
                            }
                        }
                        catch (Exception ex) // Bağlantı hatası olursa
                        {
                            MessageBox.Show("Hata oluştu:" + ex.Message); // Hata mesajı
                        }
                    }
                }
                else // Admin girişi
                {
                    ((Kisayol)this.Parent).FormGecis(new Admin()); // Admin formuna geç
                }
            }
            else // Boş alan varsa
            {
                MessageBox.Show("Boş Alan Bırakmayınız!"); // Uyarı ver
                TextboxSifre.Focus(); // Şifre kutusuna odaklan
            }
        }

        private void CİkisYapButton_Click(object sender, EventArgs e) // Çıkış butonuna basıldığında
        {
            Application.Exit(); // Uygulamayı kapat
        }

        private void TextboxSifre_Enter(object sender, EventArgs e) // Şifre kutusuna girildiğinde
        {
            this.AcceptButton = buttonGirisYap; // Enter tuşu ile giriş yapılmasını sağla
        }

        private void guna2Button1_Click(object sender, EventArgs e) // Hakkımızda butonuna basıldığında
        {
            MessageBox.Show("Programı yapanlar ;\nArda GÜLAY,\n Cansın İÇİM,\n Ege Yağız KAYA,\n Mert OKUMUŞ."); // Bilgi mesajı göster
        }
    }
}
