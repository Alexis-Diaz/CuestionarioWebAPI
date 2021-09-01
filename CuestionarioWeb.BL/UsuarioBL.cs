using CuestionarioWeb.DAL;
using CuestionarioWeb.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuestionarioWeb.BL
{
    public class UsuarioBL
    {
        private UsuarioDAL _usuarioDAL;
        public UsuarioBL(Contexto context)
        {
            _usuarioDAL = new UsuarioDAL(context);
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
        public int BuscarUsuarioYContraseña(UsuarioAtenticado usuario)
        {
            if (!string.IsNullOrEmpty(usuario.NickName) && !string.IsNullOrEmpty(usuario.Password))
            {
                var user = _usuarioDAL.BuscarUsuarioPorNombreDeUsuario(usuario.NickName);
                if (usuario.Password == user[0].Password)
                {
                    //falta agregar la logica de seguridad. Por ejemplo crear una cookie de sesion para
                    //enviarla al cliente y la guarde. Cada vez que el cliente haga una peticion el servidor
                    //puede verificar si el usuario esta autenticado.
                    return 1;
                }
            }
            return 0;
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
    }
}
