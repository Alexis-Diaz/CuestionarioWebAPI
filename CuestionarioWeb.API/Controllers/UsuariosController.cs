using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CuestionarioWeb.EN;
using CuestionarioWeb.BL;
using CuestionarioWeb.API.Controllers.Filter;
using CuestionarioWeb.EN.LoginView;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CuestionarioWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private const string key = "%$DUJH238";
        private UsuarioBL _usuarioBL;

        public UsuariosController(Contexto context)
        {
            _usuarioBL = new UsuarioBL(context);
        }

        // GET: api/<UsuariosController>
        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> GetUsuarios()
        {
            string cookie = Request.Cookies[key];
            ResponseModel rm =_usuarioBL.Verify(cookie);
            if (rm.IsAuthenticated)
            {
                return _usuarioBL.ListarUsuarios();
            }
            return Unauthorized(rm);
        }

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuario(int id)
        {
            string cookie = Request.Cookies[key];
            ResponseModel rm = _usuarioBL.Verify(cookie);
            if (rm.IsAuthenticated)
            {
                var usuario = _usuarioBL.BuscarUsuarioPorId(id);
                if (usuario == null)
                {
                    return NotFound();
                }
                return usuario;
            }
            return Unauthorized(rm);
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public ActionResult<Usuario> PostUsuario(Usuario usuario)
        {
            //string cookie = Request.Cookies[key];
            //ResponseModel rm = _usuarioBL.Verify(cookie);
            //if (rm.IsAuthenticated)
            //{
            //    int res = _usuarioBL.GuardarNuevoUsuario(usuario);
            //    if (res > 0)
            //    {
            //        return CreatedAtAction("GetUsuario", new { id = usuario.IdUsuario }, usuario);
            //    }
            //    return null;
            //}
            //return Unauthorized(rm);
            int res = _usuarioBL.GuardarNuevoUsuario(usuario);
            if (res > 0)
            {
                return CreatedAtAction("GetUsuario", new { id = usuario.IdUsuario }, usuario);
            }
            return null;

        }

        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        public IActionResult PutUsuario (int id, Usuario usuario)
        {
            string cookie = Request.Cookies[key];
            ResponseModel rm = _usuarioBL.Verify(cookie);
            if (rm.IsAuthenticated)
            {
                if (id != usuario.IdUsuario)
                {
                    return BadRequest();
                }
                int res = _usuarioBL.EditarUsuario(usuario);
                return Ok(res);
            }
            return Unauthorized(rm);
           
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            string cookie = Request.Cookies[key];
            ResponseModel rm = _usuarioBL.Verify(cookie);
            if (rm.IsAuthenticated)
            {
                int res = _usuarioBL.EliminarUsuario(id);
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
