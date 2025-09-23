using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Drawing.Printing;

namespace SistemaContable01.Dashboard.Informes.InformePorTercero
{
    public partial class FormInformePorTercero : Form
    {
        private readonly string _connectionString =
            @"Server=localhost\SQLEXPRESS;Database=SysCon01Db;Trusted_Connection=True;Encrypt=False;";

        private readonly DataTable _dt = new();
        private readonly PrintDocument _printDoc = new();

        private sealed class TerceroItem
        {
            public int Id { get; init; }
            public string Identificacion { get; init; } = "";
            public string Nombre { get; init; } = "";
            public string? Direccion { get; init; }
            public string? Ciudad { get; init; }
            public string? Telefono { get; init; }
            public string Display => $"{Identificacion} - {Nombre}";
            public override string ToString() => Display;
        }

        public FormInformePorTercero()
        {
            InitializeComponent();
            InitializeGrid();
            InitializeHeader();
            HookPrinting();
            LoadTerceros();

            var hoy = DateTime.Today;
            dtpDesde.Value = new DateTime(hoy.Year, hoy.Month, 1);
            dtpHasta.Value = hoy;
        }

        private void InitializeHeader()
        {
            lblEmpresa.Text = "Empresa";
            lblTitulo.Text = "Informe de terceros";
            UpdatePeriodoLabel();
            lblGenerado.Text = $"Generado: {DateTime.Now:g}";
        }

        private void UpdatePeriodoLabel() =>
            lblPeriodo.Text = $"Período: {dtpDesde.Value:dd/MM/yyyy} - {dtpHasta.Value:dd/MM/yyyy}";

        private void InitializeGrid()
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();

            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Fecha", HeaderText = "Fecha", DataPropertyName = "Fecha", Width = 95, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Format = "d" } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "TipoDocumento", HeaderText = "Tipo", DataPropertyName = "TipoDocumento", Width = 90, ReadOnly = true });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "NumeroDocumento", HeaderText = "Número", DataPropertyName = "NumeroDocumento", Width = 100, ReadOnly = true });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Concepto", HeaderText = "Concepto", DataPropertyName = "Concepto", Width = 210, ReadOnly = true });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Detalle", HeaderText = "Detalle", DataPropertyName = "Detalle", Width = 230, ReadOnly = true }); // NUEVA
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cuenta", HeaderText = "Cuenta", DataPropertyName = "Cuenta", Width = 110, ReadOnly = true });

            var colDeb = new DataGridViewTextBoxColumn { Name = "Debito", HeaderText = "Débito", DataPropertyName = "Debito", Width = 110, ReadOnly = true };
            colDeb.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDeb.DefaultCellStyle.Format = "N2";
            dgv.Columns.Add(colDeb);

            var colCre = new DataGridViewTextBoxColumn { Name = "Credito", HeaderText = "Crédito", DataPropertyName = "Credito", Width = 110, ReadOnly = true };
            colCre.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colCre.DefaultCellStyle.Format = "N2";
            dgv.Columns.Add(colCre);

            var colSaldo = new DataGridViewTextBoxColumn { Name = "SaldoAcumulado", HeaderText = "Saldo acum.", DataPropertyName = "SaldoAcumulado", Width = 120, ReadOnly = true };
            colSaldo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSaldo.DefaultCellStyle.Format = "N2";
            dgv.Columns.Add(colSaldo);

            _dt.Columns.Clear();
            _dt.Columns.Add("Fecha", typeof(DateTime));
            _dt.Columns.Add("TipoDocumento", typeof(string));
            _dt.Columns.Add("NumeroDocumento", typeof(string));
            _dt.Columns.Add("Concepto", typeof(string));
            _dt.Columns.Add("Detalle", typeof(string)); // NUEVA
            _dt.Columns.Add("Cuenta", typeof(string));
            _dt.Columns.Add("Debito", typeof(decimal));
            _dt.Columns.Add("Credito", typeof(decimal));
            _dt.Columns.Add("SaldoAcumulado", typeof(decimal));

            dgv.DataSource = _dt;
        }

        private void HookPrinting()
        {
            _printDoc.DocumentName = "Informe por tercero";
            _printDoc.PrintPage += PrintDoc_PrintPage;
        }

        private void LoadTerceros()
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(@"
SELECT
    T.IdTercero                               AS Id,
    T.NumeroIdentificacion                    AS Identificacion,
    COALESCE(NULLIF(LTRIM(RTRIM(T.RazonSocial)), ''), 
             LTRIM(RTRIM(CONCAT(T.PrimerNombre, ' ', ISNULL(T.OtrosNombres,''), ' ', T.PrimerApellido, ' ', ISNULL(T.SegundoApellido,''))))) 
                                               AS Nombre,
    T.Direccion,
    T.Ciudad,
    T.Telefono
FROM dbo.Terceros T
ORDER BY Nombre;", conn);

                conn.Open();
                using var rd = cmd.ExecuteReader();

                var list = new System.Collections.Generic.List<TerceroItem>();
                while (rd.Read())
                {
                    list.Add(new TerceroItem
                    {
                        Id = rd.GetInt32(0),
                        Identificacion = rd.GetString(1),
                        Nombre = rd.GetString(2),
                        Direccion = rd.IsDBNull(3) ? null : rd.GetString(3),
                        Ciudad = rd.IsDBNull(4) ? null : rd.GetString(4),
                        Telefono = rd.IsDBNull(5) ? null : rd.GetString(5),
                    });
                }

                cboTercero.DisplayMember = nameof(TerceroItem.Display);
                cboTercero.ValueMember = nameof(TerceroItem.Id);
                cboTercero.DataSource = list;

                cboTercero.SelectedIndexChanged += (_, __) => UpdateTerceroHeader();
                if (list.Count > 0) UpdateTerceroHeader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando terceros: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTerceroHeader()
        {
            if (cboTercero.SelectedItem is not TerceroItem t)
            {
                lblTerceroIdent.Text = "Identificación:";
                lblTerceroNombre.Text = "Nombre:";
                lblTerceroDir.Text = "Dirección:";
                lblTerceroCiudadTel.Text = "Ciudad / Tel:";
                return;
            }

            lblTerceroIdent.Text = $"Identificación: {t.Identificacion}";
            lblTerceroNombre.Text = $"Nombre: {t.Nombre}";
            lblTerceroDir.Text = $"Dirección: {t.Direccion ?? "-"}";
            lblTerceroCiudadTel.Text = $"Ciudad / Tel: {(t.Ciudad ?? "-")} / {(t.Telefono ?? "-")}";
        }

        private void btnBuscar_Click(object? sender, EventArgs e)
        {
            UpdatePeriodoLabel();
            CargarMovimientos();
        }

        private void CargarMovimientos()
        {
            if (cboTercero.SelectedItem is not TerceroItem terceroSel)
            {
                MessageBox.Show("Seleccione un tercero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var desde = dtpDesde.Value.Date;
            var hasta = dtpHasta.Value.Date;
            var cuentaFiltro = string.IsNullOrWhiteSpace(txtCuenta.Text) ? null : txtCuenta.Text.Trim();

            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(@"
SELECT
    T.Fecha,
    ISNULL(T.TipoDocContable,'')              AS TipoDocumento,
    CONVERT(nvarchar(20), T.NumeroDocumento)  AS NumeroDocumento,
    ISNULL(T.DescripcionTransaccion,'')       AS Concepto,
    ISNULL(T.DescripcionLinea,'')             AS Detalle,
    RTRIM(T.CuentaContable)                   AS Cuenta,
    T.Debito,
    T.Credito
FROM dbo.Transacciones T
WHERE T.Fecha >= @Desde AND T.Fecha <= @Hasta
  AND T.IdTercero = @TerceroId
  AND (@Cuenta IS NULL OR T.CuentaContable LIKE @Cuenta + '%')
ORDER BY T.Fecha, T.TipoDocContable, T.NumeroDocumento;", conn);

                cmd.Parameters.AddWithValue("@Desde", desde);
                cmd.Parameters.AddWithValue("@Hasta", hasta);
                cmd.Parameters.AddWithValue("@TerceroId", terceroSel.Id);
                cmd.Parameters.AddWithValue("@Cuenta", (object?)cuentaFiltro ?? DBNull.Value);

                conn.Open();
                using var rd = cmd.ExecuteReader();

                _dt.BeginLoadData();
                _dt.Clear();
                _dt.Load(rd);
                _dt.EndLoadData();

                decimal running = 0m;
                foreach (DataRow r in _dt.Rows)
                {
                    var deb = r.Field<decimal>("Debito");
                    var cre = r.Field<decimal>("Credito");
                    running += deb - cre;
                    r["SaldoAcumulado"] = running;
                }

                var totalDeb = _dt.AsEnumerable().Sum(r => r.Field<decimal>("Debito"));
                var totalCre = _dt.AsEnumerable().Sum(r => r.Field<decimal>("Credito"));
                var saldoFinal = running;

                lblTotDebito.Text = $"Débito: {totalDeb:N2}";
                lblTotCredito.Text = $"Crédito: {totalCre:N2}";
                lblSaldoFinal.Text = $"Saldo final: {saldoFinal:N2}";
                lblSaldoFinal.ForeColor = Math.Round(saldoFinal, 2) >= 0m ? Color.ForestGreen : Color.DarkRed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando movimientos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object? sender, EventArgs e)
        {
            using var preview = new PrintPreviewDialog
            {
                Document = _printDoc,
                Width = 1000,
                Height = 800
            };
            preview.ShowDialog(this);
        }

        private void PrintDoc_PrintPage(object? sender, PrintPageEventArgs e)
        {
            var g = e.Graphics;
            if (g is null)
            {
                e.HasMorePages = false;
                return;
            }

            float y = e.MarginBounds.Top;
            var fontH = new Font("Segoe UI", 11, FontStyle.Bold);
            var font = new Font("Segoe UI", 9);
            var brush = Brushes.Black;

            g.DrawString(lblEmpresa.Text, fontH, brush, e.MarginBounds.Left, y); y += 22;
            g.DrawString(lblTitulo.Text, fontH, brush, e.MarginBounds.Left, y); y += 18;
            g.DrawString(lblPeriodo.Text, font, brush, e.MarginBounds.Left, y); y += 16;
            g.DrawString(lblGenerado.Text, font, brush, e.MarginBounds.Left, y); y += 20;

            g.DrawString(lblTerceroIdent.Text, font, brush, e.MarginBounds.Left, y); y += 16;
            g.DrawString(lblTerceroNombre.Text, font, brush, e.MarginBounds.Left, y); y += 16;
            g.DrawString(lblTerceroDir.Text, font, brush, e.MarginBounds.Left, y); y += 16;
            g.DrawString(lblTerceroCiudadTel.Text, font, brush, e.MarginBounds.Left, y); y += 20;

            g.DrawString(lblTotDebito.Text, font, brush, e.MarginBounds.Left, y);
            g.DrawString(lblTotCredito.Text, font, brush, e.MarginBounds.Left + 220, y);
            g.DrawString(lblSaldoFinal.Text, font, brush, e.MarginBounds.Left + 440, y); y += 22;

            string[] headers = ["Fecha", "Tipo", "Número", "Concepto", "Detalle", "Cuenta", "Débito", "Crédito", "Saldo"];
            int[] widths   = [80, 70, 75, 160, 180, 70, 80, 80, 90];
            float x = e.MarginBounds.Left;
            for (int i = 0; i < headers.Length; i++)
            {
                g.DrawString(headers[i], fontH, brush, x, y);
                x += widths[i];
            }
            y += 20;

            int startRow = 0;
            int maxRows = Math.Max(0, (int)((e.MarginBounds.Bottom - y) / 18f));
            for (int i = startRow; i < _dt.Rows.Count && i < startRow + maxRows; i++)
            {
                x = e.MarginBounds.Left;
                var r = _dt.Rows[i];
                object[] vals =
                [
                    ((DateTime)r["Fecha"]).ToString("d"),
                    r["TipoDocumento"],
                    r["NumeroDocumento"],
                    r["Concepto"],
                    r["Detalle"],
                    r["Cuenta"],
                    ((decimal)r["Debito"]).ToString("N2"),
                    ((decimal)r["Credito"]).ToString("N2"),
                    ((decimal)r["SaldoAcumulado"]).ToString("N2")
                ];
                for (int c = 0; c < vals.Length; c++)
                {
                    g.DrawString(vals[c]?.ToString(), font, brush, x, y);
                    x += widths[c];
                }
                y += 18f;
            }

            e.HasMorePages = false;
        }
    }
}