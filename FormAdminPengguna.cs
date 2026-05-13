using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SleepWise
{
    public partial class FormAdminPengguna : Form
    {
        koneksiDB db = new koneksiDB();
        private BindingSource bindingSource = new BindingSource();
        private int selectedIdUser = 0;

        public FormAdminPengguna()
        {
            InitializeComponent();
        }

        
        private void FormAdminPengguna_Load(object sender, EventArgs e)
        {
            // Isi pilihan role di ComboBox
            cmbRole.Items.Clear();
            cmbRole.Items.AddRange(new string[] { "User", "Admin" });

            dgvPengguna.DataSource = bindingSource;
            bindingNavigator1.BindingSource = bindingSource;

            TampilkanDataPengguna();
        }

        
        private void TampilkanDataPengguna()
        {
            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SP_GetSemuaPengguna", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    bindingSource.DataSource = dt;
                    BersihkanForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data pengguna: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvPengguna_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPengguna.CurrentRow == null) return;
            DataRowView row = dgvPengguna.CurrentRow.DataBoundItem as DataRowView;
            if (row == null) return;

            selectedIdUser = Convert.ToInt32(row["id_user"]);
            txtNama.Text = row["nama_lengkap"].ToString();
            txtTarget.Text = row["target_tidur_jam"].ToString();

            /
            cmbRole.SelectedItem = row["role"].ToString();
        }

        
        private void btnCari_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCari.Text))
            {
                TampilkanDataPengguna();
                return;
            }

            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SP_SearchPengguna", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_keyword", txtCari.Text.Trim());

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    bindingSource.DataSource = dt;

                    if (dt.Rows.Count == 0)
                        MessageBox.Show("Data pengguna tidak ditemukan.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mencari pengguna: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ─── Edit pengguna via SP_UpdatePengguna ────────────────────────────────
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedIdUser == 0)
            {
                MessageBox.Show("Pilih pengguna dari tabel terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNama.Text))
            {
                MessageBox.Show("Nama lengkap tidak boleh kosong.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Pilih role terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtTarget.Text, out int target) || target < 1 || target > 24)
            {
                MessageBox.Show("Target tidur harus angka antara 1-24.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SP_UpdatePengguna", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_id_user", selectedIdUser);
                    cmd.Parameters.AddWithValue("p_nama_lengkap", txtNama.Text.Trim());
                    cmd.Parameters.AddWithValue("p_role", cmbRole.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("p_target_tidur", target);

                    MySqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    int affected = Convert.ToInt32(dr["affected_rows"]);
                    dr.Close();

                    if (affected > 0)
                    {
                        MessageBox.Show("Data pengguna berhasil diupdate!", "Sukses",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TampilkanDataPengguna();
                    }
                    else
                        MessageBox.Show("Pengguna tidak ditemukan.", "Gagal",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mengupdate pengguna: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ─── Hapus pengguna via SP_HapusPengguna ────────────────────────────────
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (selectedIdUser == 0)
            {
                MessageBox.Show("Pilih pengguna dari tabel terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedIdUser == UserSession.UserId)
            {
                MessageBox.Show("Tidak bisa menghapus akun yang sedang login!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult konfirmasi = MessageBox.Show(
                $"Yakin ingin menghapus pengguna '{txtNama.Text}'?\nSemua data log tidurnya juga akan terhapus.",
                "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (konfirmasi != DialogResult.Yes) return;

            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SP_HapusPengguna", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_id_user", selectedIdUser);

                    MySqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    int affected = Convert.ToInt32(dr["affected_rows"]);
                    dr.Close();

                    if (affected > 0)
                    {
                        MessageBox.Show("Pengguna berhasil dihapus!", "Sukses",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TampilkanDataPengguna();
                    }
                    else
                        MessageBox.Show("Pengguna tidak ditemukan.", "Gagal",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menghapus pengguna: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ─── Bersihkan field edit ────────────────────────────────────────────────
        private void BersihkanForm()
        {
            selectedIdUser = 0;
            txtNama.Text = "";
            txtTarget.Text = "";
            txtCari.Text = "";
            cmbRole.SelectedIndex = -1;
        }

        
        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}