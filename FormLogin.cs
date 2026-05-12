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
    public partial class FormLogin : Form
    {
        koneksiDB db = new koneksiDB();
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnConn_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open(); 
                    MessageBox.Show("Koneksi Aman!");
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show("Koneksi Gagal: " + ex.Message);
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Username dan password tidak boleh kosong!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();


                    MySqlCommand cmd = new MySqlCommand("SP_Login", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_username", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("p_password", txtPassword.Text);

                    MySqlDataReader dr = cmd.ExecuteReader();


                    if (dr.Read())
                    {
                        UserSession.UserId = Convert.ToInt32(dr["id_user"]);
                        UserSession.Username = txtUsername.Text;
                        UserSession.Role = dr["role"].ToString();
                        UserSession.TargetTidur = Convert.ToInt32(dr["target_tidur_jam"]);
                        dr.Close();

                        MessageBox.Show("Login Berhasil! Wel Co Me, " + UserSession.Username);

                        
                        if (UserSession.Role == "Admin")
                        {
                            FormAdmin formAdmin = new FormAdmin();
                            formAdminMenu.Show();
                        }
                        else
                        {
                            FormSleepTracker formTracker = new FormSleepTracker();
                            formTracker.Show();
                        }

                        this.Hide(); 
                    }
                    else
                    {
                        
                        MessageBox.Show("Username atau Password salah satunya salah, mungkin!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Koneksi ke database ada trouble dikit: " + ex.Message);
                }
            }
        }

        private void btnLoginVulnerable_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username tidak boleh kosong!"); return;
            }

            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    string queryVuln = "SELECT id_user, username, nama_lengkap, role, target_tidur_jam " +
                                       "FROM ms_user WHERE username = '" + txtUsername.Text +
                                       "' AND password = '" + txtPassword.Text + "'";

                    MySqlCommand cmd = new MySqlCommand(queryVuln, conn);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        UserSession.UserId = Convert.ToInt32(dr["id_user"]);
                        UserSession.Username = dr["username"].ToString();
                        UserSession.Role = dr["role"].ToString();
                        UserSession.TargetTidur = Convert.ToInt32(0);
                        dr.Close();

                        MessageBox.Show("[VULNERABLE] Login berhasil sebagai: " + UserSession.Username);
                        // Untuk demo, cukup tampilkan pesan
                    }
                    else
                    {
                        dr.Close();
                        MessageBox.Show("[VULNERABLE] Login gagal.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private void btnSignup_Click(object sender, EventArgs e)
        {
            FormSignUp formDaftar = new FormSignUp();
            formDaftar.Show();
            this.Hide();
        }
    }
}
