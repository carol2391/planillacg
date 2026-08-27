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
        public string Reporte { set; get; }
        public frmReporteNomina(Conexion conexion, int mes, int anio, string tipo, string reporte)
        {
            InitializeComponent();
            this.Mes = mes;
            this.Anio = anio;
            this.conexion = conexion;
            Tipo= tipo;
            this.Reporte = reporte;
        }

        private void frmReporteNomina_Load(object sender, EventArgs e)
        {

            switch (Reporte) {
                case "P":
                    reportesConexion = new ReportesConexion(conexion);

                    DataTable dt = reportesConexion.ObtenerReportesNomina("sp_reporte_nomina_libro_salarios", Mes, Anio, Tipo);

                    reportViewer2.LocalReport.DataSources.Clear();

                    reportViewer2.LocalReport.ReportEmbeddedResource = "nomina.Forms.Reportes.rpPlanilla.rdlc";
                    reportViewer2.LocalReport.DataSources.Add(
                        new ReportDataSource("Nomina", dt)
                    );
                    reportViewer2.RefreshReport();
                    break;

                case "R":
                    reportesConexion = new ReportesConexion(conexion);

                    DataTable dt1 = reportesConexion.ObtenerReportesAsalariados("sp_obtener_retenciones_mensuales", Mes, Anio);

                    reportViewer2.LocalReport.DataSources.Clear();
                    reportViewer2.LocalReport.ReportEmbeddedResource = "nomina.Forms.Reportes.rpRetencionImpuestos.rdlc";

                    reportViewer2.LocalReport.DataSources.Add(
                        new ReportDataSource("PlanillaRetencionImpuestos", dt1)
                    );

                   
                    reportViewer2.RefreshReport();
                    if (dt1.Rows.Count > 0)
                    {
                        reportViewer2.LocalReport.DisplayName = $"Plantilla_{Anio}{Mes:D2}_111";
                    }

                    break;



            }
            
            
        }
    }
}
