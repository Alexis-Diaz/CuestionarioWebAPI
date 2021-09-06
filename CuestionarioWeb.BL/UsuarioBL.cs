using CuestionarioWeb.DAL;
using CuestionarioWeb.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuestionarioWeb.EN.LoginView;

namespace CuestionarioWeb.BL
{
    public class UsuarioBL
    {
        private UsuarioDAL _usuarioDAL;
        private RolUsuarioDAL _rol;

        public UsuarioBL(Contexto context)
        {
            _usuarioDAL = new UsuarioDAL(context);
            _rol = new RolUsuarioDAL(context);
        }

        //guardar
        public int GuardarNuevoUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                int coincidencia = _usuarioDAL.BuscarUsuarioPorNombreDeUsuario(usuario.NickName).Count();
                if (coincidencia == 0)
                {
                    return _usuarioDAL.GuardarUsuario(usuario);
                }
            }
            return 0;
        }

        //buscar usuario y contraseña
        public Usuario BuscarUsuarioPorCredenciales(UsuarioAtenticado usuario)
        {
            if (!string.IsNullOrEmpty(usuario.NickName) && !string.IsNullOrEmpty(usuario.Password))
            {
                Usuario user = _usuarioDAL.BuscarUsuarioPorCredenciales(usuario);
                if (user != null)
                {
                    return user;
                }
            }
            return null;
        }

        //listar usuarios
        public List<Usuario> ListarUsuarios()
        {
            return _usuarioDAL.ListaUsuarios();
        }

        //Buscar usuario por id
        public Usuario BuscarUsuarioPorId(int? id)
        {
            if(id==null || id < 0)
            {
                return null;
            }

            return _usuarioDAL.BuscarUsuarioPorId(id);
        }

        //Editar
        public int EditarUsuario (Usuario usuario)
        {
            if(usuario != null)
            {
                int coincidencia = 0;
                var user = _usuarioDAL.ListaUsuarios();
                foreach (var item in user)
                {
                    if (item.NickName == usuario.NickName)
                    {
                        coincidencia ++;
                    }
                }

                if (coincidencia > 0)
                {
                    return 0;
                }
              
                return _usuarioDAL.EditarUsuario(usuario);
            }
            return 0;
        }

        //Eliminar
        public int EliminarUsuario(int? id)
        {
            if (id == null || id < 0)
            {
                return 0;
            }

            return _usuarioDAL.EliminarUsuario(id);
        }

        //verificar usuario
        public ResponseModel Verify(string cookie)
        {
            ResponseModel rm = new ResponseModel();
            Usuario usuario = new Usuario();

            if (!string.IsNullOrEmpty(cookie))
            {
                var datos = cookie.Split('|');
                int id = 0;
                bool res = int.TryParse(datos[0], out id);
                if (res)
                {
                     usuario = _usuarioDAL.BuscarUsuarioPorId(id);
                }
            }
            if(usuario.IdUsuario> 0)
            {
                rm.Mensaje = "User_Is_Valid";
                rm.StatusCode = 200;
                rm.IsAuthenticated = true;
                rm.user = usuario;
                rm.rol = _rol.BuscarRolUsuarioPorId(usuario.IdRolUsuario);

            }
            else
            {
                rm.Mensaje = "Unauthorized";
                rm.StatusCode = 401;
                rm.IsAuthenticated = false;
            }
            return rm;
        }
    }
}
