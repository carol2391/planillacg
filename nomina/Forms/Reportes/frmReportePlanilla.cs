using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using nomina.Clases.ConexionManager;
namespace nomina.Forms.Reportes
{
    public partial class frmReportePlanilla : Form
    {
        Conexion conexion;
        string codigoPlanilla;
        DateTime fecha;
        Timer timer;
        public frmReportePlanilla(Conexion conexion,string codigoPlanilla,DateTime fecha)
        {
            InitializeComponent();
            this.conexion = conexion;
            this.codigoPlanilla = codigoPlanilla;
            this.fecha = fecha;
            //this.timer = timer;
            generarPlanilla();
           
        }

        private void generarPlanilla()
        {
            MySqlCommand comando = new MySqlCommand("obtener_planilla", this.conexion.getConexion());
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@P_COD_PLANILLA", codigoPlanilla);
            comando.Parameters.AddWithValue("@P_FECHA", fecha);

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(comando);

            //DataTable dataSet = new DataTable();

            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            //dgv.DataSource = dataTable;
            CrystalDecisions.CrystalReports.Engine.ReportDocument reporte;
            reporte = new rptPlanilla();
           

            reporte.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            reporte.SetDataSource(dataTable);

            this.crvPlanilla.ReportSource = reporte;
            this.WindowState = FormWindowState.Maximized;
            //timer.Enabled = false;
           // timer.Stop();
        }     
    }
}
