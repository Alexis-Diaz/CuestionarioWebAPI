using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace CuestionarioWeb.EN
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) 
        : base(options)
        {

        }

        public DbSet<Pregunta> Preguntas { get; set; }
        public DbSet<Reaccion> Reacciones { get; set; }
        public DbSet<ReaccionUsuarioRespuesta> ReaccionUsuarioRespuestas { get; set; }
        public DbSet<Respuesta> Respuestas { get; set; }
        public DbSet<RolUsuario> RolUsuarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }

}
