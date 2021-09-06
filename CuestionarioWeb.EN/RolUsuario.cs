using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;

namespace CuestionarioWeb.EN
{
    public class RolUsuario
    {
        [Key]
        public int IdRolUsuario { get; set; }

        [StringLength(50, ErrorMessage = "El {0} deber tener entre {1} y {2} caracteres.", MinimumLength = 2)]
        [Required(ErrorMessage = "El tipo de rol es requerido")]
        [Display(Name = "Rol Usuario")]
        public string TipoRolUsuario { get; set; }

        //relaciones
        //uno a muchos
        [JsonIgnore]
        public ICollection<Usuario> Usuario_list { get; set; }
    }
}
