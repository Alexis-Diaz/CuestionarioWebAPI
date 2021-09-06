using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuestionarioWeb.EN.LoginView
{
    [NotMapped]
    public class ResponseModel
    {
        public Usuario user { get; set; }
        public RolUsuario rol { get; set; }
        public string Mensaje { get; set; }
        public int StatusCode { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
