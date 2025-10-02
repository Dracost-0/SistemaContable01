#nullable disable
namespace SistemaContable01.Dashboard.Transacciones.CrudTransacciones
{
    partial class FormCrudTransacciones
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtTipoDoc;
        private System.Windows.Forms.ComboBox cboCampoNumero;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelarEdicion;
        private System.Windows.Forms.Button btnAnular;

        private System.Windows.Forms.DataGridView dgvTransacciones;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblTotales;
        private System.Windows.Forms.Label lblEstado;

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelEncabezado;
        private System.Windows.Forms.Panel panelBottom;

        private System.Windows.Forms.Label lblTipoDoc;
        private System.Windows.Forms.Label lblCampoNumero;
        private System.Windows.Forms.Label lblNumero;

        // Controles encabezado editable
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lblTercero;
        private System.Windows.Forms.ComboBox cboTercero;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcionTrans;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtTipoDoc = new System.Windows.Forms.TextBox();
            cboCampoNumero = new System.Windows.Forms.ComboBox();
            txtNumero = new System.Windows.Forms.TextBox();
            btnBuscar = new System.Windows.Forms.Button();
            btnLimpiar = new System.Windows.Forms.Button();
            btnEditar = new System.Windows.Forms.Button();
            btnGuardar = new System.Windows.Forms.Button();
            btnCancelarEdicion = new System.Windows.Forms.Button();
            btnAnular = new System.Windows.Forms.Button();
            dgvTransacciones = new System.Windows.Forms.DataGridView();
            lblHeader = new System.Windows.Forms.Label();
            lblTotales = new System.Windows.Forms.Label();
            lblEstado = new System.Windows.Forms.Label();
            panelTop = new System.Windows.Forms.Panel();
            panelEncabezado = new System.Windows.Forms.Panel();
            panelBottom = new System.Windows.Forms.Panel();
            lblTipoDoc = new System.Windows.Forms.Label();
            lblCampoNumero = new System.Windows.Forms.Label();
            lblNumero = new System.Windows.Forms.Label();
            lblFecha = new System.Windows.Forms.Label();
            dtpFecha = new System.Windows.Forms.DateTimePicker();
            lblTercero = new System.Windows.Forms.Label();
            cboTercero = new System.Windows.Forms.ComboBox();
            lblDescripcion = new System.Windows.Forms.Label();
            txtDescripcionTrans = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvTransacciones).BeginInit();
            panelTop.SuspendLayout();
            panelEncabezado.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // panelTop
            panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            panelTop.Height = 54;
            panelTop.Padding = new System.Windows.Forms.Padding(8, 8, 8, 4);
            panelTop.BackColor = System.Drawing.Color.Gainsboro;
            panelTop.Controls.Add(lblTipoDoc);
            panelTop.Controls.Add(txtTipoDoc);
            panelTop.Controls.Add(lblCampoNumero);
            panelTop.Controls.Add(cboCampoNumero);
            panelTop.Controls.Add(lblNumero);
            panelTop.Controls.Add(txtNumero);
            panelTop.Controls.Add(btnBuscar);
            panelTop.Controls.Add(btnLimpiar);
            panelTop.Controls.Add(btnEditar);
            panelTop.Controls.Add(btnGuardar);
            panelTop.Controls.Add(btnCancelarEdicion);
            panelTop.Controls.Add(btnAnular);
            // lblTipoDoc
            lblTipoDoc.AutoSize = true;
            lblTipoDoc.Location = new System.Drawing.Point(10, 17);
            lblTipoDoc.Text = "TipoDoc:";
            // txtTipoDoc
            txtTipoDoc.Location = new System.Drawing.Point(65, 14);
            txtTipoDoc.Width = 70;
            txtTipoDoc.TabIndex = 0;
            // lblCampoNumero
            lblCampoNumero.AutoSize = true;
            lblCampoNumero.Location = new System.Drawing.Point(145, 17);
            lblCampoNumero.Text = "Campo Número:";
            // cboCampoNumero
            cboCampoNumero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboCampoNumero.Items.AddRange(new object[] { "NumeroComprobante", "NumeroDocumento" });
            cboCampoNumero.Location = new System.Drawing.Point(245, 14);
            cboCampoNumero.Width = 140;
            // lblNumero
            lblNumero.AutoSize = true;
            lblNumero.Location = new System.Drawing.Point(395, 17);
            lblNumero.Text = "Número:";
            // txtNumero
            txtNumero.Location = new System.Drawing.Point(450, 14);
            txtNumero.Width = 110;
            // btnBuscar
            btnBuscar.Text = "Buscar";
            btnBuscar.Location = new System.Drawing.Point(570, 13);
            btnBuscar.Width = 65;
            // btnLimpiar
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.Location = new System.Drawing.Point(640, 13);
            btnLimpiar.Width = 65;
            // btnEditar
            btnEditar.Text = "Editar";
            btnEditar.Location = new System.Drawing.Point(710, 13);
            btnEditar.Width = 60;
            // btnGuardar
            btnGuardar.Text = "Guardar";
            btnGuardar.Location = new System.Drawing.Point(775, 13);
            btnGuardar.Width = 70;
            // btnCancelarEdicion
            btnCancelarEdicion.Text = "Cancelar";
            btnCancelarEdicion.Location = new System.Drawing.Point(850, 13);
            btnCancelarEdicion.Width = 70;
            // btnAnular
            btnAnular.Text = "Anular";
            btnAnular.Location = new System.Drawing.Point(925, 13);
            btnAnular.Width = 60;
            // panelEncabezado
            panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            panelEncabezado.Height = 130;
            panelEncabezado.Padding = new System.Windows.Forms.Padding(8);
            panelEncabezado.BackColor = System.Drawing.Color.WhiteSmoke;
            panelEncabezado.Controls.Add(lblHeader);
            panelEncabezado.Controls.Add(lblFecha);
            panelEncabezado.Controls.Add(dtpFecha);
            panelEncabezado.Controls.Add(lblTercero);
            panelEncabezado.Controls.Add(cboTercero);
            panelEncabezado.Controls.Add(lblDescripcion);
            panelEncabezado.Controls.Add(txtDescripcionTrans);
            // lblHeader
            lblHeader.AutoSize = false;
            lblHeader.Location = new System.Drawing.Point(10, 8);
            lblHeader.Size = new System.Drawing.Size(340, 20);
            lblHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblHeader.Text = "";
            // lblFecha
            lblFecha.AutoSize = true;
            lblFecha.Location = new System.Drawing.Point(370, 10);
            lblFecha.Text = "Fecha:";
            // dtpFecha
            dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpFecha.Location = new System.Drawing.Point(420, 6);
            dtpFecha.Width = 110;
            // lblTercero
            lblTercero.AutoSize = true;
            lblTercero.Location = new System.Drawing.Point(10, 40);
            lblTercero.Text = "Tercero:";
            // cboTercero
            cboTercero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboTercero.Location = new System.Drawing.Point(70, 36);
            cboTercero.Width = 460;
            // lblDescripcion
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new System.Drawing.Point(10, 70);
            lblDescripcion.Text = "Descripción:";
            // txtDescripcionTrans
            txtDescripcionTrans.Multiline = true;
            txtDescripcionTrans.Location = new System.Drawing.Point(90, 66);
            txtDescripcionTrans.Width = 600;
            txtDescripcionTrans.Height = 50;
            // dgvTransacciones
            dgvTransacciones.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvTransacciones.AllowUserToAddRows = false;
            dgvTransacciones.AllowUserToDeleteRows = false;
            dgvTransacciones.AllowUserToResizeRows = false;
            dgvTransacciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvTransacciones.MultiSelect = false;
            dgvTransacciones.RowHeadersVisible = false;
            dgvTransacciones.BackgroundColor = System.Drawing.Color.White;
            // panelBottom
            panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelBottom.Height = 46;
            panelBottom.Padding = new System.Windows.Forms.Padding(8);
            panelBottom.BackColor = System.Drawing.Color.Gainsboro;
            panelBottom.Controls.Add(lblTotales);
            panelBottom.Controls.Add(lblEstado);
            // lblTotales
            lblTotales.AutoSize = true;
            lblTotales.Location = new System.Drawing.Point(10, 15);
            lblTotales.Text = "Total Débito: 0.00    Total Crédito: 0.00";
            // lblEstado
            lblEstado.AutoSize = true;
            lblEstado.Location = new System.Drawing.Point(330, 15);
            // FormCrudTransacciones
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1015, 640);
            Text = "CRUD / Visualizar / Editar Transacciones";
            Controls.Add(dgvTransacciones);
            Controls.Add(panelBottom);
            Controls.Add(panelEncabezado);
            Controls.Add(panelTop);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            MinimizeBox = true;
            MaximizeBox = true;
            ((System.ComponentModel.ISupportInitialize)dgvTransacciones).EndInit();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelEncabezado.ResumeLayout(false);
            panelEncabezado.PerformLayout();
            panelBottom.ResumeLayout(false);
            panelBottom.PerformLayout();
            ResumeLayout(false);
        }
    }
}