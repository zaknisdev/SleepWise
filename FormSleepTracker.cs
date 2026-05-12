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
                    
                    string query = "INSERT INTO tr_log_tidur (id_user, tanggal, jam_tidur, jam_bangun, durasi_menit) " +
                                   "VALUES (@id, @tgl, @tidur, @bangun, @durasi)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", UserSession.UserId);
                    cmd.Parameters.AddWithValue("@tgl", tanggalTidur.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@tidur", jamTidurStr);
                    cmd.Parameters.AddWithValue("@bangun", jamBangunStr);
                    cmd.Parameters.AddWithValue("@durasi", durasi_menit);

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
                    string query = "SELECT saran_harian FROM ms_kategori_tidur WHERE @durasi BETWEEN min_menit AND max_menit";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@durasi", durasi_menit);

                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        saran = dr["saran_harian"].ToString(); 
                    }
                    else
                    {
                        saran = GenerateSaranManual(jam);
                    }
                }
                catch (Exception)
                {
                    saran = GenerateSaranManual(jam);
                }
            }

            MessageBox.Show($"Data Berhasil Disimpan!\n\nTanggal: {tanggalTidur.ToString("dd/MM/yyyy")}\nDurasi tidur kamu: {jam} jam {menit} menit.\n\nSaran untukmu:\n{saran}");
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
                    string query = "SELECT tanggal, jam_tidur, jam_bangun, durasi_menit FROM tr_log_tidur WHERE id_user = @id ORDER BY tanggal DESC";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", UserSession.UserId);

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvRiwayat.DataSource = dt;
                }
                catch (Exception)
                {
                    
                }
            }
        }
        private void FormSleepTracker_Load(object sender, EventArgs e)
        {
            TampilkanDataTabel(); 
        }
    }
}
