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
    public class Reaccion
    {
        [Key]
        public int IdReaccion { get; set; }

        [Required(ErrorMessage = "La reaccion es requerida")]
        [StringLength(9)]
        public string TipoReaccion { get; set; }
      
        //relaciones

        //muchos a uno
        [JsonIgnore]
        public ICollection<ReaccionUsuarioRespuesta> ReaccionUsuarioRespuesta_list { get; set; }

        //enum
        //public enum ElegirReaccion
        //{
        //    like = 1,
        //    dislike = 2,
        //    meEncanta = 3,
        //    meAsombra = 4,
        //    meEnoja = 5,
        //    meEnamora = 6,
        //    meDivierte = 7
        //}
    }
}
