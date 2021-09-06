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
    public class RespuestasController : ControllerBase
    {
        private RespuestaBL _respuestaBL;
        private UsuarioBL _usuarioBL;

        private const string key = "%$DUJH238";

        public RespuestasController(Contexto context)
        {
            _respuestaBL = new RespuestaBL(context);
            _usuarioBL = new UsuarioBL(context);
        }

        // GET: api/<RespuestasController>
        [HttpGet]
        public ActionResult<IEnumerable<Respuesta>> GetListarRespuestas(int? idPregunta = null, int? idReaccion = null)
        {
            string cookie = Request.Cookies[key];
            ResponseModel rm = _usuarioBL.Verify(cookie);
            if (rm.IsAuthenticated)
            {
                if (idPregunta != null)
                {
                    if (idReaccion != null)
                    {
                        return _respuestaBL.ListarRespuestasPorReaccion(idPregunta, idReaccion);

                    }
                    return _respuestaBL.ListarRespuestasPorPregunta(idPregunta);
                }
                return null;
            }
            return Unauthorized(rm);
          
        }

        // GET api/<RespuestasController>/5
        [HttpGet("{id}")]
        public ActionResult<Respuesta> GetRespuesta(int? id)
        {
            string cookie = Request.Cookies[key];
            ResponseModel rm = _usuarioBL.Verify(cookie);
            if (rm.IsAuthenticated)
            {
                return _respuestaBL.BuscarRespuestaPorId(id);
            }
            return Unauthorized(rm);
        }

        // POST api/<RespuestasController>
        [HttpPost]
        public ActionResult<Respuesta> PostGuardarRespuesta(Respuesta respuesta)
        {
            string cookie = Request.Cookies[key];
            ResponseModel rm = _usuarioBL.Verify(cookie);
            if (rm.IsAuthenticated)
            {
                int res = _respuestaBL.NuevaRespuesta(respuesta);
                if (res > 0)
                {
                    if (res == 2)
                    {
                        return Ok("Pregunta cerrada por el administrador");
                    }
                    return CreatedAtAction("GetRespuesta", new { id = respuesta.IdRespuesta }, respuesta);
                }
                return null;
            }
            return Unauthorized(rm);
           
        }

        // POST api/<RespuestasController>
        [HttpPost("{idRespuesta}")]
        public ActionResult<Respuesta> PostReaccionarEnRespuesta(int? idRespuesta, int? codigoEmoji, int? idUsuario)
        {
            string cookie = Request.Cookies[key];
            ResponseModel rm = _usuarioBL.Verify(cookie);
            if (rm.IsAuthenticated)
            {
                int res = _respuestaBL.ReaccionarSobreRespuesta(idRespuesta, codigoEmoji, idUsuario);
                if (res > 0)
                {
                    return Ok("reaccion exitosa");
                }
                return null;
            }
            return Unauthorized(rm);
           
        }

        // PUT api/<RespuestasController>/5
        //[HttpPut("{id}")]
        //public IActionResult PutRespuesta(int id, Respuesta respuesta)
        //{
        //    if (id != respuesta.IdRespuesta)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(res);
        //}

        // DELETE api/<RespuestasController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRespuesta(int id)
        {
            string cookie = Request.Cookies[key];
            ResponseModel rm = _usuarioBL.Verify(cookie);
            if (rm.IsAuthenticated)
            {
                int res = _respuestaBL.EliminarRespuesta(id);
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
