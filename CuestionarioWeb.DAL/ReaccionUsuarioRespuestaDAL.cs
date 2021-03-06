using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CuestionarioWeb.EN;

namespace CuestionarioWeb.DAL
{
    public class ReaccionUsuarioRespuestaDAL
    {
        private Contexto _context { get; set; }
        public ReaccionUsuarioRespuestaDAL(Contexto context)
        {
            _context = context;
        }

        // guardar
        public int GuardarReaccionSobreRespuesta(ReaccionUsuarioRespuesta reaccion)
        {
            if (reaccion != null)
            {
                _context.ReaccionUsuarioRespuestas.Add(reaccion);
                return _context.SaveChanges();
            }
            return 0;
        }
        public List<ReaccionUsuarioRespuesta> ListaReaccion()
        {
            return _context.ReaccionUsuarioRespuestas.ToList();
        }

        //Buscar por cantidad de reaccion especifica
        public List<ReaccionUsuarioRespuesta> ListarRespuestaPorReacciones(int? idRespuesta, int? idReaccion)
        {
            if (idReaccion == null || idReaccion < 0)
            {
                return null;
            }
            return _context.ReaccionUsuarioRespuestas.Where(x => x.IdReaccion == idReaccion && x.IdRespuesta == idRespuesta).ToList();
        }

        //Buscar por cantidad de reacciones
        public List<ReaccionUsuarioRespuesta> ListarRespuestaPorReacciones(int? idRespuesta)
        {
            if (idRespuesta == null || idRespuesta < 0)
            {
                return null;
            }
            return _context.ReaccionUsuarioRespuestas.Where(x => x.IdRespuesta == idRespuesta).ToList();
        }
    }
}
