using CuestionarioWeb.BL;
using CuestionarioWeb.EN;
using CuestionarioWeb.EN.LoginView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CuestionarioWeb.UI.Controllers
{
    public class UsuariosController : Controller
    {
      
        private UsuarioBL _usuarioBL;
        private RolUsuarioBL _rolBL;
      
        private const string key = "%$DUJH238";

        public UsuariosController(Contexto context)
        { 
            _usuarioBL = new UsuarioBL(context);
            _rolBL = new RolUsuarioBL(context);
        }

        // GET: PreguntasController
        public ActionResult Index()
        {
            string cookie = Request.Cookies[key];
            ResponseModel rm = _usuarioBL.Verify(cookie);
            if (rm.IsAuthenticated && (rm.rol.TipoRolUsuario.ToLower() == "administrador" || rm.rol.TipoRolUsuario.ToLower() == "superadministrador"))
            {
                var ListUsuarios=_usuarioBL.ListarUsuarios();
                if (ListUsuarios.Count() > 0) 
                {
                    foreach(var user in ListUsuarios)
                    {
                        int longitudPassword = user.Password.Count();
                        user.Password = "";
                        for(int i = 0; i<longitudPassword; i++)
                        {
                            user.Password += "*";
                        }
                    }
                }
                List<RolUsuario> roles = _rolBL.ListarRolesDeUsuarios();
                ViewData["Roles"] = roles;
                ViewData["User"] = rm;
                return View(ListUsuarios);
            }
            if (rm.IsAuthenticated && rm.rol.TipoRolUsuario.ToLower() == "comun")
            {
                var ListUsuarios = _usuarioBL.ListarUsuarios();
                ListUsuarios = ListUsuarios.Where(x => x.NickName == rm.user.NickName).ToList();
                if (ListUsuarios.Count() > 0)
                {
                    foreach (var user in ListUsuarios)
                    {
                        int longitudPassword = user.Password.Count();
                        user.Password = "";
                        for (int i = 0; i < longitudPassword; i++)
                        {
                            user.Password += "*";
                        }
                    }
                }
                List<RolUsuario> roles = _rolBL.ListarRolesDeUsuarios();
                ViewData["Roles"] = roles;
                ViewData["User"] = rm;
                return View(ListUsuarios);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: UsuariosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuariosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
