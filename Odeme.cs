using System; // Temel .NET bileşenleri
using System.Collections.Generic; // Generic koleksiyonları kullanmak için
using System.ComponentModel; // Bileşen modelleme için
using System.Data; // Veri işlemleri için
using System.Data.OleDb; // Access veritabanı bağlantısı için
using System.Drawing; // Grafik işlemleri için
using System.Linq; // LINQ sorguları için
using System.Text; // Metin işlemleri için
using System.Threading.Tasks; // Asenkron işlemler için
using System.Windows.Forms; // Windows Formları için
using System.Net; // Ağ işlemleri için
using System.Net.Mail; // Mail gönderimi için

namespace Sinema_Otomasyon
{
    public partial class Odeme : Form
    {
        private string _koltukNo; // Seçilen koltukların bilgisi
        public Odeme()
        {
            InitializeComponent(); // Form bileşenlerini başlatır
            ToplamUcretHesaplama(); // Ücret hesaplama işlemi yapılır
            toplamucret.Text += Giris.ToplamUcret.ToString() + "₺"; // Toplam ücreti ekrana yazdırır
            guna2TextBoxKartNo.Text = ""; // Kart numarası alanını boşaltır
        }

        private void ToplamUcretHesaplama()
        {
            int biletsayi = BiletSecim.BiletTur.Count; // Alınan bilet sayısını alır
            int tam = 0; // Tam bilet sayısı
            int ogrenci = 0; // Öğrenci bilet sayısı
            int cocuk = 0; // Çocuk bilet sayısı
            for (int i = 0; i < biletsayi; i++) // Bütün biletleri kontrol eder
            {
                if (BiletSecim.BiletTur[i] == "Tam")
                {
                    tam++; // Tam bilet sayısını arttırır
                }
                else if (BiletSecim.BiletTur[i] == "Öğrenci")
                {
                    ogrenci++; // Öğrenci bilet sayısını arttırır
                }
                else if (BiletSecim.BiletTur[i] == "Çocuk")
                {
                    cocuk++; // Çocuk bilet sayısını arttırır
                }
            }
            // Toplam ücreti tam bilet + öğrenci (%25 indirimli) + çocuk (%50 indirimli) olarak hesaplar
            Giris.ToplamUcret = (tam * Giris.FilmUcret) + ((ogrenci * Giris.FilmUcret) * 0.75) + ((cocuk * Giris.FilmUcret) * 0.5);
        }

        private void Odeme_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde yapılacak işlemler (şu an boş)
        }

        private void KartİsimTextBox_TextChanged(object sender, EventArgs e)
        {
            guna2TextBoxisimsol.Text = KartİsimTextBox.Text; // Kart ismini sol tarafta gösterir
        }

        private void GuvenlikTextBox_TextChanged(object sender, EventArgs e)
        {
            guna2TextBoxcvvsol.Text = GuvenlikTextBox.Text; // CVV'yi sol tarafta gösterir
        }

        private void AySecCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            guna2TextBoxAy.Text = AySecCombo.Text; // Seçilen ayı sol alana yazar
        }

        private void YilSecCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            guna2TextBoxYil.Text = YilSecCom.Text; // Seçilen yılı sol alana yazar
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            // Seçilen koltukları ve bilet türlerini birleştirerek string oluşturur
            for (int i = 0; i < Secim.koltukİsim.Count; i++)
            {
                _koltukNo += Secim.koltukİsim[i] + "-" + BiletSecim.BiletTur[i] + " | ";
            }

            // Kart bilgilerini kontrol eder
            bool sonuc = Kisayol.KartBilgiKontrol(KartİsimTextBox.Text, guna2TextBoxKartNo.Text, AySecCombo.Text, YilSecCom.Text, GuvenlikTextBox.Text);

            if (sonuc == true) // Kart bilgileri doğruysa
            {
                using (OleDbConnection baglanti = new OleDbConnection(Giris.veribaglanti)) // Veritabanı bağlantısı kurar
                {
                    try
                    {
                        if (guna2TextBoxKartNo.Text == "1111111111111111") // Test kart numarası kontrolü
                        {
                            baglanti.Open(); // Veritabanı bağlantısını açar
                            string sorgu = "INSERT INTO odeme (id,kisiadi,kisisoyad,tel_no,mail,filmadi,koltukno,seans_bilgi,t_fiyat) " +
                                           "VALUES (?,?,?,?,?,?,?,?,?)"; // SQL sorgusu
                            using (OleDbCommand komut = new OleDbCommand(sorgu, baglanti)) // Komutu hazırlar
                            {
                                komut.Parameters.AddWithValue("?", Giris.Randomİd()); // Rastgele ID
                                komut.Parameters.AddWithValue("?", Giris.Kullaniciadi); // Kullanıcı adı
                                komut.Parameters.AddWithValue("?", Giris.KullaniciSoyadi); // Kullanıcı soyadı
                                komut.Parameters.AddWithValue("?", Giris.KullaniciTel); // Kullanıcı telefon
                                komut.Parameters.AddWithValue("?", Giris.girilenEmail); // Kullanıcı e-posta
                                komut.Parameters.AddWithValue("?", Anasayfa.secilenfilmadi); // Seçilen film adı
                                komut.Parameters.AddWithValue("?", _koltukNo); // Koltuk bilgileri
                                komut.Parameters.AddWithValue("?", Secim.seansTarihi); // Seans bilgisi
                                komut.Parameters.AddWithValue("?", Giris.ToplamUcret.ToString()); // Toplam fiyat
                                int etkilenenSatir = komut.ExecuteNonQuery(); // Sorguyu çalıştırır
                            }

                            // Başarılı ödeme işlemi sonrası e-posta gönderir
                            Kisayol.BasariliOdeme(Giris.girilenEmail, Giris.Kullaniciadi, Anasayfa.secilenfilmadi, _koltukNo, Secim.seansTarihi);
                            this.Close(); // Formu kapatır
                        }
                        else // Kart numarası geçersizse
                        {
                            MessageBox.Show("Kart Bilgileri Hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); // Hata mesajı
                            KartİsimTextBox.Clear(); // Alanları temizler
                            guna2TextBoxKartNo.Clear();
                            AySecCombo.SelectedIndex = -1;
                            YilSecCom.SelectedIndex = -1;
                            GuvenlikTextBox.Clear();
                        }
                    }
                    catch (Exception ex) // Hata oluşursa
                    {
                        MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); // Hata mesajı göster
                    }
                }
            }
            else // Kart bilgileri yanlışsa
            {
                MessageBox.Show("Kart Bilgileri Hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); // Hata mesajı
                KartİsimTextBox.Clear(); // Alanları temizler
                guna2TextBoxKartNo.Clear();
                AySecCombo.SelectedIndex = -1;
                YilSecCom.SelectedIndex = -1;
                GuvenlikTextBox.Clear();
            }
        }

        private void TextBoxKartNo_TextChanged(object sender, EventArgs e)
        {
            guna2TextBoxKartNoSol.Text = guna2TextBoxKartNo.Text; // Kart numarasını sol bölüme yazar
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Geliştirici isimlerini gösterir
            MessageBox.Show("Programı yapanlar ;Arda GÜLAY, Cansın İÇİM, Ege Yağız KAYA, Mert OKUMUŞ.");
        }

        private void guna2TextBoxKartNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece rakam ve kontrol karakterlerine izin verir
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Diğer karakterleri engeller
            }
        }

        private void GuvenlikTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece rakam ve kontrol karakterlerine izin verir
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Diğer karakterleri engeller
            }
        }
    }
}