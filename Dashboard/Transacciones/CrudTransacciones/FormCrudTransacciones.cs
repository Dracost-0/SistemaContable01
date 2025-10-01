
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient; // cambiado (antes System.Data.SqlClient)

namespace SistemaContable01.Dashboard.Transacciones.CrudTransacciones
{
    public partial class FormCrudTransacciones : Form
    {

        private readonly string _connectionString =
         @"Server=localhost\SQLEXPRESS;Database=SysCon01Db;Trusted_Connection=True;Encrypt=False;";

        public FormCrudTransacciones()
        {
            InitializeComponent();
            ConfigurarGrid();
            cboCampoNumero.SelectedIndex = 0;

            btnBuscar.Click += async (_, __) => await BuscarAsync();
            btnLimpiar.Click += (_, __) => Limpiar();

            txtTipoDoc.KeyDown += async (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await BuscarAsync(); }
            };
            txtNumero.KeyDown += async (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await BuscarAsync(); }
            };
        }

        private void ConfigurarGrid()
        {
            dgvTransacciones.AutoGenerateColumns = false;
            dgvTransacciones.Columns.Clear();

            void Col(string header, string prop, int width, string? format = null)
            {
                var c = new DataGridViewTextBoxColumn
                {
                    HeaderText = header,
                    DataPropertyName = prop,
                    Width = width,
                    ReadOnly = true
                };
                if (!string.IsNullOrWhiteSpace(format))
                    c.DefaultCellStyle.Format = format;
                dgvTransacciones.Columns.Add(c);
            }

            Col("IdTransacción", "IdTransaccion", 90);
            Col("Tipo", "TipoDocContable", 60);
            Col("Fecha", "Fecha", 90, "yyyy-MM-dd");
            Col("Tercero", "IdTercero", 80);
            Col("Num. Comprobante", "NumeroComprobante", 120);
            Col("Cuenta", "CuentaContable", 90);
            Col("Descripción Línea", "DescripcionLinea", 200);
            Col("Débito", "Debito", 90, "N2");
            Col("Crédito", "Credito", 90, "N2");
            Col("Asiento", "IdAsiento", 70);
            Col("Num. Documento", "NumeroDocumento", 120);
        }

        private async Task BuscarAsync()
        {
            lblHeader.Text = "";
            lblTotales.Text = "";
            lblEstado.Text = "";
            dgvTransacciones.DataSource = null;

            string tipo = txtTipoDoc.Text.Trim();
            string numero = txtNumero.Text.Trim();
            string campo = cboCampoNumero.SelectedItem?.ToString() ?? "NumeroComprobante";

            if (string.IsNullOrWhiteSpace(tipo))
            {
                MessageBox.Show("Ingrese el TipoDocContable.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTipoDoc.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(numero))
            {
                MessageBox.Show("Ingrese el número.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumero.Focus();
                return;
            }

            try
            {
                var dt = await ConsultarTransaccionAsync(tipo, numero, campo);
                if (dt.Rows.Count == 0)
                {
                    lblEstado.Text = "No se encontraron líneas para la transacción.";
                    return;
                }

                dgvTransacciones.DataSource = dt;

                var first = dt.Rows[0];
                lblHeader.Text =
                    $"Transacción: {first["IdTransaccion"]}  Tipo: {first["TipoDocContable"]}  Fecha: {((DateTime)first["Fecha"]).ToShortDateString()}  " +
                    $"Tercero: {first["IdTercero"]}  Desc: {first["DescripcionTransaccion"]}";

                decimal totalDebito = dt.AsEnumerable().Sum(r => r.Field<decimal>("Debito"));
                decimal totalCredito = dt.AsEnumerable().Sum(r => r.Field<decimal>("Credito"));
                lblTotales.Text = $"Total Débito: {totalDebito:N2}    Total Crédito: {totalCredito:N2}";
                lblEstado.Text = totalDebito == totalCredito
                    ? "Asiento CUADRADO ✔"
                    : $"Diferencia (Débito - Crédito): {(totalDebito - totalCredito):N2}";
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                if (ex.InnerException != null)
                    msg += Environment.NewLine + "Inner: " + ex.InnerException.Message;

                MessageBox.Show(
                    "Error abriendo conexión / consultando:" + Environment.NewLine + msg,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async Task<DataTable> ConsultarTransaccionAsync(string tipo, string numero, string campo)
        {
            if (campo != "NumeroComprobante" && campo != "NumeroDocumento")
                throw new InvalidOperationException("Campo no permitido.");

            string sql = $@"
SELECT TOP (1000)
    [IdTransaccion],
    [TipoDocContable],
    [Fecha],
    [IdTercero],
    [NumeroComprobante],
    [DescripcionTransaccion],
    [CuentaContable],
    [Debito],
    [Credito],
    [IdAsiento],
    [NumeroDocumento],
    [DescripcionLinea]
FROM [SysCon01Db].[dbo].[Transacciones]
WHERE [TipoDocContable] = @tipo AND [{campo}] = @numero
ORDER BY [IdAsiento];";

            var dt = new DataTable();
            await using var cn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Add("@tipo", SqlDbType.VarChar, 20).Value = tipo;
            cmd.Parameters.Add("@numero", SqlDbType.VarChar, 50).Value = numero;

            await cn.OpenAsync();
            await using var rd = await cmd.ExecuteReaderAsync();
            dt.Load(rd);
            return dt;
        }

        private async Task ProbarConexionAsync()
        {
            try
            {
                using var cn = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
                await cn.OpenAsync();
                using var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT TOP 1 name FROM sys.databases";
                var valor = await cmd.ExecuteScalarAsync();
                MessageBox.Show("Conexión OK. Ejemplo: " + valor);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fallo conexión: " + ex.Message);
            }
        }

        private void Limpiar()
        {
            txtNumero.Clear();
            dgvTransacciones.DataSource = null;
            lblHeader.Text = "";
            lblTotales.Text = "";
            lblEstado.Text = "";
            txtNumero.Focus();
        }
    }
}