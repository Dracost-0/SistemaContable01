using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace SistemaContable01.Dashboard.EstadosFinancieros.BalanceGeneral
{
    public partial class FormBalanceGeneral : Form
    {
        private readonly string _connectionString =
            @"Server=localhost\SQLEXPRESS;Database=SysCon01Db;Trusted_Connection=True;Encrypt=False;";

        private readonly DataTable _dt = new();

        private readonly Panel _panelTop = new();
        private readonly Label _lblFecha = new();
        private readonly DateTimePicker _dtpCorte = new();
        private readonly Button _btnCalcular = new();
        private readonly DataGridView _dgv = new();

        private readonly Panel _panelBottom = new();
        private readonly Label _lblActivos = new();
        private readonly Label _lblPasivos = new();
        private readonly Label _lblPatrimonio = new();
        private readonly Label _lblBalance = new();

        public FormBalanceGeneral()
        {
            InitializeComponent();

            Text = "Balance General";
            StartPosition = FormStartPosition.CenterParent;
            Width = 1000;
            Height = 650;

            BuildUi();
            BindGrid();

            _dtpCorte.Value = DateTime.Today;
            _btnCalcular.PerformClick();
        }

        private void BuildUi()
        {
            _panelTop.Dock = DockStyle.Top;
            _panelTop.Height = 46;
            _panelTop.Padding = new Padding(8);

            _lblFecha.Text = "Fecha de corte:";
            _lblFecha.AutoSize = true;
            _lblFecha.Location = new Point(8, 14);

            _dtpCorte.Format = DateTimePickerFormat.Short;
            _dtpCorte.Width = 110;
            _dtpCorte.Location = new Point(_lblFecha.Right + 8, 10);

            _btnCalcular.Text = "Calcular";
            _btnCalcular.Width = 100;
            _btnCalcular.Location = new Point(_dtpCorte.Right + 10, 10);
            _btnCalcular.Click += (_, __) => CargarBalance();

            _panelTop.Controls.AddRange(new Control[] { _lblFecha, _dtpCorte, _btnCalcular });

            _dgv.Dock = DockStyle.Fill;
            _dgv.AllowUserToAddRows = false;
            _dgv.AllowUserToDeleteRows = false;
            _dgv.AllowUserToResizeRows = false;
            _dgv.ReadOnly = true;
            _dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgv.RowHeadersVisible = false;
            _dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            _panelBottom.Dock = DockStyle.Bottom;
            _panelBottom.Height = 48;
            _panelBottom.Padding = new Padding(8);

            _lblActivos.AutoSize = true;
            _lblPasivos.AutoSize = true;
            _lblPatrimonio.AutoSize = true;
            _lblBalance.AutoSize = true;

            _lblActivos.Location = new Point(12, 16);
            _lblPasivos.Location = new Point(230, 16);
            _lblPatrimonio.Location = new Point(420, 16);
            _lblBalance.Location = new Point(650, 16);

            _panelBottom.Controls.AddRange(new Control[] { _lblActivos, _lblPasivos, _lblPatrimonio, _lblBalance });

            Controls.Add(_dgv);
            Controls.Add(_panelBottom);
            Controls.Add(_panelTop);
        }

        private void BindGrid()
        {
            // Esquema SIN columna Nombre: se combinará en Cuenta (Código - Nombre)
            _dt.Columns.Add("Grupo", typeof(string));
            _dt.Columns.Add("Cuenta", typeof(string)); // contendrá "Codigo - Nombre" o solo "Codigo"
            _dt.Columns.Add("Saldo", typeof(decimal));

            _dgv.AutoGenerateColumns = false;
            _dgv.Columns.Clear();

            _dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Grupo",
                HeaderText = "Grupo",
                DataPropertyName = "Grupo",
                Width = 110,
                ReadOnly = true
            });

            _dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cuenta",
                HeaderText = "Cuenta",
                DataPropertyName = "Cuenta",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            var colSaldo = new DataGridViewTextBoxColumn
            {
                Name = "Saldo",
                HeaderText = "Saldo",
                DataPropertyName = "Saldo",
                Width = 140,
                ReadOnly = true
            };
            colSaldo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSaldo.DefaultCellStyle.Format = "N2";
            _dgv.Columns.Add(colSaldo);

            _dgv.DataSource = _dt;
        }

        private void CargarBalance()
        {
            DateTime fecha = _dtpCorte.Value.Date;

            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(@"
WITH Mov AS (
    SELECT
        T.CuentaContable,
        SUM(CASE 
                WHEN LEFT(T.CuentaContable,1) = '1' THEN T.Debito - T.Credito
                WHEN LEFT(T.CuentaContable,1) IN ('2','3') THEN T.Credito - T.Debito
                ELSE 0
            END) AS Saldo
    FROM dbo.Transacciones AS T
    WHERE T.Fecha <= @FechaCorte
    GROUP BY T.CuentaContable
)
SELECT 
    CASE LEFT(M.CuentaContable,1)
        WHEN '1' THEN 'ACTIVO'
        WHEN '2' THEN 'PASIVO'
        WHEN '3' THEN 'PATRIMONIO'
        ELSE 'OTROS'
    END AS Grupo,
    M.CuentaContable +
        CASE WHEN ISNULL(LTRIM(RTRIM(P.Nombre)),'') = '' THEN '' 
             ELSE ' - ' + LTRIM(RTRIM(P.Nombre)) END AS Cuenta,
    M.Saldo
FROM Mov AS M
LEFT JOIN dbo.PUC AS P ON P.Codigo = M.CuentaContable
WHERE LEFT(M.CuentaContable,1) IN ('1','2','3')
  AND ABS(M.Saldo) > 0.0000001
ORDER BY Grupo, Cuenta;", conn);

                cmd.Parameters.AddWithValue("@FechaCorte", fecha);

                conn.Open();
                using var rd = cmd.ExecuteReader();

                _dt.BeginLoadData();
                _dt.Clear();
                _dt.Load(rd);
                _dt.EndLoadData();

                decimal totalActivos = _dt.AsEnumerable()
                    .Where(r => string.Equals(r.Field<string>("Grupo"), "ACTIVO", StringComparison.OrdinalIgnoreCase))
                    .Sum(r => r.Field<decimal>("Saldo"));

                decimal totalPasivos = _dt.AsEnumerable()
                    .Where(r => string.Equals(r.Field<string>("Grupo"), "PASIVO", StringComparison.OrdinalIgnoreCase))
                    .Sum(r => r.Field<decimal>("Saldo"));

                decimal totalPatrimonio = _dt.AsEnumerable()
                    .Where(r => string.Equals(r.Field<string>("Grupo"), "PATRIMONIO", StringComparison.OrdinalIgnoreCase))
                    .Sum(r => r.Field<decimal>("Saldo"));

                decimal balance = totalActivos - (totalPasivos + totalPatrimonio);

                _lblActivos.Text = $"Activos: {totalActivos:N2}";
                _lblPasivos.Text = $"Pasivos: {totalPasivos:N2}";
                _lblPatrimonio.Text = $"Patrimonio: {totalPatrimonio:N2}";
                _lblBalance.Text = $"Balance: {balance:N2}";
                _lblBalance.ForeColor = Math.Round(balance, 2) == 0m ? Color.ForestGreen : Color.DarkRed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calculando Balance: " + ex.Message);
            }
        }
    }
}