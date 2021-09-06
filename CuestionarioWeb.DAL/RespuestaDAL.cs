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
            try
            {
                if (respuesta != null)
                {
                    respuesta.FechaDeRespuesta = DateTime.Now;
                    _context.Respuestas.Add(respuesta);
                    return _context.SaveChanges();
                }
                return 0;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        //editar
        //no se puede editar una respuesta

        //eliminar
        public int EliminarRespuesta(int? id)
        {
            try
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
            catch (Exception)
            {

                return 0;
            }
        }

        //buscar por id
        public Respuesta BuscarRespuestaPorId(int? id)
        {
            try
            {
                if (id == null || id < 0)
                {
                    return null;
                }
                return _context.Respuestas.Find(id);
            }
            catch (Exception)
            {

                return null;
            }
        }

        //listar
        public List<Respuesta> ListarRespuestas()
        {
            try
            {
                return _context.Respuestas.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        //listar
        public List<ReaccionUsuarioRespuesta> ListarReaccionesPorRespuestas(int? idRespuesta)
        {
            try
            {
                return _context.ReaccionUsuarioRespuestas.Where(x => x.IdRespuesta == idRespuesta).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        //listar
        public List<Respuesta> ListarComentariosPorRespuesta (int? idRespuesta)
        {
            try
            {
                return _context.Respuestas.Where(x => x.AutoReferencia == idRespuesta).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
