using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SleepWise
{
    public partial class FormSleepTracker : Form
    {
        koneksiDB db = new koneksiDB();
        private BindingSource bindingSource = new BindingSource();
        int durasi_menit = 0;
        string jamTidurStr = "";
        string jamBangunStr = "";
        DateTime tanggalTidur;

        public FormSleepTracker()
        {
            InitializeComponent();
        }

        private void HitungDurasiHarian()
        {
            DateTime waktuTidur = dtpTidur.Value;
            DateTime waktuBangun = dtpBangun.Value;

            
            if (waktuBangun < waktuTidur)
            {
                waktuBangun = waktuBangun.AddDays(1);
            }

            TimeSpan selisih = waktuBangun - waktuTidur;
            durasi_menit = (int)selisih.TotalMinutes;

            jamTidurStr = dtpTidur.Value.ToString("HH:mm:ss");
            jamBangunStr = dtpBangun.Value.ToString("HH:mm:ss");


            tanggalTidur = dtpTanggal.Value.Date;
        }

        private void SimpanDataHarian()
        {
            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("SP_InsertLogTidur", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_id_user", UserSession.UserId);
                    cmd.Parameters.AddWithValue("p_tanggal", tanggalTidur.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("p_jam_tidur", jamTidurStr);
                    cmd.Parameters.AddWithValue("p_jam_bangun", jamBangunStr);
                    cmd.Parameters.AddWithValue("p_durasi_menit", durasi_menit);
                    cmd.ExecuteNonQuery();

                    TampilkanDataTabel();
                }

                catch (Exception ex)
                {
                    MessageBox.Show("data gagal disimpan ke database: " + ex.Message);
                }
            }
        }

        private void TampilSaranHarian()
        {
            int jam = durasi_menit / 60;
            int menit = durasi_menit % 60;
            string saran = "";

            using (MySqlConnection conn = db.GetConnection())
            {
               

                try
                {

                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SP_GetSaranHarian", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_durasi_menit", durasi_menit);

                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                        saran = dr["saran_harian"].ToString();
                    dr.Close();
                }
                catch
                {
                    saran = GenerateSaranManual(jam);
                }
            }

            MessageBox.Show(
               $"Data Berhasil Disimpan!\n\n" +
               $"Tanggal: {tanggalTidur:dd/MM/yyyy}\n" +
               $"Durasi tidur kamu: {jam} jam {menit} menit.\n\n" +
               $"Saran untukmu:\n{saran}");
        }

        
        private string GenerateSaranManual(int jamTidur)
        {
            if (jamTidur < 6) return "Tidurmu kurang! Usahakan istirahat lebih awal besok.";
            if (jamTidur >= 6 && jamTidur <= 8) return "Mantap! Waktu tidurmu ideal. Pertahankan jam tidurmu.";
            return "Tidurmu kelamaan! kelamaan tidur malah bikin badan lemes seharian.";
        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HitungDurasiHarian();  
            SimpanDataHarian();
            TampilSaranHarian();
        }

        private void TampilkanDataTabel()
        {
            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query =
                        "SELECT tanggal       AS Tanggal, " +
                        "       jam_tidur     AS `Jam Tidur`, " +
                        "       jam_bangun    AS `Jam Bangun`, " +
                        "       durasi_menit  AS `Durasi (Menit)`, " +
                        "       durasi_jam    AS `Durasi (Jam)`, " +
                        "       nama_kategori AS Kategori " +
                        "FROM   vw_log_tidur_lengkap " +
                        "WHERE  id_user = @id " +
                        "ORDER  BY tanggal DESC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", UserSession.UserId);

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    bindingSource.DataSource = dt;
                }
                catch (Exception)
                {
                    
                }
            }
        }
        private void FormSleepTracker_Load(object sender, EventArgs e)
        {
            dgvRiwayat.DataSource = bindingSource;
            bindingNavigator1.BindingSource = bindingSource;

            TampilkanDataTabel();
        }

        private void btnSaranMingguan_Click(object sender, EventArgs e)
        {
            FormSaranMingguan f = new FormSaranMingguan();
            f.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            UserSession.ClearSession();
            MessageBox.Show("Berhasil Logout!", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            FormLogin fl = new FormLogin();
            fl.Show();
            this.Hide();
        }
    }
}
