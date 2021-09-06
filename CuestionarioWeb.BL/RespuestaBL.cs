using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using CuestionarioWeb.DAL;
using CuestionarioWeb.EN;

namespace CuestionarioWeb.BL
{
    public class RespuestaBL
    {
        private RespuestaDAL _respuestaDAL { get; set; }
        private PreguntaDAL _preguntaDAL { get; set; }
        private ReaccionUsuarioRespuestaDAL _reaccionUsuarioRespuesta { get; set; }
        private ReaccionDAL _reaccionDAL { get; set; }

        public RespuestaBL(Contexto context)
        {
            _respuestaDAL = new RespuestaDAL(context);
            _preguntaDAL = new PreguntaDAL(context);
            _reaccionUsuarioRespuesta = new ReaccionUsuarioRespuestaDAL(context);
            _reaccionDAL = new ReaccionDAL(context);
        }

        //responder preguntas y a respuestas
        public int NuevaRespuesta(Respuesta respuesta)
        {
            //Se responde una pregunta
            
            if (respuesta != null)
            {
                if(respuesta.RespuestaEmitida != "")
                {
                    Pregunta EsPreguntaCerrada = _preguntaDAL.BuscarPreguntaPorId(respuesta.IdPregunta);
                    if (EsPreguntaCerrada.Estado == 0)
                    {
                        return 2;//pregunta cerrada
                    }

                    //se responde a una respuesta
                    if (respuesta.AutoReferencia > 0)
                    {
                        Respuesta respuestaEncontrada = _respuestaDAL.BuscarRespuestaPorId(respuesta.AutoReferencia);
                        if (respuestaEncontrada != null)
                        {
                            return _respuestaDAL.GuardarRespuesta(respuesta);
                        }
                    }
                    if (respuesta.IdPregunta > 0)
                    {
                        Pregunta preguntaExistente = _preguntaDAL.BuscarPreguntaPorId(respuesta.IdPregunta);
                        if (preguntaExistente != null)
                        {
                            return _respuestaDAL.GuardarRespuesta(respuesta);
                        }
                    }
                }
            }
            return 0;
        }

        //editar
        //no se puede editar

        //eliminar
        public int EliminarRespuesta(int? id)
        {
            if (id == null || id > 0)
            {
                return 0;
            }
            return _respuestaDAL.EliminarRespuesta(id);
        }

        //listar segun pregunta ordenada de la mas reciente a mas antigua
        //no incluye respuestas sobre respuestas
        public List<Respuesta> ListarRespuestasPorPregunta(int? idPregunta)
        {
            var lista = _respuestaDAL.ListarRespuestas();
            lista = lista.Where(x => x.IdPregunta == idPregunta && x.AutoReferencia == 0).ToList();
            lista.Sort((x, y) => DateTime.Compare(y.FechaDeRespuesta, x.FechaDeRespuesta));
            return lista;
        }

        public List<List<Respuesta>>ListasDeRespuestasOrdenadasPorPreguntas()
        {
            try
            {
                List<List<Respuesta>> Lista = new List<List<Respuesta>>();
                var listaDePreguntas = _preguntaDAL.ListarPreguntas();
                foreach (var pregunta in listaDePreguntas)
                {
                    var lista = ListarRespuestasPorPregunta(pregunta.IdPregunta);
                    Lista.Add(lista);
                }
                return Lista;
            }
            catch (Exception)
            {

                return null;
            }
        }

        //por like
        /// <summary>
        /// Lista las respuestas por reaccion.
        /// Parametros:
        /// Param 1=Id de la pregunta
        /// Parm 2=Codigo de la reaccion
        /// Param 3 = 
        /// </summary>
        /// <param name="codigoReaccion"></param>
        /// <returns>La lista filtrada por reacciones de mas a menos votadas</returns>
        public List<Respuesta> ListarRespuestasPorReaccion(int? IdPregunta, int? idReaccion)
        {
            if (IdPregunta != null || IdPregunta > 0)
            {
                Pregunta pregunta = _preguntaDAL.BuscarPreguntaPorId(IdPregunta);
                if (pregunta != null)
                {
                    List<Respuesta> lista = _respuestaDAL.ListarRespuestas();
                    //trae las respuesta de una pregunta sin incluir respueta de respuesta sobre la misma pregunta
                    lista = lista.Where(x => x.IdPregunta == IdPregunta && x.AutoReferencia == 0).ToList();

                    //filtramos la lista por reaccion y capturamos la cantidad
                    foreach (var item in lista)
                    {
                        item.cantidadDeReacciones = _reaccionUsuarioRespuesta.ListarRespuestaPorReacciones(item.IdRespuesta, idReaccion).Count();
                    }
                    //ordenamos de mayor a menor reacciones
                    lista.OrderByDescending(x => x.cantidadDeReacciones);
                    return lista;
                }
            }
            return null;
        }

        public List<Respuesta> ListarCatidadReaccionesPorRespuesta(int? IdPregunta)
        {
            if (IdPregunta != null || IdPregunta > 0)
            {
                Pregunta pregunta = _preguntaDAL.BuscarPreguntaPorId(IdPregunta);
                if (pregunta != null)
                {
                    List<Respuesta> lista = _respuestaDAL.ListarRespuestas();
                    //trae las respuesta de una pregunta sin incluir respueta de respuesta sobre la misma pregunta
                    lista = lista.Where(x => x.IdPregunta == IdPregunta && x.AutoReferencia == 0).ToList();

                    //filtramos la lista por reaccion y capturamos la cantidad
                    foreach (var item in lista)
                    {
                        item.cantidadDeReacciones = _reaccionUsuarioRespuesta.ListarRespuestaPorReacciones(item.IdRespuesta).Count();
                    }
                    //ordenamos de mayor a menor reacciones
                    lista = lista.OrderByDescending(x => x.cantidadDeReacciones).ToList();
                    return lista;
                }
            }
            return null;
        }

        public int ReaccionarSobreRespuesta(int? idRespuesta, int? idEmoji, int? idUsuario)
        {
            Reaccion emoji = new Reaccion();
            if (idRespuesta == null || idRespuesta < 0 || idEmoji == null || idEmoji <= 0)
            {
                return 0;
            }

            emoji = _reaccionDAL.BuscarEmojiPorId(idEmoji);
        
            if (emoji != null)
            {
                if (idRespuesta != null || idRespuesta > 0)
                {
                    ReaccionUsuarioRespuesta reaccion = new ReaccionUsuarioRespuesta();
                    reaccion.IdRespuesta = (int)idRespuesta;
                    reaccion.IdReaccion = emoji.IdReaccion;
                    reaccion.IdUsuario = (int)idUsuario;
                    return _reaccionUsuarioRespuesta.GuardarReaccionSobreRespuesta(reaccion);
                }
            }
            return 0;
        }

        public Respuesta BuscarRespuestaPorId(int? id)
        {
            if(id==null || id < 0)
            {
                return null;
            }
            return _respuestaDAL.BuscarRespuestaPorId(id);
        }

        public List<Reaccion> ListarTodasLasReacciones()
        {
            return _reaccionDAL.ListarTodasLasReacciones();
        }

        public List<ReaccionUsuarioRespuesta> ListarTodasLasReaccionesPorRespuesta(int? idRespuesta)
        {
            return _respuestaDAL.ListarReaccionesPorRespuestas(idRespuesta);
        }

        public List<Respuesta> ListarLosComentariosPorRespuesta(int? idRespuesta)
        {
            if(idRespuesta != null || idRespuesta > 0)
            {
                List<Respuesta> lista=_respuestaDAL.ListarComentariosPorRespuesta(idRespuesta);
                lista = lista.OrderByDescending(x => x.FechaDeRespuesta).ToList();
                return lista;
            }
            return null;
        }
    }
}
