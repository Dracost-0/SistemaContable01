namespace SistemaContable01.PlanCuentas
{
    partial class FormPlanCuentas
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

        private void InitializeComponent()
        {
            this.dgvPUC = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPUC)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPUC
            // 
            this.dgvPUC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPUC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPUC.Location = new System.Drawing.Point(0, 0);
            this.dgvPUC.Name = "dgvPUC";
            this.dgvPUC.Size = new System.Drawing.Size(800, 450);
            this.dgvPUC.TabIndex = 0;
            // 
            // FormPlanCuentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvPUC);
            this.Name = "FormPlanCuentas";
            this.Text = "Plan de Cuentas";
            this.Load += new System.EventHandler(this.FormPlanCuentas_Load); // <-- ESTA LÍNEA ES CLAVE
            ((System.ComponentModel.ISupportInitialize)(this.dgvPUC)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView dgvPUC;
    }
}
