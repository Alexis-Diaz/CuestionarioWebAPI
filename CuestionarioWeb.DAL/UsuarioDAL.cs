using CuestionarioWeb.EN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuestionarioWeb.EN.LoginView;

namespace CuestionarioWeb.DAL
{
    public class UsuarioDAL
    {
        private Contexto _context { get; set; }
        public UsuarioDAL(Contexto context)
        {
            _context = context;
        }

        //agregar
        public int GuardarUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                _context.Usuarios.Add(usuario);
                return _context.SaveChanges();
            }
            return 0;
        }

        //editar
        public int EditarUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                Usuario usuarioEncontrado = _context.Usuarios.Find(usuario.IdRolUsuario);
                if (usuarioEncontrado != null)
                {
                    usuarioEncontrado.Nombre = usuario.Nombre;
                    usuarioEncontrado.Apellido = usuario.Apellido;
                    usuarioEncontrado.FechaNacimiento = usuario.FechaNacimiento;
                    usuarioEncontrado.NickName = usuario.NickName;
                    usuarioEncontrado.Password = usuario.Password;
                    usuarioEncontrado.IdRolUsuario = usuario.IdRolUsuario;

                    _context.Entry(usuarioEncontrado).State = EntityState.Modified;
                    return _context.SaveChanges();
                }
            }
            return 0;
        }

        //eliminar
        public int EliminarUsuario(int? id)
        {
            if (id == null || id < 0)
            {
                return 0;
            }

            Usuario usuarioEncontrado = _context.Usuarios.Find(id);
            if (usuarioEncontrado != null)
            {
                _context.Usuarios.Remove(usuarioEncontrado);
                return _context.SaveChanges();
            }
            return 0;
        }


        //buscar por id
        public Usuario BuscarUsuarioPorId(int? id)
        {
            if (id == null || id < 0)
            {
                return null;
            }
            return _context.Usuarios.Find(id);
        }

        //buscar por nombre de usuario
        public List<Usuario> BuscarUsuarioPorNombreDeUsuario(string nombreUsuario)
        {
            if (!string.IsNullOrEmpty(nombreUsuario))
            {
                List<Usuario> listaUsuario = _context.Usuarios.Where(x => x.NickName == nombreUsuario).ToList();
                return listaUsuario;
            }
            return null;
        }

        //buscar por nombre de usuario y contraseña
        public int BuscarUsuarioPorCredenciales(UsuarioAtenticado user)
        {
            if (user != null)
            {
                int res = _context.Usuarios.Where(x => x.NickName == user.NickName && x.Password == user.Password).Count();
                return res;
            }
            return 0;
        }

        //listar
        public List<Usuario> ListaUsuarios()
        {
            return _context.Usuarios.ToList();
        }
    }
}
