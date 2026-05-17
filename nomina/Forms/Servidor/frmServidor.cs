using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nomina.Estructuras;
using nomina.Clases.ConexionManager;
using System.Xml.Serialization;
using System.IO;
using nomina.Clases.Utilidades;
namespace nomina.Forms.Servidor
{
    public partial class frmServidor : Form
    {
        ServidorData serverData;
        public frmServidor()
        {
            InitializeComponent();
            Utilidad.configuarForm(this, "Configuración Servidor");
            this.lblTitulo.BackColor = Color.SkyBlue;
            this.btnSave.BackColor = Color.SkyBlue;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            serverData = new ServidorData();
            Registro registryMananger = new Registro();

            serverData.server = txtServer.Text;
            serverData.user = txtUser.Text;
            serverData.password = txtPassword.Text;
            serverData.port = txtPort.Text;
            //serverData.limitedConnection = txtLimited.Text;
            //serverData.database = txtDataBase.Text;

            registryMananger.createRegister(serverData);
        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void txtServer_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }

        private void txtPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilidad.cambiarControlEnter(e);
        }
    }
}
