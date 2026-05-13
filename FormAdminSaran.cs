using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SleepWise
{
    public partial class FormAdminSaran : Form
    {
        koneksiDB db = new koneksiDB();
        private BindingSource bindingSourceHarian = new BindingSource();
        private BindingSource bindingSourceMingguan = new BindingSource();

        private int selectedIdKategori = 0; 
        private int selectedIdEvaluasi = 0; 

        public FormAdminSaran()
        {
            InitializeComponent();
        }

        
        private void FormAdminSaran_Load(object sender, EventArgs e)
        {
            // Hubungkan BindingSource ke masing-masing DataGridView
            dgvSaranHarian.DataSource = bindingSourceHarian;
            dgvSaranMingguan.DataSource = bindingSourceMingguan;

            // BindingNavigator ikut bindingSourceHarian sebagai default
            bindingNavigator1.BindingSource = bindingSourceHarian;

            TampilkanSaranHarian();
            TampilkanSaranMingguan();
        }

        
        

        
        private void TampilkanSaranHarian()
        {
            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SP_GetKategoriTidur", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    bindingSourceHarian.DataSource = dt;
                    BersihkanFormHarian();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat saran harian: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ─── Saat baris dgvSaranHarian dipilih → isi txtSaranHarian ─────────────
        private void dgvSaranHarian_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSaranHarian.CurrentRow == null) return;
            DataRowView row = dgvSaranHarian.CurrentRow.DataBoundItem as DataRowView;
            if (row == null) return;

            selectedIdKategori = Convert.ToInt32(row["id_kategori"]);
            txtSaranHarian.Text = row["saran_harian"].ToString();
        }

        // ─── Tombol Load Harian ──────────────────────────────────────────────────
        private void btnLoadHarian_Click(object sender, EventArgs e)
        {
            TampilkanSaranHarian();
        }

        // ─── Tombol Simpan Harian via SP_UpdateSaranHarian ──────────────────────
        private void btnSimpanHarian_Click(object sender, EventArgs e)
        {
            if (selectedIdKategori == 0)
            {
                MessageBox.Show("Pilih kategori dari tabel terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSaranHarian.Text))
            {
                MessageBox.Show("Saran harian tidak boleh kosong.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SP_UpdateSaranHarian", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_id_kategori", selectedIdKategori);
                    cmd.Parameters.AddWithValue("p_saran_harian", txtSaranHarian.Text.Trim());

                    MySqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    int affected = Convert.ToInt32(dr["affected_rows"]);
                    dr.Close();

                    if (affected > 0)
                    {
                        MessageBox.Show("Saran harian berhasil diupdate!", "Sukses",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TampilkanSaranHarian();
                    }
                    else
                        MessageBox.Show("Kategori tidak ditemukan.", "Gagal",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menyimpan saran harian: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ─── Bersihkan form harian ───────────────────────────────────────────────
        private void BersihkanFormHarian()
        {
            selectedIdKategori = 0;
            txtSaranHarian.Text = "";
        }

        // ══════════════════════════════════════════════════════════════════════════
        // SARAN MINGGUAN
        // ══════════════════════════════════════════════════════════════════════════

        // ─── Load saran mingguan via SP_GetEvaluasiMingguan ─────────────────────
        private void TampilkanSaranMingguan()
        {
            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SP_GetEvaluasiMingguan", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    bindingSourceMingguan.DataSource = dt;
                    BersihkanFormMingguan();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat saran mingguan: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ─── Saat baris dgvSaranMingguan dipilih → isi txtSaranMingguan ─────────
        private void dgvSaranMingguan_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSaranMingguan.CurrentRow == null) return;
            DataRowView row = dgvSaranMingguan.CurrentRow.DataBoundItem as DataRowView;
            if (row == null) return;

            selectedIdEvaluasi = Convert.ToInt32(row["id_evaluasi"]);
            txtSaranMingguan.Text = row["saran_jadwal_minggu_depan"].ToString();
        }

        // ─── Tombol Load Mingguan ────────────────────────────────────────────────
        private void btnLoadMingguan_Click(object sender, EventArgs e)
        {
            TampilkanSaranMingguan();
        }

        // ─── Tombol Simpan Mingguan via SP_UpdateSaranMingguan ──────────────────
        private void btnSimpanMingguan_Click(object sender, EventArgs e)
        {
            if (selectedIdEvaluasi == 0)
            {
                MessageBox.Show("Pilih evaluasi dari tabel terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSaranMingguan.Text))
            {
                MessageBox.Show("Saran mingguan tidak boleh kosong.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SP_UpdateSaranMingguan", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_id_evaluasi", selectedIdEvaluasi);
                    cmd.Parameters.AddWithValue("p_saran_jadwal_minggu_depan", txtSaranMingguan.Text.Trim());

                    MySqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    int affected = Convert.ToInt32(dr["affected_rows"]);
                    dr.Close();

                    if (affected > 0)
                    {
                        MessageBox.Show("Saran mingguan berhasil diupdate!", "Sukses",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TampilkanSaranMingguan();
                    }
                    else
                        MessageBox.Show("Evaluasi tidak ditemukan.", "Gagal",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menyimpan saran mingguan: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ─── Bersihkan form mingguan ─────────────────────────────────────────────
        private void BersihkanFormMingguan()
        {
            selectedIdEvaluasi = 0;
            txtSaranMingguan.Text = "";
        }

        // ─── Kembali ─────────────────────────────────────────────────────────────
        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}