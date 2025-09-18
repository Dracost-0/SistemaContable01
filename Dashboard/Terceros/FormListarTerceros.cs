using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace SistemaContable01.Dashboard.Terceros
{
    public partial class FormListarTerceros : Form
    {
        private readonly string _connectionString =
            @"Server=localhost\SQLEXPRESS;Database=SysCon01Db;Trusted_Connection=True;Encrypt=False;";

        public FormListarTerceros()
        {
            InitializeComponent();
        }

        private void FormListarTerceros_Load(object sender, EventArgs e)
        {
            CargarTerceros();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarTerceros(txtBuscar.Text.Trim());
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CargarTerceros(txtBuscar.Text.Trim());
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void dgvTerceros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            // Aquí podrías abrir un formulario de edición en el futuro.
        }

        // Cambio: string? filtro
        private void CargarTerceros(string? filtro = null)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                conn.Open();

                string sql = @"
SELECT IdTercero, TipoIdentificacion, NumeroIdentificacion, RazonSocial,
       PrimerNombre, OtrosNombres, PrimerApellido, SegundoApellido,
       Direccion, Ciudad, Departamento, Pais, Telefono, Email,
       RegimenTributario, ActividadEconomicaCIIU
FROM dbo.Terceros
WHERE (@Filtro IS NULL
    OR RazonSocial LIKE @Filtro
    OR NumeroIdentificacion LIKE @Filtro
    OR PrimerNombre LIKE @Filtro
    OR PrimerApellido LIKE @Filtro)
ORDER BY IdTercero DESC;";

                using var cmd = new SqlCommand(sql, conn);
                if (string.IsNullOrWhiteSpace(filtro))
                    cmd.Parameters.AddWithValue("@Filtro", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Filtro", $"%{filtro}%");

                using var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);

                dgvTerceros.DataSource = dt;
                lblTotal.Text = $"Total: {dt.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar terceros: " + ex.Message);
            }
        }
    }
}