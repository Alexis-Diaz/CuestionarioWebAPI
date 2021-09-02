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
        public string Mensaje { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
