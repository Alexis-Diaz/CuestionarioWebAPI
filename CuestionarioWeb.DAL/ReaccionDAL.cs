using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using CuestionarioWeb.EN;

namespace CuestionarioWeb.DAL
{
    public class ReaccionDAL
    {
        private Contexto _context { get; set; }

        public ReaccionDAL(Contexto context)
        {
            _context = context;
        }

        //Buscar emoji por codigo 
        public Reaccion BuscarEmojiPorCodigo(string codigo)
        {
            if (string.IsNullOrEmpty(codigo))
            {
                return null;
            }
          
            Reaccion emoji = _context.Reacciones.Where(x=>x.TipoReaccion == codigo).FirstOrDefault();
            return emoji;
        }
    }
}
