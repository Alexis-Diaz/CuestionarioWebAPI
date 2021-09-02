using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//
using CuestionarioWeb.EN;
using CuestionarioWeb.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CuestionarioWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntasController : ControllerBase
    {
        private PreguntaBL _preguntaBL;

        public PreguntasController(Contexto context)
        {
            _preguntaBL = new PreguntaBL(context);
        }

        // GET: api/<PreguntasController>
        [HttpGet]
        public IEnumerable<Pregunta> GetPreguntas()
        {
            return _preguntaBL.ListarPreguntasPorMasRecientes();
        }

        // GET api/<PreguntasController>/5
        [HttpGet("{id}")]
        public ActionResult<Pregunta> GetPregunta(int? id)
        {

            var pregunta = _preguntaBL.BuscarPreguntaPorId(id);
            if (pregunta == null)
            {
                return NotFound();
            }
            return pregunta;
        }

        // POST api/<PreguntasController>
        [HttpPost]
        public ActionResult<Pregunta> PostGuardarPregunta(Pregunta pregunta)
        {
            int res = _preguntaBL.GuardarPregunta(pregunta);
            if (res > 0)
            {
                return CreatedAtAction("GetPregunta", new { id = pregunta.IdUsuario }, pregunta);
            }
            return null;
        }

        // POST api/<PreguntasController>
        [HttpPost("{idPregunta}")]
        public ActionResult<Pregunta> PostCerrarPregunta(int? idPregunta)
        {
            int res = _preguntaBL.CerrarPregunta((int)idPregunta);
            if (res > 0)
            {
                return Ok("Pregunta Cerrada");
            }
            return null;
        }

        // PUT api/<PreguntasController>/5
        [HttpPut("{id}")]
        public IActionResult PutPregunta(int id, Pregunta pregunta)
        {
            if (id != pregunta.IdPregunta)
            {
                return BadRequest();
            }
            int res = _preguntaBL.EditarPregunta(pregunta);
            return Ok(res);
        }

        // DELETE api/<PreguntasController>/5
        [HttpDelete("{id}")]
        public IActionResult DeletePregunta(int id)
        {
            int res = _preguntaBL.EliminarPregunta(id);
            if (res == 0)
            {
                return NotFound();
            }
            return Ok(res);
        }
    }
}
