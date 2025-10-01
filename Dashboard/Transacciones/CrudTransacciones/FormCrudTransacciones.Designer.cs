#nullable enable
namespace SistemaContable01.Dashboard.Transacciones.CrudTransacciones
{
    partial class FormCrudTransacciones
    {
        private System.ComponentModel.IContainer? components = null;
        private System.Windows.Forms.TextBox txtTipoDoc = null!;
        private System.Windows.Forms.ComboBox cboCampoNumero = null!;
        private System.Windows.Forms.TextBox txtNumero = null!;
        private System.Windows.Forms.Button btnBuscar = null!;
        private System.Windows.Forms.Button btnLimpiar = null!;
        private System.Windows.Forms.DataGridView dgvTransacciones = null!;
        private System.Windows.Forms.Label lblHeader = null!;
        private System.Windows.Forms.Label lblTotales = null!;
        private System.Windows.Forms.Label lblEstado = null!;
        private System.Windows.Forms.Panel panelBusqueda = null!;
        private System.Windows.Forms.Panel panelInfo = null!;
        private System.Windows.Forms.SplitContainer splitContainer = null!;
        private System.Windows.Forms.FlowLayoutPanel flowBusqueda = null!;
        private System.Windows.Forms.FlowLayoutPanel flowInfo = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.Text = "CRUD / Visualizar Transacciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Width = 1150;
            this.Height = 700;

            txtTipoDoc = new System.Windows.Forms.TextBox { Width = 100, PlaceholderText = "Tipo" };
            cboCampoNumero = new System.Windows.Forms.ComboBox { Width = 150, DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList };
            cboCampoNumero.Items.AddRange(new object[] { "NumeroComprobante", "NumeroDocumento" });
            txtNumero = new System.Windows.Forms.TextBox { Width = 140, PlaceholderText = "Número..." };
            btnBuscar = new System.Windows.Forms.Button { Text = "Buscar", Width = 90, Height = 30 };
            btnLimpiar = new System.Windows.Forms.Button { Text = "Limpiar", Width = 90, Height = 30 };

            dgvTransacciones = new System.Windows.Forms.DataGridView
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                MultiSelect = false,
                SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            };

            lblHeader = new System.Windows.Forms.Label { AutoSize = true };
            lblTotales = new System.Windows.Forms.Label { AutoSize = true };
            lblEstado = new System.Windows.Forms.Label { AutoSize = true };

            panelBusqueda = new System.Windows.Forms.Panel { Dock = System.Windows.Forms.DockStyle.Top, Height = 68 };
            panelInfo = new System.Windows.Forms.Panel { Dock = System.Windows.Forms.DockStyle.Top, Height = 80 };

            flowBusqueda = new System.Windows.Forms.FlowLayoutPanel
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight,
                Padding = new System.Windows.Forms.Padding(8),
                WrapContents = false
            };
            flowBusqueda.Controls.Add(new System.Windows.Forms.Label { Text = "TipoDoc:", AutoSize = true, Padding = new System.Windows.Forms.Padding(0, 8, 0, 0) });
            flowBusqueda.Controls.Add(txtTipoDoc);
            flowBusqueda.Controls.Add(new System.Windows.Forms.Label { Text = "Campo Número:", AutoSize = true, Padding = new System.Windows.Forms.Padding(8, 8, 0, 0) });
            flowBusqueda.Controls.Add(cboCampoNumero);
            flowBusqueda.Controls.Add(new System.Windows.Forms.Label { Text = "Número:", AutoSize = true, Padding = new System.Windows.Forms.Padding(8, 8, 0, 0) });
            flowBusqueda.Controls.Add(txtNumero);
            flowBusqueda.Controls.Add(btnBuscar);
            flowBusqueda.Controls.Add(btnLimpiar);
            panelBusqueda.Controls.Add(flowBusqueda);

            flowInfo = new System.Windows.Forms.FlowLayoutPanel
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                FlowDirection = System.Windows.Forms.FlowDirection.TopDown,
                Padding = new System.Windows.Forms.Padding(8)
            };
            flowInfo.Controls.Add(lblHeader);
            flowInfo.Controls.Add(lblTotales);
            flowInfo.Controls.Add(lblEstado);
            panelInfo.Controls.Add(flowInfo);

            splitContainer = new System.Windows.Forms.SplitContainer
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                Orientation = System.Windows.Forms.Orientation.Horizontal,
                SplitterDistance = 150,
                FixedPanel = System.Windows.Forms.FixedPanel.Panel1
            };

            var topHost = new System.Windows.Forms.Panel { Dock = System.Windows.Forms.DockStyle.Fill };
            topHost.Controls.Add(panelInfo);
            topHost.Controls.Add(panelBusqueda);
            splitContainer.Panel1.Controls.Add(topHost);
            splitContainer.Panel2.Controls.Add(dgvTransacciones);

            this.Controls.Add(splitContainer);
        }
    }
}