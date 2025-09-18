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
                    new FormPlanCuentas().ShowDialog();
                    break;
                case "Node0": // Administrar Terceros
                    e.Node.Expand();
                    break;
                case "Node1": // Agregar un Tercero
                    new FormAgregarTercero().ShowDialog();
                    break;
                case "ListarTerceros": // Visualizar Terceros (Name actual)
                    new FormListarTerceros().ShowDialog();
                    break;

                    // Aquí vas agregando más opciones en el futuro
                    // case "NodeClientesAgregar": ...
            }
        }




    }
}
