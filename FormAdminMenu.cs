using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SleepWise
{
    
    public partial class FormAdminMenu : Form
    {
        public FormAdminMenu()
        {
            InitializeComponent();
        }

     

        
        private void btnEditSaran_Click(object sender, EventArgs e)
        {
            FormAdminSaran formSaran = new FormAdminSaran();
            formSaran.Show();
        }

        private void btnKelolaUser_Click(object sender, EventArgs e)
        {
            FormAdminPengguna formUser = new FormAdminPengguna();
            formUser.Show();
        }

        
        private void btnLogout_Click(object sender, EventArgs e)
        {
            UserSession.ClearSession();
            MessageBox.Show("Berhasil Logout!", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            this.Close();
        }

        private void FormAdminMenu_Load_1(object sender, EventArgs e)
        {

        }
    }
}