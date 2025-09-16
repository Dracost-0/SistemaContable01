using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            treeView1.ExpandAll();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Expandir nodo padre cuando se selecciona
            if (e.Node?.Name == "NodeAdminPuc")
            {
                e.Node.Expand(); // Esto muestra los subnodos
            }

            // Detectar selección del subnodo
            if (e.Node?.Name == "NodePuc")
            {
                MessageBox.Show("Se seleccionó Plan Único de Cuentas");
            }
        }



    }
}
