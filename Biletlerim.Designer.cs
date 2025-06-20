namespace Sinema_Otomasyon
{
    partial class Biletlerim
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Biletlerim));
            this.AraTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.BiletlerDataGrid = new Guna.UI2.WinForms.Guna2DataGridView();
            this.filmad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.koltuksayi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.koltukno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.biletTarih = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.biletfiyat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rezervasyon = new System.Windows.Forms.DataGridViewButtonColumn();
            this.DonusButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.BiletlerDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // AraTextBox
            // 
            this.AraTextBox.BackColor = System.Drawing.Color.Transparent;
            this.AraTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(80)))));
            this.AraTextBox.BorderRadius = 10;
            this.AraTextBox.BorderThickness = 2;
            this.AraTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.AraTextBox.DefaultText = "";
            this.AraTextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.AraTextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.AraTextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.AraTextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.AraTextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AraTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AraTextBox.ForeColor = System.Drawing.Color.Black;
            this.AraTextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AraTextBox.Location = new System.Drawing.Point(228, 49);
            this.AraTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AraTextBox.Name = "AraTextBox";
            this.AraTextBox.PlaceholderText = "";
            this.AraTextBox.SelectedText = "";
            this.AraTextBox.Size = new System.Drawing.Size(187, 36);
            this.AraTextBox.TabIndex = 1;
            this.AraTextBox.TextChanged += new System.EventHandler(this.AraTextBox_TextChanged);
            // 
            // BiletlerDataGrid
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.BiletlerDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(80)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.3F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.BiletlerDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.BiletlerDataGrid.ColumnHeadersHeight = 38;
            this.BiletlerDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.filmad,
            this.bid,
            this.koltuksayi,
            this.koltukno,
            this.biletTarih,
            this.biletfiyat,
            this.rezervasyon});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(1);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.BiletlerDataGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.BiletlerDataGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.BiletlerDataGrid.Location = new System.Drawing.Point(1, 138);
            this.BiletlerDataGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BiletlerDataGrid.Name = "BiletlerDataGrid";
            this.BiletlerDataGrid.ReadOnly = true;
            this.BiletlerDataGrid.RowHeadersVisible = false;
            this.BiletlerDataGrid.RowHeadersWidth = 51;
            this.BiletlerDataGrid.RowTemplate.Height = 24;
            this.BiletlerDataGrid.Size = new System.Drawing.Size(661, 252);
            this.BiletlerDataGrid.TabIndex = 2;
            this.BiletlerDataGrid.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.BiletlerDataGrid.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.BiletlerDataGrid.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.BiletlerDataGrid.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.BiletlerDataGrid.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.BiletlerDataGrid.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.BiletlerDataGrid.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.BiletlerDataGrid.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.Black;
            this.BiletlerDataGrid.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.BiletlerDataGrid.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BiletlerDataGrid.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.BiletlerDataGrid.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.BiletlerDataGrid.ThemeStyle.HeaderStyle.Height = 38;
            this.BiletlerDataGrid.ThemeStyle.ReadOnly = true;
            this.BiletlerDataGrid.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.BiletlerDataGrid.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.BiletlerDataGrid.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BiletlerDataGrid.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.BiletlerDataGrid.ThemeStyle.RowsStyle.Height = 24;
            this.BiletlerDataGrid.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.BiletlerDataGrid.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.BiletlerDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BiletlerDataGrid_CellClick);
            // 
            // filmad
            // 
            this.filmad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.filmad.HeaderText = "Ad";
            this.filmad.MinimumWidth = 6;
            this.filmad.Name = "filmad";
            this.filmad.ReadOnly = true;
            this.filmad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.filmad.Width = 85;
            // 
            // bid
            // 
            this.bid.HeaderText = "bid";
            this.bid.MinimumWidth = 6;
            this.bid.Name = "bid";
            this.bid.ReadOnly = true;
            // 
            // koltuksayi
            // 
            this.koltuksayi.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.koltuksayi.HeaderText = "Koltuk Sayısı";
            this.koltuksayi.MinimumWidth = 6;
            this.koltuksayi.Name = "koltuksayi";
            this.koltuksayi.ReadOnly = true;
            this.koltuksayi.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.koltuksayi.Width = 70;
            // 
            // koltukno
            // 
            this.koltukno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.koltukno.HeaderText = "Koltuk No.";
            this.koltukno.MinimumWidth = 6;
            this.koltukno.Name = "koltukno";
            this.koltukno.ReadOnly = true;
            this.koltukno.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.koltukno.Width = 80;
            // 
            // biletTarih
            // 
            this.biletTarih.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.biletTarih.HeaderText = "Tarih";
            this.biletTarih.MinimumWidth = 6;
            this.biletTarih.Name = "biletTarih";
            this.biletTarih.ReadOnly = true;
            this.biletTarih.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.biletTarih.Width = 95;
            // 
            // biletfiyat
            // 
            this.biletfiyat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.biletfiyat.HeaderText = "Fiyat";
            this.biletfiyat.MinimumWidth = 6;
            this.biletfiyat.Name = "biletfiyat";
            this.biletfiyat.ReadOnly = true;
            this.biletfiyat.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.biletfiyat.Width = 82;
            // 
            // rezervasyon
            // 
            this.rezervasyon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.rezervasyon.HeaderText = "Rezervasyon";
            this.rezervasyon.MinimumWidth = 6;
            this.rezervasyon.Name = "rezervasyon";
            this.rezervasyon.ReadOnly = true;
            this.rezervasyon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.rezervasyon.Text = "İPTAL";
            this.rezervasyon.ToolTipText = "İptal";
            this.rezervasyon.UseColumnTextForButtonValue = true;
            this.rezervasyon.Width = 84;
            // 
            // DonusButton
            // 
            this.DonusButton.BackColor = System.Drawing.Color.Transparent;
            this.DonusButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.DonusButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.DonusButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.DonusButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.DonusButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.DonusButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(0)))), ((int)(((byte)(80)))));
            this.DonusButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(0)))), ((int)(((byte)(80)))));
            this.DonusButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DonusButton.ForeColor = System.Drawing.Color.White;
            this.DonusButton.Location = new System.Drawing.Point(235, 416);
            this.DonusButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DonusButton.Name = "DonusButton";
            this.DonusButton.Size = new System.Drawing.Size(180, 46);
            this.DonusButton.TabIndex = 3;
            this.DonusButton.Text = "Geri Dön";
            this.DonusButton.Click += new System.EventHandler(this.DonusButton_Click);
            // 
            // guna2Button1
            // 
            this.guna2Button1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.Transparent;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.guna2Button1.ForeColor = System.Drawing.Color.Black;
            this.guna2Button1.Location = new System.Drawing.Point(540, 460);
            this.guna2Button1.Margin = new System.Windows.Forms.Padding(4);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(123, 22);
            this.guna2Button1.TabIndex = 34;
            this.guna2Button1.Text = "Hakkımızda";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // guna2Button2
            // 
            this.guna2Button2.BackColor = System.Drawing.Color.Transparent;
            this.guna2Button2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button2.FillColor = System.Drawing.Color.Transparent;
            this.guna2Button2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.guna2Button2.ForeColor = System.Drawing.Color.Black;
            this.guna2Button2.Location = new System.Drawing.Point(152, 49);
            this.guna2Button2.Margin = new System.Windows.Forms.Padding(4);
            this.guna2Button2.Name = "guna2Button2";
            this.guna2Button2.Size = new System.Drawing.Size(69, 36);
            this.guna2Button2.TabIndex = 35;
            this.guna2Button2.Text = "Ara";
            // 
            // Biletlerim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.ClientSize = new System.Drawing.Size(664, 482);
            this.Controls.Add(this.guna2Button2);
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.DonusButton);
            this.Controls.Add(this.BiletlerDataGrid);
            this.Controls.Add(this.AraTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Biletlerim";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Biletlerim";
            ((System.ComponentModel.ISupportInitialize)(this.BiletlerDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2TextBox AraTextBox;
        private Guna.UI2.WinForms.Guna2DataGridView BiletlerDataGrid;
        private Guna.UI2.WinForms.Guna2GradientButton DonusButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn filmad;
        private System.Windows.Forms.DataGridViewTextBoxColumn bid;
        private System.Windows.Forms.DataGridViewTextBoxColumn koltuksayi;
        private System.Windows.Forms.DataGridViewTextBoxColumn koltukno;
        private System.Windows.Forms.DataGridViewTextBoxColumn biletTarih;
        private System.Windows.Forms.DataGridViewTextBoxColumn biletfiyat;
        private System.Windows.Forms.DataGridViewButtonColumn rezervasyon;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
    }
}