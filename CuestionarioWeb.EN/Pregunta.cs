using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuestionarioWeb.EN
{
    public class Pregunta
    {
        [Key]
        public int IdPregunta { get; set; }

        [Required(ErrorMessage = "La pregunta es obligatoria.")]
        [StringLength(200, ErrorMessage = "La {0} debe tener entre {1} y {2} caracteres.", MinimumLength = 2)]
        [Display(Name = "Pregunta")]
        public string PreguntaFormulada { get; set; }

        private DateTime FechaDePregunta;
        public DateTime Fecha
        {
            get
            {
                return FechaDePregunta;
            }
        }

        private byte EstadoPregunta;
        public byte Estado
        {
            get
            {
                return EstadoPregunta;
            }

            set
            {
                switch (value)
                {
                    case 1:
                        EstadoPregunta = (byte)EstadoDePregunta.Abierta;
                        break;
                    case 2:
                        EstadoPregunta = (byte)EstadoDePregunta.Cerrada;
                        break;
                    default:
                        EstadoPregunta = 1;
                        break;
                }
            }
        }

        //relaciones
        //uno a muchos
        [Required]
        [ForeignKey("Usuario_en")]
        public int IdUsuario { get; set; }

        [JsonIgnore]
        public Usuario Usuario_en { get; set; }

        //muchos a uno
        [JsonIgnore]
        public ICollection<Respuesta> Respuestas_list { get; set; }

        //constructor
        public Pregunta()
        {
            FechaDePregunta = DateTime.Now;
        }

        public enum EstadoDePregunta
        {
            Abierta,
            Cerrada
        }
    }
}
