using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//
using CuestionarioWeb.EN;
using CuestionarioWeb.BL;
using CuestionarioWeb.EN.LoginView;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CuestionarioWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntasController : ControllerBase
    {
        private PreguntaBL _preguntaBL;
        private UsuarioBL _usuarioBL;
        private const string key = "%$DUJH238";

        public PreguntasController(Contexto context)
        {
            _preguntaBL = new PreguntaBL(context);
            _usuarioBL = new UsuarioBL(context);
        }

        // GET: api/<PreguntasController>
        [HttpGet]
        public ActionResult<IEnumerable<Pregunta>> GetPreguntas()
        {
            string cookie = Request.Cookies[key];
            ResponseModel rm = _usuarioBL.Verify(cookie);
            if (rm.IsAuthenticated)
            {
                return _preguntaBL.ListarPreguntasPorMasRecientes();
            }
            return Unauthorized(rm);
           
        }

        // GET api/<PreguntasController>/5
        [HttpGet("{id}")]
        public ActionResult<Pregunta> GetPregunta(int? id)
        {
            string cookie = Request.Cookies[key];
            ResponseModel rm = _usuarioBL.Verify(cookie);
            if (rm.IsAuthenticated)
            {
                var pregunta = _preguntaBL.BuscarPreguntaPorId(id);
                if (pregunta == null)
                {
                    return NotFound();
                }
                return pregunta;
            }
            return Unauthorized(rm);
            
        }

        // POST api/<PreguntasController>
        [HttpPost]
        public ActionResult<Pregunta> PostGuardarPregunta(Pregunta pregunta)
        {
            string cookie = Request.Cookies[key];
            ResponseModel rm = _usuarioBL.Verify(cookie);
            if (rm.IsAuthenticated)
            {
                int res = _preguntaBL.GuardarPregunta(pregunta);
                if (res > 0)
                {
                    return CreatedAtAction("GetPregunta", new { id = pregunta.IdUsuario }, pregunta);
                }
                return null;
            }
            return Unauthorized(rm);
           
        }

        // POST api/<PreguntasController>
        [HttpPost("{idPregunta}")]
        public ActionResult<Pregunta> PostCerrarPregunta(int? idPregunta)
        {
            string cookie = Request.Cookies[key];
            ResponseModel rm = _usuarioBL.Verify(cookie);
            if (rm.IsAuthenticated)
            {
                string rol = rm.rol.TipoRolUsuario.ToLower();
                if(rol == "administrador" )
                {
                    int res = _preguntaBL.CerrarPregunta((int)idPregunta);
                    if (res > 0)
                    {
                        return Ok("Pregunta_Cerrada");
                    }
                    return null;
                }
                else
                {
                    rm.Mensaje = "Unauthorized";
                    rm.StatusCode = 401;
                    rm.IsAuthenticated = false;
                }
            }
            return Unauthorized(rm);
        }

        // PUT api/<PreguntasController>/5
        [HttpPut("{id}")]
        public IActionResult PutPregunta(int id, Pregunta pregunta)
        {
            string cookie = Request.Cookies[key];
            ResponseModel rm = _usuarioBL.Verify(cookie);
            if (rm.IsAuthenticated)
            {
                if (id != pregunta.IdPregunta)
                {
                    return BadRequest();
                }
                int res = _preguntaBL.EditarPregunta(pregunta);
                return Ok(res);
            }
            return Unauthorized(rm);
           
        }

        // DELETE api/<PreguntasController>/5
        [HttpDelete("{id}")]
        public IActionResult DeletePregunta(int id)
        {
            string cookie = Request.Cookies[key];
            ResponseModel rm = _usuarioBL.Verify(cookie);
            if (rm.IsAuthenticated)
            {
                int res = _preguntaBL.EliminarPregunta(id);
                if (res == 0)
                {
                    return NotFound();
                }
                return Ok(res);
            }
            return Unauthorized(rm);
          
        }
    }
}
