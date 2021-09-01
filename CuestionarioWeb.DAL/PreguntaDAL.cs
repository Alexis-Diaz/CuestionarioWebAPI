using System;
using System.Collections.Generic;
using System.Linq;
//
using CuestionarioWeb.EN;
using Microsoft.EntityFrameworkCore;

namespace CuestionarioWeb.DAL
{
    public class PreguntaDAL
    {
        private Contexto _context { get; set; }
        public PreguntaDAL(Contexto context)
        {
            _context = context;
        }

        //agregar
        public int GuardarPregunta(Pregunta pregunta)
        {
            if (pregunta != null)
            {
                _context.Preguntas.Add(pregunta);
                return _context.SaveChanges();
            }
            return 0;
        }
        //editar
        public int EditarPregunta(Pregunta pregunta)
        {
            if (pregunta != null)
            {
                if (pregunta.IdPregunta > 0)
                {
                    Pregunta preguntaEncontrada = _context.Preguntas.Find(pregunta.IdPregunta);
                    if (preguntaEncontrada != null)
                    {
                        preguntaEncontrada.PreguntaFormulada = pregunta.PreguntaFormulada;
                        preguntaEncontrada.Estado = pregunta.Estado;
                        _context.Entry(preguntaEncontrada).State = EntityState.Modified;
                        return _context.SaveChanges();
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
            Pregunta preguntaEncontrada = _context.Preguntas.Find(id);
            if (preguntaEncontrada != null)
            {
                _context.Preguntas.Remove(preguntaEncontrada);
                return _context.SaveChanges();
            }
            return 0;
        }
        //buscar por id
        public Pregunta BuscarPreguntaPorId(int? id)
        {
            if (id == null || id < 0)
            {
                return null;
            }
            return _context.Preguntas.Find(id);
        }
        //listar
        public List<Pregunta> ListarPreguntas()
        {
            return _context.Preguntas.ToList();
        }

        //listar preguntas por estado
        public List<Pregunta> ListarPreguntasPorEstado(byte estado)
        {
            if (estado > 0)
            {
                return _context.Preguntas.Where(x => x.Estado == estado).ToList();
            }
            return null;
        }
    }
}
