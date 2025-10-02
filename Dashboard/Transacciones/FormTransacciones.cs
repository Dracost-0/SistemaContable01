using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SistemaContable01.Dashboard.Transacciones
{
    public partial class FormTransacciones : Form
    {
        private readonly string _connectionString =
            @"Server=localhost\SQLEXPRESS;Database=SysCon01Db;Trusted_Connection=True;Encrypt=False;";

        private readonly DataTable _dtLineas = new();
        private readonly AutoCompleteStringCollection _acCuentas = new();
        private readonly HashSet<string> _cuentasValidas = new(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, string> _cuentasNombre = new(StringComparer.OrdinalIgnoreCase);

        private readonly AutoCompleteStringCollection _acTerceros = new();
        private readonly Dictionary<string, int> _tercerosNombreToId = new(StringComparer.OrdinalIgnoreCase);

        private readonly CheckBox chkNumeroManual = new();
        private readonly Button btnRegistrarTransaccion = new();
        private readonly BindingSource _bsTerceros = new();

        private sealed class TerceroItem
        {
            public int Id { get; init; }
            public string Identificacion { get; init; } = "";
            public string Nombre { get; init; } = "";
            public string Display => $"{Identificacion} - {Nombre}";
        }

        public FormTransacciones()
        {
            InitializeComponent();
            CrearControlesDinamicos();
            PrepararTablaLineas();
            ConfigurarGrid();
        }

        private void FormTransacciones_Load(object sender, EventArgs e)
        {
            CargarListaTerceros();
            CargarTercerosAutoComplete();
            CargarCuentasPUC();

            if (cboTipoDoc.Items.Count > 0 && cboTipoDoc.SelectedIndex < 0)
                cboTipoDoc.SelectedIndex = 0;

            dtpFecha.Value = DateTime.Today;
            AsignarNumeroAutomaticoPreview();
            AcceptButton = btnRegistrarTransaccion;

            if (cboIdDocumento != null) cboIdDocumento.Text = "";
        }

        #region UI Dinámico
        private void CrearControlesDinamicos()
        {
            chkNumeroManual.Text = "Número manual";
            chkNumeroManual.AutoSize = true;
            chkNumeroManual.Left = txtNumero.Right + 10;
            chkNumeroManual.Top = txtNumero.Top + 2;
            chkNumeroManual.CheckedChanged += (_, _) =>
            {
                txtNumero.ReadOnly = !chkNumeroManual.Checked;
                if (!chkNumeroManual.Checked)
                    AsignarNumeroAutomaticoPreview();
            };
            panelHeader.Controls.Add(chkNumeroManual);

            btnRegistrarTransaccion.Text = "Registrar Transacción";
            btnRegistrarTransaccion.Width = 150;
            btnRegistrarTransaccion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRegistrarTransaccion.Top = 10;
            btnRegistrarTransaccion.Left = panelBottom.ClientSize.Width - btnRegistrarTransaccion.Width - 10;
            btnRegistrarTransaccion.Click += BtnRegistrarTransaccion_Click;
            panelBottom.Controls.Add(btnRegistrarTransaccion);
            panelBottom.Resize += (_, __) =>
            {
                btnRegistrarTransaccion.Left = panelBottom.ClientSize.Width - btnRegistrarTransaccion.Width - 10;
            };

            cboTercero.DropDownStyle = ComboBoxStyle.DropDown;
            cboTercero.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboTercero.AutoCompleteSource = AutoCompleteSource.CustomSource;

            if (cboIdDocumento != null)
            {
                cboIdDocumento.DropDownStyle = ComboBoxStyle.DropDown;
                cboIdDocumento.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cboIdDocumento.AutoCompleteSource = AutoCompleteSource.ListItems;
                cboIdDocumento.Name = "cboIdDocumento";

                lblIdDocumento.Text = "Id Documento:";
                lblIdDocumento.AutoSize = true;
                lblIdDocumento.Left = cboTercero.Right + 20;
                lblIdDocumento.Top = cboTercero.Top + 4;

                cboIdDocumento.Left = lblIdDocumento.Right + 6;
                cboIdDocumento.Top = cboTercero.Top;
                cboIdDocumento.Width = 200;

                if (!panelHeader.Controls.Contains(lblIdDocumento)) panelHeader.Controls.Add(lblIdDocumento);
                if (!panelHeader.Controls.Contains(cboIdDocumento)) panelHeader.Controls.Add(cboIdDocumento);
            }
        }
        #endregion

        #region Cargas de datos
        private void CargarListaTerceros()
        {
            var list = new List<TerceroItem>();
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(@"
SELECT
    T.IdTercero AS Id,
    T.NumeroIdentificacion AS Identificacion,
    COALESCE(NULLIF(LTRIM(RTRIM(T.RazonSocial)), ''), 
             LTRIM(RTRIM(CONCAT(T.PrimerNombre,' ',ISNULL(T.OtrosNombres,''),' ',T.PrimerApellido,' ',ISNULL(T.SegundoApellido,''))))) AS Nombre
FROM dbo.Terceros T
ORDER BY Nombre;", conn);
            conn.Open();
            using var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                list.Add(new TerceroItem
                {
                    Id = rd.GetInt32(0),
                    Identificacion = rd.GetString(1),
                    Nombre = rd.GetString(2)
                });
            }

            cboTercero.DisplayMember = nameof(TerceroItem.Display);
            cboTercero.ValueMember = nameof(TerceroItem.Id);
            cboTercero.DataSource = list.ToList();

            _bsTerceros.DataSource = list;
            if (dgvLineas.Columns["colIdTercero"] is DataGridViewComboBoxColumn col)
            {
                col.DataSource = _bsTerceros;
                col.DisplayMember = "Display";
                col.ValueMember = "Id";
            }
        }

        private void CargarTercerosAutoComplete()
        {
            _acTerceros.Clear();
            _tercerosNombreToId.Clear();
            foreach (TerceroItem t in _bsTerceros.List)
            {
                _acTerceros.Add(t.Display);
                _tercerosNombreToId[t.Display] = t.Id;
            }
            cboTercero.AutoCompleteCustomSource = _acTerceros;
        }

        private void CargarCuentasPUC()
        {
            _acCuentas.Clear();
            _cuentasValidas.Clear();
            _cuentasNombre.Clear();
            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand("SELECT Codigo, Nombre FROM dbo.PUC ORDER BY Codigo;", conn);
                conn.Open();
                using var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string codigo = rd.GetString(0).Trim();
                    string nombre = rd.IsDBNull(1) ? "" : rd.GetString(1).Trim();
                    if (codigo.Length == 0) continue;

                    _cuentasValidas.Add(codigo);
                    _cuentasNombre[codigo] = nombre;
                    _acCuentas.Add(string.IsNullOrWhiteSpace(nombre) ? codigo : $"{codigo} - {nombre}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando PUC: " + ex.Message);
            }
        }
        #endregion

        #region Numeración
        private void cboTipoDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!chkNumeroManual.Checked)
                AsignarNumeroAutomaticoPreview();
        }

        private void AsignarNumeroAutomaticoPreview()
        {
            if (cboTipoDoc.SelectedItem == null)
            {
                txtNumero.Text = "";
                return;
            }
            string tipo = cboTipoDoc.SelectedItem.ToString()!;
            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(
                    "SELECT ISNULL(MAX(NumeroComprobante),0)+1 FROM dbo.Transacciones WHERE TipoDocContable=@t", conn);
                cmd.Parameters.AddWithValue("@t", tipo);
                conn.Open();
                int next = Convert.ToInt32(cmd.ExecuteScalar());
                txtNumero.Text = next.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error obteniendo número: " + ex.Message);
            }
        }
        #endregion

        #region Grid
        private void PrepararTablaLineas()
        {
            _dtLineas.Columns.Add("CuentaContable", typeof(string));
            _dtLineas.Columns.Add("IdTercero", typeof(int));
            _dtLineas.Columns.Add("DescripcionLinea", typeof(string));
            _dtLineas.Columns.Add("Debito", typeof(decimal));
            _dtLineas.Columns.Add("Credito", typeof(decimal));
            // Eliminado IdComprobante
        }

        private void ConfigurarGrid()
        {
            dgvLineas.AutoGenerateColumns = false;
            dgvLineas.Columns.Clear();

            dgvLineas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCuenta",
                HeaderText = "Cuenta",
                DataPropertyName = "CuentaContable",
                Width = 240
            });

            var colTercero = new DataGridViewComboBoxColumn
            {
                Name = "colIdTercero",
                HeaderText = "Tercero",
                DataPropertyName = "IdTercero",
                DataSource = _bsTerceros,
                DisplayMember = "Display",
                ValueMember = "Id",
                DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton,
                FlatStyle = FlatStyle.Flat,
                Width = 200
            };
            dgvLineas.Columns.Add(colTercero);

            dgvLineas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDescripcion",
                HeaderText = "Descripción Línea",
                DataPropertyName = "DescripcionLinea",
                Width = 220
            });
            dgvLineas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDebito",
                HeaderText = "Débito",
                DataPropertyName = "Debito",
                Width = 80,
                DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvLineas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCredito",
                HeaderText = "Crédito",
                DataPropertyName = "Credito",
                Width = 80,
                DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvLineas.DataSource = _dtLineas;

            dgvLineas.EditingControlShowing += DgvLineas_EditingControlShowing;
            dgvLineas.DefaultValuesNeeded += DgvLineas_DefaultValuesNeeded;
            dgvLineas.CellBeginEdit += DgvLineas_CellBeginEdit;
            dgvLineas.CellValidating += dgvLineas_CellValidating;
            dgvLineas.CellEndEdit += dgvLineas_CellEndEdit;
            dgvLineas.CellFormatting += DgvLineas_CellFormatting;
            dgvLineas.CellParsing += DgvLineas_CellParsing;
            dgvLineas.DataError += (_, e) => e.ThrowException = false;
        }

        private void DgvLineas_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvLineas.Columns[e.ColumnIndex].Name == "colCuenta")
            {
                var codigo = e.Value?.ToString();
                if (!string.IsNullOrWhiteSpace(codigo) &&
                    _cuentasNombre.TryGetValue(codigo, out var nombre) &&
                    !string.IsNullOrWhiteSpace(nombre))
                {
                    e.Value = $"{codigo} - {nombre}";
                    e.FormattingApplied = true;
                }
            }
        }

        private void DgvLineas_CellParsing(object? sender, DataGridViewCellParsingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvLineas.Columns[e.ColumnIndex].Name == "colCuenta" &&
                e.Value is string txt && txt.Contains('-'))
            {
                e.Value = txt.Split('-')[0].Trim();
                e.ParsingApplied = true;
            }
        }

        private void DgvLineas_DefaultValuesNeeded(object? sender, DataGridViewRowEventArgs e)
        {
            if (cboTercero.SelectedValue is int idTer)
                e.Row.Cells["colIdTercero"].Value = idTer;
            e.Row.Cells["colDebito"].Value = 0m;
            e.Row.Cells["colCredito"].Value = 0m;
            // Eliminado IdComprobante
        }

        private void DgvLineas_CellBeginEdit(object? sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var name = dgvLineas.Columns[e.ColumnIndex].Name;
            if ((name == "colDebito" || name == "colCredito") &&
                dgvLineas.Rows[e.RowIndex].Cells["colIdTercero"].Value is null &&
                cboTercero.SelectedValue is int idTer)
            {
                dgvLineas.Rows[e.RowIndex].Cells["colIdTercero"].Value = idTer;
            }
        }

        private void DgvLineas_EditingControlShowing(object? sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvLineas.CurrentCell == null) return;
            if (dgvLineas.Columns[dgvLineas.CurrentCell.ColumnIndex].Name != "colCuenta") return;

            if (e.Control is TextBox tb)
            {
                tb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                tb.AutoCompleteSource = AutoCompleteSource.CustomSource;
                tb.AutoCompleteCustomSource = _acCuentas;
            }
        }

        private void btnAgregarLinea_Click(object sender, EventArgs e)
        {
            var row = _dtLineas.NewRow();
            row["CuentaContable"] = "";
            row["DescripcionLinea"] = "";
            row["Debito"] = 0m;
            row["Credito"] = 0m;
            if (cboTercero.SelectedValue is int idTer)
                row["IdTercero"] = idTer;
            _dtLineas.Rows.Add(row);
            if (dgvLineas.Rows.Count > 0)
                dgvLineas.CurrentCell = dgvLineas.Rows[^1].Cells["colCuenta"];
        }

        private void btnEliminarLinea_Click(object sender, EventArgs e)
        {
            if (dgvLineas.CurrentRow == null) return;
            dgvLineas.Rows.Remove(dgvLineas.CurrentRow);
            RecalcularTotales();
        }

        private void dgvLineas_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string prop = dgvLineas.Columns[e.ColumnIndex].DataPropertyName;

            if (prop is "Debito" or "Credito")
            {
                string txt = e.FormattedValue?.ToString() ?? "";
                if (string.IsNullOrWhiteSpace(txt))
                {
                    dgvLineas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0m;
                    return;
                }
                if (!decimal.TryParse(txt, out var val) || val < 0)
                {
                    e.Cancel = true;
                    MessageBox.Show("Valor inválido.");
                }
            }
            else if (prop == "CuentaContable")
            {
                string cuenta = (e.FormattedValue?.ToString() ?? "").Trim();
                int idx = cuenta.IndexOf('-');
                if (idx > 0) cuenta = cuenta[..idx].Trim();

                if (cuenta.Length == 0) return;
                if (!_cuentasValidas.Contains(cuenta))
                {
                    e.Cancel = true;
                    MessageBox.Show($"Cuenta '{cuenta}' no existe en el PUC.");
                }
                else
                {
                    dgvLineas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = cuenta;
                }
            }
        }

        private void dgvLineas_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = _dtLineas.Rows[e.RowIndex];
            decimal d = row.Field<decimal>("Debito");
            decimal c = row.Field<decimal>("Credito");
            if (d > 0 && c > 0)
            {
                var prop = dgvLineas.Columns[e.ColumnIndex].DataPropertyName;
                if (prop == "Debito") row["Credito"] = 0m; else row["Debito"] = 0m;
            }
            RecalcularTotales();
        }
        #endregion

        #region Registro
        private void BtnRegistrarTransaccion_Click(object? sender, EventArgs e) => RegistrarTransaccionUI();

        private void RegistrarTransaccionUI()
        {
            if (!ValidarFormulario(out string msg))
            {
                MessageBox.Show(msg, "No se pudo registrar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tipo = cboTipoDoc.SelectedItem!.ToString()!;
            if (chkNumeroManual.Checked)
            {
                if (!int.TryParse(txtNumero.Text, out int numero) || numero <= 0)
                {
                    MessageBox.Show("Número manual inválido.", "No se pudo registrar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (ExisteComprobante(tipo, numero))
                {
                    MessageBox.Show("Transacción existente.", "No se pudo registrar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            int antes = _dtLineas.Rows.Count;
            GuardarTransaccion();
            if (_dtLineas.Rows.Count == antes)
                MessageBox.Show("No se pudo registrar la transacción.", "No se pudo registrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void GuardarTransaccion()
        {
            if (!ValidarFormulario(out string msg))
            {
                MessageBox.Show(msg);
                return;
            }

            string tipo = cboTipoDoc.SelectedItem!.ToString()!;
            DateTime fecha = dtpFecha.Value.Date;
            string descripcion = txtDescripcion.Text.Trim();
            string idDocumento = cboIdDocumento?.Text?.Trim() ?? string.Empty;

            int? idTerceroHeader = null;
            if (cboTercero.SelectedValue != null && int.TryParse(cboTercero.SelectedValue.ToString(), out int idSel))
                idTerceroHeader = idSel;
            else if (_tercerosNombreToId.TryGetValue(cboTercero.Text.Trim(), out int idResolved))
                idTerceroHeader = idResolved;

            int numero;
            if (chkNumeroManual.Checked)
            {
                if (!int.TryParse(txtNumero.Text, out numero) || numero <= 0)
                {
                    MessageBox.Show("Número manual inválido.");
                    return;
                }
                if (ExisteComprobante(tipo, numero))
                {
                    MessageBox.Show("Ya existe un comprobante con ese número.");
                    return;
                }
            }
            else
            {
                numero = ObtenerYReservarNumero(tipo);
                if (numero <= 0)
                {
                    MessageBox.Show("No se pudo obtener número.");
                    return;
                }
                txtNumero.Text = numero.ToString();
            }

            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var tx = conn.BeginTransaction();
            try
            {
                const string sql = @"
INSERT INTO dbo.Transacciones
 (TipoDocContable, Fecha, IdTercero, NumeroComprobante,
  DescripcionTransaccion, NumeroDocumento, CuentaContable, Debito, Credito, DescripcionLinea)
VALUES
 (@Tipo, @Fecha, @IdTer, @Numero,
  @Descripcion, @NumeroDocumento, @Cuenta, @Debito, @Credito, @DescripcionLinea);";

                using var cmd = new SqlCommand(sql, conn, tx);
                cmd.Parameters.Add("@Tipo", SqlDbType.NVarChar, 10);
                cmd.Parameters.Add("@Fecha", SqlDbType.Date);
                cmd.Parameters.Add("@IdTer", SqlDbType.Int);
                cmd.Parameters.Add("@Numero", SqlDbType.Int);
                cmd.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 300);
                cmd.Parameters.Add("@NumeroDocumento", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@Cuenta", SqlDbType.VarChar, 20);
                var pDeb = cmd.Parameters.Add("@Debito", SqlDbType.Decimal); pDeb.Precision = 18; pDeb.Scale = 2;
                var pCre = cmd.Parameters.Add("@Credito", SqlDbType.Decimal); pCre.Precision = 18; pCre.Scale = 2;
                cmd.Parameters.Add("@DescripcionLinea", SqlDbType.NVarChar, 300);

                foreach (DataRow r in _dtLineas.Rows)
                {
                    int? idTerLinea = r.IsNull("IdTercero") ? idTerceroHeader : r.Field<int?>("IdTercero");

                    cmd.Parameters["@Tipo"].Value = tipo;
                    cmd.Parameters["@Fecha"].Value = fecha;
                    cmd.Parameters["@IdTer"].Value = (object?)idTerLinea ?? DBNull.Value;
                    cmd.Parameters["@Numero"].Value = numero;
                    cmd.Parameters["@Descripcion"].Value = descripcion;
                    cmd.Parameters["@NumeroDocumento"].Value =
                        string.IsNullOrWhiteSpace(idDocumento) ? (object)DBNull.Value : idDocumento;
                    cmd.Parameters["@Cuenta"].Value = r.Field<string?>("CuentaContable") ?? "";
                    cmd.Parameters["@Debito"].Value = r.Field<decimal>("Debito");
                    cmd.Parameters["@Credito"].Value = r.Field<decimal>("Credito");
                    string? descLinea = r.Field<string?>("DescripcionLinea")?.Trim();
                    cmd.Parameters["@DescripcionLinea"].Value =
                        string.IsNullOrWhiteSpace(descLinea) ? (object)DBNull.Value : descLinea;

                    cmd.ExecuteNonQuery();
                }

                tx.Commit();

                // Mostrar el código formateado (si existe la columna calculada en la BD)
                string codigo = $"{tipo}{numero:000000}";
                MessageBox.Show($"Transacción guardada. Código: {codigo}");

                _dtLineas.Rows.Clear();
                if (cboIdDocumento != null) cboIdDocumento.Text = "";
                RecalcularTotales();
                if (!chkNumeroManual.Checked)
                    AsignarNumeroAutomaticoPreview();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private bool ExisteComprobante(string tipo, int numero)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(
                    "SELECT 1 FROM dbo.Transacciones WHERE TipoDocContable=@t AND NumeroComprobante=@n", conn);
                cmd.Parameters.AddWithValue("@t", tipo);
                cmd.Parameters.AddWithValue("@n", numero);
                conn.Open();
                return cmd.ExecuteScalar() != null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error verificando comprobante: " + ex.Message);
                return true;
            }
        }

        private int ObtenerYReservarNumero(string tipo)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                conn.Open();
                using var cmd = new SqlCommand(
                    "SELECT ISNULL(MAX(NumeroComprobante),0)+1 FROM dbo.Transacciones WHERE TipoDocContable=@t", conn);
                cmd.Parameters.AddWithValue("@t", tipo);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reservando número: " + ex.Message);
                return -1;
            }
        }
        #endregion

        #region Validaciones / Totales
        private bool ValidarFormulario(out string mensaje)
        {
            mensaje = "";
            if (cboTipoDoc.SelectedItem == null)
            {
                mensaje = "Seleccione tipo de documento.";
                return false;
            }
            if (_dtLineas.Rows.Count == 0)
            {
                mensaje = "Agregue líneas.";
                return false;
            }

            foreach (DataRow r in _dtLineas.Rows)
            {
                string cuenta = (r["CuentaContable"]?.ToString() ?? "").Trim();
                if (cuenta.Length == 0)
                {
                    mensaje = "Línea sin cuenta.";
                    return false;
                }
                if (!_cuentasValidas.Contains(cuenta))
                {
                    mensaje = $"Cuenta inexistente en PUC: {cuenta}";
                    return false;
                }
                decimal d = r.Field<decimal>("Debito");
                decimal c = r.Field<decimal>("Credito");
                if ((d > 0 && c > 0) || (d == 0 && c == 0))
                {
                    mensaje = "Cada línea debe tener solo Débito o Crédito.";
                    return false;
                }
            }

            decimal totalDeb = _dtLineas.AsEnumerable().Sum(r => r.Field<decimal>("Debito"));
            decimal totalCre = _dtLineas.AsEnumerable().Sum(r => r.Field<decimal>("Credito"));
            if (totalDeb != totalCre)
            {
                mensaje = "Los débitos y créditos no están balanceados.";
                return false;
            }

            if (chkNumeroManual.Checked &&
                (!int.TryParse(txtNumero.Text, out int manual) || manual <= 0))
            {
                mensaje = "Número manual inválido.";
                return false;
            }
            return true;
        }

        private void RecalcularTotales()
        {
            decimal td = _dtLineas.AsEnumerable().Sum(r => r.Field<decimal>("Debito"));
            decimal tc = _dtLineas.AsEnumerable().Sum(r => r.Field<decimal>("Credito"));
            lblTotalDebito.Text = $"Débito: {td:N2}";
            lblTotalCredito.Text = $"Crédito: {tc:N2}";
            lblBalance.Text = $"Balance: {(td - tc):N2}";
            lblBalance.ForeColor = td == tc ? System.Drawing.Color.ForestGreen : System.Drawing.Color.DarkRed;
        }
        #endregion
    }
}