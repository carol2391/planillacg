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
using nomina.Clases.Parametro;
using nomina.Clases.Utilidades;
using nomina.Forms.Main;
using nomina.Clases.PermisosUsuario;

namespace nomina.Forms.Parametros
{
    public partial class frmParametros : Form
    {
        Conexion conexion;
        List<ParametroData> lParametros;
        ParametroData parametroData;
        ParametroConexion bdParametro;
        PermisoUsuarioConexion bdPermisos;
        frmMain frmMain;

        public frmParametros(Conexion conexion,frmMain frmMain)
        {
            InitializeComponent();
            this.conexion = conexion;
            bdParametro = new ParametroConexion(conexion);
            bdPermisos = new PermisoUsuarioConexion();
            this.frmMain = frmMain;
            Utilidad.configurarDataGrid(dgvDatos);
            this.nudPeriodo.Select();
        }

        #region menu

        public void cambiarControlEnter(KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
                buscarParametros();

            }
        }
        private void NudPeriodo_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambiarControlEnter(e);
        }
        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            if (bdPermisos.existePermiso(this.frmMain.usuarioId, 53)) {
                frmAddParametro frm = new frmAddParametro(conexion);
                frm.Tag = "agregar";
                frm.ShowDialog();
            }
            else
                btnNuevo.Enabled = false;

        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            if ( bdPermisos.existePermiso(this.frmMain.usuarioId, 54) )
                 modificar();
            else
                btnModificar.Enabled = false;
        }

        private void BtnQuitar_Click(object sender, EventArgs e)
        {
            if ( bdPermisos.existePermiso(this.frmMain.usuarioId, 55) )
                eliminar();
            else
                btnQuitar.Enabled = false;
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        #endregion

        public void buscarParametros() {

            lParametros = bdParametro.buscarParametros(Convert.ToInt32(nudPeriodo.Value));
            dgvDatos.DataSource = lParametros;
        }

        #region modificar
        public void modificar()
        {
            if (dgvDatos.RowCount > 0)
            {
                DateTime fechaActual = DateTime.Now;
                int mesActual = fechaActual.Month;
                int añoActual = fechaActual.Year;
                this.cargarDatosEditar();
                if (this.parametroData.Periodo == añoActual)
                {

                    frmAddParametro frm = new frmAddParametro(conexion, parametroData);
                    frm.Tag = "modificar";
                    DialogResult result = frm.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        ///cargar el datagrid con la información
                        buscarParametros();
                    }

                }
                else

                    MessageBox.Show("Solo puede actualizar el parametro de este año", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        #endregion     
        
        #region instanciar el objeto parametro
        private void cargarDatosEditar()
        {
            this.parametroData = new ParametroData();
            if (dgvDatos.RowCount > 0)
            {
                int nlinea = dgvDatos.CurrentCell.RowIndex;
                parametroData.Id = Convert.ToInt32(this.dgvDatos.Rows[nlinea].Cells["Id"].Value.ToString());
                parametroData.Periodo = Convert.ToInt32(this.dgvDatos.Rows[nlinea].Cells["Periodo"].Value.ToString());
            }
        }
        #endregion


        #region eliminar
        public void eliminar()
        {

            if (dgvDatos.RowCount > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar el Parametro?", "Eliminar Parametro", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (resultado == System.Windows.Forms.DialogResult.Yes)
                {
                    cargarDatosEditar();
                    DateTime fechaActual = DateTime.Now;
                    int mesActual = fechaActual.Month;
                    int añoActual = fechaActual.Year;
                    if (this.parametroData.Periodo == añoActual)
                    {
                        bool elimino = this.bdParametro.eliminarParametro(this.parametroData.Id);
                        //            aumentoData.CodigoCategoria, aumentoData.Fecha);

                        if (elimino)
                        {
                            MessageBox.Show("Parametro eliminado exitosamente", "Eliminar Parametro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.buscarParametros();
                        }
                        else
                        {
                            MessageBox.Show("No se puede eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                        MessageBox.Show("Solo puede eliminar los prestamos de este mes y año", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        #endregion
    }
}
