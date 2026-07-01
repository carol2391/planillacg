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
using nomina.Clases.Empresas;
namespace nomina.Forms.Reportes.ReporteNomina
{
    public partial class frmReporteVarios : Form
    {
        Conexion conexion;
        ReportesConexion reportesConexion;
        EmpresaConexion empresaConexion;
        string NombreReporte;
        int Id;
        string Tipo;
        string User;
        string Modo;
        public frmReporteVarios(Conexion conexion, string nombreReporte, string tipo,string user ,string modo)
        {
            InitializeComponent();
            this.conexion = conexion;
            NombreReporte = nombreReporte;
            this.Tipo = tipo;
            this.Modo = modo;
            this.User = user;
        }

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

                case "RGE":
                    reportesConexion = new ReportesConexion(conexion);
                    DataTable dt4 = reportesConexion.ObtenerReportes(NombreReporte, Id);
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.ReportEmbeddedResource = "nomina.Forms.Reportes.rpReporteGeneralEmpleados.rdlc";
                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("ReporteGeneralEmpleados", dt4)
                    );
                    reportViewer1.RefreshReport();
                    break;

                case "RGC":
                    reportesConexion = new ReportesConexion(conexion);
                    DataTable dt5 = reportesConexion.ObtenerReportes(NombreReporte, Id);
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.ReportEmbeddedResource = "nomina.Forms.Reportes.rpReporteGeneralCategorias.rdlc";
                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("ReporteGeneralCategorias", dt5)
                    );
                    reportViewer1.RefreshReport();
                    break;

                case "RGD":
                    reportesConexion = new ReportesConexion(conexion);
                    DataTable dt6 = reportesConexion.ObtenerReportes(NombreReporte, Id);
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.ReportEmbeddedResource = "nomina.Forms.Reportes.rpReporteGeneralDeducciones.rdlc";
                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("rpReporteGeneralDeducciones", dt6)
                    );
                    reportViewer1.RefreshReport();
                    break;

                  case "RE":
                    reportesConexion = new ReportesConexion(conexion);
                    DataTable dt7 = reportesConexion.ObtenerReportes(NombreReporte, Id);
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.ReportEmbeddedResource = "nomina.Forms.Reportes.rpReporteGeneralDeducciones.rdlc";
                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("rpReporteGeneralDeducciones", dt7)
                    );
                    reportViewer1.RefreshReport();
                    break;


                case "RRE":
                    reportesConexion = new ReportesConexion(conexion);
                    DataTable dt8= reportesConexion.ObtenerReportes(NombreReporte, Id);

                    reportViewer1.LocalReport.ReportEmbeddedResource = "nomina.Forms.Reportes.rpResumenPrestamoEmpleado.rdlc";
                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("ResumenPrestamoEmpleado", dt8)
                    );
                    reportViewer1.RefreshReport();
                    break;


                case "RDE": 
                    reportesConexion = new ReportesConexion(conexion);
                    DataTable dt9= reportesConexion.ObtenerReportes(NombreReporte, Id);

                    reportViewer1.LocalReport.ReportEmbeddedResource = "nomina.Forms.Reportes.rpDescriptivoPrestamoEmpleado.rdlc";
                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("DescriptivoPrestamoEmpleado", dt9)
                    );
                    reportViewer1.RefreshReport();
                    break;

                case "RL":
                    reportesConexion = new ReportesConexion(conexion);
                    DataTable dt10 = reportesConexion.ObtenerReporteLiquidacion(Id, DateTime.Now);

                    reportViewer1.LocalReport.ReportEmbeddedResource = "nomina.Forms.Reportes.rpReporteLiquidacion.rdlc";
                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("ReporteLiquidacion", dt10)
                    );
                    reportViewer1.RefreshReport();
                    break;

                case "RF":
                    reportesConexion = new ReportesConexion(conexion);
                    DataTable dt11 = reportesConexion.ObtenerFichaEmpleado(Id);

                    reportViewer1.LocalReport.ReportEmbeddedResource = "nomina.Forms.Reportes.rpFichaEmpleado.rdlc";
                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("FichaEmpleado", dt11)
                    );
                    reportViewer1.RefreshReport();
                    break;

                case "BI":
                    reportesConexion = new ReportesConexion(conexion);
                    DataTable dt12 = reportesConexion.ObtenerBitacora(User, Modo);

                    reportViewer1.LocalReport.ReportEmbeddedResource = "nomina.Forms.Reportes.rpBitacora.rdlc";
                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("BITACORA", dt12)
                    );
                    reportViewer1.RefreshReport();
                    break;

                case "BIULTIMO":
                    empresaConexion = new EmpresaConexion();
                    DataTable dt13 = empresaConexion.ObtenerBitacoraEmpresa(User, Modo);

                    reportViewer1.LocalReport.ReportEmbeddedResource = "nomina.Forms.Reportes.rpBitacora.rdlc";
                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("BITACORA", dt13)
                    );
                    reportViewer1.RefreshReport();
                    break;

            }
            
        }


    }
}
