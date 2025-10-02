#nullable enable
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace SistemaContable01.Dashboard.Transacciones.CrudTransacciones
{
    public partial class FormCrudTransacciones : Form
    {
        private readonly string _connectionString =
            @"Server=localhost\SQLEXPRESS;Database=SysCon01Db;Trusted_Connection=True;Encrypt=False;";

        private DataTable? _dtActual;
        private bool _modoEdicion;
        private bool _cargando;
        private long? _terceroPendiente;
        private Task? _loadTercerosTask;

        // Expresión reutilizable para armar el nombre del tercero (persona o empresa)
        // Se prioriza Razón Social; si está vacía se arma con nombres y apellidos.
        private const string ExprNombreTercero = @"
COALESCE(
    NULLIF(LTRIM(RTRIM(t3.RazonSocial)),''),
    LTRIM(RTRIM(
        CONCAT(
            COALESCE(NULLIF(LTRIM(RTRIM(t3.PrimerNombre)) ,''),''),
            CASE WHEN ISNULL(t3.OtrosNombres,'')<>'' THEN ' '+LTRIM(RTRIM(t3.OtrosNombres)) ELSE '' END,
            CASE WHEN ISNULL(t3.PrimerApellido,'')<>'' THEN ' '+LTRIM(RTRIM(t3.PrimerApellido)) ELSE '' END,
            CASE WHEN ISNULL(t3.SegundoApellido,'')<>'' THEN ' '+LTRIM(RTRIM(t3.SegundoApellido)) ELSE '' END
        )
    ))
)";

        public FormCrudTransacciones()
        {
            InitializeComponent();
            ConfigurarGrid();

            _loadTercerosTask = CargarTercerosAsync();
            cboCampoNumero.SelectedIndex = 0;

            btnBuscar.Click += async (_, __) => await BuscarAsync();
            btnLimpiar.Click += (_, __) => Limpiar();
            btnEditar.Click += (_, __) => HabilitarEdicion(true);
            btnCancelarEdicion.Click += (_, __) => CancelarEdicion();
            btnGuardar.Click += async (_, __) => await GuardarCambiosAsync();
            btnAnular.Click += async (_, __) => await AnularAsync();

            txtTipoDoc.KeyDown += async (_, e) =>
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await BuscarAsync(); }
            };
            txtNumero.KeyDown += async (_, e) =>
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await BuscarAsync(); }
            };

            dtpFecha.ValueChanged += (_, __) => { if (_modoEdicion) MarcarHeaderModificado(); };
            cboTercero.SelectedIndexChanged += (_, __) => { if (_modoEdicion && !_cargando) MarcarHeaderModificado(); };
            txtDescripcionTrans.TextChanged += (_, __) => { if (_modoEdicion && !_cargando) MarcarHeaderModificado(); };

            dgvTransacciones.CellValueChanged += (_, __) =>
            {
                if (_modoEdicion) RecalcularTotales();
            };
            dgvTransacciones.DataError += (_, e) =>
            {
                MessageBox.Show("Dato inválido: " + (e.Exception?.Message ?? ""),
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.ThrowException = false;
            };

            ActualizarEstadoBotones();
        }

        #region Helpers
        private static string GetString(DataRow r, string col, string fallback = "") =>
            r.IsNull(col) ? fallback : (r[col] as string) ?? fallback;
        private static long GetLong(DataRow r, string col) =>
            r.IsNull(col) ? 0L : Convert.ToInt64(r[col]);
        private static decimal GetDecimal(DataRow r, string col) =>
            r.Field<decimal?>(col) ?? 0m;
        #endregion

        private async Task CargarTercerosAsync()
        {
            try
            {
                using var cn = new SqlConnection(_connectionString);
                await cn.OpenAsync();

                // Se construye el “NombreTercero” usando la expresión (sin alias t3 aquí)
                string sql = $@"
SELECT 
    IdTercero,
    COALESCE(
        NULLIF(LTRIM(RTRIM(RazonSocial)),''),
        LTRIM(RTRIM(
            CONCAT(
                COALESCE(NULLIF(LTRIM(RTRIM(PrimerNombre)) ,''),''),
                CASE WHEN ISNULL(OtrosNombres,'')<>'' THEN ' '+LTRIM(RTRIM(OtrosNombres)) ELSE '' END,
                CASE WHEN ISNULL(PrimerApellido,'')<>'' THEN ' '+LTRIM(RTRIM(PrimerApellido)) ELSE '' END,
                CASE WHEN ISNULL(SegundoApellido,'')<>'' THEN ' '+LTRIM(RTRIM(SegundoApellido)) ELSE '' END
            )
        ))
    ) AS NombreTercero
FROM dbo.Terceros
ORDER BY NombreTercero;";

                using var cmd = new SqlCommand(sql, cn);
                var dt = new DataTable();
                using var rd = await cmd.ExecuteReaderAsync();
                dt.Load(rd);

                if (!dt.Columns.Contains("Display"))
                    dt.Columns.Add("Display", typeof(string),
                        "Convert(IdTercero, 'System.String') + ' - ' + NombreTercero");

                cboTercero.DisplayMember = "Display";
                cboTercero.ValueMember = "IdTercero";
                cboTercero.DataSource = dt;

                if (_terceroPendiente.HasValue)
                {
                    try { cboTercero.SelectedValue = _terceroPendiente.Value; }
                    catch { }
                    _terceroPendiente = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar terceros: " + ex.Message);
            }
        }

        private void ConfigurarGrid()
        {
            var g = dgvTransacciones;
            g.AutoGenerateColumns = false;
            g.Columns.Clear();
            g.AllowUserToResizeRows = false;

            void Col(string header, string prop, int width, string? format = null, bool readOnly = true, bool visible = true)
            {
                var c = new DataGridViewTextBoxColumn
                {
                    HeaderText = header,
                    DataPropertyName = prop,
                    Name = prop,
                    Width = width,
                    ReadOnly = readOnly,
                    Visible = visible
                };
                if (!string.IsNullOrWhiteSpace(format))
                    c.DefaultCellStyle.Format = format;
                g.Columns.Add(c);
            }

            Col("Línea", "Linea", 55);
            Col("IdTransacción", "IdTransaccion", 90, visible: false);
            Col("Tipo", "TipoDocContable", 60, visible: false);
            Col("Fecha", "Fecha", 90, "yyyy-MM-dd", visible: false);
            Col("IdTercero", "IdTercero", 70, visible: false);
            Col("Tercero", "NombreTercero", 160, visible: false);
            Col("Num. Comprobante", "NumeroComprobante", 120, visible: false);
            Col("Cuenta", "CuentaContable", 110, null, readOnly: false);
            Col("Descripción Línea", "DescripcionLinea", 260, null, readOnly: false);
            Col("Débito", "Debito", 100, "N2", readOnly: false);
            Col("Crédito", "Credito", 100, "N2", readOnly: false);
            Col("Asiento", "IdAsiento", 80);
            Col("Num. Documento", "NumeroDocumento", 120, visible: false);
            Col("Desc. Transacción", "DescripcionTransaccion", 200, visible: false);
        }

        private void FormatearTrasCarga()
        {
            if (dgvTransacciones.Columns.Contains("Debito"))
                dgvTransacciones.Columns["Debito"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            if (dgvTransacciones.Columns.Contains("Credito"))
                dgvTransacciones.Columns["Credito"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
        }

        private async Task BuscarAsync()
        {
            if (_modoEdicion)
            {
                MessageBox.Show("Termina o cancela la edición primero.");
                return;
            }

            LimpiarHeaderVisual();

            string tipo = txtTipoDoc.Text.Trim();
            string numero = txtNumero.Text.Trim();
            string campo = cboCampoNumero.SelectedItem?.ToString() ?? "NumeroComprobante";

            if (string.IsNullOrWhiteSpace(tipo))
            {
                MessageBox.Show("Indica TipoDocContable."); txtTipoDoc.Focus(); return;
            }
            if (string.IsNullOrWhiteSpace(numero))
            {
                MessageBox.Show("Indica el número."); txtNumero.Focus(); return;
            }

            try
            {
                var dt = await ConsultarTransaccionAsync(tipo, numero, campo);
                if (dt.Rows.Count == 0)
                {
                    lblEstado.Text = "Sin líneas.";
                    dgvTransacciones.DataSource = null;
                    _dtActual = null;
                    return;
                }

                _cargando = true;
                _dtActual = dt;
                dgvTransacciones.DataSource = dt;
                FormatearTrasCarga();

                var first = dt.Rows[0];
                var idTrans = GetLong(first, "IdTransaccion");
                var idTercero = GetLong(first, "IdTercero");
                string nombreTercero = GetString(first, "NombreTercero");
                dtpFecha.Value = first.Field<DateTime>("Fecha");
                txtDescripcionTrans.Text = GetString(first, "DescripcionTransaccion");

                if (cboTercero.DataSource != null)
                {
                    try { cboTercero.SelectedValue = idTercero; }
                    catch { _terceroPendiente = idTercero; }
                }
                else
                {
                    _terceroPendiente = idTercero;
                }

                lblHeader.Text =
                    $"Transacción: {idTrans}    Tipo: {tipo}    Número: {numero}    " +
                    $"Tercero: {idTercero}" + (string.IsNullOrWhiteSpace(nombreTercero) ? "" : $" - {nombreTercero}");

                RecalcularTotales();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error consultando: " + ex.Message);
            }
            finally
            {
                _cargando = false;
                ActualizarEstadoBotones();
            }
        }

        private async Task<DataTable> ConsultarTransaccionAsync(string tipo, string numero, string campo)
        {
            if (campo is not ("NumeroComprobante" or "NumeroDocumento"))
                throw new InvalidOperationException("Campo no permitido.");

            string sql = $@"
SELECT ROW_NUMBER() OVER(PARTITION BY t.IdTransaccion ORDER BY t.IdAsiento) AS Linea,
       t.IdTransaccion,
       t.TipoDocContable,
       t.Fecha,
       t.IdTercero,
       {ExprNombreTercero} AS NombreTercero,
       t.NumeroComprobante,
       t.DescripcionTransaccion,
       t.CuentaContable,
       t.Debito,
       t.Credito,
       t.IdAsiento,
       t.NumeroDocumento,
       t.DescripcionLinea
FROM dbo.Transacciones t
LEFT JOIN dbo.Terceros t3 ON t3.IdTercero = t.IdTercero
WHERE t.TipoDocContable = @tipo AND t.[{campo}] = @numero
ORDER BY t.IdAsiento;";

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

        private void HabilitarEdicion(bool habilitar)
        {
            if (_dtActual == null)
            {
                MessageBox.Show("Busca primero.");
                return;
            }
            _modoEdicion = habilitar;
            dgvTransacciones.ReadOnly = !habilitar;

            string[] noEdit =
            {
                "Linea","IdTransaccion","TipoDocContable","Fecha","IdTercero",
                "NumeroComprobante","IdAsiento","NumeroDocumento","DescripcionTransaccion","NombreTercero"
            };
            foreach (DataGridViewColumn c in dgvTransacciones.Columns)
                c.ReadOnly = !habilitar || noEdit.Contains(c.DataPropertyName);

            dtpFecha.Enabled = habilitar;
            cboTercero.Enabled = habilitar;
            txtDescripcionTrans.ReadOnly = !habilitar;

            ActualizarEstadoBotones();
        }

        private void CancelarEdicion()
        {
            if (_dtActual == null) return;
            if (_dtActual.GetChanges() != null &&
                MessageBox.Show("Descartar cambios?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            _dtActual.RejectChanges();
            var first = _dtActual.Rows[0];
            _cargando = true;
            dtpFecha.Value = first.Field<DateTime>("Fecha");
            long idTercero = GetLong(first, "IdTercero");
            if (cboTercero.DataSource != null)
            {
                try { cboTercero.SelectedValue = idTercero; } catch { }
            }
            txtDescripcionTrans.Text = GetString(first, "DescripcionTransaccion");
            _cargando = false;

            HabilitarEdicion(false);
            RecalcularTotales();
        }

        private bool ValidarLineas(out string mensaje)
        {
            mensaje = "";
            if (_dtActual == null) return false;

            foreach (DataRow r in _dtActual.Rows)
            {
                if (r.RowState == DataRowState.Deleted) continue;
                decimal deb = GetDecimal(r, "Debito");
                decimal cre = GetDecimal(r, "Credito");
                if (deb != 0 && cre != 0) { mensaje = $"Línea {r["Linea"]}: Débito y Crédito."; return false; }
                if (deb < 0 || cre < 0) { mensaje = $"Línea {r["Linea"]}: valores negativos."; return false; }
            }
            var totalD = _dtActual.AsEnumerable().Where(r => r.RowState != DataRowState.Deleted)
                .Sum(r => GetDecimal(r, "Debito"));
            var totalC = _dtActual.AsEnumerable().Where(r => r.RowState != DataRowState.Deleted)
                .Sum(r => GetDecimal(r, "Credito"));
            if (totalD != totalC)
            {
                mensaje = $"Descuadrado. Débito {totalD:N2} / Crédito {totalC:N2}.";
                return false;
            }
            return true;
        }

        private void PropagarHeaderATabla()
        {
            if (_dtActual == null) return;
            if (cboTercero.SelectedValue == null) return;

            long idTercero = Convert.ToInt64(cboTercero.SelectedValue);
            DateTime fecha = dtpFecha.Value.Date;
            string desc = txtDescripcionTrans.Text.Trim();
            string displayTercero = cboTercero.Text; // “Id - Nombre”

            foreach (DataRow r in _dtActual.Rows)
            {
                if (r.RowState == DataRowState.Deleted) continue;
                r["IdTercero"] = idTercero;
                r["Fecha"] = fecha;
                r["DescripcionTransaccion"] = desc;
            }

            if (_dtActual.Rows.Count > 0)
            {
                long transId = GetLong(_dtActual.Rows[0], "IdTransaccion");
                string tipo = GetString(_dtActual.Rows[0], "TipoDocContable");
                string num = GetString(_dtActual.Rows[0], "NumeroComprobante");
                lblHeader.Text = $"Transacción: {transId}    Tipo: {tipo}    Número: {num}    Tercero: {displayTercero}";
            }
        }

        private async Task GuardarCambiosAsync()
        {
            if (!_modoEdicion || _dtActual == null)
            {
                MessageBox.Show("Nada que guardar.");
                return;
            }
            if (!ValidarLineas(out var msg))
            {
                MessageBox.Show(msg, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PropagarHeaderATabla();

            var changes = _dtActual.GetChanges();
            if (changes == null || changes.Rows.Count == 0)
            {
                MessageBox.Show("Sin modificaciones.");
                HabilitarEdicion(false);
                return;
            }

            try
            {
                using var cn = new SqlConnection(_connectionString);
                await cn.OpenAsync();
                using var tx = cn.BeginTransaction();

                const string sqlUpdate = @"
UPDATE dbo.Transacciones
SET CuentaContable=@CuentaContable,
    DescripcionLinea=@DescripcionLinea,
    Debito=@Debito,
    Credito=@Credito,
    Fecha=@Fecha,
    IdTercero=@IdTercero,
    DescripcionTransaccion=@DescripcionTransaccion
WHERE IdTransaccion=@IdTransaccion AND IdAsiento=@IdAsiento;";

                foreach (DataRow r in _dtActual.Rows)
                {
                    if (r.RowState != DataRowState.Modified) continue;

                    using var cmd = new SqlCommand(sqlUpdate, cn, tx);
                    cmd.Parameters.Add("@CuentaContable", SqlDbType.VarChar, 50).Value = GetString(r, "CuentaContable");
                    cmd.Parameters.Add("@DescripcionLinea", SqlDbType.VarChar, 500).Value = GetString(r, "DescripcionLinea");
                    cmd.Parameters.Add("@Debito", SqlDbType.Decimal).Value = GetDecimal(r, "Debito");
                    cmd.Parameters.Add("@Credito", SqlDbType.Decimal).Value = GetDecimal(r, "Credito");
                    cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value = r.Field<DateTime>("Fecha");
                    cmd.Parameters.Add("@IdTercero", SqlDbType.BigInt).Value = GetLong(r, "IdTercero");
                    cmd.Parameters.Add("@DescripcionTransaccion", SqlDbType.VarChar, 500)
                        .Value = GetString(r, "DescripcionTransaccion");
                    cmd.Parameters.Add("@IdTransaccion", SqlDbType.BigInt).Value = GetLong(r, "IdTransaccion");
                    cmd.Parameters.Add("@IdAsiento", SqlDbType.UniqueIdentifier).Value = r.Field<Guid>("IdAsiento");
                    await cmd.ExecuteNonQueryAsync();
                }

                tx.Commit();
                _dtActual.AcceptChanges();
                HabilitarEdicion(false);
                RecalcularTotales();
                MessageBox.Show("Cambios guardados.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error guardando: " + ex.Message);
            }
        }

        private async Task AnularAsync()
        {
            if (_dtActual == null || _dtActual.Rows.Count == 0)
            {
                MessageBox.Show("Carga una transacción.");
                return;
            }
            if (_modoEdicion)
            {
                MessageBox.Show("Guarda o cancela antes de anular.");
                return;
            }

            var row0 = _dtActual.Rows[0];
            string tipo = GetString(row0, "TipoDocContable");
            string numero = GetString(row0, "NumeroComprobante");
            if (string.Equals(numero, "ANULADO", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Ya anulada.");
                return;
            }
            if (MessageBox.Show($"Anular comprobante {numero}?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using var cn = new SqlConnection(_connectionString);
                await cn.OpenAsync();
                using var tx = cn.BeginTransaction();
                const string sql = @"
UPDATE dbo.Transacciones
SET NumeroComprobante='ANULADO',
    DescripcionTransaccion = CASE WHEN DescripcionTransaccion LIKE '%ANULADO%' THEN DescripcionTransaccion ELSE DescripcionTransaccion + ' (ANULADO)' END,
    DescripcionLinea = CASE WHEN DescripcionLinea LIKE '%ANULADO%' THEN DescripcionLinea ELSE DescripcionLinea + ' (ANULADO)' END,
    Debito=0, Credito=0
WHERE TipoDocContable=@Tipo AND NumeroComprobante=@Numero;";
                using var cmd = new SqlCommand(sql, cn, tx);
                cmd.Parameters.Add("@Tipo", SqlDbType.VarChar, 20).Value = tipo;
                cmd.Parameters.Add("@Numero", SqlDbType.VarChar, 50).Value = numero;
                int aff = await cmd.ExecuteNonQueryAsync();
                if (aff == 0)
                {
                    tx.Rollback();
                    MessageBox.Show("No encontrado.");
                    return;
                }
                tx.Commit();
                MessageBox.Show("Anulado.");
                await BuscarAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al anular: " + ex.Message);
            }
        }

        private void RecalcularTotales()
        {
            if (_dtActual == null)
            {
                lblTotales.Text = "Total Débito: 0.00    Total Crédito: 0.00";
                lblEstado.Text = "";
                return;
            }
            decimal totalDebito = _dtActual.AsEnumerable().Sum(r => GetDecimal(r, "Debito"));
            decimal totalCredito = _dtActual.AsEnumerable().Sum(r => GetDecimal(r, "Credito"));
            lblTotales.Text = $"Total Débito: {totalDebito:N2}    Total Crédito: {totalCredito:N2}";
            lblEstado.Text = totalDebito == totalCredito
                ? "Asiento CUADRADO ✔"
                : $"Diferencia: {(totalDebito - totalCredito):N2}";
        }

        private void Limpiar()
        {
            if (_modoEdicion)
            {
                MessageBox.Show("Cancela edición primero.");
                return;
            }
            _dtActual = null;
            dgvTransacciones.DataSource = null;
            LimpiarHeaderVisual();
            ActualizarEstadoBotones();
        }

        private void LimpiarHeaderVisual()
        {
            lblHeader.Text = "";
            lblTotales.Text = "Total Débito: 0.00    Total Crédito: 0.00";
            lblEstado.Text = "";
            txtDescripcionTrans.Clear();
        }

        private void ActualizarEstadoBotones()
        {
            bool hay = _dtActual is { Rows.Count: > 0 };
            btnEditar.Enabled = hay && !_modoEdicion;
            btnGuardar.Enabled = hay && _modoEdicion;
            btnCancelarEdicion.Enabled = hay && _modoEdicion;
            btnAnular.Enabled = hay && !_modoEdicion;
        }

        private void MarcarHeaderModificado()
        {
            if (_dtActual == null) return;
            if (!lblHeader.Text.EndsWith("*"))
                lblHeader.Text += " *";
        }
    }
}