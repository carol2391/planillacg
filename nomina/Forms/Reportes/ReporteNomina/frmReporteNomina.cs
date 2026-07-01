using Microsoft.Reporting.WinForms;
using nomina.Clases.ConexionManager;
using nomina.Clases.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nomina.Forms.Reportes.ReporteNomina
{
    public partial class frmReporteNomina : Form
    {
        Conexion conexion;
        ReportesConexion reportesConexion;
        public int Mes { set; get; }
        public int Anio { set; get; }
        public string Tipo { set; get; }
        public frmReporteNomina(Conexion conexion, int mes, int anio, string tipo)
        {
            InitializeComponent();
            this.Mes = mes;
            this.Anio = anio;
            this.conexion = conexion;
            Tipo= tipo;
        }

        private void frmReporteNomina_Load(object sender, EventArgs e)
        {

            reportesConexion = new ReportesConexion(conexion);

            DataTable dt = reportesConexion.ObtenerReportesNomina("sp_reporte_nomina_libro_salarios", Mes, Anio, Tipo);

            reportViewer2.LocalReport.DataSources.Clear();

            reportViewer2.LocalReport.ReportEmbeddedResource = "nomina.Forms.Reportes.rpPlanilla.rdlc";
            reportViewer2.LocalReport.DataSources.Add(
                new ReportDataSource("Nomina", dt)
            );
            reportViewer2.RefreshReport();
            
        }
    }
}
