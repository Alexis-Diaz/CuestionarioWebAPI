using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CuestionarioWeb.EN;
using CuestionarioWeb.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CuestionarioWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private UsuarioBL _usuarioBL;

        public UsuariosController(Contexto context)
        {
            _usuarioBL = new UsuarioBL(context);
        }


        // GET: api/<UsuariosController>
        [HttpGet]
        public IEnumerable<Usuario> GetUsuarios()
        {
            return _usuarioBL.ListarUsuarios();
        }

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuario(int id)
        {

            var usuario = _usuarioBL.BuscarUsuarioPorId(id);
            if(usuario == null)
            {
                return NotFound();
            }
            return usuario;
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public ActionResult<Usuario> PostUsuario(Usuario usuario)
        {
            int res =_usuarioBL.GuardarNuevoUsuario(usuario);
            if(res > 0)
            {
                return CreatedAtAction("GetUsuario", new { id = usuario.IdUsuario }, usuario);
            }
            return null;
        }

        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        public IActionResult PutUsuario (int id, Usuario usuario)
        {
            if(id != usuario.IdUsuario)
            {
                return BadRequest();
            }
            int res = _usuarioBL.EditarUsuario(usuario);
            return Ok(res);
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            int res = _usuarioBL.EliminarUsuario(id);
            if (res == 0)
            {
                return NotFound();
            }
            return Ok(res);
        }
    }
}
