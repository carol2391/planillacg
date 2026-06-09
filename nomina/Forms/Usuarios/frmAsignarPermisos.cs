using MySql.Data.MySqlClient;
using nomina.Clases.ConexionManager;
using nomina.Clases.PermisosUsuario;
using nomina.Clases.Usuarios;
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
            ConfigurarGridPermisos();
        }

        private void ConfigurarGridPermisos()
        {
            dgvPermisos.Columns.Clear();
            dgvPermisos.AutoGenerateColumns = false;
            dgvPermisos.AllowUserToAddRows = false;
            dgvPermisos.SelectionMode = DataGridViewSelectionMode.CellSelect;

            // 1. ID Módulo (Oculto, indispensable para guardar)
            dgvPermisos.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "IdModulo", DataPropertyName = "IdModulo", Visible = false });

            // 2. Columna Nombre del Módulo (Texto Fijo)
            dgvPermisos.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "Modulo", DataPropertyName = "Modulo", HeaderText = "Módulo / Pantalla", ReadOnly = true, Width = 220 });

            // 3. Columnas de Acciones (Todas son CheckBoxes)
            dgvPermisos.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Ver", DataPropertyName = "Ver", HeaderText = "VER", Width = 70 });
            dgvPermisos.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Nuevo", DataPropertyName = "Nuevo", HeaderText = "NUEVO", Width = 70 });
            dgvPermisos.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Modificar", DataPropertyName = "Modificar", HeaderText = "MODIFICAR", Width = 90 });
            dgvPermisos.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Eliminar", DataPropertyName = "Eliminar", HeaderText = "ELIMINAR", Width = 85 });
            dgvPermisos.Columns.Add(new DataGridViewCheckBoxColumn { Name = "VerAntecedentes", DataPropertyName = "VerAntecedentes", HeaderText = "VER ANTECEDENTES", Width = 140 });
            dgvPermisos.Columns.Add(new DataGridViewCheckBoxColumn { Name = "VerHistorialAumento", DataPropertyName = "VerHistorialAumento", HeaderText = "VER HISTORIAL AUMENTO", Width = 140 });
        }
        public void cargarPermisosData()
        {
            this.dgvPermisos.Enabled = true;
            
           bd.CargarMatrizPermisos(this.user.UsuarioId, this.dgvPermisos);
        }

        private void txtBuscarPermiso_TextChanged(object sender, EventArgs e)
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


        private void guardar()
        {
            // 1. Forzamos el cierre de cualquier edición para asegurar que se guarden los últimos clics
            dgvPermisos.EndEdit();

             // 3. Recorremos el DataGridView fila por fila (Módulo por Módulo)
            foreach (DataGridViewRow fila in dgvPermisos.Rows)
            {
                // Convertimos la fila del grid directamente a tu objeto de C#
                if (fila.DataBoundItem is ModuloPermisoRow filaPermiso)
                {
                    int idModulo = filaPermiso.IdModulo;

                    // Mandamos a insertar cada una de las 4 acciones básicas convirtiendo el bool a (1 o 0)
                    bd.insertarPermisos(user.UsuarioId, idModulo, 1, filaPermiso.Ver ? 1 : 0);
                    bd.insertarPermisos(user.UsuarioId, idModulo, 2, filaPermiso.Nuevo ? 1 : 0);
                    bd.insertarPermisos(user.UsuarioId, idModulo, 3, filaPermiso.Modificar ? 1 : 0);
                    bd.insertarPermisos(user.UsuarioId, idModulo, 4, filaPermiso.Eliminar ? 1 : 0);

                    // Regla especial: "VER ANTECEDENTES" solo se procesa si es el módulo de Empleados
                    if (filaPermiso.Modulo.ToUpper().Trim() == "EMPLEADO")
                    {
                        bd.insertarPermisos(user.UsuarioId, idModulo, 5, filaPermiso.VerAntecedentes ? 1 : 0);
                        bd.insertarPermisos(user.UsuarioId, idModulo, 6, filaPermiso.VerHistorialAumento ? 1 : 0);
                    }
                }
            }

            MessageBox.Show("Permisos actualizados exitosamente de forma masiva.", "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        }

        private void dgvPermisos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void dgvPermisos_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 1. Validamos que estemos sobre celdas de datos válidas (ignoramos cabeceras)
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // 2. Validamos que la fila tenga un objeto enlazado correcto
                if (dgvPermisos.Rows[e.RowIndex].DataBoundItem is ModuloPermisoRow filaPermiso)
                {
                    string nombreColumna = dgvPermisos.Columns[e.ColumnIndex].Name;
                    string moduloActual = filaPermiso.Modulo.ToUpper().Trim();
                    bool ocultarCelda = false;

                    // REGLA 1 CORREGIDA: Si es "ASIGNAR PERMISOS", ocultamos todo EXCEPTO "Modificar" y "Modulo"
                    if (moduloActual == "Asignar Permisos" &&
                        nombreColumna != "Modificar" &&
                        nombreColumna != "Modulo" &&
                        nombreColumna != "IdModulo") // Añadimos las columnas que NO deben borrarse
                    {
                        ocultarCelda = true;
                    }

                    // REGLA 2: VerAntecedentes SOLO se ve en "EMPLEADO"
                    if (nombreColumna == "VerAntecedentes" && moduloActual != "EMPLEADO")
                    {
                        ocultarCelda = true;
                    }

                    // REGLA 3: VerHistorialAumento SOLO se ve en "EMPLEADO"
                    if (nombreColumna == "VerHistorialAumento" && moduloActual != "EMPLEADO")
                    {
                        ocultarCelda = true;
                    }

                    // 3. Si la celda cumple con alguna regla, la pintamos vacía
                    if (ocultarCelda)
                    {
                        // Dibuja el fondo normal de la celda
                        e.PaintBackground(e.CellBounds, true);

                        // Le indicamos a Windows Forms que ya manejamos el dibujo (así omite dibujar el Checkbox o Texto)
                        e.Handled = true;
                    }
                }
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                base.Dispose();
            }
        }
    }
    }


