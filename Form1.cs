using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaContable01
{
    public partial class Form1 : Form
    {
        public Form1()
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
    }
}
