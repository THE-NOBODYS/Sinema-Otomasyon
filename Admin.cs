// Gerekli kütüphaneler ekleniyor
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Sinema_Otomasyon.Kisayol; // Kisayol sınıfı içindeki üyeler kullanılıyor

namespace Sinema_Otomasyon
{
    public partial class Admin : Form
    {
        DataTable biletTablosu; // Bilet verilerinin tutulduğu tablo
        private string secilenDosyaYolu = ""; // Seçilen afiş dosyasının yolu
        private int seciliId = 0; // Seçili film ID’si
        public string filmId = ""; // Film ID’si (genel erişime açık)
        private string kisiId = ""; // Kişi ID’si

        public Admin()
        {
            InitializeComponent(); // Form bileşenleri başlatılıyor
        }

        // Form gösterildiğinde çalışan olay
        private async void Admin_Shown(object sender, EventArgs e)
        {
            // Arka planda film ve biletleri listele
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)(() => FilmleriListele()));
                this.Invoke((MethodInvoker)(() => BiletleriListele()));
            });
        }

        // Bilet verilerini listeleyen metot
        private void BiletleriListele()
        {
            using (OleDbConnection baglanti = new OleDbConnection(Giris.veribaglanti))
            {
                dataGridViewBiletler.Rows.Clear(); // Önceki veriler temizleniyor
                try
                {
                    baglanti.Open(); // Bağlantı açılıyor
                    OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM odeme", baglanti); // Sorgu hazırlanıyor
                    biletTablosu = new DataTable(); // Yeni tablo oluşturuluyor
                    da.Fill(biletTablosu); // Veriler tabloya aktarılıyor

                    // Her satırı DataGridView’e ekle
                    foreach (DataRow row in biletTablosu.Rows)
                    {
                        int i = dataGridViewBiletler.Rows.Add(); // Yeni satır ekle
                        dataGridViewBiletler.Rows[i].Cells["kadi"].Value = row["kisiadi"];
                        dataGridViewBiletler.Rows[i].Cells["ksoyad"].Value = row["kisisoyad"];
                        dataGridViewBiletler.Rows[i].Cells["ktel"].Value = row["tel_no"];
                        dataGridViewBiletler.Rows[i].Cells["kmail"].Value = row["mail"];
                        dataGridViewBiletler.Rows[i].Cells["kfadi"].Value = row["filmadi"];
                        dataGridViewBiletler.Rows[i].Cells["kkno"].Value = row["koltukno"];
                        dataGridViewBiletler.Rows[i].Cells["kseans"].Value = row["seans_bilgi"];
                        dataGridViewBiletler.Rows[i].Cells["kid"].Value = row["id"];
                    }

                    // DataGridView ayarları
                    dataGridViewBiletler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridViewBiletler.ReadOnly = true;
                    dataGridViewBiletler.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    Giris.BaglantiKapat(baglanti); // Bağlantı kapatılıyor
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Listeleme hatası: " + ex.Message);
                    Giris.BaglantiKapat(baglanti); // Hata durumunda bağlantı kapatılıyor
                }
            }
        }

        // Filmleri listeleyen metot
        private void FilmleriListele()
        {
            using (OleDbConnection baglanti = new OleDbConnection(Giris.veribaglanti))
            {
                dataGridViewFilmler.Rows.Clear(); // Önceki veriler temizleniyor
                try
                {
                    baglanti.Open(); // Bağlantı açılıyor
                    string sorgu = "SELECT filmadi, filmtur, filmyil, yas_arasi, film_tarzi, afis,id,fiyat FROM filmler";
                    OleDbDataAdapter adaptasyon = new OleDbDataAdapter(sorgu, baglanti); // Sorgu hazırlanıyor
                    DataTable veriler = new DataTable(); // Yeni tablo oluşturuluyor
                    adaptasyon.Fill(veriler); // Veriler tabloya aktarılıyor

                    // Her satırı DataGridView’e ekle
                    foreach (DataRow row in veriler.Rows)
                    {
                        int i = dataGridViewFilmler.Rows.Add(); // Yeni satır ekle
                        dataGridViewFilmler.Rows[i].Cells["fadi"].Value = row["filmadi"];
                        dataGridViewFilmler.Rows[i].Cells["ftur"].Value = row["filmtur"];
                        dataGridViewFilmler.Rows[i].Cells["fyil"].Value = row["filmyil"];
                        dataGridViewFilmler.Rows[i].Cells["yas_arasi"].Value = row["yas_arasi"];
                        dataGridViewFilmler.Rows[i].Cells["ftarzi"].Value = row["film_tarzi"];
                        dataGridViewFilmler.Rows[i].Cells["fafis"].Value = row["afis"];
                        dataGridViewFilmler.Rows[i].Cells["fid"].Value = row["id"];
                        dataGridViewFilmler.Rows[i].Cells["fiyat"].Value = row["fiyat"];    
                    }

                    // ID ve afiş sütunları gizleniyor
                    dataGridViewFilmler.Columns["fid"].Visible = false;
                    dataGridViewFilmler.Columns["fafis"].Visible = false;

                    // DataGridView ayarları
                    dataGridViewFilmler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridViewFilmler.ReadOnly = true;
                    dataGridViewFilmler.MultiSelect = false;
                    Giris.BaglantiKapat(baglanti); // Bağlantı kapatılıyor
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                    Giris.BaglantiKapat(baglanti); // Hata durumunda bağlantı kapatılıyor
                }
            }
        }

        // Çıkış işlemi: kullanıcıyı giriş formuna yönlendirir
        private void CikisTextbox_Click(object sender, EventArgs e)
        {
            Giris.girilenEmail = ""; // Giriş email sıfırlanıyor
            this.Hide(); // Bu form gizleniyor
            ((Kisayol)this.Parent).FormGecis(new Giris()); // Ana forma dönüş yapılıyor
        }

        // Afiş resmi seçme işlemi
        private void button1_Click(object sender, EventArgs e)
        {
            // Dosya seçici açılıyor
            OpenFileDialog dosyasec = new OpenFileDialog();
            dosyasec.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";
            dosyasec.Title = "Film Broşürü Seç";

            if (dosyasec.ShowDialog() == DialogResult.OK)
            {
                secilenDosyaYolu = dosyasec.FileName; // Seçilen dosya yolu alınıyor
                pictureBoxBrosur.Image = Image.FromFile(secilenDosyaYolu); // Resim yükleniyor
                pictureBoxBrosur.SizeMode = PictureBoxSizeMode.StretchImage; // Görüntü boyutu ayarlanıyor
            }
        }

        // Film ekleme işlemi
        private void buttonEkle_Click(object sender, EventArgs e)
        {
            // Formdan gelen değerler alınıyor
            string filmAdi = textFilmAdi.Text;
            string tur = comboTur.Text;
            int filmYili = Convert.ToInt32(comboYil.Text);
            string yasAraligi = comboYas.Text;
            string filmTarzi = comboTarz.Text;
            string brosurUrl = secilenDosyaYolu;
            string fiyat = FiyatTextbox.Text;

            // Afiş geçerli mi kontrol et
            if (string.IsNullOrWhiteSpace(brosurUrl) || !File.Exists(brosurUrl))
            {
                MessageBox.Show("Lütfen geçerli bir broşür seçin.");
                return;
            }

            using (OleDbConnection baglanti = new OleDbConnection(Giris.veribaglanti))
            {
                try
                {
                    baglanti.Open(); // Bağlantı açılıyor
                    string sorgu = "INSERT INTO filmler (id,filmadi, filmtur, filmyil, yas_arasi, film_tarzi, afis,fiyat) " +
                                   "VALUES (?, ?, ?, ?, ?, ?, ?,?)";

                    using (OleDbCommand komut = new OleDbCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("?", Giris.Randomİd()); // Rastgele ID
                        komut.Parameters.AddWithValue("?", filmAdi);
                        komut.Parameters.AddWithValue("?", tur);
                        komut.Parameters.AddWithValue("?", filmYili);
                        komut.Parameters.AddWithValue("?", yasAraligi);
                        komut.Parameters.AddWithValue("?", filmTarzi);
                        komut.Parameters.AddWithValue("?", brosurUrl);
                        komut.Parameters.AddWithValue("?", fiyat);

                        komut.ExecuteNonQuery(); // Sorguyu çalıştır
                        MessageBox.Show("Film başarıyla eklendi!");
                        FilmleriListele(); // Liste güncelleniyor
                        Giris.BaglantiKapat(baglanti); // Bağlantı kapatılıyor
                        FormuTemizle(); // Form temizleniyor
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                    Giris.BaglantiKapat(baglanti); // Hata durumunda bağlantı kapatılıyor
                }
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            // Eğer bir satır seçilmişse silme işlemi yapılacak
            if (dataGridViewFilmler.SelectedRows.Count > 0)
            {
                // Seçilen film satırının ID'si alınıyor
                string seciliId = dataGridViewFilmler.SelectedRows[0].Cells["fid"].Value.ToString();

                // Kullanıcıya silme onayı soruluyor
                DialogResult cevap = MessageBox.Show("Bu filmi silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (cevap == DialogResult.Yes)
                {
                    // Veritabanı bağlantısı açılıyor ve silme işlemi yapılıyor
                    using (OleDbConnection baglanti = new OleDbConnection(Giris.veribaglanti))
                    {
                        try
                        {
                            baglanti.Open();
                            string sorgu = "DELETE FROM filmler WHERE id = ?";
                            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);
                            komut.Parameters.AddWithValue("?", seciliId);
                            komut.ExecuteNonQuery();

                            // Silme sonrası kullanıcı bilgilendiriliyor ve tablo güncelleniyor
                            MessageBox.Show("Film başarıyla silindi!");
                            FilmleriListele();
                            FormuTemizle();
                            Giris.BaglantiKapat(baglanti);
                        }
                        catch (Exception ex)
                        {
                            // Hata oluşursa kullanıcı bilgilendiriliyor
                            MessageBox.Show("Silme sırasında hata oluştu: " + ex.Message);
                            Giris.BaglantiKapat(baglanti);
                        }
                    }
                }
            }
            else
            {
                // Satır seçilmemişse uyarı veriliyor
                MessageBox.Show("Lütfen silmek için bir film seçin.");
            }
        }

        private void txtFilmAra_TextChanged(object sender, EventArgs e)
        {
            // Film arama kutusuna yazıldıkça filtreleme işlemi yapılıyor
            Filtrele.filtre(dataGridViewFilmler, "Fadi", txtFilmAra);
        }

        private void btnİadeEt_Click(object sender, EventArgs e)
        {
            // Eğer bir bilet satırı seçilmişse silme işlemi yapılacak
            if (dataGridViewBiletler.SelectedRows.Count > 0)
            {
                // Seçilen biletin ID'si alınıyor
                string seciliId = dataGridViewBiletler.SelectedRows[0].Cells["kid"].Value.ToString();

                // Kullanıcıya silme onayı soruluyor
                DialogResult cevap = MessageBox.Show("Bu bileti silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (cevap == DialogResult.Yes)
                {
                    // Veritabanı bağlantısı açılıyor ve silme işlemi yapılıyor
                    using (OleDbConnection baglanti = new OleDbConnection(Giris.veribaglanti))
                    {
                        try
                        {
                            baglanti.Open();
                            string sorgu = "DELETE FROM odeme WHERE id = ?";
                            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);
                            komut.Parameters.AddWithValue("?", seciliId);
                            komut.ExecuteNonQuery();

                            // Bilet iade işlemi gerçekleştiriliyor
                            Kisayol.Biletİade(textBoxEmail.Text, textBoxAd.Text, textBoxFilm.Text, textBoxKoltuk.Text, dateTimePicker1.Text);
                            MessageBox.Show("Bilet başarıyla silindi.");
                            BiletleriListele();
                            KisiTemizle();
                            Giris.BaglantiKapat(baglanti);
                        }
                        catch (Exception ex)
                        {
                            // Hata oluşursa kullanıcı bilgilendiriliyor
                            MessageBox.Show("Silme hatası: " + ex.Message);
                            Giris.BaglantiKapat(baglanti);
                        }
                    }
                }
            }
            else
            {
                // Satır seçilmemişse uyarı veriliyor
                MessageBox.Show("Lütfen silmek için bir bilet seçin.");
            }
        }

        private void dataGridViewFilmler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Tıklanan hücre geçerliyse işlem yapılır
            if (e.RowIndex >= 0)
            {
                // Seçilen satır alınır
                DataGridViewRow row = dataGridViewFilmler.Rows[e.RowIndex];

                // Satırdaki veriler form bileşenlerine aktarılır
                textFilmAdi.Text = row.Cells["fadi"].Value.ToString();
                comboTur.Text = row.Cells["ftur"].Value.ToString();
                comboYil.Text = row.Cells["fyil"].Value.ToString();
                comboYas.Text = row.Cells["yas_arasi"].Value.ToString();
                comboTarz.Text = row.Cells["ftarzi"].Value.ToString();
                FiyatTextbox.Text = row.Cells["fiyat"].Value.ToString();
                filmId = row.Cells["fid"].Value.ToString();

                // Broşür yolunu al ve PictureBox'ta göster
                string brosurYolu = row.Cells["fafis"].Value.ToString();
                secilenDosyaYolu = brosurYolu;

                if (!string.IsNullOrEmpty(brosurYolu) && File.Exists(brosurYolu))
                {
                    pictureBoxBrosur.Image = Image.FromFile(brosurYolu);
                    pictureBoxBrosur.SizeMode = PictureBoxSizeMode.Zoom; // Görseli boyutlandır
                }
                else
                {
                    pictureBoxBrosur.Image = null; // Görsel bulunamazsa temizle
                }

                // Ekle butonu gizlenir, güncelle butonu görünür yapılır
                buttonEkle.Visible = false;
                btnGuncelle.Visible = true;
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            // Güncellenecek film seçilmemişse uyarı ver
            if (filmId == "-1")
            {
                MessageBox.Show("Lütfen güncellenecek bir film seçin.");
                return;
            }
            else
            {
                // Formdan alınan bilgileri değişkenlere ata
                string filmAdi = textFilmAdi.Text;
                string tur = comboTur.Text;
                int filmYili = Convert.ToInt32(comboYil.Text);
                string yasAraligi = comboYas.Text;
                string filmTarzi = comboTarz.Text;
                string brosurUrl = secilenDosyaYolu;
                string ffilm = FiyatTextbox.Text;

                // Veritabanı bağlantısı oluştur
                using (OleDbConnection baglanti = new OleDbConnection(Giris.veribaglanti))
                {
                    try
                    {
                        baglanti.Open(); // Bağlantıyı aç

                        // Güncelleme sorgusu
                        string sorgu = "UPDATE filmler SET filmadi=?, filmtur=?, filmyil=?, yas_arasi=?, film_tarzi=?, afis=?, fiyat=? WHERE id=?";

                        using (OleDbCommand komut = new OleDbCommand(sorgu, baglanti))
                        {
                            
                            // Parametreleri sorguya ekle
                            komut.Parameters.AddWithValue("?", filmAdi);
                            komut.Parameters.AddWithValue("?", tur);
                            komut.Parameters.AddWithValue("?", filmYili);
                            komut.Parameters.AddWithValue("?", yasAraligi);
                            komut.Parameters.AddWithValue("?", filmTarzi);
                            komut.Parameters.AddWithValue("?", brosurUrl);
                            komut.Parameters.AddWithValue("?", ffilm);
                            komut.Parameters.AddWithValue("?", filmId);


                            // Sorguyu çalıştır
                            int etkilenenSatir = komut.ExecuteNonQuery();

                            // Güncelleme başarılıysa
                            if (etkilenenSatir > 0)
                            {
                                MessageBox.Show("Film başarıyla güncellendi.");
                                FilmleriListele(); // Listeyi güncelle
                                FormuTemizle(); // Formu temizle
                                buttonEkle.Visible = true; // Ekle butonunu göster
                                btnGuncelle.Visible = false; // Güncelle butonunu gizle
                            }
                            else
                            {
                                MessageBox.Show("Film güncellenemedi." + etkilenenSatir);
                                FilmleriListele(); // Listeyi yeniden yükle
                                Giris.BaglantiKapat(baglanti); // Bağlantıyı kapat
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Güncelleme sırasında bir hata oluştu: " + ex.Message);
                        Giris.BaglantiKapat(baglanti); // Hata durumunda bağlantıyı kapat
                    }
                }
            }
        }

        // Film formundaki alanları temizleyen metod
        private void FormuTemizle()
        {
            textFilmAdi.Text = "";
            comboTur.SelectedIndex = -1;
            comboYil.SelectedIndex = -1;
            comboYas.SelectedIndex = -1;
            comboTarz.SelectedIndex = -1;
            secilenDosyaYolu = "";
            pictureBoxBrosur.Image = null;
            FiyatTextbox.Text = "";
            filmId = "";
        }

        // Biletler tablosundaki hücreye tıklanınca bilgileri forma aktar
        private void dataGridViewBiletler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Seçilen satırı al
                DataGridViewRow row = dataGridViewBiletler.Rows[e.RowIndex];

                // Satırdan alınan değerleri ilgili alanlara aktar
                textBoxAd.Text = row.Cells["kadi"].Value.ToString();
                textBoxSoyisim.Text = row.Cells["ksoyad"].Value.ToString();
                TelMaskBox.Text = row.Cells["ktel"].Value.ToString();
                textBoxEmail.Text = row.Cells["kmail"].Value.ToString();
                textBoxFilm.Text = row.Cells["kfadi"].Value.ToString();
                textBoxKoltuk.Text = row.Cells["kkno"].Value.ToString();
                kisiId = row.Cells["kid"].Value.ToString(); // Kişi ID'sini al
                dateTimePicker1.Text = row.Cells["kseans"].Value.ToString(); // Seans bilgisini al
            }
        }

        // Kişi bilgilerini güncelle butonu tıklanınca çalışan metod
        private void kisiGuncelle_Click(object sender, EventArgs e)
        {
            // Formdan alınan bilgileri değişkenlere ata
            string Kisiad = textBoxAd.Text;
            string soyad = textBoxSoyisim.Text;
            string tel_no = TelMaskBox.Text;
            string mail = textBoxEmail.Text;
            string gidilenfilm = textBoxFilm.Text;
            string koltukno = textBoxKoltuk.Text;

            // Veritabanı bağlantısı oluştur
            using (OleDbConnection baglanti = new OleDbConnection(Giris.veribaglanti))
            {
                try
                {
                    baglanti.Open(); // Bağlantıyı aç

                    // Güncelleme sorgusu
                    string sorgu = "UPDATE odeme SET kisiadi=?, kisisoyad=?, tel_no=?, mail=?, filmadi=?, koltukno=?,seans_bilgi=? WHERE id=?";
                    using (OleDbCommand komut = new OleDbCommand(sorgu, baglanti))
                    {
                        // Parametreleri ekle
                        komut.Parameters.AddWithValue("?", Kisiad);
                        komut.Parameters.AddWithValue("?", soyad);
                        komut.Parameters.AddWithValue("?", tel_no);
                        komut.Parameters.AddWithValue("?", mail);
                        komut.Parameters.AddWithValue("?", gidilenfilm);
                        komut.Parameters.AddWithValue("?", koltukno);
                        komut.Parameters.AddWithValue("?", dateTimePicker1.Text);
                        komut.Parameters.AddWithValue("?", kisiId);

                        // Sorguyu çalıştır
                        int etkilenenSatir = komut.ExecuteNonQuery();

                        // Güncelleme başarılıysa
                        if (etkilenenSatir > 0)
                        {
                            MessageBox.Show("Bilgiler başarıyla güncellendi.");
                            BiletleriListele(); // Listeyi güncelle
                            Kisayol.BiletGuncelleme(mail, Kisiad, gidilenfilm, koltukno, dateTimePicker1.Text); // Mail gönder
                            FormuTemizle(); // Formu temizle
                            seciliId += 1; // ID artır
                            Giris.BaglantiKapat(baglanti); // Bağlantıyı kapat
                        }
                        else
                        {
                            MessageBox.Show("Kişi güncellenemedi." + etkilenenSatir);
                            BiletleriListele(); // Listeyi güncelle
                            Giris.BaglantiKapat(baglanti); // Bağlantıyı kapat
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Güncelleme sırasında bir hata oluştu: " + ex.Message);
                    Giris.BaglantiKapat(baglanti); // Hata durumunda bağlantıyı kapat
                }
            }
        }

        // Kişi arama kutusunda yazı değişince filtre uygula
        private void KisiAraTextBox_TextChanged(object sender, EventArgs e)
        {
            Filtrele.filtre(dataGridViewBiletler, "kadi", KisiAraTextBox);
        }

        // Temizle butonu tıklanınca formu temizle ve butonları sıfırla
        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            FormuTemizle();
            buttonEkle.Visible = true;
            btnGuncelle.Visible = false;
        }

        // Anasayfaya dönüş butonu - form geçişi yapar
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            ((Kisayol)this.Parent).FormGecis(new Anasayfa());
        }

        // Kişi formunu temizle butonu tıklanınca temizle
        private void KisiTemileButton_Click(object sender, EventArgs e)
        {
            KisiTemizle();
        }

        // Kişi ile ilgili form alanlarını temizleyen metod
        private void KisiTemizle()
        {
            textBoxAd.Text = "";
            textBoxSoyisim.Text = "";
            TelMaskBox.Text = "";
            textBoxEmail.Text = "";
            textBoxFilm.Text = "";
            textBoxKoltuk.Text = "";
            dateTimePicker1.CustomFormat = " "; // Tarihi temizle
        }
    }
}