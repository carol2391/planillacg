using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nomina.Clases.Departamento
{
    public class Departamento
    {
        public string getConnetionString()
        {
           return "datasource=127.0.0.1;port=3306;username=root;password=;database=paises;";
        }
    }
}
