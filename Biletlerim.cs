using System; // Temel .NET sınıflarını kullanmak için
using System.Collections.Generic; // Koleksiyonlar için
using System.ComponentModel; // Bileşen modeli için
using System.Data; // Veri işlemleri için
using System.Drawing; // Grafik işlemleri için
using System.Linq; // LINQ işlemleri için
using System.Text; // Metin işlemleri için
using System.Threading.Tasks; // Asenkron işlemler için
using System.Windows.Forms; // Form kontrolleri için
using System.Data.OleDb; // Access veritabanı bağlantısı için
using System.IO; // Dosya işlemleri için

namespace Sinema_Otomasyon // Proje adı
{
    public partial class Biletlerim : Form // Biletlerim formu
    {

        public Biletlerim() // Yapıcı metod
        {
            InitializeComponent(); // Form bileşenlerini başlat
            BiletlerDataGrid.CellClick += BiletlerDataGrid_CellClick; // Hücreye tıklanınca olayı bağla
            BiletleriListele(); // Biletleri listele
        }

        private void DonusButton_Click(object sender, EventArgs e) // Geri dön butonu
        {
            Kullanici kullanici = new Kullanici(); // Yeni kullanıcı formu oluştur
            this.Close(); // Bu formu kapat
        }

        private void BiletleriListele() // Biletleri listeleyen fonksiyon
        {
            using (OleDbConnection baglanti = new OleDbConnection(Giris.veribaglanti)) // Bağlantıyı oluştur
            {
                BiletlerDataGrid.Rows.Clear(); // Grid'deki eski verileri temizle
                try
                {
                    baglanti.Open(); // Bağlantıyı aç
                    string sorgu = "SELECT * FROM odeme WHERE mail=?"; // SQL sorgusu (kullanıcının maile göre)
                    OleDbCommand komut = new OleDbCommand(sorgu, baglanti); // Komutu oluştur

                    komut.Parameters.AddWithValue("?", Giris.girilenEmail); // Mail parametresini ekle
                    OleDbDataAdapter da = new OleDbDataAdapter(komut); // DataAdapter oluştur
                    DataTable biletTablosu = new DataTable(); // Geçici tablo oluştur
                    da.Fill(biletTablosu); // Verileri tabloya doldur

                    foreach (DataRow row in biletTablosu.Rows) // Her satır için döngü
                    {
                        int i = BiletlerDataGrid.Rows.Add(); // Yeni satır ekle
                        BiletlerDataGrid.Rows[i].Cells["biletfiyat"].Value = row["t_fiyat"]; // Fiyatı ata
                        BiletlerDataGrid.Rows[i].Cells["filmad"].Value = row["filmadi"]; // Film adını ata
                        BiletlerDataGrid.Rows[i].Cells["koltukno"].Value = row["koltukno"]; // Koltuk no'yu ata
                        BiletlerDataGrid.Rows[i].Cells["biletTarih"].Value = row["seans_bilgi"]; // Seans bilgisi ata
                        BiletlerDataGrid.Rows[i].Cells["bid"].Value = row["id"]; // ID değerini ata
                    }
                    BiletlerDataGrid.Columns["bid"].Visible = false; // ID sütununu gizle
                    BiletlerDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Tüm satır seçilsin
                    BiletlerDataGrid.ReadOnly = true; // Tabloyu sadece okunur yap
                    BiletlerDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Sütunları otomatik boyutlandır
                    Giris.BaglantiKapat(baglanti); // Bağlantıyı kapat
                }
                catch (Exception ex) // Hata olursa
                {
                    MessageBox.Show("Listeleme hatası: " + ex.Message); // Hata mesajı göster
                    Giris.BaglantiKapat(baglanti); // Bağlantıyı kapat
                }
            }
        }

        private void BiletiSil(string biletID) // Belirtilen ID'ye sahip bileti sil
        {
            using (OleDbConnection baglanti = new OleDbConnection(Giris.veribaglanti)) // Bağlantı oluştur
            {
                try
                {
                    baglanti.Open(); // Bağlantıyı aç
                    string sorgu = "DELETE FROM odeme WHERE id = ?"; // Silme sorgusu
                    using (OleDbCommand cmd = new OleDbCommand(sorgu, baglanti)) // Komut oluştur
                    {
                        cmd.Parameters.AddWithValue("?", biletID); // Parametre olarak ID'yi ver
                        cmd.ExecuteNonQuery(); // Sorguyu çalıştır
                    }
                }
                catch (Exception ex) // Hata olursa
                {
                    MessageBox.Show("İptal hatası: " + ex.Message); // Hata mesajı göster
                }
                finally
                {
                    baglanti.Close(); // Bağlantıyı kapat
                }
            }
        }

        private void AraTextBox_TextChanged(object sender, EventArgs e) // Arama kutusu değişince çalışır
        {
            Kisayol.Filtrele2.filtre(BiletlerDataGrid, "filmad", AraTextBox); // Filtreleme işlemi
        }

        private void BiletlerDataGrid_CellClick(object sender, DataGridViewCellEventArgs e) // Hücreye tıklanınca çalışır
        {
            if (e.RowIndex >= 0 && BiletlerDataGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn) // Buton kolona tıklanmışsa
            {
                var satir = BiletlerDataGrid.Rows[e.RowIndex]; // Tıklanan satırı al
                string biletID = satir.Cells["bid"].Value.ToString(); // Satırdan ID'yi al

                DialogResult sonuc = MessageBox.Show("Bu bileti iptal etmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Warning); // Onay kutusu göster
                if (sonuc == DialogResult.Yes) // Eğer kullanıcı evet dediyse
                {
                    BiletiSil(biletID); // Veritabanından bileti sil
                    BiletlerDataGrid.Rows.RemoveAt(e.RowIndex); // Grid'den bileti kaldır
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e) // Hakkında butonuna tıklanınca
        {
            MessageBox.Show("Programı yapanlar ;\nArda GÜLAY,\n Cansın İÇİM,\n Ege Yağız KAYA,\n Mert OKUMUŞ."); // Bilgi mesajı
        }
    }
}
