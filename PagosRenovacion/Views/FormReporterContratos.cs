using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PagosRenovacion.Views
{
    public partial class FormReporterContratos : Form
    {

        IList miResultado;
        public FormReporterContratos(IList resultado)
        {
            InitializeComponent();
            miResultado = resultado;
        }

        private void FormReporterContratos_Load(object sender, EventArgs e)
        {

            this.miReportViewer.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            IList lista = miResultado;

            miReportViewer.LocalReport.DataSources.Clear();
            miReportViewer.LocalReport.ReportEmbeddedResource = "PagosRenovacion.Reporting.ReportContratos.rdlc";

            Microsoft.Reporting.WinForms.ReportDataSource dataset = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", lista);
            miReportViewer.LocalReport.DataSources.Add(dataset);
            dataset.Value = lista;

            miReportViewer.LocalReport.Refresh();
            miReportViewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            this.miReportViewer.RefreshReport();
        }
    }
}
