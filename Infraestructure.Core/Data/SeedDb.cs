using Common.Utils.Enums;
using Infraestructure.Entity.Models;
using Infraestructure.Entity.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Core.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        #region Builder
        public SeedDb(DataContext context)
        {
            _context = context;
        }
        #endregion

        public async Task ExecSeedAsync()
        {
            await _context.Database.EnsureCreatedAsync(); //Comando para asegurarse que la base de datos este creada.
            await CheckTypeStateAsync();
            await CheckStateAsync();
            await CheckTypePermissionAsync();
            await CheckPermissionAsync();
            //await CheckRolAsync();
            await CheckRolPermissionAsync();
        }

        //Tipos de Estados
        private async Task CheckTypeStateAsync()
        {
            if (!_context.TypeStateEntity.Any())
            {
                _context.TypeStateEntity.AddRange(new List<TypeStateEntity>
                {
                    new TypeStateEntity
                    {
                        IdTypeState=(int)Enums.TypeState.EstadoUsuario,
                        TypeState="Estado de Usuarios"
                    },                    
                    new TypeStateEntity
                    {
                        IdTypeState=(int)Enums.TypeState.EstadoCitas,
                        TypeState="Estado de las Citas"
                    },
                });

                await _context.SaveChangesAsync();
            }
        }

        //Estados
        private async Task CheckStateAsync()
        {
            if (!_context.StateEntity.Any())
            {
                _context.StateEntity.AddRange(new List<StateEntity>
                {
                    new StateEntity
                    {
                        IdTypeState=(int)Enums.TypeState.EstadoUsuario,
                        IdState=(int)Enums.State.UsuarioActivo,
                        State="Activo"
                    },                    
                    new StateEntity
                    {
                        IdTypeState=(int)Enums.TypeState.EstadoUsuario,
                        IdState=(int)Enums.State.UsuarioInactivo,
                        State="Inactivo"
                    },                      
                    new StateEntity
                    {
                        IdTypeState=(int)Enums.TypeState.EstadoUsuario,
                        IdState=(int)Enums.State.UsuarioSuspendido,
                        State="Suspendido"
                    },           
                    new StateEntity
                    {
                        IdTypeState=(int)Enums.TypeState.EstadoCitas,
                        IdState=(int)Enums.State.CitaActiva,
                        State="Cita Activa"
                    },                     
                    new StateEntity
                    {
                        IdTypeState=(int)Enums.TypeState.EstadoCitas,
                        IdState=(int)Enums.State.CitaCancelada,
                        State="Cita Cancelada"
                    },                     
                    new StateEntity
                    {
                        IdTypeState=(int)Enums.TypeState.EstadoCitas,
                        IdState=(int)Enums.State.CitaFinalizada,
                        State="Cita Finalizada"
                    }, 
                });

                await _context.SaveChangesAsync();
            }
        }

        //Tipos de Permisos
        private async Task CheckTypePermissionAsync()
        {
            if (!_context.TypePermissionEntity.Any())//Si la tabla esta vacia y la condicion es verdadera se insertan los registros
            {
                _context.TypePermissionEntity.AddRange(new List<TypePermissionEntity>
                {
                    new TypePermissionEntity
                    {
                        IdTypePermission=(int)Enums.TypePermission.Usuario,
                        TypePermission="Usuarios"
                    },

                    new TypePermissionEntity
                    {
                        IdTypePermission=(int)Enums.TypePermission.Roles,
                        TypePermission="Roles"
                    },                    
                    
                    new TypePermissionEntity
                    {
                        IdTypePermission=(int)Enums.TypePermission.Permisos,
                        TypePermission="Permisos"
                    },

                    new TypePermissionEntity
                    {
                        IdTypePermission=(int)Enums.TypePermission.Veterinaria,
                        TypePermission="Veterinaria"
                    },                    
                    
                    new TypePermissionEntity
                    {
                        IdTypePermission=(int)Enums.TypePermission.Estados,
                        TypePermission="Estados"
                    },                    
                    new TypePermissionEntity
                    {
                        IdTypePermission=(int)Enums.TypePermission.Mascota,
                        TypePermission="Mascotas"
                    },
                });

                await _context.SaveChangesAsync();
            }
        }

        //Permisos
        private async Task CheckPermissionAsync()
        {
            if (!_context.PermissionEntity.Any())
            {
                _context.PermissionEntity.AddRange(new List<PermissionEntity>
                {
                    //Usuarios
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.CrearUsuarios,
                        IdTypePermission=(int)Enums.TypePermission.Usuario,
                        Permission="Crear Usuarios",
                        Description="Crear Usuarios al Sistemas"
                    },                    
                    
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ActualizarUsuarios,
                        IdTypePermission=(int)Enums.TypePermission.Usuario,
                        Permission="Actualizar Usuarios",
                        Description="Actualizar datos de un usuarios en el sistema"
                    },                    
                    
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.EliminarUsuarios,
                        IdTypePermission=(int)Enums.TypePermission.Usuario,
                        Permission="Eliminar Usuarios",
                        Description="Eliminar un usuario del sistema"
                    },                    
                    
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ConsultarUsuarios,
                        IdTypePermission=(int)Enums.TypePermission.Usuario,
                        Permission="Consultar Usuarios",
                        Description="Consulta todos los usuarios"
                    },

                    //Roles
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ActualizarRoles,
                        IdTypePermission=(int)Enums.TypePermission.Roles,
                        Permission="Actualizar Roles",
                        Description="Actualizar datos de los roles en el sistema"
                    },

                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ConsultarRoles,
                        IdTypePermission=(int)Enums.TypePermission.Roles,
                        Permission="Consultar Roles",
                        Description="Consultar Roles del sistema"
                    },

                    //Permisos
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ActualizarPermisos,
                        IdTypePermission=(int)Enums.TypePermission.Permisos,
                        Permission="Actualizar Permisos",
                        Description="Actualizar datos de un permiso en el sistema"
                    },

                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ConsultarPermisos,
                        IdTypePermission=(int)Enums.TypePermission.Permisos,
                        Permission="Consultar Permisos",
                        Description="Consultar Permisos del sistema"
                    },

                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.DenegarPermisos,
                        IdTypePermission=(int)Enums.TypePermission.Permisos,
                        Permission="Denegar Permisos",
                        Description="Denegar Permisos a un rol del sistema"
                    },

                    //Mascotas
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.CrearMascota,
                        IdTypePermission=(int)Enums.TypePermission.Mascota,
                        Permission="Crear Mascota",
                        Description="Crear la información de la mascota"
                    },

                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ActualizarMascota,
                        IdTypePermission=(int)Enums.TypePermission.Mascota,
                        Permission="Actualizar Mascota",
                        Description="Actualizar la informacion de la mascota"
                    },

                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.EliminarMascota,
                        IdTypePermission=(int)Enums.TypePermission.Mascota,
                        Permission="Eliminar Mascota",
                        Description="Eliminar la informacion de la mascota"
                    },

                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ConsultarMascota,
                        IdTypePermission=(int)Enums.TypePermission.Mascota,
                        Permission="Consultar Mascota",
                        Description="Consultar la informacion de la mascota"
                    },


                    //Veterinaria
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.CrearCitas,
                        IdTypePermission=(int)Enums.TypePermission.Veterinaria,
                        Permission="Crear Citas",
                        Description="Crear la informacion de las citas"
                    },

                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ConsultarCitas,
                        IdTypePermission=(int)Enums.TypePermission.Veterinaria,
                        Permission="Consultar Citas",
                        Description="Consultar la informacion de las citas"
                    },

                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.CancelarCitas,
                        IdTypePermission=(int)Enums.TypePermission.Veterinaria,
                        Permission="Cancelar Citas",
                        Description="Cancelar la informacion de las citas"
                    },

                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ActualizarCitas,
                        IdTypePermission=(int)Enums.TypePermission.Veterinaria,
                        Permission="Actualizar Citas",
                        Description="Actualizar la informacion de la cita"
                    },

                    //Estados
                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ConsultarEstados,
                        IdTypePermission=(int)Enums.TypePermission.Estados,
                        Permission="Consultar Estado",
                        Description="Consultar los estados del sistema"
                    },

                    new PermissionEntity
                    {
                        IdPermission=(int)Enums.Permission.ActualizarEstados,
                        IdTypePermission=(int)Enums.TypePermission.Estados,
                        Permission="Actualizar Estado",
                        Description="Actualizar los estados del sistema"
                    }
                });
                await _context.SaveChangesAsync();
            }
        }

        //Roles
        //private async Task CheckRolAsync()
        //{
        //    if (_context.RolEntity.Any())
        //    {
        //        _context.RolEntity.AddRange(new List<RolEntity>
        //        {
        //            new RolEntity
        //            {
        //                IdRol=(int)Enums.Rol.Administrador,
        //                Rol="Administrador"
        //            },
        //            new RolEntity
        //            {
        //                IdRol=(int)Enums.Rol.Veterinario,
        //                Rol="Veterinario"
        //            },
        //            new RolEntity
        //            {
        //                IdRol=(int)Enums.Rol.Estandar,
        //                Rol="Estandar"
        //            },
        //        });
        //        await _context.SaveChangesAsync();
        //    }
        //}

        //RolPermisos
        private async Task CheckRolPermissionAsync()
        {
            if (!_context.RolPermissionEntity.Where(x => x.IdRol == (int)Enums.Rol.Administrador).Any())
            {
                var rolesPermisosAdmin = _context.PermissionEntity.Select(x => new RolPermissionEntity
                {
                    IdRol = (int)Enums.Rol.Administrador,
                    IdPermission = x.IdPermission
                }).ToList();

                _context.RolPermissionEntity.AddRange(rolesPermisosAdmin);
                await _context.SaveChangesAsync();
            }
        }
    }
}
