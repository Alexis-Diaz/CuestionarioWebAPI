using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CuestionarioWeb.API.Controllers.SessionHelper
{
    public class SessionToolController : ControllerBase
    {
        public string GetUserInSession()
        {
            string key = "%$DUJH238";
            string message = "";
            //Se verifica si el usuario esta autenticado
            try
            {
                var cookieSession = Request.Cookies[key];
                //if (cookieSession[0] != "")
                //{
                //    var nickNameUser = cookieSession.Split('|');
                //    message = nickNameUser[2];
                //}
            }
            catch (Exception)
            {
                return message;
            }
            return message;
        }
    }
}
