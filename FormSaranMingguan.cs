using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SleepWise
{
    public partial class FormSaranMingguan : Form
    {
        koneksiDB db = new koneksiDB();

        private BindingSource bindingSource = new BindingSource();

        public FormSaranMingguan()
        {
            InitializeComponent();
        }

        
        private void FormSaranMingguan_Load(object sender, EventArgs e)
        {
            dgvMingguan.DataSource = bindingSource;
            bindingNavigator1.BindingSource = bindingSource;

            IsiDropdownMinggu();   
            MuatDataMingguan();    
        }

        
        private void IsiDropdownMinggu()
        {
            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SP_GetMingguTersedia", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_id_user", UserSession.UserId);

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbMinggu.Items.Clear();

                    
                    cmbMinggu.Items.Add(new MingguItem
                    {
                        Label = "7 Hari Terakhir (Default)",
                        TglMulai = DateTime.Today.AddDays(-6),
                        TglAkhir = DateTime.Today
                    });

                    foreach (DataRow row in dt.Rows)
                    {
                        cmbMinggu.Items.Add(new MingguItem
                        {
                            Label = row["label_minggu"].ToString(),
                            TglMulai = Convert.ToDateTime(row["tgl_mulai"]),
                            TglAkhir = Convert.ToDateTime(row["tgl_akhir"])
                        });
                    }

                    cmbMinggu.SelectedIndex = 0; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat daftar minggu: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MuatDataMingguan()
        {
            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SP_GetLog7Hari", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_id_user", UserSession.UserId);

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    bindingSource.DataSource = dt;
                    GambarGrafik(dt, "7 Hari Terakhir");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data mingguan: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MuatDataByMinggu(DateTime tglMulai, DateTime tglAkhir, string labelMinggu)
        {
            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SP_GetLogByMinggu", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_id_user", UserSession.UserId);
                    cmd.Parameters.AddWithValue("p_tgl_mulai", tglMulai.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("p_tgl_akhir", tglAkhir.ToString("yyyy-MM-dd"));

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    bindingSource.DataSource = dt;
                    GambarGrafik(dt, labelMinggu);

                    if (dt.Rows.Count == 0)
                        txtSaranMingguan.Text = "Tidak ada data tidur pada minggu ini.";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data minggu: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        
        private void cmbMinggu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMinggu.SelectedItem is MingguItem item)
            {
                if (cmbMinggu.SelectedIndex == 0)
                {
                    
                    MuatDataMingguan();
                    txtSaranMingguan.Clear();
                }
                else
                {
                    MuatDataByMinggu(item.TglMulai, item.TglAkhir, item.Label);
                    txtSaranMingguan.Clear();
                }
            }
        }

        
        private void GambarGrafik(DataTable dt, string judulMinggu)
        {
            if (pnlGrafik == null) return;

            int panelW = pnlGrafik.Width;
            int panelH = pnlGrafik.Height;
            int padding = 40;
            int barArea = panelW - padding * 2;
            int maxMenit = 600; 

            Bitmap bmp = new Bitmap(panelW, panelH);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                int n = dt.Rows.Count;

                
                using (Font fTitle = new Font("Arial", 9, FontStyle.Bold))
                    g.DrawString($"Durasi Tidur – {judulMinggu}", fTitle, Brushes.SteelBlue, padding, 6);

                if (n == 0)
                {
                    using (Font fEmpty = new Font("Arial", 9))
                        g.DrawString("Tidak ada data untuk minggu ini.", fEmpty,
                                     Brushes.Gray, padding, panelH / 2);
                    pnlGrafik.BackgroundImage = bmp;
                    pnlGrafik.BackgroundImageLayout = ImageLayout.Stretch;
                    return;
                }

                int barW = (barArea / n) - 8;
                int chartH = panelH - padding * 2;

                
                using (Pen axisPen = new Pen(Color.Gray, 1))
                {
                    g.DrawLine(axisPen, padding, padding, padding, panelH - padding);
                    g.DrawLine(axisPen, padding, panelH - padding, panelW - padding, panelH - padding);
                }

                using (Font fSmall = new Font("Arial", 7))
                {
                    int[] yLabels = { 0, 180, 360, 540 };
                    foreach (int mnt in yLabels)
                    {
                        int yPos = panelH - padding - (int)((double)mnt / maxMenit * chartH);
                        g.DrawString((mnt / 60) + "j", fSmall, Brushes.Gray, 2, yPos - 6);
                        using (Pen gridPen = new Pen(Color.LightGray, 1))
                            g.DrawLine(gridPen, padding, yPos, panelW - padding, yPos);
                    }
                }

                
                Color[] colorMap = {
                    Color.FromArgb(99, 179, 237),
                    Color.FromArgb(72, 149, 239),
                    Color.FromArgb(58, 123, 213)
                };

                int idx = 0;
                foreach (DataRow row in dt.Rows)
                {
                    int durasi = Convert.IsDBNull(row["durasi_menit"]) ? 0 : Convert.ToInt32(row["durasi_menit"]);
                    string tgl = Convert.IsDBNull(row["tanggal"]) ? "" : Convert.ToDateTime(row["tanggal"]).ToString("dd/MM");
                    string kategori = row["nama_kategori"]?.ToString() ?? "";

                    int barH = (int)((double)durasi / maxMenit * chartH);
                    int x = padding + idx * (barW + 8) + 4;
                    int y = panelH - padding - barH;

                    // Warna berdasarkan kategori tidur
                    Color barColor = colorMap[idx % colorMap.Length];
                    if (kategori == "Kurang") barColor = Color.FromArgb(252, 129, 129);
                    if (kategori == "Berlebih") barColor = Color.FromArgb(246, 173, 85);

                    using (SolidBrush br = new SolidBrush(barColor))
                        g.FillRectangle(br, x, y, barW, barH);

                    using (Pen bp = new Pen(Color.FromArgb(50, 0, 0, 0), 1))
                        g.DrawRectangle(bp, x, y, barW, barH);

                    
                    using (Font fVal = new Font("Arial", 7, FontStyle.Bold))
                    {
                        int jam = durasi / 60; int mnt = durasi % 60;
                        g.DrawString($"{jam}j{mnt}m", fVal, Brushes.DimGray, x, y - 14);
                    }

                    
                    using (Font fDate = new Font("Arial", 7))
                        g.DrawString(tgl, fDate, Brushes.DimGray,
                                     x + barW / 2 - 10, panelH - padding + 4);

                    idx++;
                }
            }

            pnlGrafik.BackgroundImage = bmp;
            pnlGrafik.BackgroundImageLayout = ImageLayout.Stretch;
        }

        
        private void btnLoadSaran_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SP_HitungEvaluasiMingguan", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_id_user", UserSession.UserId);

                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        int rata = Convert.IsDBNull(dr["rata_durasi_menit"]) ? 0
                                       : Convert.ToInt32(dr["rata_durasi_menit"]);
                        string saran = dr["saran_mingguan"]?.ToString() ?? "-";
                        dr.Close();

                        int jam = rata / 60;
                        int mnt = rata % 60;
                        txtSaranMingguan.Text =
                            $"Rata-rata tidur minggu ini: {jam} jam {mnt} menit\r\n\r\n{saran}";

                        
                        IsiDropdownMinggu();
                        MuatDataMingguan();
                    }
                    else
                    {
                        dr.Close();
                        txtSaranMingguan.Text = "Belum ada data tidur dalam 7 hari terakhir.";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat saran mingguan: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        
        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }


    public class MingguItem
    {
        public string Label { get; set; }
        public DateTime TglMulai { get; set; }
        public DateTime TglAkhir { get; set; }

        
        public override string ToString() => Label;
    }
}