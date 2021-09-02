using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CuestionarioWeb.API.Controllers.SessionHelper;
using Microsoft.AspNetCore.Routing;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CuestionarioWeb.API.Controllers.Filter
{
    public class AutenticadoAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Este metodo es llamado si el controlador tiene la bandera [Auntenticado].
        /// Sirve de filtro para restringir el acceso a usuarios no autorizados
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            SessionToolController tool = new SessionToolController();
            string user = tool.GetUserInSession();
            if (string.IsNullOrEmpty(user))
            {
                //Redireccionamos al usurio no autenticado al login
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Login",
                    action = "Index"
                }));
            }
        }
    }

    public class NoLoginAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Este metodo es llamado si el controlador tiene la bandera [NoLogin].
        /// Sirve para permitir el acceso a solicitudes sin necesidad de loguerse. Si detecta una sesion abierta
        /// redirige al usuario a la pagina de inicio de su cuenta abierta.
        /// </summary>
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    base.OnActionExecuting(filterContext);

        //    //Se verifica si el usuario esta autenticado
        //    if (SessionHelper.ExistUserInSession())
        //    {
        //        //Redireccionamos al usurio autenticado a su cuenta abierta
        //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
        //        {
        //            controller = "Admin",
        //            action = "Index"
        //        }));
        //    }
        //}
    }
}
