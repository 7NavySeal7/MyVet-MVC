using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models.Vet;
using MyVet.Domain.Dto;
using MyVet.Domain.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVet.Domain.Services
{
    public class PetServices: IPetServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork; 
        #endregion

        #region Builder
        public PetServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        //Funcion que devuelve el listado de mascotas que tiene un cliente
        //Para saber las mascotas de un usuario, necesito saber el Id del Usuario
        public List<PetDto> GetAllMyPets(int idUser)
        {
            //var pets2 = _unitOfWork.PetRepository.FindAll(x => x.UserPetEntity.IdUser == idUser).ToList();
            var pets = _unitOfWork.PetRepository.FindAll(x => x.UserPetEntity.IdUser == idUser,
                                                        p => p.UserPetEntity,
                                                        p => p.SexEntity,
                                                        p => p.TypePetEntity).ToList();

            List<PetDto> list = pets.Select(x => new PetDto 
            { 
                Name = x.Name,
                DateBorns = x.DateBorns,
                Id = x.IdPet,
                IdTypePet = x.IdTypePet,
                IdSex = x.IdSex,
                Sexo = x.SexEntity.Sex,
                TypePet = x.TypePetEntity.TypePet,
                Edad = CalculateAge(x.DateBorns)
            }).ToList();

            return list;
        }

        private string CalculateAge(DateTime dateBorn)
        {
            string result = string.Empty;

            int age = Math.Abs((DateTime.Now.Month - dateBorn.Month) + 12 * (DateTime.Now.Year - dateBorn.Year));

            if (age != 0)
                result = $"{age} meses";
            else
            {
                TimeSpan resultDate = DateTime.Now.Date - dateBorn.Date;
                result = $"{resultDate.Days} días";
            }

            return result;
        }

        //Esta función me trae los tipos de datos
        public List<TypePetDto> GetAllTypePet()
        {
            //Esta primera lista captura todos los valores de la entidad TypeRepository
            List<TypePetEntity> typePets = _unitOfWork.TypePetRepository.GetAll().ToList();

            //Esta Lista es similar a un foreach
            List<TypePetDto> list = typePets.Select(x => new TypePetDto
            {
                IdTypePet = x.IdTypePet,
                TypePet = x.TypePet
            }).ToList();

            //Esta segunda captura los valores anteriormente capturados.
            //foreach (var item in typePets)
            //{
            //    TypePetDto typePetDto = new TypePetDto()
            //    {
            //        IdTypePet = item.IdTypePet,
            //        TypePet = item.TypePet
            //    };
            //    list.Add(typePetDto);
            //}
            return list;
        }

        public List<SexDto> GetAllSex()
        {
            List<SexEntity> sexs = _unitOfWork.SexRepository.GetAll().ToList();
            List<SexDto> list = sexs.Select(x => new SexDto
            {
                IdSex = x.IdSex,
                Sex = x.Sex
            }).ToList();

            return list;
        } 

        public async Task<ResponseDto> DeletePet(int idPet)
        {
            ResponseDto response = new ResponseDto();

            _unitOfWork.PetRepository.Delete(idPet);
            response.IsSuccess = await _unitOfWork.Save() > 0;
            if (response.IsSuccess)
            {
                response.Message = "Se elimino correctamente la mascota";
            }
            else
            {
                response.Message = "Hubo un error en la eliminación, por favor vuelva a intentarlo";
            }
            return response;
        }

        public async Task<bool> InsertPetAsync(PetDto pet)
        {
            //En base de datos recibimos las entidad,no el dto de PetDto

            //Primero guardar la entidad interna que contiene el userPetEntity
            //Y luego guarda el userPetEntity, esto lo relaciona.
            UserPetEntity userPetEntity = new UserPetEntity()
            {
                IdUser = pet.IdUser,
                PetEntity = new PetEntity()
                {
                    DateBorns = pet.DateBorns,
                    IdSex = pet.IdSex,
                    IdTypePet = pet.IdTypePet,
                    Name = pet.Name
                } 
            };
            _unitOfWork.UserPetRepository.Insert(userPetEntity);
            return await _unitOfWork.Save() > 0;
        }
        
        public async Task<bool> UpdatePetAsync(PetDto pet)
        {
            bool result = false;
            
            PetEntity petEntity = _unitOfWork.PetRepository.FirstOrDefault(x => x.IdPet == pet.Id);

            if (petEntity != null)
            {
                petEntity.DateBorns = pet.DateBorns;
                petEntity.IdSex = pet.IdSex;
                petEntity.IdTypePet = pet.IdTypePet;
                petEntity.Name = pet.Name;

                _unitOfWork.PetRepository.Update(petEntity);

                result = await _unitOfWork.Save() > 0;
            }

            return result;
        }

        #endregion
    }
}
