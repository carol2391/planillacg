using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nomina.Clases.Categoria;
using nomina.Clases.ConexionManager;
using nomina.Forms.Main;
using nomina.Clases.Utilidades;
using nomina.Clases.PermisosUsuario;
using nomina.Clases.UsuarioPermisos;



namespace nomina.Forms.Categoria
{
    public partial class frmCategoria : Form
    {
        #region propiedades
        Conexion conexion;
        frmMain frmMain;
        CategoriaData categoria;
        List<CategoriaData> categorias = new List<CategoriaData>();
        CategoriaConexion bdCategoria;
        PermisoUsuarioConexion bdPermisos;
        
        #endregion
        public frmCategoria(Conexion conexion, frmMain frmMain, List<CategoriaData> categorias)
        {
            InitializeComponent();
            Utilidad.configuarForm(this, "Categorías");
            Utilidad.configurarDataGrid(dgvCategorias);
            this.frmMain = frmMain;
            this.conexion = conexion;
            bdPermisos = new PermisoUsuarioConexion();
            bdCategoria = new CategoriaConexion(conexion);
            dgvCategorias.DataSource = bdCategoria.obtenerCategorias();
            txtCodigo.Visible= false;
            txtNombre.Visible= false;
            panel1.BackColor = Color.SkyBlue;
            btnModificar.BackColor = btnNuevo.BackColor = bntQuitar.BackColor = btnSalir.BackColor = Color.SkyBlue;
          

        }
        #region eventos

        private void frmCategoria_Load(object sender, EventArgs e)
        {
            
        }
        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            if (bdPermisos.existePermiso(this.frmMain.usuarioId, 2,2))
            {
                frmAddCategoria frm = new frmAddCategoria(conexion);
                frm.Tag = "agregar";
                frm.ShowDialog();
                dgvCategorias.DataSource = bdCategoria.obtenerCategorias();
               if (frm.DialogResult == DialogResult.OK) { 
                   dgvCategorias.DataSource = bdCategoria.obtenerCategorias();
               }
            }
            else
                btnNuevo.Visible = false;
            }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
                if (dgvCategorias.RowCount > 0)
                {
                    this.cargarDatosEditar();
                    frmAddCategoria frm = new frmAddCategoria(conexion, categoria);
                    frm.Tag = "modificar";
                    DialogResult result = frm.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                      dgvCategorias.DataSource = bdCategoria.obtenerCategorias();
                    }
                }

        }
        private void BntQuitar_Click(object sender, EventArgs e)
        {
                if (dgvCategorias.RowCount > 0)
                {
                    DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar esta categoria?", "Eliminar categoria", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (resultado == System.Windows.Forms.DialogResult.Yes)
                    {
                        CategoriaConexion catConexion = new CategoriaConexion(conexion);
                        int nlinea = dgvCategorias.CurrentCell.RowIndex;
                        int id = Convert.ToInt32(this.dgvCategorias.Rows[nlinea].Cells["Id"].Value.ToString());
                        string codigo = this.dgvCategorias.Rows[nlinea].Cells["CodigoCategoria"].Value.ToString();
                        CategoriaData d = new CategoriaData();
                        //if (d.empleados.Count == 0)
                        //{
                        if (catConexion.accionesCategoria("E",id,"","",(decimal)0.00,(decimal)0.00))
                        {
                            dgvCategorias.DataSource = bdCategoria.obtenerCategorias();
                            MessageBox.Show("Categoria eliminada exitosamente", "Eliminar categoria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("No se puede eliminar, porque tiene movimientos", "Eliminar categoria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //}
                    }
                }
        }


        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                dgvCategorias.DataSource = bdCategoria.buscarCategoria(txtCodigo.Text.Trim(), "COD_CAT");
                
            }
            else {
                dgvCategorias.DataSource = bdCategoria.obtenerCategorias();
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtNombre.Text))
            {
                dgvCategorias.DataSource = bdCategoria.buscarCategoria(txtNombre.Text.Trim(), "NOM_CAT");
            }
            else
            {
                dgvCategorias.DataSource = bdCategoria.obtenerCategorias();
            }
        }

        private void rbCodigo_Click(object sender, EventArgs e)
        {
            
            txtCodigo.Visible = true;
            txtNombre.Visible = false;
            txtNombre.Text=txtCodigo.Text = "";
            txtCodigo.Select();
        }

        private void RbNombre_Click(object sender, EventArgs e)
        {
            
            txtCodigo.Visible = false;
            txtNombre.Visible = true;
            txtNombre.Text = txtCodigo.Text = "";
            txtNombre.Select();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea salir?", "Salir", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (resultado == System.Windows.Forms.DialogResult.Yes)
            {
                Dispose();
            }
        }
        #endregion

        # region Actualizar objetos en el formulario principal y en los controles


        private void actualizarObjetos()
        {
            CategoriaConexion bd = new CategoriaConexion(conexion);
            this.categorias = bd.obtenerCategorias();
        }

        public void refrescarControles()
        {
            dgvCategorias.DataSource = this.categorias;
        }

        #endregion

        #region instanciar el objeto categoria con la fila seleccionada del datagrid
        private void cargarDatosEditar()
        {
            this.categoria = new CategoriaData();
            if (dgvCategorias.RowCount > 0)
            {
                int nlinea = dgvCategorias.CurrentCell.RowIndex;
                categoria.CodigoCategoria = this.dgvCategorias.Rows[nlinea].Cells["CodigoCategoria"].Value.ToString();
                categoria.NombreCategoria = this.dgvCategorias.Rows[nlinea].Cells["NombreCategoria"].Value.ToString();
                categoria.SalarioInicial= Convert.ToDecimal(this.dgvCategorias.Rows[nlinea].Cells["SalarioInicial"].Value.ToString());
                categoria.SalarioFinal = Convert.ToDecimal(this.dgvCategorias.Rows[nlinea].Cells["SalarioFinal"].Value.ToString());
                categoria.Id = Convert.ToInt32(this.dgvCategorias.Rows[nlinea].Cells["Id"].Value.ToString());
            }

        }
        #endregion

        private void DgvCategorias_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cargarDatosEditar();
            frmAddCategoria frm = new frmAddCategoria(conexion, categoria);
            frm.Tag = "ver";
            frm.bloquearControles();
            frm.ShowDialog();
        }

        private void frmCategoria_Shown(object sender, EventArgs e)
        {
            
            Validator.validarPermisos( frmMain.usuarioId, btnNuevo, btnModificar, bntQuitar, btnSalir, this,5);
        }
    }
}
