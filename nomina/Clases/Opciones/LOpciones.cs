using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nomina.Clases.ConexionManager;
using System.Data;

namespace nomina.Clases.Opciones
{
    public class LOpciones
    {
        public string id { set; get; }
        public string descripcion { set; get; }
        public int idEntero { set; get; }
       public LOpciones(string id, string nombre)
        {
            this.descripcion = nombre;
            this.id = id;
        }
        public LOpciones(int id, string nombre)
        {
            this.descripcion = nombre;
            this.idEntero = id;
        }

        public LOpciones()
        {
            
        }
        
    }
}
