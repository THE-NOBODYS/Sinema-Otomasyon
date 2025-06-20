using System; // Temel sistem kütüphanesi
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb; // Access veritabanı işlemleri için
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Sinema_Otomasyon;
using System.Security.AccessControl;
using System.IO; // Dosya işlemleri için

namespace Sinema_Otomasyon
{
    public partial class Kayit : Form
    {
        private OleDbConnection connection; // Veritabanı bağlantısı nesnesi
        private string isim = "", soyisim = "", telefon = "", eposta = "", sifre = "", id = "", cinsiyet = ""; // Kullanıcı bilgileri değişkenleri

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Bilgilendirme mesaj kutusu
            MessageBox.Show("Programı yapanlar ;\nArda GÜLAY,\n Cansın İÇİM,\n Ege Yağız KAYA,\n Mert OKUMUŞ.");
        }

        public Kayit()
        {
            InitializeComponent(); // Form bileşenlerini başlat
            connection = new OleDbConnection(Giris.veribaglanti); // Giriş formundaki bağlantı bilgisini kullan
        }

        private void Kayit_Load(object sender, EventArgs e)
        {
            SifreTextBox.UseSystemPasswordChar = true; // Şifre kutusunda karakterleri gizle
            ComboCinsiyet.SelectedIndex = 0; // Cinsiyet combobox ilk seçenek
        }

        private void VeritabaniBaglantisiAc()
        {
            try
            {
                if (connection.State == ConnectionState.Closed) // Bağlantı kapalıysa
                {
                    connection.Open(); // Bağlantıyı aç
                    MessageBox.Show("Veritabanı bağlantısı başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Hata oluşursa kullanıcıya göster
                MessageBox.Show("Bağlantı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VeriEkle()
        {
            isim = IsimTextbox.Text.Trim(); // İsim değerini al
            soyisim = SoyisimTextbox.Text.Trim(); // Soyisim değerini al
            telefon = TelMaskBox.Text.Trim(); // Telefon numarasını al
            eposta = MailTextBox.Text.Trim(); // E-posta adresini al
            sifre = SifreTextBox.Text.Trim(); // Şifreyi al
            cinsiyet = "";
            id = Giris.Randomİd().ToString(); // Rastgele kullanıcı ID'si oluştur

            if (ComboCinsiyet.SelectedIndex != 0) // Cinsiyet seçildiyse
            {
                cinsiyet = ComboCinsiyet.Text.Trim(); // Cinsiyet değerini al
            }
            else
            {
                MessageBox.Show("Lütfen Seçim Yapınız."); // Uyarı ver
                return;
            }
        }

        private void KullaniciyiDosyayaKaydet(string id, string isim, string soyisim, string telefon, string eposta, string sifre, string cinsiyet)
        {
            try
            {
                // Kullanıcı bilgileri için klasör yolu
                string klasorYolu = @"C:\Users\Lenovo User\Desktop\Sinema Otomasyon V13 SON\Sinema_Otomasyon\Kisi_Bilgileri";

                if (!Directory.Exists(klasorYolu)) // Klasör yoksa
                {
                    try
                    {
                        Directory.CreateDirectory(klasorYolu); // Klasör oluştur
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        // Yetki yoksa uyarı ver
                        MessageBox.Show($"Klasör oluşturma yetkisi yok: {ex.Message}", "Yetki Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    catch (Exception ex)
                    {
                        // Diğer hatalarda uyarı ver
                        MessageBox.Show($"Klasör oluşturulurken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Dosya yolu ve adı oluştur
                string dosyaYolu = Path.Combine(klasorYolu, isim + "_" + soyisim + "_" + id + ".txt");

                // Dosyaya yazılacak bilgiler
                StringBuilder kayitBilgisi = new StringBuilder();
                kayitBilgisi.AppendLine($"ID: {id}");
                kayitBilgisi.AppendLine($"İsim: {isim}");
                kayitBilgisi.AppendLine($"Soyisim: {soyisim}");
                kayitBilgisi.AppendLine($"Telefon: {telefon}");
                kayitBilgisi.AppendLine($"E-posta: {eposta}");
                kayitBilgisi.AppendLine($"Şifre: {sifre}");
                kayitBilgisi.AppendLine($"Cinsiyet: {cinsiyet}");
                kayitBilgisi.AppendLine($"Kayıt Tarihi: {DateTime.Now}");

                try
                {
                    File.WriteAllText(dosyaYolu, kayitBilgisi.ToString()); // Dosyaya yaz
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show($"Dosya oluşturma yetkisi yok: {ex.Message}", "Yetki Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"Dosya işlemi hatası: {ex.Message}", "I/O Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Dosya yazma hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Beklenmeyen hata: {ex.Message}", "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            VeritabaniBaglantisiAc(); // Veritabanı bağlantısını aç
            string EklemeSorgu = "INSERT INTO kisiler (id,isim,soyisim,telno,mail,cinsiyet,sifre) VALUES (@id,@isim,@soyisim,@telno,@mail,@cinsiyet,@sifre)"; // SQL sorgusu
            using (OleDbCommand EklemeKomut = new OleDbCommand(EklemeSorgu, connection)) // Sorgu komutu oluştur
            {
                // Parametreleri sorguya ekle
                EklemeKomut.Parameters.AddWithValue("@id", id);
                EklemeKomut.Parameters.AddWithValue("@isim", isim);
                EklemeKomut.Parameters.AddWithValue("@soyisim", soyisim);
                EklemeKomut.Parameters.AddWithValue("@telno", telefon);
                EklemeKomut.Parameters.AddWithValue("@mail", eposta);
                EklemeKomut.Parameters.AddWithValue("@cinsiyet", cinsiyet);
                EklemeKomut.Parameters.AddWithValue("@sifre", sifre);

                int result = EklemeKomut.ExecuteNonQuery(); // Sorguyu çalıştır

                Giris.BaglantiKapat(connection); // Bağlantıyı kapat
            }
        }

        private void KayitEtButton_Click(object sender, EventArgs e)
        {
            VeriEkle(); // Kullanıcı bilgilerini al

            if (!string.IsNullOrWhiteSpace(isim) &&
                !string.IsNullOrWhiteSpace(soyisim) &&
                !string.IsNullOrWhiteSpace(telefon) &&
                !string.IsNullOrWhiteSpace(eposta) &&
                !string.IsNullOrWhiteSpace(sifre) &&
                !string.IsNullOrWhiteSpace(cinsiyet))
            {
                KullaniciyiDosyayaKaydet(id, isim, soyisim, telefon, eposta, sifre, cinsiyet); // Bilgileri dosyaya ve veritabanına kaydet
                this.Close(); // Formu kapat
            }
            else
            {
                // Eksik bilgi varsa uyarı ver
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
