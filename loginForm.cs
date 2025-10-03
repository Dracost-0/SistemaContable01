using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using SistemaContable01.Dashboard;
using SistemaContable01.conexion;

namespace SistemaContable01
{
    public partial class LoginForm : Form   // <-- CAMBIADO A PascalCase
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void BtnConexion_Click(object sender, EventArgs e)
        {
            DatabaseConnection db = new();

            using SqlConnection conn = db.GetConnection();
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("✅ Conexión exitosa a SysCon01Db");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error: " + ex.Message);
                }
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = TxtUsername.Text;
            string password = TxtPassword.Text;

            DatabaseConnection db = new();

            using SqlConnection conn = db.GetConnection();
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(1) FROM user_login WHERE username=@username AND password=@password";
                    SqlCommand cmd = new(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 1)
                    {
                        MessageBox.Show("¡Bienvenido!");
                        DashboardForm dashboard = new();
                        dashboard.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrecta.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de conexión: " + ex.Message);
                }
            }
        }

        private void Usuario_Click(object sender, EventArgs e)
        {

        }
    }
}
