using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
//
using CuestionarioWeb.EN;
using CuestionarioWeb.BL;
using CuestionarioWeb.EN.LoginView;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CuestionarioWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class LoginController : ControllerBase
    {
        private UsuarioBL _usuarioBL;
        private RolUsuarioBL _rol;
        public LoginController(Contexto context)
        {
            _usuarioBL = new UsuarioBL(context);
            _rol = new RolUsuarioBL(context);
        }
        // GET: api/<SesionController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<SesionController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<SesionController>
        [HttpPost]
        
        public ActionResult<ResponseModel> PostAutenticar(UsuarioAtenticado user)
        {
            string key = string.Format("%$DUJH238");

            string cookie = Request.Cookies[key];
            ResponseModel resm = _usuarioBL.Verify(cookie);
            if (resm.IsAuthenticated)
            {
                return Ok(resm);
            }
            else
            {
                ResponseModel rm = new ResponseModel();

                if (!string.IsNullOrEmpty(user.NickName) && !string.IsNullOrEmpty(user.Password))
                {
                    Usuario usuarioLoguedo = _usuarioBL.BuscarUsuarioPorCredenciales(user);
                    if (usuarioLoguedo.IdRolUsuario > 0)
                    {
                        RolUsuario rol = _rol.BuscarRolUsuarioPorId(usuarioLoguedo.IdRolUsuario);
                        if (rol.IdRolUsuario > 0)
                        {
                            DateTime expires = DateTime.Now.AddHours(12);
                            string value = string.Format("{0}|{1}|{2}|{3}", usuarioLoguedo.IdUsuario, user.NickName, usuarioLoguedo.Nombre, rol.TipoRolUsuario);

                            CookieOptions cookieOption = new CookieOptions();
                            cookieOption.IsEssential = true;
                            cookieOption.Domain = "localhost";
                            cookieOption.Expires = expires;
                            HttpContext.Response.Cookies.Append(key, value, cookieOption);
                           
                            rm.user = usuarioLoguedo;
                            rm.rol = rol;
                            rm.Mensaje = "Usuario_Autenticado";
                            rm.StatusCode = 200;
                            rm.IsAuthenticated = true;
                            return Ok(rm);
                           
                        }
                    }
                }
                rm.Mensaje = "Bad_Request";
                rm.StatusCode = 400;
                rm.IsAuthenticated = false;
                return Unauthorized(rm);
            }

            
        }

        //// PUT api/<SesionController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<SesionController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
       
    }
}
