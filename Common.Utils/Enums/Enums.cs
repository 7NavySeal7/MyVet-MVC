using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utils.Enums
{
    public class Enums
    {
        public enum TypeState
        {
            //Usuario
            EstadoUsuario = 1,
            EstadoCitas = 2
        }

        public enum State
        {
            //Usuario
            UsuarioActivo = 1,
            UsuarioInactivo = 2,
            UsuarioSuspendido = 3,

            //Citas
            CitaActiva = 4,
            CitaCancelada = 5,
            CitaFinalizada = 6
        }

        public enum TypePermission
        {
            Usuario = 1,
            Roles = 2,
            Permisos = 3,
            Veterinaria = 4,
            Estados = 5,
            Mascota = 6
        }

        public enum Permission
        {
            //Usuarios
            CrearUsuarios = 1,
            ActualizarUsuarios = 2,
            EliminarUsuarios = 3,
            ConsultarUsuarios = 4,

            //Roles
            ActualizarRoles = 5,
            ConsultarRoles = 6,

            //Permisos
            ActualizarPermisos = 7,
            ConsultarPermisos = 8,
            DenegarPermisos = 9,

            //Mascota
            CrearMascota = 10,
            ActualizarMascota = 12,
            EliminarMascota = 13,
            ConsultarMascota = 14,

            //Veterinaria
            CrearCitas = 15,
            ConsultarCitas = 16,
            CancelarCitas = 17,
            ActualizarCitas = 18,

            //Estados
            ConsultarEstados = 19,
            ActualizarEstados =20
        }

        public enum Rol
        {
            Administrador = 1,
            Veterinario = 2,
            Estandar = 3 
        }
    }
}
