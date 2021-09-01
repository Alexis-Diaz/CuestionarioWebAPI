using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using CuestionarioWeb.EN;

namespace CuestionarioWeb.DAL
{
    public class RespuestaDAL
    {
        private Contexto _context { get; set; }
        public RespuestaDAL (Contexto context)
        {
            _context = context;
        }

        //agregar
        public int GuardarRespuesta(Respuesta respuesta)
        {
            if (respuesta != null)
            {
                _context.Respuestas.Add(respuesta);
                return _context.SaveChanges();
            }
            return 0;
        }

        //editar
        //no se puede editar una respuesta

        //eliminar
        public int EliminarRespuesta(int? id)
        {
            if (id == null || id > 0)
            {
                return 0;
            }
            Respuesta respuestaEncontrada = _context.Respuestas.Find(id);
            if (respuestaEncontrada != null)
            {
                _context.Respuestas.Remove(respuestaEncontrada);
                return _context.SaveChanges();
            }
            return 0;
        }

        //buscar por id
        public Respuesta BuscarRespuestaPorId(int? id)
        {
            if (id == null || id < 0)
            {
                return null;
            }
            return _context.Respuestas.Find(id);
        }

        //listar
        public List<Respuesta> ListarRespuestas()
        {
            return _context.Respuestas.ToList();
        }
    }
}
