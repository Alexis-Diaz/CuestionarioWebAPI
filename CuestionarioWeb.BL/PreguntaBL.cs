using System;
using System.Collections.Generic;
using CuestionarioWeb.DAL;
using CuestionarioWeb.EN;

namespace CuestionarioWeb.BL
{
    public class PreguntaBL
    {

        private PreguntaDAL _preguntaDAL;
        public PreguntaBL (Contexto context)
        {
            _preguntaDAL = new PreguntaDAL(context);
        }

        //agregar
        public int GuardarPregunta(Pregunta pregunta)
        {
            if (pregunta != null)
            {
                return _preguntaDAL.GuardarPregunta(pregunta);
            }
            return 0;
        }

        //editar
        public int EditarPregunta(Pregunta pregunta)
        {
            if (pregunta == null)
            {
                return 0;
            }

            Pregunta preguntaEcontrada = _preguntaDAL.BuscarPreguntaPorId(pregunta.IdPregunta);
            if (preguntaEcontrada == null)
            {
                return 0;
            }

            if (preguntaEcontrada.PreguntaFormulada == pregunta.PreguntaFormulada)
            {
                return 0;
            }

            return _preguntaDAL.EditarPregunta(pregunta);
        }

        //cerrar pregunta
        public int CerrarPregunta(int id)
        {
            if (id > 0)
            {
                Pregunta preguntaACerrar = _preguntaDAL.BuscarPreguntaPorId(id);
                if (preguntaACerrar != null)
                {
                    if (preguntaACerrar.Estado == (byte)Pregunta.EstadoDePregunta.Abierta)
                    {
                        preguntaACerrar.Estado = (byte)Pregunta.EstadoDePregunta.Cerrada;
                        return _preguntaDAL.EditarPregunta(preguntaACerrar);
                    }
                }
            }
            return 0;
        }

        //eliminar
        public int EliminarPregunta(int? id)
        {
            if (id == null || id < 0)
            {
                return 0;
            }

            return _preguntaDAL.EliminarPregunta(id);
        }

        //buscar por id
        public Pregunta BuscarPreguntaPorId(int? id)
        {
            if (id < 0)
            {
                return null;
            }
            return _preguntaDAL.BuscarPreguntaPorId(id);
        }

        //listar de mas reciente a mas antiguo
        public List<Pregunta> ListarPreguntasPorMasRecientes()
        {
            List<Pregunta> lista = _preguntaDAL.ListarPreguntas();
            lista.Sort((x, y) => DateTime.Compare(x.Fecha, y.Fecha));//ordenamos de mas reciente
            return lista;
        }
    }
}
