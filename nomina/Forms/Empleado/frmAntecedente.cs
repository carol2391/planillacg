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
using nomina.Clases.Utilidades;
using nomina.Clases.Antecedentes;
using nomina.Forms.Main;

namespace nomina.Forms.Empleado
{
    enum Opcion
    {
        Buscar,
        Salir
    }
    public partial class frmAntecedente : Form
    {
        int idEmpleado;
        Conexion conexion;
        int filaSeleccionada;
        int idAntecedente;
        Opcion opcion;
        AntedecenteConexion bdAntecedente;
        string tipoAntecedente;
        frmMain frmMain;
        public frmAntecedente(int idEmpleado, Conexion conexion, string tipoAntecedente, frmMain  frmMain)
        {
            InitializeComponent();
            if (tipoAntecedente.Equals("PO")) {
                this.lblTitulo.Text = "ANTECEDENTES POLICIALES";
            }
             else
                this.lblTitulo.Text = "ANTECEDENTES PENALES";
            Utilidad.configuarForm(this, "Antecedentes");
            Utilidad.configurarDataGrid(dgvDatos);
            this.frmMain = frmMain;
            this.conexion = conexion;
            this.idEmpleado = idEmpleado;
            this.bdAntecedente = new AntedecenteConexion(conexion);
            this.tipoAntecedente = tipoAntecedente;
            this.dgvDatos.DataSource = bdAntecedente.obtenerAntecedentes(tipoAntecedente,idEmpleado);
        }


        #region eventos scape, f2,f4
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool result;
            if (keyData != Keys.Escape)
            {
                result = base.ProcessCmdKey(ref msg, keyData);
            }
            else
            {

                if (keyData == Keys.Escape)
                {
                    if (Opcion.Buscar.Equals(opcion))
                    {
                        dgvDatos.DataSource = bdAntecedente.obtenerAntecedentes(tipoAntecedente,idEmpleado);
                       
                        
                    }
                    else
                        DialogResult = DialogResult.OK;

                }//fin scape
                result = true;
            }
            return result;
        }
        #endregion
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            
              //  int rowIndex = this.dgvDatos.CurrentCell.RowIndex;
                //this.idAntecedente = Convert.ToInt32(this.dgvDatos.Rows[rowIndex].Cells["Id"].Value.ToString());
                frmAddAntecedente frm = new frmAddAntecedente(idEmpleado, 0, conexion,"nuevo",tipoAntecedente, frmMain);
                frm.ShowDialog();
                this.dgvDatos.DataSource = bdAntecedente.obtenerAntecedentes(tipoAntecedente,idEmpleado);
               this.opcion = Opcion.Salir;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (this.dgvDatos.RowCount > 0)
            {
                int rowIndex = this.dgvDatos.CurrentCell.RowIndex;
                this.idAntecedente = Convert.ToInt32(this.dgvDatos.Rows[rowIndex].Cells["Id"].Value.ToString());
                frmAddAntecedente frm = new frmAddAntecedente(idEmpleado,idAntecedente, conexion,"modificar",tipoAntecedente, frmMain);
                frm.ShowDialog();
                this.dgvDatos.DataSource = bdAntecedente.obtenerAntecedentes(tipoAntecedente,idEmpleado);
            }
            this.opcion = Opcion.Salir;
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (this.dgvDatos.RowCount > 0)
            {
                int rowIndex = this.dgvDatos.CurrentCell.RowIndex;
                this.idAntecedente = Convert.ToInt32(this.dgvDatos.Rows[rowIndex].Cells["Id"].Value.ToString());
                this.bdAntecedente.accionesAntecedentes("E", this.idAntecedente, idEmpleado, 0, DateTime.Now, DateTime.Now, DateTime.Now, "","", frmMain.usuarioName);
                this.dgvDatos.DataSource = bdAntecedente.obtenerAntecedentes(tipoAntecedente,idEmpleado);
            }
            this.opcion = Opcion.Salir;
        }

     

        private void btnBuscar_Click(object sender, EventArgs e)
        {
          
              dgvDatos.DataSource = 
              bdAntecedente.buscarAntecedente(this.dtpFechaInicial.Value.Date,this.dtpFechaFinal.Value.Date, tipoAntecedente);
              this.opcion = Opcion.Buscar;
        }

        private void dtpFechaFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void dtpFechaInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }
    }
}
