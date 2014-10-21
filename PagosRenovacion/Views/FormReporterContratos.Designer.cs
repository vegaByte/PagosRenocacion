namespace PagosRenovacion.Views
{
    partial class FormReporterContratos
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
            this.miReportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // miReportViewer
            // 
            this.miReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.miReportViewer.LocalReport.ReportEmbeddedResource = "PagosRenovacion.Reporting.ReportContratos.rdlc";
            this.miReportViewer.Location = new System.Drawing.Point(0, 0);
            this.miReportViewer.Name = "miReportViewer";
            this.miReportViewer.Size = new System.Drawing.Size(284, 261);
            this.miReportViewer.TabIndex = 0;
            this.miReportViewer.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // FormReporterContratos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.miReportViewer);
            this.Name = "FormReporterContratos";
            this.Text = "Pagos y Renovación de Contratos- Reporte de Contratos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormReporterContratos_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer miReportViewer;
    }
}