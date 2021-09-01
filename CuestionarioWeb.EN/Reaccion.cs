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
        
        private string Emoji;

        [Required(ErrorMessage = "La reaccion es requerida")]
        [StringLength(8)]
        public string TipoReaccion
        {
            get
            {
                return Emoji;
            }
            set
            {
                int val = int.Parse(value);
                switch (val)
                {
                    case 1:
                        Emoji = "&#1F44D;";
                        break;
                    case 2:
                        Emoji = "&#1F44E;";
                        break;
                    case 3:
                        Emoji = "&#1F497;";
                        break;
                    case 4:
                        Emoji = "&#1F62E;";
                        break;
                    case 5:
                        Emoji = "&#1F624;";
                        break;
                    case 6:
                        Emoji = "&#1F60D;";
                        break;
                    case 7:
                        Emoji = "&#1F602;";
                        break;
                }
            }
        }

        //relaciones

        //muchos a uno
        [JsonIgnore]
        public ICollection<ReaccionUsuarioRespuesta> ReaccionUsuarioRespuesta_list { get; set; }

        //enum
        public enum ElegirReaccion
        {
            like = 1,
            dislike = 2,
            meEncanta = 3,
            meAsombra = 4,
            meEnoja = 5,
            meEnamora = 6,
            meDivierte = 7
        }
    }
}
