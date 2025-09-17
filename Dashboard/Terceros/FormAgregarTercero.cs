using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient; // porque estás usando Microsoft.Data.SqlClient

namespace SistemaContable01.Dashboard.Terceros
{
    public partial class FormAgregarTercero : Form
    {
        public FormAgregarTercero()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = @"Server=localhost\SQLEXPRESS;Database=SysCon01Db;Trusted_Connection=True;Encrypt=False;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Terceros (TipoIdentificacion, Identificacion, Nombre, Direccion, Telefono, Email) " +
                                   "VALUES (@TipoId, @Identificacion, @Nombre, @Direccion, @Telefono, @Email)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TipoId", cboTipoId.SelectedItem?.ToString());
                        cmd.Parameters.AddWithValue("@Identificacion", txtIdentificacion.Text);
                        cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                        cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Tercero guardado correctamente.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar tercero: " + ex.Message);
            }
        }
    }
}
