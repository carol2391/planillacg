using nomina.Clases.Categoria;
using nomina.Clases.ConexionManager;
using nomina.Forms.Categoria;
using nomina.Forms.Departamento;
using nomina.Forms.Empleado;
using nomina.Forms.Main;
using nomina.Forms.Usuarios;
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
        public string UserName { set; get; }
        public int Id { set; get; }
        public frmFiltro(string tipoReporte, Conexion conexion, frmMain frmMain)
        {
            InitializeComponent();
            this.conexion = conexion;
            this.frmMain = frmMain;
            this.TipoReporte  = tipoReporte;
            if (tipoReporte.Equals("BI"))
            {
                lblTitulo.Text = "Buscar Usuario";
                this.btnBuscar.Visible = true;
                this.btnBuscar.Text = "Buscar Usuario";
            }
            else {
                lblTitulo.Text = "Buscar Empleado";
                this.btnBuscar.Visible = true;
                this.btnBuscar.Text = "Buscar Empleado";
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

                case "RE":
                    frmEmpleado frmEmp1 = new frmEmpleado(conexion, frmMain);
                    frmEmp1.Tag = "buscar";
                    frmEmp1.ShowDialog();

                    if (frmEmp1.DialogResult == DialogResult.OK)
                    {
                        this.Id = frmEmp1.empleado.Id;
                        this.DialogResult = DialogResult.OK;
                    }
                    break;


                case "BI":
                    frmUsuarios frmUsuarios = new frmUsuarios(conexion, frmMain);
                    frmUsuarios.Tag = "buscar";
                    frmUsuarios.ShowDialog();

                    if (frmUsuarios.DialogResult == DialogResult.OK)
                    {
                        this.UserName = frmUsuarios.user.Usuario;
                        this.DialogResult = DialogResult.OK;
                    }
                    break;

            }
        }

        private void rbTodos_Click(object sender, EventArgs e)
        {
            
            
        }

        private void rbEspecifico_Click(object sender, EventArgs e)
        {
            
        }
    }
}
