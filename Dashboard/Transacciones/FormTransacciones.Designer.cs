namespace SistemaContable01.Dashboard.Transacciones
{
    partial class FormTransacciones
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTipoDoc;
        private System.Windows.Forms.ComboBox cboTipoDoc;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lblTercero;
        private System.Windows.Forms.ComboBox cboTercero;
        private System.Windows.Forms.Label lblIdDocumento;
        private System.Windows.Forms.ComboBox cboIdDocumento;

        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.DataGridView dgvLineas;
        private System.Windows.Forms.Button btnAgregarLinea;
        private System.Windows.Forms.Button btnEliminarLinea;
        private System.Windows.Forms.Label lblTotalDebito;
        private System.Windows.Forms.Label lblTotalCredito;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelBottom;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTipoDoc = new Label();
            cboTipoDoc = new ComboBox();
            lblNumero = new Label();
            txtNumero = new TextBox();
            lblFecha = new Label();
            dtpFecha = new DateTimePicker();
            lblTercero = new Label();
            cboTercero = new ComboBox();

            lblIdDocumento = new Label();
            cboIdDocumento = new ComboBox();

            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            dgvLineas = new DataGridView();
            btnAgregarLinea = new Button();
            btnEliminarLinea = new Button();
            lblTotalDebito = new Label();
            lblTotalCredito = new Label();
            lblBalance = new Label();
            panelHeader = new Panel();
            panelBottom = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvLineas).BeginInit();
            panelHeader.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // lblTipoDoc
            // 
            lblTipoDoc.AutoSize = true;
            lblTipoDoc.Location = new Point(10, 10);
            lblTipoDoc.Name = "lblTipoDoc";
            lblTipoDoc.Size = new Size(58, 15);
            lblTipoDoc.TabIndex = 0;
            lblTipoDoc.Text = "Tipo Doc:";
            // 
            // cboTipoDoc
            // 
            cboTipoDoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTipoDoc.Items.AddRange(new object[] { "FC", "RC", "ND", "NC", "EJE" });
            cboTipoDoc.Location = new Point(70, 7);
            cboTipoDoc.Name = "cboTipoDoc";
            cboTipoDoc.Size = new Size(90, 23);
            cboTipoDoc.TabIndex = 1;
            cboTipoDoc.SelectedIndexChanged += cboTipoDoc_SelectedIndexChanged;
            // 
            // lblNumero
            // 
            lblNumero.AutoSize = true;
            lblNumero.Location = new Point(175, 10);
            lblNumero.Name = "lblNumero";
            lblNumero.Size = new Size(54, 15);
            lblNumero.TabIndex = 2;
            lblNumero.Text = "Número:";
            // 
            // txtNumero
            // 
            txtNumero.Location = new Point(235, 7);
            txtNumero.Name = "txtNumero";
            txtNumero.ReadOnly = true;
            txtNumero.Size = new Size(100, 23);
            txtNumero.TabIndex = 3;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(515, 10);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(41, 15);
            lblFecha.TabIndex = 4;
            lblFecha.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(577, 7);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(110, 23);
            dtpFecha.TabIndex = 5;
            // 
            // lblTercero
            // 
            lblTercero.AutoSize = true;
            lblTercero.Location = new Point(10, 42);
            lblTercero.Name = "lblTercero";
            lblTercero.Size = new Size(49, 15);
            lblTercero.TabIndex = 6;
            lblTercero.Text = "Tercero:";
            // 
            // cboTercero
            // 
            cboTercero.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTercero.Location = new Point(70, 39);
            cboTercero.Name = "cboTercero";
            cboTercero.Size = new Size(300, 23);
            cboTercero.TabIndex = 7;

            // lblIdDocumento
            lblIdDocumento.AutoSize = true;
            lblIdDocumento.Location = new Point(10, 42);
            lblIdDocumento.Name = "lblIdDocumento";
            lblIdDocumento.Size = new Size(49, 15);
            lblIdDocumento.TabIndex = 6;
            lblIdDocumento.Text = "IdDocumento:";
            // 
            // cboIdDocumento
            // 
            cboIdDocumento.DropDownStyle = ComboBoxStyle.DropDownList;
            cboIdDocumento.Location = new Point(70, 39);
            cboIdDocumento.Name = "cboIdDocumento";
            cboIdDocumento.Size = new Size(300, 23);
            cboIdDocumento.TabIndex = 7;

            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(10, 74);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(72, 15);
            lblDescripcion.TabIndex = 8;
            lblDescripcion.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(80, 71);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(430, 55);
            txtDescripcion.TabIndex = 9;
            // 
            // dgvLineas
            // 
            dgvLineas.AllowUserToAddRows = false;
            dgvLineas.AllowUserToResizeRows = false;
            dgvLineas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLineas.Dock = DockStyle.Fill;
            dgvLineas.Location = new Point(0, 140);
            dgvLineas.Name = "dgvLineas";
            dgvLineas.RowHeadersVisible = false;
            dgvLineas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLineas.Size = new Size(800, 390);
            dgvLineas.TabIndex = 0;
            dgvLineas.CellEndEdit += dgvLineas_CellEndEdit;
            dgvLineas.CellValidating += dgvLineas_CellValidating;
            // 
            // btnAgregarLinea
            // 
            btnAgregarLinea.Location = new Point(10, 10);
            btnAgregarLinea.Name = "btnAgregarLinea";
            btnAgregarLinea.Size = new Size(110, 23);
            btnAgregarLinea.TabIndex = 0;
            btnAgregarLinea.Text = "Agregar Línea";
            btnAgregarLinea.Click += btnAgregarLinea_Click;
            // 
            // btnEliminarLinea
            // 
            btnEliminarLinea.Location = new Point(130, 10);
            btnEliminarLinea.Name = "btnEliminarLinea";
            btnEliminarLinea.Size = new Size(110, 23);
            btnEliminarLinea.TabIndex = 1;
            btnEliminarLinea.Text = "Eliminar Línea";
            btnEliminarLinea.Click += btnEliminarLinea_Click;
            // 
            // lblTotalDebito
            // 
            lblTotalDebito.AutoSize = true;
            lblTotalDebito.Location = new Point(260, 15);
            lblTotalDebito.Name = "lblTotalDebito";
            lblTotalDebito.Size = new Size(69, 15);
            lblTotalDebito.TabIndex = 3;
            lblTotalDebito.Text = "Débito: 0.00";
            // 
            // lblTotalCredito
            // 
            lblTotalCredito.AutoSize = true;
            lblTotalCredito.Location = new Point(360, 15);
            lblTotalCredito.Name = "lblTotalCredito";
            lblTotalCredito.Size = new Size(73, 15);
            lblTotalCredito.TabIndex = 4;
            lblTotalCredito.Text = "Crédito: 0.00";
            // 
            // lblBalance
            // 
            lblBalance.AutoSize = true;
            lblBalance.Location = new Point(470, 15);
            lblBalance.Name = "lblBalance";
            lblBalance.Size = new Size(75, 15);
            lblBalance.TabIndex = 5;
            lblBalance.Text = "Balance: 0.00";
            // 
            // panelHeader
            // 
            panelHeader.Controls.Add(lblTipoDoc);
            panelHeader.Controls.Add(cboTipoDoc);
            panelHeader.Controls.Add(lblNumero);
            panelHeader.Controls.Add(txtNumero);
            panelHeader.Controls.Add(lblFecha);
            panelHeader.Controls.Add(dtpFecha);
                    panelHeader.Controls.Add(lblTercero);
            panelHeader.Controls.Add(cboTercero);
            panelHeader.Controls.Add(lblDescripcion);
            panelHeader.Controls.Add(txtDescripcion);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Padding = new Padding(8);
            panelHeader.Size = new Size(800, 140);
            panelHeader.TabIndex = 2;
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(btnAgregarLinea);
            panelBottom.Controls.Add(btnEliminarLinea);
            panelBottom.Controls.Add(lblTotalDebito);
            panelBottom.Controls.Add(lblTotalCredito);
            panelBottom.Controls.Add(lblBalance);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 530);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(8);
            panelBottom.Size = new Size(800, 70);
            panelBottom.TabIndex = 1;
            // 
            // FormTransacciones
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 600);
            Controls.Add(dgvLineas);
            Controls.Add(panelBottom);
            Controls.Add(panelHeader);
            Name = "FormTransacciones";
            Text = "Registro de Transacción Contable";
            Load += FormTransacciones_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLineas).EndInit();
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelBottom.ResumeLayout(false);
            panelBottom.PerformLayout();
            ResumeLayout(false);
        }
    }
}