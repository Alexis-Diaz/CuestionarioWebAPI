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
    public class Respuesta
    {
        [Key]
        public int IdRespuesta { get; set; }
        
        public int AutoReferencia { get; set; }

        [StringLength(5000, ErrorMessage = "La respuesta deber tener entre {1} y {2} caracteres.", MinimumLength = 1)]
        [Required(ErrorMessage = "La respuesta es requerido")]
        [Display(Name = "Respuesta")]
        public string RespuestaEmitida { get; set; }

        [Required]
        [DataType(DataType.Date)]
        private DateTime FechaDeRespuesta;
        public DateTime Fecha
        {
            get
            {
                return FechaDeRespuesta;
            }
        }

        //campos
        public int cantidadDeReacciones;

        //relaciones

        //uno a uno
        [ForeignKey("Usuario_en")]
        public int IdUsuario { get; set; }
        [JsonIgnore]
        public Usuario Usuario_en { get; set; }

        //uno a muchos
        [ForeignKey("Pregunta_en")]
        public int IdPregunta { get; set; }
        [JsonIgnore]
        public Pregunta Pregunta_en { get; set; }

        //muchos a uno
        [JsonIgnore]
        public ICollection<ReaccionUsuarioRespuesta> ReaccionUsuarioRespuesta_list { get; set; }

        //constructores
        public Respuesta()
        {
            FechaDeRespuesta = DateTime.Now;
        }
    }
}
