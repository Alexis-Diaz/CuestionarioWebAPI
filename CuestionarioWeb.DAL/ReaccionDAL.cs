using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using CuestionarioWeb.EN;

namespace CuestionarioWeb.DAL
{
    class ReaccionDAL
    {
        private Contexto _context { get; set; }

        public ReaccionDAL(Contexto context)
        {
            _context = context;
        }

        //Buscar emoji por codigo 
        public Reaccion BuscarEmojiPorCodigo(int? codigo)
        {
            if (codigo == null || codigo >= 0)
            {
                return null;
            }
            var emoji = _context.Reacciones.FirstOrDefault();
            return emoji;
        }
    }
}
