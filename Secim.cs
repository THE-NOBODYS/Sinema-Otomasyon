using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sinema_Otomasyon;

namespace Sinema_Otomasyon
{
    public partial class Secim : Form
    {
        // Dolu ve boş koltuk sayısı için sayaçlar
        private static int doluKoltukSayisi = 0, bosKoltukSayisi = 0;

        // Seçilen koltuk isimlerini tutan liste
        public static List<string> koltukİsim = new List<string>();

        // Seçilen seans saati ve tarihi
        public static string seansSaati = "";
        public static string seansTarihi = "";

        public Secim()
        {
            InitializeComponent(); // Form bileşenlerini başlat
        }

        private void Secim_Load(object sender, EventArgs e)
        {
            InitializeSeatCounters(); // Koltuk sayaçlarını başlat
            koltukİsim.Clear(); // Koltuk listesi temizlenir
            seansSaati = ""; // Seans saati sıfırlanır
            SeansTarihDateTime.MinDate = DateTime.Today; // Bugünden önce tarih seçilemez

            BindClickEventToSeats(); // Koltuklara tıklama olayları bağlanır
        }

        private void InitializeSeatCounters()
        {
            bosKoltukSayisi = 32; // Başlangıçta tüm koltuklar boş
            doluKoltukSayisi = 0; // Hiç koltuk seçili değil

            boskoltuksayi.Text = "" + bosKoltukSayisi; // Boş koltuk sayısı etiketi
            dolukoltuksayi.Text = "" + doluKoltukSayisi; // Dolu koltuk sayısı etiketi
        }

        private void BindClickEventToSeats()
        {
            foreach (Control control in this.Controls)
            {
                // Kontrol bir PictureBox ve adı 'koltuk' içeriyorsa
                if (control is PictureBox pictureBox && control.Name.ToLower().Contains("koltuk"))
                {
                    pictureBox.BackColor = Color.DimGray; // Koltuk rengi gri yapılır
                    pictureBox.Click += Koltuk_Click; // Tıklama olayı atanır
                }
            }
        }

        private void Koltuk_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox; // Tıklanan koltuğu al

            if (clickedPictureBox != null)
            {
                string koltukAdi = clickedPictureBox.Name; // Koltuk adı alınır
                bool koltukSecili = koltukİsim.Contains(koltukAdi); // Daha önce seçilmiş mi kontrolü

                if (koltukSecili == false)
                {
                    clickedPictureBox.BackColor = Color.Green; // Seçilen koltuk yeşil yapılır
                    doluKoltukSayisi++; // Dolu koltuk artırılır
                    bosKoltukSayisi--; // Boş koltuk azaltılır
                    koltukİsim.Add(koltukAdi); // Koltuk listeye eklenir
                    dolukoltuksayi.Text = "" + doluKoltukSayisi; // Ekrana yazdır
                    boskoltuksayi.Text = "" + bosKoltukSayisi; // Ekrana yazdır
                }
                else
                {
                    clickedPictureBox.BackColor = Color.Transparent; // Koltuk seçimi kaldırılır
                    doluKoltukSayisi--; // Dolu koltuk azaltılır
                    bosKoltukSayisi++; // Boş koltuk artırılır
                    koltukİsim.Remove(koltukAdi); // Koltuk listeden çıkarılır
                    dolukoltuksayi.Text = "" + doluKoltukSayisi; // Ekrana yazdır
                    boskoltuksayi.Text = "" + bosKoltukSayisi; // Ekrana yazdır
                }
            }
        }

        private void buttoniptal_Click(object sender, EventArgs e)
        {
            bosKoltukSayisi = 0; // Boş koltuk sıfırlanır
            doluKoltukSayisi = 0; // Dolu koltuk sıfırlanır
            ((Kisayol)this.Parent).FormGecis(new Anasayfa()); // Anasayfa'ya dönüş yapılır
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Programı yazan kişiler hakkında bilgi mesajı
            MessageBox.Show("Programı yapanlar ;Arda GÜLAY, Cansın İÇİM, Ege Yağız KAYA, Mert OKUMUŞ.");
        }

        private void buttonödemeyegec_Click(object sender, EventArgs e)
        {
            // Tarih geçmişteyse uyarı ver
            if (SeansTarihDateTime.Value.Date < DateTime.Today)
            {
                MessageBox.Show("Lütfen bugünün tarihinden önce bir tarih seçmeyin.");
                return;
            }

            // Seans saatini al
            foreach (Control control in groupBoxSeansSaati.Controls)
            {
                if (control is RadioButton radio && radio.Checked)
                {
                    seansSaati = radio.Text; // Seçilen saat alınır
                    break;
                }
            }

            seansTarihi = SeansTarihDateTime.Value.ToString("dd/MM/yyyy") + " " + seansSaati; // Tarih + saat birleştirilir

            if (doluKoltukSayisi != 0 && seansSaati != "")
            {
                BiletSecim biletsecim = new BiletSecim(); // Bilet seçim ekranı açılır
                biletsecim.ShowDialog();

                bosKoltukSayisi = 0; // Sayaçlar sıfırlanır
                doluKoltukSayisi = 0;
                ((Kisayol)this.Parent).FormGecis(new Anasayfa()); // Anasayfa'ya dönülür
            }
            else
            {
                if (doluKoltukSayisi == 0) MessageBox.Show("Lütfen koltuk seçiniz."); // Koltuk seçilmediyse uyarı

                if (seansSaati == "") MessageBox.Show("Lütfen seans saatini seçiniz."); // Saat seçilmediyse uyarı
            }
        }
    }
}
