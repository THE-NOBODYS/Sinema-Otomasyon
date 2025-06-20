using System; // Temel .NET sınıflarını kullanmak için
using System.Collections.Generic; // List gibi koleksiyonlar için
using System.ComponentModel;
using System.Data;
using System.Data.OleDb; // Access veritabanı bağlantısı için
using System.Drawing; // Grafik ve görseller için
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; // Form elemanları için

namespace Sinema_Otomasyon
{
    public partial class Anasayfa : Form
    {
        public static string secilenfilmadi = ""; // Seçilen filmin adını saklamak için

        public Anasayfa()
        {
            InitializeComponent(); // Form bileşenlerini başlat
            FilmYukle(); // Filmleri yükle
            Kisayol.ProfilFotografGuncelle(picProfilFoto); // Profil fotoğrafını güncelle
        }

        public static class AktifKullanici
        {
            public static int id { get; set; } // Aktif kullanıcının ID'si
        }

        private void Anasayfa_Load(object sender, EventArgs e)
        {
            AraTextBox.TextChanged += AramaFiltrele; // Arama kutusu değişince filtreleme yap
            checkedListBoxFilmTarzi.ItemCheck += FiltreKontrol; // Film tarzı seçimi değişince filtrele
            checkedListBoxFilmTuri.ItemCheck += FiltreKontrol; // Film türü seçimi değişince filtrele
            checkedListBoxFilmYili.ItemCheck += FiltreKontrol; // Film yılı seçimi değişince filtrele
            checkedListBoxYasAraligi.ItemCheck += FiltreKontrol; // Yaş aralığı seçimi değişince filtrele
            if (Giris.girilenEmail == "") picProfilFoto.Visible = false; // Giriş yapılmamışsa profil fotoğrafını gizle
            Giris.FilmUcret = 0; // Film ücreti sıfırla
        }

        private void AramaFiltrele(object sender, EventArgs e)
        {
            Filtrele(); // Arama filtresini uygula
        }

        private void FiltreKontrol(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(Filtrele)); // Filtrelemeyi sıraya al ve çalıştır
        }

        private void FilmYukle()
        {
            SinemaListeleFlowLoyutPanel.Controls.Clear(); // Paneldeki önceki içerikleri temizle
            string sorgu = "SELECT * FROM filmler"; // Tüm filmleri çek

            using (OleDbConnection baglanti = new OleDbConnection(Giris.veribaglanti)) // Veritabanı bağlantısı
            using (OleDbCommand komut = new OleDbCommand(sorgu, baglanti)) // SQL komutu
            {
                baglanti.Open(); // Bağlantıyı aç
                using (OleDbDataReader veri_okuma = komut.ExecuteReader()) // Verileri oku
                {
                    while (veri_okuma.Read()) // Tüm satırları oku
                    {
                        String filmAdi = veri_okuma["filmadi"].ToString(); // Film adı al
                        String filmYil = veri_okuma["filmyil"].ToString(); // Film yılı al
                        String yasAraligi = veri_okuma["yas_arasi"].ToString(); // Yaş aralığı al
                        String filmTarzi = veri_okuma["film_tarzi"].ToString(); // Film tarzı al
                        String tur = veri_okuma["filmtur"].ToString(); // Film türü al
                        String afisYol = veri_okuma["afis"].ToString(); // Afiş yolu al
                        int _filmUcret = Convert.ToInt32(veri_okuma["fiyat"]); // Fiyatı al

                        Panel filmPanel = new Panel(); // Yeni panel oluştur
                        filmPanel.Width = 150; // Panel genişliği ayarla
                        filmPanel.Height = 220; // Panel yüksekliği ayarla
                        filmPanel.Margin = new Padding(10); // Panel kenar boşluğu

                        FilmBilgi filmBilgi = new FilmBilgi // Film bilgilerini nesneye aktar
                        {
                            filmAdi = filmAdi,
                            filmYil = filmYil,
                            yasAraligi = yasAraligi,
                            filmTarzi = filmTarzi,
                            tur = tur,
                            _filmUcret = _filmUcret,
                        };

                        filmPanel.Tag = filmBilgi; // Panelin etiketine film bilgisini ekle

                        PictureBox afisResim = new PictureBox(); // Afiş için resim kutusu oluştur
                        afisResim.Width = 140; // Genişlik ayarla
                        afisResim.Height = 160; // Yükseklik ayarla
                        afisResim.SizeMode = PictureBoxSizeMode.StretchImage; // Görüntü boyutlandırma modu
                        afisResim.Image = Image.FromFile(afisYol); // Afiş görüntüsünü yükle
                        afisResim.Click += PictureBox_Click; // Tıklama olayını bağla
                        afisResim.Tag = filmAdi; // Tag olarak film adını ekle

                        Label lblAdi = new Label(); // Film adı etiketi oluştur
                        lblAdi.Text = filmAdi; // Film adını göster
                        lblAdi.Font = new Font("Arial", 10, FontStyle.Bold); // Yazı tipi ayarla
                        lblAdi.AutoSize = false; // Otomatik boyutlandırma kapalı
                        lblAdi.TextAlign = ContentAlignment.MiddleCenter; // Ortala
                        lblAdi.Dock = DockStyle.Bottom; // Alta yerleştir

                        Label lblTur = new Label(); // Film türü etiketi oluştur
                        lblTur.Text = tur; // Türü yaz
                        lblTur.Font = new Font("Arial", 10, FontStyle.Bold); // Yazı tipi ayarla
                        lblTur.AutoSize = false; // Otomatik boyutlandırma kapalı
                        lblTur.TextAlign = ContentAlignment.MiddleCenter; // Ortala
                        lblTur.Dock = DockStyle.Bottom; // Alta yerleştir

                        filmPanel.Controls.Add(afisResim); // Afiş resmi ekle
                        filmPanel.Controls.Add(lblTur); // Tür etiketini ekle
                        filmPanel.Controls.Add(lblAdi); // Ad etiketi ekle

                        SinemaListeleFlowLoyutPanel.Controls.Add(filmPanel); // Paneli ana alana ekle
                    }
                    Giris.BaglantiKapat(baglanti); // Bağlantıyı kapat
                }
            }
        }

        private class FilmBilgi
        {
            public string filmAdi { get; set; } // Film adı
            public string filmYil { get; set; } // Film yılı
            public string yasAraligi { get; set; } // Yaş aralığı
            public string filmTarzi { get; set; } // Film tarzı
            public string tur { get; set; } // Film türü
            public int _filmUcret { get; set; } // Film ücreti
        }

        private void Filtrele()
        {
            string aramaMetni = AraTextBox.Text.ToLower(); // Arama metnini küçük harfe çevir
            List<string> SeciliTur = new List<string>(); // Seçilen türleri listele
            for (int i = 0; i < checkedListBoxFilmTuri.Items.Count; i++)
            {
                if (checkedListBoxFilmTuri.GetItemChecked(i)) SeciliTur.Add(checkedListBoxFilmTuri.Items[i].ToString()); // Seçili türü listeye ekle
            }
            List<string> SeciliTarz = new List<string>(); // Seçilen tarzları listele
            for (int i = 0; i < checkedListBoxFilmTarzi.Items.Count; i++)
            {
                if (checkedListBoxFilmTarzi.GetItemChecked(i)) SeciliTarz.Add(checkedListBoxFilmTarzi.Items[i].ToString()); // Seçili tarzı listeye ekle
            }
            List<string> SeciliYas = new List<string>(); // Seçilen yaşları listele
            for (int i = 0; i < checkedListBoxYasAraligi.Items.Count; i++)
            {
                if (checkedListBoxYasAraligi.GetItemChecked(i)) SeciliYas.Add(checkedListBoxYasAraligi.Items[i].ToString()); // Seçili yaş aralığını ekle
            }
            List<string> SeciliYil = new List<string>(); // Seçilen yılları listele
            for (int i = 0; i < checkedListBoxFilmYili.Items.Count; i++)
            {
                if (checkedListBoxFilmYili.GetItemChecked(i)) SeciliYil.Add(checkedListBoxFilmYili.Items[i].ToString()); // Seçili yılı ekle
            }

            foreach (Control control in SinemaListeleFlowLoyutPanel.Controls) // Tüm panelleri dolaş
            {
                if (control is Panel filmPanel)
                {
                    FilmBilgi filmBilgi = filmPanel.Tag as FilmBilgi; // Paneldeki film bilgilerini al
                    if (filmBilgi != null)
                    {
                        bool aramaUygun = aramaMetni.Length == 0 || filmBilgi.filmAdi.ToLower().Contains(aramaMetni); // Arama kriteri uygunsa
                        bool turUygun = SeciliTur.Count == 0 || SeciliTur.Contains(filmBilgi.tur); // Tür uygunsa
                        bool tarzUygun = SeciliTarz.Count == 0 || SeciliTarz.Contains(filmBilgi.filmTarzi); // Tarz uygunsa
                        bool yasUygun = SeciliYas.Count == 0 || SeciliYas.Contains(filmBilgi.yasAraligi); // Yaş uygunsa
                        bool yilUygun = SeciliYil.Count == 0 || SeciliYil.Contains(filmBilgi.filmYil); // Yıl uygunsa

                        filmPanel.Visible = aramaUygun && turUygun && tarzUygun && yasUygun && yilUygun; // Tüm kriterler uygunsa paneli göster
                    }
                }
            }
        }
        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox afis_tikla = sender as PictureBox; // Tıklanan PictureBox'ı al
            if (afis_tikla != null)
            {
                Panel parentPanel = afis_tikla.Parent as Panel; // Afişin bulunduğu paneli al
                if (parentPanel != null && parentPanel.Tag is FilmBilgi film)
                {
                    Giris.FilmUcret = film._filmUcret; // Film ücretini aktar
                    secilenfilmadi = afis_tikla.Tag.ToString(); // Seçilen filmi ata
                    ((Kisayol)this.Parent).FormGecis(new Secim()); // Seçim formuna geç
                }
            }
        }

        private void CikisButton_Click(object sender, EventArgs e)
        {
            Giris.girilenEmail = ""; // Kullanıcı çıkışı yap
            ((Kisayol)this.Parent).FormGecis(new Giris()); // Giriş formuna dön
        }

        private void KullaniciBilgilerineGit(object sender, EventArgs e)
        {
            Kullanici kullaniciForm = new Kullanici(); // Kullanıcı formunu oluştur
            kullaniciForm.ShowDialog(); // Formu göster
        }

        private void picProfilFoto_Click(object sender, EventArgs e)
        {
            Kullanici kullaniciForm = new Kullanici(); // Kullanıcı formunu oluştur
            kullaniciForm.ShowDialog(); // Formu göster
            picProfilFoto.Image = Image.FromFile(Giris.ProfilFoto); // Yeni profil fotoğrafını yükle
            this.Refresh(); // Formu yenile
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Programı yapanlar ;\nArda GÜLAY,\n Cansın İÇİM,\n Ege Yağız KAYA,\n Mert OKUMUŞ."); // Hakkında mesajı göster
        }
    }
}