using Guna.UI2.WinForms; // Guna2 UI bileşenleri
using System.Net.Mail; // Mail işlemleri için gerekli
using System.Net; // Ağ bağlantıları
using System.Windows.Forms; // Windows Formları
using System; // Temel sistem sınıfları
using System.Drawing; // Grafik işlemleri

namespace Sinema_Otomasyon
{
    public partial class Kisayol : Form
    {
        public Kisayol()
        {
            InitializeComponent(); // Form bileşenlerini başlatır
            FormGecis(new Giris()); // Başlangıçta Giriş formunu gösterir
        }

        // Yeni bir formu ana forma gömerek geçiş yapar
        public void FormGecis(Form yeniForm)
        {
            this.Controls.Clear(); // Mevcut kontrolleri temizler
            this.Size = new Size(1300, 750); // Form boyutunu ayarlar

            yeniForm.TopLevel = false; // Alt form olarak ayarlanır
            yeniForm.FormBorderStyle = FormBorderStyle.None; // Kenarlık kaldırılır
            yeniForm.Dock = DockStyle.Fill; // Tüm alana yayılır

            this.Controls.Add(yeniForm); // Yeni form forma eklenir
            yeniForm.Show(); // Yeni form görüntülenir
        }

        // Kullanıcının profil fotoğrafını PictureBox'a yükler
        public static void ProfilFotografGuncelle(PictureBox ad)
        {
            if (Giris.ProfilFoto != "") // Kullanıcının fotoğraf yolu varsa
            {
                ad.Image = Image.FromFile(Giris.ProfilFoto); // Fotoğraf yüklenir
                ad.SizeMode = PictureBoxSizeMode.StretchImage; // Görsel kutuya sığacak şekilde ayarlanır
            }
            else // Fotoğraf yolu boşsa varsayılan fotoğraf kullanılır
            {
                ad.Image = Image.FromFile(@"C:\Users\Lenovo User\Desktop\Sinema Otomasyon V13 SON\Sinema_Otomasyon\Sinema_Otomasyon\Resources\icon.jpg");
                ad.SizeMode = PictureBoxSizeMode.StretchImage; // Görsel ayarlanır
            }
        }

        // Kart bilgileri eksik mi kontrol eder
        public static bool KartBilgiKontrol(string isim, string no, string ay, string yil, string cvv)
        {
            if (isim == "" || no == "" || ay == "" || yil == "" || cvv == "") // Herhangi bir alan boşsa
            {
                MessageBox.Show("kontrolLütfen tüm alanları doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Eksik varsa işlem yapılmaz
            }
            else
                return true; // Tüm bilgiler varsa true döner
        }

        public static class Filtrele
        {
            // Standart DataGridView için filtreleme yapar
            public static void filtre(DataGridView grid, string sutunAdi, Guna2TextBox aramaKutusu)
            {
                string aramaMetni = aramaKutusu.Text.ToLower(); // Arama metni küçük harfe çevrilir

                foreach (DataGridViewRow row in grid.Rows) // Tüm satırlar için
                {
                    if (!row.IsNewRow) // Yeni satır değilse
                    {
                        string hucreDegeri = row.Cells[sutunAdi].Value?.ToString().ToLower() ?? ""; // Hücre verisi okunur
                        row.Visible = string.IsNullOrEmpty(aramaMetni) || hucreDegeri.Contains(aramaMetni); // Uyanlar görünür, diğerleri gizlenir
                    }
                }
            }
        }

        public static class Filtrele2
        {
            // Guna2DataGridView için filtreleme işlemi yapar
            public static void filtre(Guna2DataGridView grid, string sutunAdi, Guna2TextBox aramaKutusu)
            {
                string aramaMetni = aramaKutusu.Text.ToLower(); // Arama metni alınır

                foreach (DataGridViewRow row in grid.Rows) // Tüm satırlar dolaşılır
                {
                    if (!row.IsNewRow) // Yeni satır değilse
                    {
                        string hucreDegeri = row.Cells[sutunAdi].Value?.ToString().ToLower() ?? ""; // Hücre verisi okunur
                        row.Visible = string.IsNullOrEmpty(aramaMetni) || hucreDegeri.Contains(aramaMetni); // Filtre uygulanır
                    }
                }
            }
        }

        // Kullanıcı bilgilerini temizler
        public static void KisiBilgileriTemizle()
        {
            Giris.Kullaniciadi = ""; // İsim temizlenir
            Giris.KullaniciSoyadi = ""; // Soyisim temizlenir
            Giris.KullaniciTel = ""; // Telefon temizlenir
            Giris.girilenEmail = ""; // E-posta temizlenir
        }

        // Başarılı ödeme sonrası kullanıcıya bilgilendirme maili gönderir
        public static void BasariliOdeme(string MusteriEmail, string MusteriAd, string FilmAdi, string KoltukNo, string SeansBilgi)
        {
            try
            {
                // SMTP ayarları yapılır
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    Port = 587,
                    Credentials = new NetworkCredential("sinemaotomasyon34@gmail.com", "dinv dxtd pqxf kbrn"), // Gmail bilgileri
                    EnableSsl = true, // SSL açılır
                };

                MailMessage mail = new MailMessage(); // Yeni mail oluşturulur
                mail.From = new MailAddress("sinemaotomasyon34@gmail.com", "SİNEMA OTOMASYON"); // Gönderici bilgisi
                mail.To.Add(MusteriEmail); // Alıcı bilgisi
                mail.Subject = "Sinema Otomasyon - Ödeme Bilgisi"; // Konu satırı
                mail.IsBodyHtml = false; // HTML kapalı
                mail.Body = $@"Sinema Bilet Bilgilendirme
Sayın {MusteriAd},

Bilet Rezervasyonunuz başarıyla tamamlanmıştır. Aşağıda bilet bilgileriniz:

Film Adı: {FilmAdi}
Koltuk No: {KoltukNo}
Seans Bilgisi: {SeansBilgi}

Biletinizi gişelerden temin edebilirsiniz.İyi seyiler!!!
--------------------------------------------------------------------------------------
Bu ileti otomatik olarak gönderilmiştir. Lütfen cevaplamayınız.
";

                smtpClient.Send(mail); // Mail gönderilir
                MessageBox.Show("Ödemeniz tamamlanmıştır. İyi Seyirler.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information); // Başarı mesajı
            }
            catch (Exception ex)
            {
                MessageBox.Show(" " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); // Hata mesajı
            }
        }

        // Bilet iade sonrası bilgilendirme maili gönderir
        public static void Biletİade(string MusteriEmail, string MusteriAd, string FilmAdi, string KoltukNo, string SeansBilgi)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    Port = 587,
                    Credentials = new NetworkCredential("sinemaotomasyon34@gmail.com", "dinv dxtd pqxf kbrn"),
                    EnableSsl = true,
                };

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("sinemaotomasyon34@gmail.com", "SİNEMA OTOMASYON");
                mail.To.Add(MusteriEmail);
                mail.Subject = "Sinema Otomasyon - Ödeme Bilgisi";
                mail.IsBodyHtml = false;
                mail.Body = $@"Sinema Bilet İade Bilgilendirme
Sayın {MusteriAd},

Bilet iadeniz başarıyla gerçekleşmiştir.En kısa sürede ödeme yaptığınız karta para iadesi olucaktır.

Film Adı: {FilmAdi}

--------------------------------------------------------------------------------------
Bu ileti otomatik olarak gönderilmiştir. Lütfen cevaplamayınız.
";

                smtpClient.Send(mail); // Mail gönderilir
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); // Hata gösterilir
            }
        }

        // Bilet güncelleme sonrası bilgilendirme maili gönderir
        public static void BiletGuncelleme(string MusteriEmail, string MusteriAd, string FilmAdi, string KoltukNo, string SeansBilgi)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    Port = 587,
                    Credentials = new NetworkCredential("sinemaotomasyon34@gmail.com", "dinv dxtd pqxf kbrn"),
                    EnableSsl = true,
                };

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("sinemaotomasyon34@gmail.com", "SİNEMA OTOMASYON");
                mail.To.Add(MusteriEmail);
                mail.Subject = "Sinema Otomasyon - Ödeme Bilgisi";
                mail.IsBodyHtml = false;
                mail.Body = $@"Sinema Bilet Güncelleme Bilgilendirme
Sayın {MusteriAd},

Bilet rezervasyonunuz başarıyla güncellenmiştir. Aşağıda bilet bilgileriniz:

Film Adı: {FilmAdi}
Koltuk No: {KoltukNo}
Seans Bilgisi: {SeansBilgi}

Biletinizi gişelerden temin edebilirsiniz.İyi seyiler!!!
--------------------------------------------------------------------------------------
Bu ileti otomatik olarak gönderilmiştir. Lütfen cevaplamayınız.
";

                smtpClient.Send(mail); // Mail gönderilir
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); // Hata mesajı
            }
        }
    }
}
