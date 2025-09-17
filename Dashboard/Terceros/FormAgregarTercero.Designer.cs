


namespace SistemaContable01.Dashboard.Terceros
{
    partial class FormAgregarTercero
    {
        private System.Windows.Forms.ComboBox cboTipoId;
        private System.Windows.Forms.TextBox txtIdentificacion;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblTipoId;
        private System.Windows.Forms.Label lblIdentificacion;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.Label lblEmail;

        private void InitializeComponent()
        {
            this.cboTipoId = new System.Windows.Forms.ComboBox();
            this.txtIdentificacion = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblTipoId = new System.Windows.Forms.Label();
            this.lblIdentificacion = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Labels
            // 
            this.lblTipoId.Text = "Tipo Identificación:";
            this.lblTipoId.Location = new System.Drawing.Point(30, 30);

            this.lblIdentificacion.Text = "Identificación:";
            this.lblIdentificacion.Location = new System.Drawing.Point(30, 70);

            this.lblNombre.Text = "Nombre:";
            this.lblNombre.Location = new System.Drawing.Point(30, 110);

            this.lblDireccion.Text = "Dirección:";
            this.lblDireccion.Location = new System.Drawing.Point(30, 150);

            this.lblTelefono.Text = "Teléfono:";
            this.lblTelefono.Location = new System.Drawing.Point(30, 190);

            this.lblEmail.Text = "Email:";
            this.lblEmail.Location = new System.Drawing.Point(30, 230);

            // 
            // Controles
            // 
            this.cboTipoId.Location = new System.Drawing.Point(180, 30);
            this.cboTipoId.Size = new System.Drawing.Size(200, 25);

            this.txtIdentificacion.Location = new System.Drawing.Point(180, 70);
            this.txtIdentificacion.Size = new System.Drawing.Size(200, 25);

            this.txtNombre.Location = new System.Drawing.Point(180, 110);
            this.txtNombre.Size = new System.Drawing.Size(200, 25);

            this.txtDireccion.Location = new System.Drawing.Point(180, 150);
            this.txtDireccion.Size = new System.Drawing.Size(200, 25);

            this.txtTelefono.Location = new System.Drawing.Point(180, 190);
            this.txtTelefono.Size = new System.Drawing.Size(200, 25);

            this.txtEmail.Location = new System.Drawing.Point(180, 230);
            this.txtEmail.Size = new System.Drawing.Size(200, 25);

            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Location = new System.Drawing.Point(180, 280);
            this.btnGuardar.Size = new System.Drawing.Size(100, 30);
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            // 
            // FormAgregarTercero
            // 
            this.ClientSize = new System.Drawing.Size(450, 350);
            this.Controls.Add(this.lblTipoId);
            this.Controls.Add(this.lblIdentificacion);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.lblDireccion);
            this.Controls.Add(this.lblTelefono);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.cboTipoId);
            this.Controls.Add(this.txtIdentificacion);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.txtDireccion);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnGuardar);
            this.Name = "FormAgregarTercero";
            this.Text = "Agregar Tercero";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
