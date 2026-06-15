using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nomina.Clases.ConexionManager;
using nomina.Clases.Reportes;
namespace nomina.Forms.Reportes.ReporteNomina
{
    public partial class frmReporteVarios : Form
    {
        Conexion conexion;
        ReportesConexion reportesConexion;
        string NombreReporte;
        int Id;
        string Tipo;
        public frmReporteVarios(Conexion conexion, string nombreReporte, int id, string tipo)
        {
            InitializeComponent();
            this.conexion = conexion;
            NombreReporte = nombreReporte;
            this.Id = id;
            this.Tipo = tipo;
        }
        public frmReporteVarios(Conexion conexion, string nombreReporte)
        {
            InitializeComponent();
            this.conexion = conexion;
            NombreReporte = nombreReporte;
        }
        private void frmReporteVarios_Load(object sender, EventArgs e)
        {

            switch (Tipo) {
                case "D":
                    reportesConexion = new ReportesConexion(conexion);

                    DataTable dt = reportesConexion.ObtenerReportes(NombreReporte, Id);

                    reportViewer1.LocalReport.DataSources.Clear();

                    reportViewer1.LocalReport.ReportEmbeddedResource = "nomina.Forms.Reportes.rpDepartamentordlc.rdlc";
                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("Departamento", dt)
                    );
                    reportViewer1.RefreshReport();
                    break;
                case "C":
                    reportesConexion = new ReportesConexion(conexion);
                    DataTable dt2 = reportesConexion.ObtenerReportes(NombreReporte, Id);
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.ReportEmbeddedResource = "nomina.Forms.Reportes.rpCategoria.rdlc";
                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("Categoria", dt2)
                    );
                    reportViewer1.RefreshReport();
                    break;

                case "L":
                    reportesConexion = new ReportesConexion(conexion);
                    DataTable dt3 = reportesConexion.ObtenerReportes(NombreReporte, Id);
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.ReportEmbeddedResource = "nomina.Forms.Reportes.rplabores.rdlc";
                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("Labores", dt3)
                    );
                    reportViewer1.RefreshReport();
                    break;

            }
            
        }


    }
}
