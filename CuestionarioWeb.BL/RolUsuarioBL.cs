using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using CuestionarioWeb.DAL;
using CuestionarioWeb.EN;

namespace CuestionarioWeb.BL
{
    public class RolUsuarioBL
    {
        private RolUsuarioDAL _rolUsuarioDAL;
        public RolUsuarioBL(Contexto context)
        {
            _rolUsuarioDAL = new RolUsuarioDAL(context);
        }

        public RolUsuario BuscarRolUsuarioPorId(int? id)
        {
            if(id==null || id < 0)
            {
                return null;
            }
            return _rolUsuarioDAL.BuscarRolUsuarioPorId(id);
        }

        public List<RolUsuario> ListarRolesDeUsuarios()
        {
            return _rolUsuarioDAL.ListarRolesDeUsuarios();
        }
    }
}
