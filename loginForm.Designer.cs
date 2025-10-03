namespace SistemaContable01
{
    partial class LoginForm   // <-- CAMBIADO A PascalCase
    {
        private System.ComponentModel.IContainer components = null;

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
            BtnConexion = new Button();
            Usuario = new Label();
            Contraseña = new Label();
            TxtUsername = new TextBox();
            TxtPassword = new TextBox();
            BtnLogin = new Button();
            SuspendLayout();
            // 
            // BtnConexion
            // 
            BtnConexion.Location = new Point(12, 11);
            BtnConexion.Margin = new Padding(3, 2, 3, 2);
            BtnConexion.Name = "BtnConexion";
            BtnConexion.Size = new Size(105, 30);
            BtnConexion.TabIndex = 0;
            BtnConexion.Text = "Conexion Status";
            BtnConexion.UseVisualStyleBackColor = true;
            BtnConexion.Click += BtnConexion_Click;
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
            // TxtUsername
            // 
            TxtUsername.Location = new Point(210, 84);
            TxtUsername.Name = "TxtUsername";
            TxtUsername.Size = new Size(100, 23);
            TxtUsername.TabIndex = 3;
            TxtUsername.Text = "Usuario";
            // 
            // TxtPassword
            // 
            TxtPassword.Location = new Point(210, 152);
            TxtPassword.Name = "TxtPassword";
            TxtPassword.Size = new Size(100, 23);
            TxtPassword.TabIndex = 4;
            TxtPassword.Text = "txtPassword";
            TxtPassword.UseSystemPasswordChar = true;
            // 
            // BtnLogin
            // 
            BtnLogin.Location = new Point(205, 209);
            BtnLogin.Name = "BtnLogin";
            BtnLogin.Size = new Size(105, 29);
            BtnLogin.TabIndex = 5;
            BtnLogin.Text = "Iniciar Sesión";
            BtnLogin.UseVisualStyleBackColor = true;
            BtnLogin.Click += BtnLogin_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(547, 316);
            Controls.Add(BtnLogin);
            Controls.Add(TxtPassword);
            Controls.Add(TxtUsername);
            Controls.Add(Contraseña);
            Controls.Add(Usuario);
            Controls.Add(BtnConexion);
            Margin = new Padding(3, 2, 3, 2);
            Name = "LoginForm";
            Text = "Conexión BD";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button BtnConexion;
        private Label Usuario;
        private Label Contraseña;
        private TextBox TxtUsername;
        private TextBox TxtPassword;
        private Button BtnLogin;
    }
}
