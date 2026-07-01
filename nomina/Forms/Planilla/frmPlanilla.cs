using nomina.BarraProgreso;
using nomina.Clases.ConexionManager;
using nomina.Clases.GenerarPlanilla;
using nomina.Clases.PermisosUsuario;
using nomina.Clases.Utilidades;
using nomina.Forms.Main;
using nomina.Forms.Reportes;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nomina.Forms.Planilla
{
    public partial class frmPlanilla : Form
    {
        Conexion conexion;
        PlanillaConexion bd;
        bool genero;
        PermisoUsuarioConexion bdPermisos;
        frmMain frmMain;

        public frmPlanilla(Conexion conexion,frmMain frmMain)
        {
            InitializeComponent();
            this.conexion = conexion;
            bd = new PlanillaConexion(conexion);
            bdPermisos = new PermisoUsuarioConexion();
            CargarComboMeses();
            this.frmMain = frmMain;
        }
        #region evento enter
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void btnGenerar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void btnCancelar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }
        #endregion
        private void frmPlanilla_Load(object sender, EventArgs e)
        {
            if (this.Tag == "generar") {
                this.btnGenerar.Text = "Generar";
                
            }   
            
           
         }

        private void CargarComboMeses()
        {
            var Tipo = new[]
            {
              new { Id = 1, Nombre = "Seleccione una opción" },
              new { Id = 2, Nombre = "Mensual" },
              new { Id = 63, Nombre = "Quincenal" },

            };

            cbTipo.DataSource = Tipo;
            cbTipo.DisplayMember = "Nombre";
            cbTipo.ValueMember = "Nombre";
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnGenerar_Click_1(object sender, EventArgs e)
        {
            if (Validar()) {
                frmBarraProgreso frm = new frmBarraProgreso(txtCodigo.Text, dtpFecha.Value, conexion, (this.chkAnticipo.Checked && dtpFecha.Value.Day <= 15 ) && cbTipo.SelectedValue.ToString().ToUpper().Equals("MENSUAL")
                    ? "ANTICIPO": cbTipo.SelectedValue.ToString().ToUpper()
                  );
                frm.tipo = Tipo.GenerarPlanilla;
                frm.ShowDialog();
                if (frm.resultado.Equals(DialogoResultado.Si))
                {
                    MessageBox.Show("Planilla generada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                   
                }
                else { 
                
                }


            }
            
        }

        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipoSeleccionado = cbTipo.SelectedValue.ToString();
            if (tipoSeleccionado.Equals("Mensual"))
            {
                this.chkAnticipo.Visible = true;
            }
            else {
                this.chkAnticipo.Visible = false;
                this.chkAnticipo.Checked = false;
            }
        }

        private bool Validar() {
            if (txtCodigo.Text.Length == 0)
            {
                MessageBox.Show("Escriba un código para la planilla", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
           

              if (cbTipo.SelectedValue.ToString().Equals("Seleccione una opción"))
              {
                        MessageBox.Show("Seleccione un tipo de planilla", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
              }
            return true;
        }

     
    }
}
