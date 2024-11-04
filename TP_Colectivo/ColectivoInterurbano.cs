using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Colectivo
{
    public class ColectivoInterurbano : Colectivo
    {
        public ColectivoInterurbano(string linea) : base(linea)
        {
            tarifa = 2500;
        }
    }
}
