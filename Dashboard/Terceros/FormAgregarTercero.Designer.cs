namespace SistemaContable01.Dashboard.Terceros
{
    partial class FormAgregarTercero
    {
        private System.ComponentModel.IContainer components = null;

        // Declaración de controles
        private System.Windows.Forms.TextBox txtTipoIdentificacion;
        private System.Windows.Forms.TextBox txtNumeroIdentificacion;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.TextBox txtPrimerNombre;
        private System.Windows.Forms.TextBox txtOtrosNombres;
        private System.Windows.Forms.TextBox txtPrimerApellido;
        private System.Windows.Forms.TextBox txtSegundoApellido;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.TextBox txtCiudad;
        private System.Windows.Forms.TextBox txtDepartamento;
        private System.Windows.Forms.TextBox txtPais;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtRegimenTributario;
        private System.Windows.Forms.TextBox txtActividadEconomica;
        private System.Windows.Forms.Button btnGuardar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // Instanciar controles
            txtTipoIdentificacion = new System.Windows.Forms.TextBox();
            txtNumeroIdentificacion = new System.Windows.Forms.TextBox();
            txtRazonSocial = new System.Windows.Forms.TextBox();
            txtPrimerNombre = new System.Windows.Forms.TextBox();
            txtOtrosNombres = new System.Windows.Forms.TextBox();
            txtPrimerApellido = new System.Windows.Forms.TextBox();
            txtSegundoApellido = new System.Windows.Forms.TextBox();
            txtDireccion = new System.Windows.Forms.TextBox();
            txtCiudad = new System.Windows.Forms.TextBox();
            txtDepartamento = new System.Windows.Forms.TextBox();
            txtPais = new System.Windows.Forms.TextBox();
            txtTelefono = new System.Windows.Forms.TextBox();
            txtEmail = new System.Windows.Forms.TextBox();
            txtRegimenTributario = new System.Windows.Forms.TextBox();
            txtActividadEconomica = new System.Windows.Forms.TextBox();
            btnGuardar = new System.Windows.Forms.Button();

            // Configurar controles (solo ejemplo, ajustar posiciones y tamaños)
            txtTipoIdentificacion.Location = new System.Drawing.Point(20, 20);
            txtTipoIdentificacion.Width = 200;
            txtTipoIdentificacion.PlaceholderText = "Tipo Identificación";

            txtNumeroIdentificacion.Location = new System.Drawing.Point(240, 20);
            txtNumeroIdentificacion.Width = 200;
            txtNumeroIdentificacion.PlaceholderText = "Número Identificación";

            txtRazonSocial.Location = new System.Drawing.Point(20, 60);
            txtRazonSocial.Width = 200;
            txtRazonSocial.PlaceholderText = "Razón Social";

            txtPrimerNombre.Location = new System.Drawing.Point(240, 60);
            txtPrimerNombre.Width = 200;
            txtPrimerNombre.PlaceholderText = "Primer Nombre";

            txtOtrosNombres.Location = new System.Drawing.Point(20, 100);
            txtOtrosNombres.Width = 200;
            txtOtrosNombres.PlaceholderText = "Otros Nombres";

            txtPrimerApellido.Location = new System.Drawing.Point(240, 100);
            txtPrimerApellido.Width = 200;
            txtPrimerApellido.PlaceholderText = "Primer Apellido";

            txtSegundoApellido.Location = new System.Drawing.Point(20, 140);
            txtSegundoApellido.Width = 200;
            txtSegundoApellido.PlaceholderText = "Segundo Apellido";

            txtDireccion.Location = new System.Drawing.Point(240, 140);
            txtDireccion.Width = 200;
            txtDireccion.PlaceholderText = "Dirección";

            txtCiudad.Location = new System.Drawing.Point(20, 180);
            txtCiudad.Width = 200;
            txtCiudad.PlaceholderText = "Ciudad";

            txtDepartamento.Location = new System.Drawing.Point(240, 180);
            txtDepartamento.Width = 200;
            txtDepartamento.PlaceholderText = "Departamento";

            txtPais.Location = new System.Drawing.Point(20, 220);
            txtPais.Width = 200;
            txtPais.PlaceholderText = "País";

            txtTelefono.Location = new System.Drawing.Point(240, 220);
            txtTelefono.Width = 200;
            txtTelefono.PlaceholderText = "Teléfono";

            txtEmail.Location = new System.Drawing.Point(20, 260);
            txtEmail.Width = 200;
            txtEmail.PlaceholderText = "Email";

            txtRegimenTributario.Location = new System.Drawing.Point(240, 260);
            txtRegimenTributario.Width = 200;
            txtRegimenTributario.PlaceholderText = "Régimen Tributario";

            txtActividadEconomica.Location = new System.Drawing.Point(20, 300);
            txtActividadEconomica.Width = 420;
            txtActividadEconomica.PlaceholderText = "Actividad Económica";

            btnGuardar.Location = new System.Drawing.Point(20, 340);
            btnGuardar.Text = "Guardar";
            btnGuardar.Width = 100;
            btnGuardar.Click += BtnGuardar_Click;

            // Agregar controles al formulario
            this.Controls.Add(txtTipoIdentificacion);
            this.Controls.Add(txtNumeroIdentificacion);
            this.Controls.Add(txtRazonSocial);
            this.Controls.Add(txtPrimerNombre);
            this.Controls.Add(txtOtrosNombres);
            this.Controls.Add(txtPrimerApellido);
            this.Controls.Add(txtSegundoApellido);
            this.Controls.Add(txtDireccion);
            this.Controls.Add(txtCiudad);
            this.Controls.Add(txtDepartamento);
            this.Controls.Add(txtPais);
            this.Controls.Add(txtTelefono);
            this.Controls.Add(txtEmail);
            this.Controls.Add(txtRegimenTributario);
            this.Controls.Add(txtActividadEconomica);
            this.Controls.Add(btnGuardar);

            this.Text = "Agregar Tercero";
            this.ClientSize = new System.Drawing.Size(480, 400);
        }
    }
}
