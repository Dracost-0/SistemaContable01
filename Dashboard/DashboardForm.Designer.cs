namespace SistemaContable01.Dashboard
{
    partial class DashboardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        /// <summary>
        /// Clean up any resources being used.
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TreeNode treeNode1 = new TreeNode("Agregar, Editar, Eliminar Cuenta");
            TreeNode treeNode2 = new TreeNode("Plan Único de Cuentas", new TreeNode[] { treeNode1 });
            TreeNode treeNode3 = new TreeNode("Administrar Cuentas", new TreeNode[] { treeNode2 });
            TreeNode treeNode4 = new TreeNode("Agregar un Tercero");
            TreeNode treeNode5 = new TreeNode("Visualizar Terceros");
            TreeNode treeNode6 = new TreeNode("Administrar Terceros", new TreeNode[] { treeNode4, treeNode5 });
            TreeNode treeNode7 = new TreeNode("Registrar Transaccion");
            TreeNode treeNode8 = new TreeNode("Transacciones", new TreeNode[] { treeNode7 });
            TreeNode treeNode9 = new TreeNode("Balance General");
            TreeNode treeNode10 = new TreeNode("Estados Financieros", new TreeNode[] { treeNode9 });
            TreeNode treeNode11 = new TreeNode("Informe Por Tercero");
            TreeNode treeNode12 = new TreeNode("Informes", new TreeNode[] { treeNode11 });
            contextMenuStrip1 = new ContextMenuStrip(components);
            dgvPUC = new DataGridView();
            treeView1 = new TreeView();
            ((System.ComponentModel.ISupportInitialize)dgvPUC).BeginInit();
            SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // dgvPUC
            // 
            dgvPUC.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPUC.Location = new Point(282, 12);
            dgvPUC.Name = "dgvPUC";
            dgvPUC.Size = new Size(446, 426);
            dgvPUC.TabIndex = 3;
            // 
            // treeView1
            // 
            treeView1.Location = new Point(12, 12);
            treeView1.Name = "treeView1";
            treeNode1.Name = "NodeCrudPlanCuentas";
            treeNode1.Text = "Agregar, Editar, Eliminar Cuenta";
            treeNode2.Name = "NodePuc";
            treeNode2.Text = "Plan Único de Cuentas";
            treeNode3.Name = "NodeAdminPuc";
            treeNode3.Text = "Administrar Cuentas";
            treeNode4.Name = "NodeAgregarTercero";
            treeNode4.Text = "Agregar un Tercero";
            treeNode5.Name = "NodeListarTerceros";
            treeNode5.Text = "Visualizar Terceros";
            treeNode6.Name = "NodeAdminTercero";
            treeNode6.Text = "Administrar Terceros";
            treeNode7.Name = "NodeAgregarTransaccion";
            treeNode7.Text = "Registrar Transaccion";
            treeNode8.Name = "NodeTransacciones";
            treeNode8.Text = "Transacciones";
            treeNode9.Name = "NodeBalanceGeneral";
            treeNode9.Text = "Balance General";
            treeNode10.Name = "NodeEstadosFinancieros";
            treeNode10.Text = "Estados Financieros";
            treeNode11.Name = "NodeInformePorTercero";
            treeNode11.Text = "Informe Por Tercero";
            treeNode12.Name = "NodeInformes";
            treeNode12.Text = "Informes";
            treeView1.Nodes.AddRange(new TreeNode[] { treeNode3, treeNode6, treeNode8, treeNode10, treeNode12 });
            treeView1.Size = new Size(253, 426);
            treeView1.TabIndex = 2;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // DashboardForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvPUC);
            Controls.Add(treeView1);
            Name = "DashboardForm";
            Text = "DashboardForm";
            Load += DashboardForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPUC).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ContextMenuStrip contextMenuStrip1;
        private DataGridView dgvPUC;
        private TreeView treeView1;
    }
}