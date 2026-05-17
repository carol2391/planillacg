using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nomina.Forms.Empresas;
using nomina.Forms.Main;
using nomina.Clases.ConexionManager;
using nomina.Forms.Usuarios;
using nomina.Clases.EmpresasUsuario;
using nomina.Clases.Usuarios;

namespace nomina.Forms.Usuarios
{
    public partial class frmAsignarEmpresa : Form
    {
        Conexion conexion;
        frmMain frmMain;
        EmpresaUsuarioConexion bdEmpresa;
        UsuarioData usuario;
        List<EmpresaUsuarioData> empresas;
        public frmAsignarEmpresa(Conexion conexion, frmMain frmMain)
        {
            InitializeComponent();
            this.conexion = conexion;
            this.frmMain = frmMain;
            bdEmpresa = new EmpresaUsuarioConexion();
            empresas = new  List<EmpresaUsuarioData>();

        }

        private void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            frmUsuarios frm = new frmUsuarios(conexion, frmMain);
            frm.Tag = "buscar";
            frm.ShowDialog();

            if (DialogResult.OK == frm.DialogResult) {
               this.usuario = frm.user;
                this.lblNombre.Text = usuario.Usuario;
                cargarPermisosData();
            }
            
        }

        public void cargarPermisosData()
        {
            this.dgvPermisos.Enabled = true;
            empresas = bdEmpresa.obtenerEmpresasUsuarios(usuario.UsuarioId);
            this.empresas.Insert(0, new EmpresaUsuarioData(0, "SELECCIONAR TODAS LAS EMPRESAS", 0));

            this.dgvPermisos.DataSource = empresas;
        }
        private void frmAsignarEmpresa_Load(object sender, EventArgs e)
        {

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

        public void desactivarPermisos()
        {
            foreach (DataGridViewRow fila in dgvPermisos.Rows)
            {

                fila.Cells[2].Value = 0;
            }
        }

        private void guardar()
        {
            int f = 0;
            foreach (DataGridViewRow fila in dgvPermisos.Rows)
            {
                if (f != 0)
                {
                    bdEmpresa.insertarEmpresasUSuario(usuario.UsuarioId, Convert.ToInt32(fila.Cells[0].Value.ToString()),
                       Convert.ToInt32(fila.Cells[2].Value.ToString()));

                }
                f++;
            }
            MessageBox.Show("Empresas agregadas a usuario exitosamente", "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

                    if (dgvPermisos.Rows[0].Cells[2].Selected)
                    {
                        for (int fila = 0; fila < dgvPermisos.Rows.Count; fila++)
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
                if (f != 0)
                {
                    this.bdEmpresa.insertarEmpresasUSuario(usuario.UsuarioId, Convert.ToInt32(fila.Cells[0].Value.ToString()),
                    Convert.ToInt32(fila.Cells[2].Value.ToString()));
                    f++;
                }
                MessageBox.Show("Empresas agregadas a usuario exitosamente", "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // desactivarPermisos();

            }
            // MessageBox.Show(fila.Cells[1].Value.ToString() + " activo:" + fila.Cells[2].Value.ToString());

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (resultado == System.Windows.Forms.DialogResult.Yes)
            {
                Dispose();
            }
        }

        private void txtBuscarEmpresa_TextChanged(object sender, EventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(txtBuscarEmpresa.Text.Trim()))
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
                        if (celda.Value.ToString().ToUpper().Contains(txtBuscarEmpresa.Text.ToUpper()))
                        {
                            fila.Visible = true;
                        }

                    }
                }
            }
            else
            {
                dgvPermisos.DataSource = bdEmpresa.obtenerEmpresasUsuarios(usuario.UsuarioId);
            }
        }
    }//fin clase
}
