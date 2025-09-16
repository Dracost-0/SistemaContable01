using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using SistemaContable01.Dashboard;


namespace SistemaContable01
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        private void btnConexion_Click(object sender, EventArgs e)
        {
          
            string connectionString = "Server=localhost\\SQLEXPRESS;Database=SysCon01Db;Trusted_Connection=True;Encrypt=False;";



            using (SqlConnection conn = new SqlConnection(connectionString))
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


        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Aquí va tu código de login
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            string connectionString = @"Server=localhost\SQLEXPRESS;Database=SysCon01Db;Trusted_Connection=True;Encrypt=False;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(1) FROM user_login WHERE username=@username AND password=@password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 1)
                    {
                        MessageBox.Show("¡Login correcto!");
                        // Abrir formulario principal
                        DashboardForm dashboard = new DashboardForm();
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


    }
}
