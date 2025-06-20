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
using static Sinema_Otomasyon.Anasayfa; // Anasayfa sınıfındaki öğelere doğrudan erişim sağlar

namespace Sinema_Otomasyon
{
    public partial class Kullanici : Form
    {
        public Kullanici()
        {
            InitializeComponent(); // Form bileşenlerini başlatır
            Kisayol.ProfilFotografGuncelle(bilgilerprofil); // Profil fotoğrafını yükler
            Hg_label.Text += "  " + Giris.Kullaniciadi; // Giriş yapan kullanıcının adını ekrana yazar
        }

        private void bilgilerprofil_Click(object sender, EventArgs e)
        {
            Bilgilerim bilgilerim = new Bilgilerim(); // Bilgilerim formunu oluşturur
            bilgilerim.ShowDialog(); // Formu diyalog olarak açar
            bilgilerprofil.Image = Image.FromFile(Giris.ProfilFoto); // Profil fotoğrafını günceller
            this.Refresh(); // Formu yeniler
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            // Kullanıcı formunu kapatır
            this.Close();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            Biletlerim biletlerim = new Biletlerim(); // Biletlerim formunu oluşturur
            biletlerim.ShowDialog(); // Formu diyalog olarak açar
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Geliştirici bilgilerini gösteren mesaj kutusu
            MessageBox.Show("Programı yapanlar ;\nArda GÜLAY,\n Cansın İÇİM,\n Ege Yağız KAYA,\n Mert OKUMUŞ.");
        }
    }
}