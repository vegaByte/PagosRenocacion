using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Collections;

namespace PagosRenovacion.Views
{
    public partial class FormReporter : Form
    {
        IList miResultado;
        public FormReporter(IList resultado)
        {
            InitializeComponent();
            miResultado = resultado;
        }
        
        private void FormReporter_Load(object sender, EventArgs e)
        {

            this.miReportViwer.RefreshReport();
        }

        private void prc_date_pagosBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            IList lista = miResultado;

            miReportViwer.LocalReport.DataSources.Clear();
            miReportViwer.LocalReport.ReportEmbeddedResource = "PagosRenovacion.Reporting.ReportPagos.rdlc";

            Microsoft.Reporting.WinForms.ReportDataSource dataset = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", lista);
            miReportViwer.LocalReport.DataSources.Add(dataset);
            dataset.Value = lista;

            miReportViwer.LocalReport.Refresh();
            miReportViwer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            this.miReportViwer.RefreshReport();

        }
    }
}
