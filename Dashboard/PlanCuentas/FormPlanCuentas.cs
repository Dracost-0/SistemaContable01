using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaContable01.PlanCuentas
{
    public partial class FormPlanCuentas : Form
    {
        string connectionString = @"Server=localhost\SQLEXPRESS;Database=SysCon01Db;Trusted_Connection=True;Encrypt=False;";

        public FormPlanCuentas()
        {
            InitializeComponent(); // Ahora sí existe
        }

        private void FormPlanCuentas_Load(object sender, EventArgs e)

        {

            CargarPUC();
           

        }

        private void CargarPUC()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM PUC";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvPUC.DataSource = dt; // dgvPUC es del diseñador de este form
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar PUC: " + ex.Message);
            }
        }

    }
}
