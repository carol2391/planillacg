using nomina.Clases.ConexionManager;
using nomina.Clases.Reportes;
using nomina.Forms.Main;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nomina.Forms.Reportes.ReporteNomina
{
    public partial class frmReportePlanila : Form
    {
        Conexion conexion;
        frmMain frmMain;
        public string Codigo {set;get;}
        public DateTime Fecha { set; get; }
        public frmReportePlanila(Conexion conexion, frmMain frmMain)
        {
            InitializeComponent();
            this.conexion = conexion;
            this.frmMain = frmMain;
            CargarComboMeses();
            CargarComboAnios();

        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            frmReporteNomina frmReporte = new frmReporteNomina(conexion,(int)(cbMes.SelectedValue), (int)(cbAnio.SelectedValue));
            frmReporte.ShowDialog();
        }

        private void frmReportePlanila_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void CargarComboMeses()
        {
            var meses = new[]
            {
                new { Id = 1, Nombre = "Enero" },
                new { Id = 2, Nombre = "Febrero" },
                new { Id = 3, Nombre = "Marzo" },
                new { Id = 4, Nombre = "Abril" },
                new { Id = 5, Nombre = "Mayo" },
                new { Id = 6, Nombre = "Junio" },
                new { Id = 7, Nombre = "Julio" },
                new { Id = 8, Nombre = "Agosto" },
                new { Id = 9, Nombre = "Septiembre" },
                new { Id = 10, Nombre = "Octubre" },
                new { Id = 11, Nombre = "Noviembre" },
                new { Id = 12, Nombre = "Diciembre" }
            };

            cbMes.DataSource = meses;
            cbMes.DisplayMember = "Nombre"; 
            cbMes.ValueMember = "Id";       
            cbMes.SelectedValue = DateTime.Now.Month;
        }

        private void CargarComboAnios()
        {
            int anioActual = DateTime.Now.Year;


            List<int> anios = new List<int>();

            for (int i = anioActual; i >= anioActual - 5; i--)
            {
                anios.Add(i);
            }

            cbAnio.DataSource = anios;

            cbAnio.SelectedItem = anioActual;
        }
    }
}
