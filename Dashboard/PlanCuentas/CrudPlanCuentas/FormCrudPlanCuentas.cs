using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace SistemaContable01.Dashboard.PlanCuentas.CrudPlanCuentas
{
    public partial class FormCrudPlanCuentas : Form
    {
        private readonly string _connectionString =
            @"Server=localhost\SQLEXPRESS;Database=SysCon01Db;Trusted_Connection=True;Encrypt=False;";

        public FormCrudPlanCuentas()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                // RTRIM to remove CHAR(6) trailing spaces
                using var da = new SqlDataAdapter(
                    "SELECT RTRIM(Codigo) AS Codigo, Nombre, Nivel, Naturaleza FROM dbo.PUC ORDER BY Codigo", conn);
                var dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando PUC: " + ex.Message);
            }
        }           

        private void btnAgregar_Click(object? sender, EventArgs e)
        {
            string codigo = (txtCodigo.Text ?? "").Trim();
            string nombre = (txtNombre.Text ?? "").Trim();
            int nivel = (int)nudNivel.Value;
            string? naturaleza = string.IsNullOrWhiteSpace(cboNaturaleza.Text) ? null : cboNaturaleza.Text.Trim();

            if (codigo.Length != 8)
            {
                MessageBox.Show("Código debe tener 8 caracteres.");
                return;
            }
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Nombre es requerido.");
                return;
            }

            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(
                    "INSERT INTO dbo.PUC (Codigo, Nombre, Nivel, Naturaleza) VALUES (@Codigo, @Nombre, @Nivel, @Naturaleza)", conn);
                cmd.Parameters.AddWithValue("@Codigo", codigo);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Nivel", nivel);
                cmd.Parameters.AddWithValue("@Naturaleza", (object?)naturaleza ?? DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
                LoadData();
            }
            catch (SqlException ex) when (ex.Number is 2627 or 2601)
            {
                MessageBox.Show("El código ya existe.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar: " + ex.Message);
            }
        }

        private void btnActualizar_Click(object? sender, EventArgs e)
        {
            string codigo = (txtCodigo.Text ?? "").Trim();
            string nombre = (txtNombre.Text ?? "").Trim();
            int nivel = (int)nudNivel.Value;
            string? naturaleza = string.IsNullOrWhiteSpace(cboNaturaleza.Text) ? null : cboNaturaleza.Text.Trim();

            if (codigo.Length != 6)
            {
                MessageBox.Show("Seleccione/ingrese un Código de 6 caracteres.");
                return;
            }

            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(
                    "UPDATE dbo.PUC SET Nombre=@Nombre, Nivel=@Nivel, Naturaleza=@Naturaleza WHERE Codigo=@Codigo", conn);
                cmd.Parameters.AddWithValue("@Codigo", codigo);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Nivel", nivel);
                cmd.Parameters.AddWithValue("@Naturaleza", (object?)naturaleza ?? DBNull.Value);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows == 0) MessageBox.Show("Código no encontrado.");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            string codigo = (txtCodigo.Text ?? "").Trim();
            if (codigo.Length != 6)
            {
                MessageBox.Show("Seleccione un Código de 8 caracteres.");
                return;
            }

            if (MessageBox.Show($"¿Eliminar la cuenta {codigo}?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand("DELETE FROM dbo.PUC WHERE Codigo=@Codigo", conn);
                cmd.Parameters.AddWithValue("@Codigo", codigo);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows == 0) MessageBox.Show("Código no encontrado.");
                LoadData();
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                MessageBox.Show("No se puede eliminar: la cuenta tiene relaciones o movimientos.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        private void dataGridView1_SelectionChanged(object? sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            txtCodigo.Text = dataGridView1.CurrentRow.Cells["Codigo"].Value?.ToString() ?? "";
            txtNombre.Text = dataGridView1.CurrentRow.Cells["Nombre"].Value?.ToString() ?? "";

            if (int.TryParse(dataGridView1.CurrentRow.Cells["Nivel"].Value?.ToString(), out int n))
                nudNivel.Value = Math.Max(nudNivel.Minimum, Math.Min(nudNivel.Maximum, n));
            else
                nudNivel.Value = nudNivel.Minimum;

            cboNaturaleza.Text = dataGridView1.CurrentRow.Cells["Naturaleza"].Value?.ToString() ?? "";
        }
    }
}