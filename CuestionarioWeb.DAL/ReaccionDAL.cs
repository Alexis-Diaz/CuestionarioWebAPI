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

        public Reaccion BuscarEmojiPorId(int? id)
        {
            if (id != null && id > 0)
            {
                Reaccion emoji = _context.Reacciones.Find(id);
                return emoji;
            }
            return null;
           
        }

        public List<Reaccion> ListarTodasLasReacciones()
        {
            try
            {
                return _context.Reacciones.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
