using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuestionarioWeb.EN.LoginView
{
    [NotMapped]
    public class UsuarioAtenticado
    {
        public string NickName { get; set; }
        public string Password { get; set; }
    }
}
