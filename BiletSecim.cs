using System; // Gerekli sistem kütüphanesi
using System.Collections.Generic; // List gibi koleksiyonlar için
using System.ComponentModel; // Bileşen modelleme için
using System.Data; // DataTable vs. için veri işlemleri
using System.Drawing; // Grafik işlemleri için
using System.Linq; // LINQ sorguları için
using System.Text; // Metin işlemleri için
using System.Threading.Tasks; // Asenkron işlemler için
using System.Windows.Forms; // Form işlemleri için
using System.Runtime.InteropServices; // Dış kütüphaneleri kullanmak için

namespace Sinema_Otomasyon // Proje ismi
{
    public partial class BiletSecim : Form // Bilet seçimi formu
    {
        [DllImport("user32.dll")] // Dış kütüphane tanımı
        public static extern int ShowScrollBar(IntPtr hWnd, int wBar, bool bShow); // Scroll çubuğunu göster/gizle

        const int SB_HORZ = 0; // Yatay scroll çubuğu sabiti
        const int SB_VERT = 1; // Dikey scroll çubuğu sabiti

        public static List<string> BiletTur = new List<string>(); // Seçilen bilet türlerini tutan liste

        public BiletSecim() // Yapıcı metot
        {
            InitializeComponent(); // Form bileşenlerini başlat
            BiletTurSecim(); // Bilet türü seçim arayüzünü oluştur
            HideScrollBars(secimLoyutPanel); // Scroll çubuklarını gizle
        }

        private void HideScrollBars(Control ctrl) // Scroll çubuklarını gizleyen metot
        {
            ShowScrollBar(ctrl.Handle, SB_VERT, false); // Dikey scroll gizle
            ShowScrollBar(ctrl.Handle, SB_HORZ, false); // Yatay scroll gizle
        }

        private void BiletTurSecim() // Bilet türü seçim paneli oluşturan metot
        {
            int y = 300; // Başlangıç yüksekliği
            int margin = 20; // Kenar boşluğu
            secimLoyutPanel.AutoScroll = true; // Scroll aktif
            secimLoyutPanel.WrapContents = false; // Satır kaydırma devre dışı
            secimLoyutPanel.FlowDirection = FlowDirection.TopDown; // Dikey akış yönü

            foreach (var koltuk in Secim.koltukİsim) // Seçilen koltuklar kadar işlem yap
            {
                Label lbl = new Label(); // Yeni label oluştur
                lbl.Text = koltuk; // Label metni koltuk adı
                lbl.AutoSize = false; // Otomatik boyutlandırma kapalı
                lbl.BackColor = Color.Transparent; // Arka plan şeffaf
                lbl.Width = 40; // Genişlik 40 piksel
                lbl.Font = new Font("Segoe UI", 14, FontStyle.Bold); // Font ayarı
                lbl.TextAlign = ContentAlignment.MiddleLeft; // Metin sola yaslı ortalanmış

                var cmb = new Guna.UI2.WinForms.Guna2ComboBox(); // Yeni ComboBox oluştur
                cmb.Name = "Guna2ComboBox"; // ComboBox ismi
                cmb.Items.AddRange(new string[] { "Tam", "Öğrenci", "Çocuk" }); // Seçenekler ekleniyor
                cmb.Width = 300; // Genişlik
                cmb.BorderRadius = 15; // Kenar yumuşatma
                cmb.SelectedIndex = 0; // Varsayılan seçim

                Panel rowPanel = new Panel(); // Her satır için panel oluşturuluyor
                rowPanel.Width = 500; // Panel genişliği
                rowPanel.Height = 40; // Panel yüksekliği
                rowPanel.BackColor = Color.Transparent; // Arka plan şeffaf

                lbl.Location = new Point(0, 5); // Label konumu
                cmb.Location = new Point(110, 5); // ComboBox konumu

                rowPanel.Controls.Add(lbl); // Label panel içine ekleniyor
                rowPanel.Controls.Add(cmb); // ComboBox panel içine ekleniyor
                secimLoyutPanel.Controls.Add(rowPanel); // Panel form paneline ekleniyor
            }

            int minHeight = 200; // Minimum form yüksekliği
            int desiredHeight = y + margin + guna2GradientPanel1.Location.Y; // İstenilen toplam yükseklik
            this.Height = Math.Max(minHeight, desiredHeight); // Form yüksekliği ayarlanıyor
        }

        public List<string> SecilenBiletTurleriAl() // Seçilen bilet türlerini alan metot
        {
            BiletTur.Clear(); // Önceki seçimler temizleniyor

            foreach (Control control in secimLoyutPanel.Controls) // Tüm kontroller dönülüyor
            {
                if (control is Panel rowPanel) // Panel olanlar seçiliyor
                {
                    foreach (Control innerControl in rowPanel.Controls) // Panel içindeki kontroller
                    {
                        if (innerControl is Guna.UI2.WinForms.Guna2ComboBox comboBox) // ComboBox kontrolü
                        {
                            if (comboBox.SelectedItem != null) // Seçim yapılmışsa
                            {
                                BiletTur.Add(comboBox.SelectedItem.ToString()); // Listeye ekleniyor
                            }
                            break; // Her panelde sadece bir ComboBox olduğu varsayımı
                        }
                    }
                }
            }

            return BiletTur; // Liste döndürülüyor
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e) // Devam butonuna basıldığında
        {
            SecilenBiletTurleriAl(); // Seçilen türler alınır

            if (Giris.girilenEmail != "") // Kullanıcı giriş yaptıysa
            {
                Odeme odeme = new Odeme(); // Ödeme formu açılır
                odeme.ShowDialog(); // Form gösterilir
                this.Close(); // Mevcut form kapanır
            }
            else // Giriş yapılmadıysa
            {
                KayitsizKullanici kayitsizKullanici = new KayitsizKullanici(); // Kayıtsız kullanıcı formu
                kayitsizKullanici.ShowDialog(); // Form gösterilir
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e) // Hakkımızda butonu
        {
            MessageBox.Show("Programı yapanlar ;\nArda GÜLAY,\n Cansın İÇİM,\n Ege Yağız KAYA,\n Mert OKUMUŞ."); // Bilgi mesajı
        }

        private void BiletSecim_Load(object sender, EventArgs e) // Form yüklendiğinde
        {
            // Boş, gerekirse başlangıç işlemleri eklenebilir
        }
    }
}
