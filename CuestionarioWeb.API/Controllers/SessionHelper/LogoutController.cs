using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
//
using CuestionarioWeb.EN;
using CuestionarioWeb.BL;
using CuestionarioWeb.API.Controllers.Filter;
using CuestionarioWeb.EN.LoginView;

namespace CuestionarioWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private UsuarioBL _usuarioBL;
        public LogoutController(Contexto context)
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
        public ActionResult<ResponseModel> PostCerrarSession()
        {
            string key = string.Format("%$DUJH238");

            string cookie = Request.Cookies[key];
            ResponseModel resm = _usuarioBL.Verify(cookie);
            if (resm.IsAuthenticated)
            {
                ResponseModel rm = new ResponseModel();
                var cookieSession = Request.Cookies[key];

                if (!string.IsNullOrEmpty(cookieSession))
                {
                    string value = "";
                    CookieOptions cookieOptions = new CookieOptions();
                    cookieOptions.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Append(key, value, cookieOptions);
                    rm.Mensaje = "Session_Expired";
                    rm.StatusCode = 200;
                    rm.IsAuthenticated = false;
                    return Ok(rm);
                }
                rm.Mensaje = "Not_Found";
                rm.IsAuthenticated = false;
                return NotFound(rm);
               
            }
            return Unauthorized(resm);
          
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
