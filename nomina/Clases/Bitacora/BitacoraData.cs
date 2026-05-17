using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nomina.Clases.Bitacora
{

    public class BitacoraData
    {
        public int idCampo { set; get; }
        public string NombreCampo { set; get; }
        public DateTime Fecha { set; get; }
        public string valorNuevo { set; get; }
        public string vaLorAntiguo { set; get; }
        public string Accion { set; get; }
        public string Modulo { set; get; }
        public string usuario { set; get; }


        public BitacoraData(DateTime Fecha, string usuario, string modulo, string accion,
            int idCampo, string Nombre,
             string valorAntiguo, string valorNuevo)
        {
            this.idCampo = idCampo;
            this.NombreCampo = Nombre;
            this.Fecha = Fecha;
            this.valorNuevo = valorNuevo;
            this.vaLorAntiguo = valorAntiguo;
            this.Accion = accion;
            this.Modulo = modulo;
            this.usuario = usuario;
        }
    }
}
