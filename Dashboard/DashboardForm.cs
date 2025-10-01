using SistemaContable01.Dashboard.Transacciones;
using SistemaContable01.Dashboard.Transacciones.CrudTransacciones;
using SistemaContable01.Dashboard.Terceros;
using SistemaContable01.PlanCuentas;
using SistemaContable01.Dashboard.EstadosFinancieros.BalanceGeneral; 
using SistemaContable01.Dashboard.PlanCuentas.CrudPlanCuentas; // CRUD form
using SistemaContable01.Dashboard.Informes;
using SistemaContable01.Dashboard.Informes.InformePorTercero;

using System;
using System.Windows.Forms;

#nullable enable

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
            treeView1.ExpandAll();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;

            switch (e.Node.Name)
            {
                case "NodePuc":
                    TryOpen(() => new FormPlanCuentas(), "Plan de Cuentas");
                    break;

                // Maneja ambos nombres de nodo para el CRUD
                case "NodeCrudPlanCuentas":
                    TryOpen(() => new FormCrudPlanCuentas(), "CRUD Plan de Cuentas");
                    break;

                case "NodeAgregarTercero":
                    TryOpen(() => new FormAgregarTercero(), "Agregar Tercero");
                    break;

                case "NodeListarTerceros":
                    TryOpen(() => new FormListarTerceros(), "Listar Terceros");
                    break;

                case "NodeAgregarTransaccion":
                    TryOpen(() => new FormTransacciones(), "Transacciones");
                    break;
                case "NodeCrudTransacciones":
                    TryOpen(() => new FormCrudTransacciones(), "Transacciones");
                    break;



                case "NodeBalanceGeneral":
                    TryOpen(() => new EstadosFinancieros.BalanceGeneral.FormBalanceGeneral(), "Balance General");
                    break;

                case "NodeInformePorTercero":
                    TryOpen(() => new FormInformePorTercero(), "Balance Por Tercero");
                    break;

                // Nodos solo para expandir
                case "NodeAdminPuc":
                case "NodeAdminTercero":
                case "NodeTransacciones":
                case "NodeEstadosFinancieros":
                    e.Node.Expand();
                    break;

                default:
                    // Opcional: descomenta para ver el Name real del nodo seleccionado
                    // MessageBox.Show($"Nodo no manejado: {e.Node.Name}");
                    break;
            }
        }

        private static void TryOpen(Func<Form> factory, string caption)
        {
            try
            {
                var f = factory();
                f.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error abriendo {caption}:{Environment.NewLine}{Environment.NewLine}{ex}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
