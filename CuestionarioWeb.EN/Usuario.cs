using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace CuestionarioWeb.EN
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [StringLength(50, ErrorMessage = "El {0} deber tener entre {1} y {2} caracteres.", MinimumLength = 2)]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [StringLength(50, ErrorMessage = "El {0} deber tener entre {1} y {2} caracteres.", MinimumLength = 2)]
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [StringLength(50, ErrorMessage = "El {0} deber tener entre {1} y {2} caracteres.", MinimumLength = 2)]
        [Required(ErrorMessage = "El nickname es obligatorio.")]
        [Display(Name = "Nombre de usuario")]
        public string NickName { get; set; }

        [StringLength(50, ErrorMessage = "El {0} deber tener entre {1} y {2} caracteres.", MinimumLength = 2)]
        [Required(ErrorMessage = "El password es obligatorio.")]
        public string Password { get; set; }

        //propiedades auxiliares

        //relaciones

        //uno a uno
        [Required]
        [ForeignKey("RolUsuario_en")]
        public int IdRolUsuario { get; set; }

        [JsonIgnore]
        public RolUsuario RolUsuario_en { get; set; }

        //muchos a uno
        [JsonIgnore]
        public ICollection<ReaccionUsuarioRespuesta> ReaccionUsuarioRespuesta_list { get; set; }

        //muchos a uno
        [JsonIgnore]
        public ICollection<Pregunta> Preguntas_list { get; set; }
    }
}
