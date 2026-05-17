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
using nomina.Clases.PermisosUsuario;
using nomina.Forms.Main;
using nomina.Clases.Usuarios;

namespace nomina.Forms.Usuarios
{
    public partial class frmAsignarPermisos : Form
    {
        Conexion conexion;
        PermisoUsuarioConexion bd;
        List<PermisoUsuarioData> permisos;
        frmMain frmMain;
        UsuarioData user;

        public frmAsignarPermisos(Conexion conexion, frmMain frmMain)
        {
            InitializeComponent();
            this.frmMain = frmMain;
            permisos = new List<PermisoUsuarioData> ();
            //this.conexion = conexion;
            bd = new PermisoUsuarioConexion();
            dgvPermisos.Enabled = false;
            //cargarPermisosData();
        }

        public void cargarPermisosData()
        {
            this.dgvPermisos.Enabled = true;
            permisos = bd.obtenerPermisos(user.UsuarioId);
            this.permisos.Insert(0,new PermisoUsuarioData(0, "SELECCIONAR TODOS LOS PERMISOS", 0));

            this.dgvPermisos.DataSource = permisos;
        }

        private void txtBuscarPermiso_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtBuscarPermiso.Text.Trim()))
            {

                dgvPermisos.CurrentCell = null;

                foreach (DataGridViewRow fila in dgvPermisos.Rows)
                {
                    fila.Visible = false;
                }

                foreach (DataGridViewRow fila in dgvPermisos.Rows)
                {
                    foreach (DataGridViewCell celda in fila.Cells)
                    {
                        if (celda.Value.ToString().ToUpper().Contains(txtBuscarPermiso.Text.ToUpper()))
                        {
                            fila.Visible = true;
                        }

                    }
                }
            }
            else
            {
                dgvPermisos.DataSource = permisos;
            }


        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(lblNombre.Text))
            {
                guardar();
            }
            else
            {
                MessageBox.Show("Seleccione un usuario", "Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void guardar()
        {
            int f = 0;
            foreach (DataGridViewRow fila in dgvPermisos.Rows)
            {
                if (f != 0) {
                    bd.insertarPermisos(user.UsuarioId, Convert.ToInt32(fila.Cells[0].Value.ToString()),
                       Convert.ToInt32(fila.Cells[2].Value.ToString()));
                   
                }
                f++;
            }
            MessageBox.Show("Permisos agregados exitosamente", "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Information);
         
        }

        private void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            frmUsuarios frm = new frmUsuarios(conexion, frmMain);
            frm.Tag = "buscar";
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {
                this.user = frm.user;
                this.lblNombre.Text = user.Usuario;
                cargarPermisosData();
            }
        }

        public void desactivarPermisos()
        {
            foreach (DataGridViewRow fila in dgvPermisos.Rows)
            {

                fila.Cells[2].Value = 0;
            }
        }

        private void dgvPermisos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)//set your checkbox column index instead of 2
            {   //When you check
                if (Convert.ToBoolean(dgvPermisos.Rows[e.RowIndex].Cells[2].EditedFormattedValue) == true)
                {
                    //EXAMPLE OF OTHER CODE
                   // dgvPermisos.Rows[e.RowIndex].Cells[5].Value = DateTime.Now.ToShortDateString();

                    //SET BY CODE THE CHECK BOX
                    dgvPermisos.Rows[e.RowIndex].Cells[2].Value = 1;

                    if (dgvPermisos.Rows[0].Cells[2].Selected) {
                        for ( int fila =0; fila<dgvPermisos.Rows.Count; fila++)
                        {
                            dgvPermisos.Rows[fila].Cells[2].Value = 1;
                        }
                    }
                }
                else //When you decheck
                {
                    //dgvPermisos.Rows[e.RowIndex].Cells[5].Value = String.Empty;

                    //SET BY CODE THE CHECK BOX
                    dgvPermisos.Rows[e.RowIndex].Cells[2].Value = 0;

                    if (dgvPermisos.Rows[0].Cells[2].Selected)
                    {
                        for (int fila = 0; fila < dgvPermisos.Rows.Count; fila++)
                        {
                            dgvPermisos.Rows[fila].Cells[2].Value = 0;
                        }
                    }
                }
            }
        }

        private void dgvPermisos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void seleccionarTodosPermisos()
        {
            int f = 0;
            foreach (DataGridViewRow fila in dgvPermisos.Rows)
            {
                if (f!=0)
                {
                   bd.insertarPermisos(user.UsuarioId, Convert.ToInt32(fila.Cells[0].Value.ToString()),
                   Convert.ToInt32(fila.Cells[2].Value.ToString()));
                    f++;
                 }
            MessageBox.Show("Permisos agregados exitosamente", "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // desactivarPermisos();

        }
        // MessageBox.Show(fila.Cells[1].Value.ToString() + " activo:" + fila.Cells[2].Value.ToString());

    }
    }
}
