namespace SistemaContable01
{
    partial class loginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            btnConexion = new Button();
            Usuario = new Label();
            Contraseña = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            SuspendLayout();
            // 
            // btnConexion
            // 
            btnConexion.Location = new Point(12, 11);
            btnConexion.Margin = new Padding(3, 2, 3, 2);
            btnConexion.Name = "btnConexion";
            btnConexion.Size = new Size(105, 30);
            btnConexion.TabIndex = 0;
            btnConexion.Text = "Conexion Status";
            btnConexion.UseVisualStyleBackColor = true;
            btnConexion.Click += btnConexion_Click;
            // 
            // Usuario
            // 
            Usuario.AutoSize = true;
            Usuario.Location = new Point(84, 87);
            Usuario.Name = "Usuario";
            Usuario.Size = new Size(94, 15);
            Usuario.TabIndex = 1;
            Usuario.Text = "Nombre Usuario";
            Usuario.Click += Usuario_Click;
            // 
            // Contraseña
            // 
            Contraseña.AutoSize = true;
            Contraseña.Location = new Point(137, 160);
            Contraseña.Name = "Contraseña";
            Contraseña.Size = new Size(57, 15);
            Contraseña.TabIndex = 2;
            Contraseña.Text = "Password";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(210, 84);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(100, 23);
            txtUsername.TabIndex = 3;
            txtUsername.Text = "Usuario";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(210, 152);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(100, 23);
            txtPassword.TabIndex = 4;
            txtPassword.Text = "txtPassword";
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(205, 209);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(105, 29);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Iniciar Sesión";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // loginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(547, 316);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(Contraseña);
            Controls.Add(Usuario);
            Controls.Add(btnConexion);
            Margin = new Padding(3, 2, 3, 2);
            Name = "loginForm";
            Text = "Conexión BD";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConexion;
        private Label Usuario;
        private Label Contraseña;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
    }
}
