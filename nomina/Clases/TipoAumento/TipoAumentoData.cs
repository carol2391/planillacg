using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nomina.Clases.TipoAumento
{
    public class TipoAumentoData
    {
        public TipoAumentoData(int id, string desc) {
            this.Id = id;
            this.Descripcion = desc;
        }
        public int Id { set; get; }
        public string Descripcion { set; get; }
    }
}
