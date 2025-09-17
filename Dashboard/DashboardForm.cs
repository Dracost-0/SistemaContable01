using Microsoft.Data.SqlClient;// Para SQL Server
using SistemaContable01.Dashboard.Terceros;
using SistemaContable01.PlanCuentas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;   // Opcional si usas app.config
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SistemaContable01.Dashboard
{
    public partial class DashboardForm : Form
    {

        // Conexión a SQL Server
        string connectionString = @"Server=localhost\SQLEXPRESS;Database=SysCon01Db;Trusted_Connection=True;Encrypt=False;";

        public DashboardForm()
        {
            InitializeComponent();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            // Expandimos todo el árbol
            treeView1.ExpandAll();
        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;

            switch (e.Node.Name)
            {
                // PUC
                case "NodeAdminPuc":
                    e.Node.Expand();
                    break;

                case "NodePuc":
                    FormPlanCuentas formPuc = new FormPlanCuentas();
                    formPuc.ShowDialog();
                    break;


                // Terceros
                case "NodeAdminTerceros":
                    e.Node.Expand();
                    break;

                case "NodeAgregarTercero":
                    FormAgregarTercero formTercero = new FormAgregarTercero();
                    formTercero.ShowDialog();
                    break;

                    // Aquí vas agregando más opciones en el futuro
                    // case "NodeClientesAgregar": ...
            }
        }




    }
}
