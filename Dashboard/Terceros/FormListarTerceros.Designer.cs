namespace SistemaContable01.Dashboard.Terceros
{
    partial class FormListarTerceros
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvTerceros;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.Label lblTotal;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            dgvTerceros = new System.Windows.Forms.DataGridView();
            txtBuscar = new System.Windows.Forms.TextBox();
            btnRefrescar = new System.Windows.Forms.Button();
            lblBuscar = new System.Windows.Forms.Label();
            lblTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)dgvTerceros).BeginInit();
            SuspendLayout();
            // 
            // dgvTerceros
            // 
            dgvTerceros.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                   | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            dgvTerceros.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvTerceros.Location = new System.Drawing.Point(12, 56);
            dgvTerceros.MultiSelect = false;
            dgvTerceros.Name = "dgvTerceros";
            dgvTerceros.ReadOnly = true;
            dgvTerceros.RowHeadersVisible = false;
            dgvTerceros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvTerceros.Size = new System.Drawing.Size(860, 392);
            dgvTerceros.TabIndex = 0;
            dgvTerceros.CellDoubleClick += dgvTerceros_CellDoubleClick;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new System.Drawing.Point(70, 18);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new System.Drawing.Size(250, 23);
            txtBuscar.TabIndex = 1;
            txtBuscar.KeyDown += txtBuscar_KeyDown;
            // 
            // btnRefrescar
            // 
            btnRefrescar.Location = new System.Drawing.Point(326, 17);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new System.Drawing.Size(90, 25);
            btnRefrescar.TabIndex = 2;
            btnRefrescar.Text = "Buscar / Refrescar";
            btnRefrescar.UseVisualStyleBackColor = true;
            btnRefrescar.Click += btnRefrescar_Click;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new System.Drawing.Point(20, 22);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new System.Drawing.Size(44, 15);
            lblBuscar.TabIndex = 3;
            lblBuscar.Text = "Buscar:";
            // 
            // lblTotal
            // 
            lblTotal.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            lblTotal.AutoSize = true;
            lblTotal.Location = new System.Drawing.Point(12, 455);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new System.Drawing.Size(44, 15);
            lblTotal.TabIndex = 4;
            lblTotal.Text = "Total: 0";
            // 
            // FormListarTerceros
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(884, 481);
            Controls.Add(lblTotal);
            Controls.Add(lblBuscar);
            Controls.Add(btnRefrescar);
            Controls.Add(txtBuscar);
            Controls.Add(dgvTerceros);
            Name = "FormListarTerceros";
            Text = "Listado de Terceros";
            Load += FormListarTerceros_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTerceros).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}