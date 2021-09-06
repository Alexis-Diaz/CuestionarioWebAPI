using CuestionarioWeb.BL;
using CuestionarioWeb.EN;
using CuestionarioWeb.EN.LoginView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CuestionarioWeb.UI.Controllers.SessionHelper
{
    public class LoginController : Controller
    {
        private UsuarioBL _usuarioBL;
        private RolUsuarioBL _rol;
        public LoginController(Contexto context)
        {
            _usuarioBL = new UsuarioBL(context);
            _rol = new RolUsuarioBL(context);
        }

        //GET: api/<LoginController>
        public ActionResult Index ()
        {
            string key = string.Format("%$DUJH238");

            string cookie = Request.Cookies[key];
            ResponseModel resm = _usuarioBL.Verify(cookie);
            if (resm.IsAuthenticated)
            {
                return RedirectToAction("Index", "Preguntas");
            }
            ViewData["User"] = resm;
            return View();
        }

        // POST api/<SesionController>
        [HttpPost]

        public ActionResult PostAutenticar(UsuarioAtenticado user)
        {
            string key = string.Format("%$DUJH238");

            string cookie = Request.Cookies[key];
            ResponseModel resm = _usuarioBL.Verify(cookie);
            if (resm.IsAuthenticated)
            {
                return RedirectToAction("Index", "Preguntas");
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
                            return RedirectToAction("Index", "Preguntas");

                        }
                    }
                }
                rm.Mensaje = "Bad_Request";
                rm.StatusCode = 400;
                rm.IsAuthenticated = false;
                return Index();
            }


        }

        [HttpGet]
        public ActionResult PostCerrarSession()
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
                    return View("Index");
                }
                rm.Mensaje = "Not_Found";
                rm.IsAuthenticated = false;
               

            }
            return View("Index");

        }

        public ActionResult Register()
        {
            return View();
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Usuario user)
        {
            try
            {
                if (user != null)
                {
                    int res = _usuarioBL.GuardarNuevoUsuario(user);
                    if (res == 1)
                    {
                        UsuarioAtenticado usuarioCreado = new UsuarioAtenticado
                        {
                            NickName = user.NickName,
                            Password = user.Password
                        };
                        return View("Index", usuarioCreado);
                    }
                }
                return View();
            }
            catch
            {
                return View("Index");
            }
        }
    }
}
