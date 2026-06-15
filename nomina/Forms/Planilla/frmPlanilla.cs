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
using nomina.Clases.ConexionManager;
using nomina.Clases.GenerarPlanilla;
using nomina.Clases.Utilidades;
using nomina.Forms.Reportes;
using nomina.Forms.Main;
using nomina.Clases.PermisosUsuario;

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
            else
            {
                this.btnGenerar.Text = "Ver";
               
             
            }
           
            }
               
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            //backgroundWorker1.RunWorkerAsync();
            /*VER PLANILLA*/
            if (this.Tag != "generar")
            {
                

                frmReportePlanilla frm = new frmReportePlanilla(conexion, this.txtCodigo.Text, this.dtpFecha.Value.Date);
                frm.ShowDialog();
            }

            if (this.Tag == "generar")
            {
                this.btnGenerar.Enabled = false;
               
                backgroundWorker1.RunWorkerAsync();
                timer1.Start();
        }

        //backgroundWorker1.RunWorkerAsync(new { Foo = "Foo", Bar = 42 });
        //    if (this.Tag == "generar")
        //        generar();
        //    else
        //    {
        //        frmReportePlanilla frm = new frmReportePlanilla(conexion, this.txtCodigo.Text, this.dtpFecha.Value.Date);
        //        frm.ShowDialog();

        //    }
    }
        private void generar(){

            //bool existePlanilla = bd.existePlanilla(this.txtCodigo.Text, this.dtpFecha.Value.Date);
            //if (!existePlanilla)
            //{
                string genero = bd.generarPlanilla(this.txtCodigo.Text, this.dtpFecha.Value.Date);
            if (genero == "0")
            {
                MessageBox.Show("Planilla  generada exitosamente", "Generar Planilla", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //genero = true;
                // MessageBox.Show("Planilla  generada exitosamente", "Generar Planilla", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.pgBarra.Enabled = true;
            }
                else {
                //genero = false;
                //MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //this.pgBarra.Enabled = true;
                MessageBox.Show(genero, "Generar Planilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ////}
            ////else
            ////{
            ////    genero = false;
            ////   // MessageBox.Show("Ya existe una planilla generada con ese código y fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////    this.pgBarra.Enabled = true;
            ////}
        }

       

    
        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            // bd.ExportProgres += generarNomina_ExportProgres;
                generar();

        }


        private void generarNomina_ExportProgres(object sender, EventArgs e)
        {

           

           
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //btn_Submit.Enabled = true; //enable button
            //timer1.Stop();
            //pgBarra.Value = 0;
            //pgBarra.Visible = true;

            //pgBarra.Increment(1);
            //lblProgreso.Text = pgBarra.Value.ToString()+"%";
            //timer1.Enabled = true;
            
            //if (pgBarra.Value == pgBarra.Maximum)
            //{
            //    timer1.Stop();
            //    pgBarra.Value = 0;
            //    lblProgreso.Text =  0.ToString() + "%";

            //    if (genero && this.Tag == "generar")
            //        MessageBox.Show("Planilla  generada exitosamente", "Generar Planilla", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    else
            //        if (!genero && this.Tag == "generar")
            //        MessageBox.Show("Ya existe una planilla generada con ese código y fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    return;
            //}

            //pgBarra.PerformStep();
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //pgBarra.Value = e.ProgressPercentage;
        }

        private void Timer1_Tick_1(object sender, EventArgs e)
        {
           
        }

        private void LblProgreso_Click(object sender, EventArgs e)
        {

        }
    }
}
