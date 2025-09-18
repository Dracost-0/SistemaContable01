using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaContable01.Dashboard.Terceros
{
    public partial class FormAgregarTercero : Form
    {
        public FormAgregarTercero()
        {
            InitializeComponent();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            // Cadena de conexión (ajusta si cambias instancia/BD)
            string connectionString = @"Server=localhost\SQLEXPRESS;Database=SysCon01Db;Trusted_Connection=True;Encrypt=False;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO dbo.Terceros
                                    (TipoIdentificacion, NumeroIdentificacion, RazonSocial,
                                     PrimerNombre, OtrosNombres, PrimerApellido, SegundoApellido,
                                     Direccion, Ciudad, Departamento, Pais, Telefono, Email,
                                     RegimenTributario, ActividadEconomicaCIIU)
                                     VALUES
                                    (@TipoIdentificacion, @NumeroIdentificacion, @RazonSocial,
                                     @PrimerNombre, @OtrosNombres, @PrimerApellido, @SegundoApellido,
                                     @Direccion, @Ciudad, @Departamento, @Pais, @Telefono, @Email,
                                     @RegimenTributario, @ActividadEconomicaCIIU)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TipoIdentificacion", txtTipoIdentificacion.Text);
                        cmd.Parameters.AddWithValue("@NumeroIdentificacion", txtNumeroIdentificacion.Text);
                        cmd.Parameters.AddWithValue("@RazonSocial", txtRazonSocial.Text);
                        cmd.Parameters.AddWithValue("@PrimerNombre", txtPrimerNombre.Text);
                        cmd.Parameters.AddWithValue("@OtrosNombres", txtOtrosNombres.Text);
                        cmd.Parameters.AddWithValue("@PrimerApellido", txtPrimerApellido.Text);
                        cmd.Parameters.AddWithValue("@SegundoApellido", txtSegundoApellido.Text);
                        cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                        cmd.Parameters.AddWithValue("@Ciudad", txtCiudad.Text);
                        cmd.Parameters.AddWithValue("@Departamento", txtDepartamento.Text);
                        cmd.Parameters.AddWithValue("@Pais", txtPais.Text);
                        cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@RegimenTributario", txtRegimenTributario.Text);
                        cmd.Parameters.AddWithValue("@ActividadEconomicaCIIU", txtActividadEconomica.Text);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Tercero agregado correctamente.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar: " + ex.Message);
                }
            }
        }
    }
}
