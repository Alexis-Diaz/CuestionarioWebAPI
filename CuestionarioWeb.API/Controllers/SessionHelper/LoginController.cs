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
        public LoginController(Contexto context)
        {
            _usuarioBL = new UsuarioBL(context);
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
            ResponseModel rm = new ResponseModel();

            if (!string.IsNullOrEmpty(user.NickName) && !string.IsNullOrEmpty(user.Password))
            {
                bool usuarioLoguedo = _usuarioBL.BuscarUsuarioPorCredenciales(user);
                if(usuarioLoguedo)
                {
                    string key = string.Format("%$DUJH238");
                    DateTime expires = DateTime.Now.AddHours(12);
                    string value = string.Format("sdfwedfwoierw3e||{0}", user.NickName);

                    CookieOptions cookieOption = new CookieOptions();
                    cookieOption.Expires = expires;
                    Response.Cookies.Append(key, value, cookieOption);
                    rm.Mensaje = "Usuario_Autenticado";
                    rm.IsAuthenticated = true;
                    return Ok(rm);
                }
            }
            rm.Mensaje = "Usuario_No_Autenticado";
            rm.IsAuthenticated = false;
            return Unauthorized(rm);
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
