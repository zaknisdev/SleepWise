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
    
    public partial class FormSignUp : Form
    {
        koneksiDB db = new koneksiDB();
        public FormSignUp()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "" || txtNamaLengkap.Text == "")
            {
                MessageBox.Show("Diisi semua ya ganteng!");
                return; 
            }

            
            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();


                    MySqlCommand cmd = new MySqlCommand("SP_InsertPengguna", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_username", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("p_password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("p_nama_lengkap", txtNamaLengkap.Text.Trim());
                    cmd.Parameters.AddWithValue("p_target_tidur_jam", 8);


                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        string status = dr["status"].ToString();
                        dr.Close();

                        if (status == "SUCCESS")
                        {
                            MessageBox.Show("Pendaftaran berhasil! Silakan login.", "Sukses",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FormLogin formLogin = new FormLogin();
                            formLogin.Show();
                            this.Hide();
                        }
                        else if (status == "DUPLICATE")
                        {
                            MessageBox.Show("Username sudah digunakan! Coba username lain.", "Gagal",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        dr.Close();
                        MessageBox.Show("Pendaftaran gagal, coba lagi.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat mendaftar:\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            this.Hide();
        }
    }
}