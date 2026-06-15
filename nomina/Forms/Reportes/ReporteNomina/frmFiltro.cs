using nomina.Clases.Categoria;
using nomina.Clases.ConexionManager;
using nomina.Forms.Categoria;
using nomina.Forms.Departamento;
using nomina.Forms.Empleado;
using nomina.Forms.Main;
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
   
    public partial class frmFiltro : Form
    {
        private string TipoReporte;
        Conexion conexion;
        frmMain frmMain;
        public int Id { set; get; }
        public frmFiltro(string tipoReporte, Conexion conexion, frmMain frmMain)
        {
            InitializeComponent();
            CargarInformacion(tipoReporte);
            this.conexion = conexion;
            this.frmMain = frmMain;
            this.TipoReporte  = tipoReporte;
        }

        private void CargarInformacion(string tipoReporte) {
            switch (tipoReporte) { 
                case "D":
                    lblTitulo.Text = "Departamento";
                    this.rbEspecifico.Text = "Departamento específico";
                    break;


                case "C":
                   lblTitulo.Text = "Categoria";
                    this.rbEspecifico.Text = "Categoría específico";
                    break;

                case "L":
                    lblTitulo.Text = "Labores";
                    this.rbEspecifico.Text = "Labores específico";
                    break;
            }
        
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
           BuscarCodigo();
        }

        private void BuscarCodigo() {
            switch (TipoReporte)
            {
                case "D":
                     frmDepartamento frmDepartamento = new frmDepartamento(conexion,frmMain);
                    frmDepartamento.Tag = "buscar";
                    frmDepartamento.ShowDialog();
                      
                    if (frmDepartamento.DialogResult == DialogResult.OK)
                    {
                        this.Id = frmDepartamento.depto.Id;
                        this.DialogResult = DialogResult.OK;
                    }
                        break;

                case "C":
                    frmCategoria frmCat = new frmCategoria(conexion, frmMain, new List<CategoriaData>());
                    frmCat.Tag = "buscar";
                    frmCat.ShowDialog();

                    if (frmCat.DialogResult == DialogResult.OK)
                    {
                        this.Id = frmCat.categoria.Id;
                        this.DialogResult = DialogResult.OK;
                    }
                    break;

                case "L":
                    frmEmpleado frmEmp = new frmEmpleado(conexion, frmMain);
                    frmEmp.Tag = "buscar";
                    frmEmp.ShowDialog();

                    if (frmEmp.DialogResult == DialogResult.OK)
                    {
                        this.Id = frmEmp.empleado.Id;
                        this.DialogResult = DialogResult.OK;
                    }
                    break;
            }
        }

        private void rbTodos_Click(object sender, EventArgs e)
        {
            if (this.rbTodos.Checked) {
                this.Id = 0;
                this.DialogResult = DialogResult.OK;
            }
            
        }

        private void rbEspecifico_Click(object sender, EventArgs e)
        {
            this.btnBuscar.Visible = true;
        }
    }
}
