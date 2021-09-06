using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using CuestionarioWeb.EN;

namespace CuestionarioWeb.DAL
{
    public class RolUsuarioDAL
    {
        private Contexto _context { get; set; }
        public RolUsuarioDAL(Contexto context)
        {
            _context = context;
        }

        public RolUsuario BuscarRolUsuarioPorId(int? id)
        {
            if (id == null || id < 0)
            {
                return null;
            }
            return _context.RolUsuarios.Find(id);
        }

        public List<RolUsuario> ListarRolesDeUsuarios()
        {
            return _context.RolUsuarios.ToList();
        }
    }
}
