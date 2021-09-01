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
    public class ReaccionUsuarioRespuesta
    {
        [Key]
        public int Id { get; set; }

        //realacion
        //uno a muchos
        [Required]
        [ForeignKey("Usuario_en")]
        public int IdUsuario { get; set; }

        [JsonIgnore]
        public Usuario Usuario_en { get; set; }

        [Required]
        [ForeignKey("Reaccion_en")]
        public int IdReaccion { get; set; }

        [JsonIgnore]
        public Reaccion Reaccion_en { get; set; }

        [Required]
        [ForeignKey("Respuesta_en")]
        public int IdRespuesta { get; set; }

        [JsonIgnore]
        public Respuesta Respuesta_en { get; set; }
    }
}
