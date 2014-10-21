namespace PagosRenovacion.Views
{
    partial class FormReporter
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.prc_date_pagosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.miReportViwer = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.prc_date_pagosBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // prc_date_pagosBindingSource
            // 
            this.prc_date_pagosBindingSource.DataSource = typeof(PagosRenovacion.prc_date_pagos);
            this.prc_date_pagosBindingSource.CurrentChanged += new System.EventHandler(this.prc_date_pagosBindingSource_CurrentChanged);
            // 
            // miReportViwer
            // 
            this.miReportViwer.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSetPagos";
            reportDataSource1.Value = this.prc_date_pagosBindingSource;
            this.miReportViwer.LocalReport.DataSources.Add(reportDataSource1);
            this.miReportViwer.LocalReport.ReportEmbeddedResource = "PagosRenovacion.Reporting.ReportPagos.rdlc";
            this.miReportViwer.Location = new System.Drawing.Point(0, 0);
            this.miReportViwer.Name = "miReportViwer";
            this.miReportViwer.Size = new System.Drawing.Size(741, 456);
            this.miReportViwer.TabIndex = 0;
            this.miReportViwer.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // FormReporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 456);
            this.Controls.Add(this.miReportViwer);
            this.Name = "FormReporter";
            this.Text = "Pagos y Renovación de Contratos- Reporte de Pagos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormReporter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.prc_date_pagosBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer miReportViwer;
        private System.Windows.Forms.BindingSource prc_date_pagosBindingSource;
    }
}