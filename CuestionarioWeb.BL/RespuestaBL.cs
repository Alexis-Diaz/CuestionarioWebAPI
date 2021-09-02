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
                Pregunta EsPreguntaCerrada = _preguntaDAL.BuscarPreguntaPorId(respuesta.IdPregunta);
                if(EsPreguntaCerrada.Estado == 0)
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
        public List<Respuesta> ListarRespuestasPorPregunta(int? idPregunta)
        {
            var lista = _respuestaDAL.ListarRespuestas();
            lista = lista.Where(x => x.IdPregunta == idPregunta).ToList();
            lista.Sort((x, y) => DateTime.Compare(x.Fecha, y.Fecha));
            return lista;
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
                        item.cantidadDeReacciones = _reaccionUsuarioRespuesta.ListarReaccionPorEmoji(item.IdRespuesta, idReaccion).Count();
                    }
                    //ordenamos de mayor a menor reacciones
                    lista.OrderByDescending(x => x.cantidadDeReacciones);
                    return lista;
                }
            }
            return null;
        }

        public int ReaccionarSobreRespuesta(int? idRespuesta, int? codigoEmoji, int? idUsuario)
        {
            Reaccion emoji = new Reaccion();
            if (idRespuesta == null || idRespuesta < 0 || codigoEmoji == null || codigoEmoji <= 0)
            {
                return 0;
            }
            switch (codigoEmoji)
            {
                case 1:
                    emoji = _reaccionDAL.BuscarEmojiPorCodigo("&#1F44D;");
                    break;
                case 2:
                    emoji = _reaccionDAL.BuscarEmojiPorCodigo("&#1F44E;");
                    break;
                case 3:
                    emoji = _reaccionDAL.BuscarEmojiPorCodigo("&#1F497;");
                    break;
                case 4:
                    emoji = _reaccionDAL.BuscarEmojiPorCodigo("&#1F62E;");
                    break;
                case 5:
                    emoji = _reaccionDAL.BuscarEmojiPorCodigo("&#1F624;");
                    break;
                case 6:
                    emoji = _reaccionDAL.BuscarEmojiPorCodigo("&#1F60D;");
                    break;
                case 7:
                    emoji = _reaccionDAL.BuscarEmojiPorCodigo("&#1F602;");
                    break;
            }
        
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
    }
}
