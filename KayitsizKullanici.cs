using System; // Temel sistem sınıfları
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; // Windows Form bileşenleri

namespace Sinema_Otomasyon
{
    public partial class KayitsizKullanici : Form
    {
        public KayitsizKullanici()
        {
            InitializeComponent(); // Form bileşenlerini başlat
        }

        private void KayitEtButton_Click(object sender, EventArgs e)
        {
            // Girilen kullanıcı bilgilerini statik alanlara aktar
            Giris.Kullaniciadi = IsimTextbox.Text; // İsim
            Giris.KullaniciSoyadi = SoyisimTextbox.Text; // Soyisim
            Giris.KullaniciTel = TelMaskBox.Text; // Telefon
            Giris.girilenEmail = MailTextBox.Text; // E-posta

            Odeme odeme = new Odeme(); // Ödeme formunu oluştur
            odeme.ShowDialog(); // Ödeme formunu modal olarak göster
            this.Hide(); // Bu formu gizle
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Programı yapanları gösteren mesaj kutusu
            MessageBox.Show("Programı yapanlar ;\nArda GÜLAY,\n Cansın İÇİM,\n Ege Yağız KAYA,\n Mert OKUMUŞ.");
        }
    }
}
