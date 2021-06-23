using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Movimiento
    {
        public Usuario Emisor { get; set; }
        public Usuario Receptor { get; set; }
        public string Numero { get; set; }
        public DateTime Dia { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }


    }
}
