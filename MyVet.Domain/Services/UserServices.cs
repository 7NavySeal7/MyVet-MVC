 using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models;
using MyVet.Domain.Dto;
using MyVet.Domain.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Utils.Helpers;
using static Common.Utils.Enums.Enums;

namespace MyVet.Domain.Services
{
    public class UserServices : IUserServices
    {
        #region Attribute
        //atributo--Interfaz de solo lectura
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Builder
        //El constructor recibe la instacia de unitOfWork por medio de la interfaz
        public UserServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; //Le pasamos el valor de esa instancia a atributo global.
        }
        #endregion

        #region Authentication
        public ResponseDto Login(UserDto user)
        {
            //Esta clase nos sirve para almacenar valores en sus propiedades.
            ResponseDto response = new ResponseDto();
            //Con esto verificamos la similitud de contraseña y usuario
            UserEntity result = _unitOfWork.UserRepository.FirstOrDefault(x => x.Email == user.UserName 
                                                                            && x.Password == user.Password,
                                                                            r=>r.RolUserEntities);
            if(result==null)
            {
                response.Message = "Usuario o contraseña inválida";
                response.IsSuccess = false;
            }
            else
            {
                response.Result = result;
                response.Message = "Usuario Autenticado";
                response.IsSuccess = true;
            }

            return response;
        }
        #endregion

        #region Methods Crud
        //Esta función sirve para devolverme una lista de usuarios
        //El metodo GetAll es del repositorio
        //El UserRepository es de la unidad de trabajo
        public List<UserEntity> GetAll()
        {
            return _unitOfWork.UserRepository.GetAll().ToList();
        }

        //Esta función sirve para traerme un usuario especifico
        public UserEntity GetUser(int idUser)
        {
            return _unitOfWork.UserRepository.FirstOrDefault(x => x.IdUser == idUser);
        }

        public async Task<bool> UpdateUser(UserEntity user)
        {
            //Esto sirve para consultar el usuario
            UserEntity _user = GetUser(user.IdUser);

            _user.Name = user.Name;
            _user.LastName = user.LastName;
            _unitOfWork.UserRepository.Update(_user);

            return await _unitOfWork.Save() > 0;
            //return _unitOfWork.UserRepository.FirstOrDefault(x=>x.IdUser == idUser);
        }

        public async Task<bool> DeleteUser(int idUser)
        {
            //Con el patron repositorio tenemos un delete
            _unitOfWork.UserRepository.Delete(idUser);
            //El metodo save proviene de la unidad de trabajo
            return await _unitOfWork.Save() > 0;
        }

        public async Task<ResponseDto> CreateUser(UserEntity data)
        {
            ResponseDto result = new ResponseDto();

            //Esta condicional me devuelve un booleano, según la validación que se realize en utils.
            if (Utils.ValidateEmail(data.Email))
            {
                //Si es igual a nulo significa que no hay nigún otro email.
                if (_unitOfWork.UserRepository.FirstOrDefault(x => x.Email == data.Email) == null)
                {
                    int idRol = data.IdUser;
                    data.Password = "1234";
                    data.IdUser = 0;
                    RolUserEntity rolUser = new RolUserEntity()
                    {
                        IdRol = idRol,
                        UserEntity = data
                    };
                    _unitOfWork.RolUserRepository.Insert(rolUser);
                    result.IsSuccess = await _unitOfWork.Save() > 0;
                }
                else
                {
                    result.Message = "Email ya se encuentra registrado, utilizar otro!";
                }
            }
            else
            {
                result.Message = "Usuario con Email inválido";
            }
            // _unitOfWork.UserRepository.Update(_user);
            return result;
            //return _unitOfWork.UserRepository.FirstOrDefault(x=>x.IdUser == idUser);
        }

        public async Task<ResponseDto> Register(UserDto data)
        {
            ResponseDto result = new ResponseDto();

            //Esta condicional me devuelve un booleano, según la validación que se realize en utils.
            if (Utils.ValidateEmail(data.UserName))
            {
                //Si es igual a nulo significa que no hay nigún otro email.
                if (_unitOfWork.UserRepository.FirstOrDefault(x => x.Email == data.UserName) == null)
                {

                    RolUserEntity rolUser = new RolUserEntity()
                    {
                        IdRol = Rol.Estandar.GetHashCode(),
                        UserEntity = new UserEntity()
                        {
                            Email = data.UserName,
                            LastName = data.LastName,
                            Name = data.Name,
                            Password = data.Password
                        }
                    };

                    _unitOfWork.RolUserRepository.Insert(rolUser);
                    result.IsSuccess = await _unitOfWork.Save() > 0;
                }
                else
                    result.Message = "Email ya se encuestra registrado, utilizar otro!";
            }
            else
            {
                result.Message = "Usuarioc con Email Inválido";
            }
            return result;
        }
        #endregion
    }
}