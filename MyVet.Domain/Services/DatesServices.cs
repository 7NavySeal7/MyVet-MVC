using Common.Utils.Enums;
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
    public class DatesServices: IDatesServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Builder
        public DatesServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods

        public List<DatesDto> GetAllDates(int idUser)
        {
            //Esta consulta me trae las citas del usuario logueado y también los registros que sean nulos.
            var dates = _unitOfWork.DatesRepository.FindAll(x => (x.idUserVet==idUser || x.idUserVet==null),
                                                            p => p.PetEntity.UserPetEntity,
                                                            p => p.PetEntity.TypePetEntity,
                                                            p => p.ServicesEntity,
                                                            p => p.StateEntity).ToList();


            List<DatesDto> list = dates.Select(x => new DatesDto
            {
                IdDate = x.IdDate,
                Date = x.Date,
                Contact = x.Contact,
                IdServices = x.IdServices,
                IdPet = x.IdPet,
                IdState = x.IdState,
                ClosingDate = x.ClosingDate,
                StrClosingDate = x.ClosingDate == null ? "No disponible" : x.ClosingDate.Value.ToString("yyyy-MM-dd"),
                NamePet = $"{ x.PetEntity.Name} [{x.PetEntity.TypePetEntity.TypePet}]",
                NameService = x.ServicesEntity.Service,
                NameState = x.StateEntity.State,
                Description = x.Description,
                StrDate = x.Date.ToString("yyyy-MM-dd")
            }).OrderBy(f=>f.Date).ToList();

            return list;
        }

        public List<DatesDto> GetAllMyDates(int idUser)
        {
            var dates = _unitOfWork.DatesRepository.FindAll(x => x.PetEntity.UserPetEntity.IdUser == idUser,
                                                            p => p.PetEntity.UserPetEntity,
                                                            p => p.PetEntity.TypePetEntity,
                                                            p => p.ServicesEntity,
                                                            p => p.StateEntity).ToList();

            List<DatesDto> list = dates.Select(x => new DatesDto
            {
                IdDate = x.IdDate,
                Date = x.Date,
                Contact = x.Contact,
                IdServices = x.IdServices,
                IdPet = x.IdPet,
                IdState = x.IdState,
                ClosingDate = x.ClosingDate,
                StrClosingDate = x.ClosingDate == null ? "No disponible" : x.ClosingDate.Value.ToString("yyyy-MM-dd"),
                NamePet = $"{ x.PetEntity.Name} [{x.PetEntity.TypePetEntity.TypePet}]",
                NameService = x.ServicesEntity.Service,
                NameState = x.StateEntity.State,
                Description = x.Description,
                StrDate = x.Date.ToString("yyyy-MM-dd")
            }).OrderBy(f=>f.Date).ToList();

            return list;
        }
        
        public List<PetDto> GetAllMyPets(int idUser)
        {
            var pets = _unitOfWork.PetRepository.FindAll(x => x.UserPetEntity.IdUser == idUser,
                                                        p => p.UserPetEntity);

            List<PetDto> list = pets.Select(x=>new PetDto{
                Id = x.IdPet,
                Name = x.Name
            }).ToList();

            return list;
        }

        public List<ServicesDto> GetAllServices()
        {
            List<ServicesEntity> services = _unitOfWork.ServicesRepository.GetAll().ToList();
            List<ServicesDto> list = services.Select(x => new ServicesDto
            {
                IdService = x.IdService,
                Service = x.Service,
                Description = x.Description

            }).ToList();

            return list;
        }

        public async Task<bool> InsertDatesAsync(DatesDto dates)
        {
            DatesEntity datesEntity = new DatesEntity()
            {
                Date = dates.Date,
                Contact = dates.Contact,
                IdServices = dates.IdServices,
                IdPet = dates.IdPet,
                IdState = (int)Enums.State.CitaActiva,
                Description = dates.Description
            };

            _unitOfWork.DatesRepository.Insert(datesEntity);
            return await _unitOfWork.Save() > 0;
        }

        //private DateTime? CalculateClosing(DateTime? date)
        //{
        //    DateTime aumento = (DateTime)date;
        //    aumento = aumento.AddDays(1);
        //    return aumento;
        //}

        public async Task<ResponseDto> DeleteDatesAsync(int idDate)
        {
            ResponseDto response = new ResponseDto();
            _unitOfWork.DatesRepository.Delete(idDate);
            response.IsSuccess = await _unitOfWork.Save() > 0; ;

            if (response.IsSuccess)
            {
                response.Message = "Se elimino la cita correctamente";
            }
            else
            {
                response.Message = "Hubo un error en la eliminación, por favor vuelva a intentarlo";
            }

            return response;
        }

        public async Task<bool> UpdateDatesAsync(DatesDto dates)
        {
            bool result = false;

            DatesEntity datesEntity = _unitOfWork.DatesRepository.FirstOrDefault(x => x.IdDate == dates.IdDate);
            
            if (datesEntity != null)
            {
                datesEntity.Date = dates.Date;
                datesEntity.Contact = dates.Contact;
                datesEntity.IdServices = dates.IdServices;
                datesEntity.IdPet = dates.IdPet;
                datesEntity.IdState = (int)Enums.State.CitaActiva;
                datesEntity.Description = dates.Description;

                _unitOfWork.DatesRepository.Update(datesEntity);
                result = await _unitOfWork.Save() > 0;  
            }
            return result;
        }        
        
        public async Task<bool> UpdateDatesVetAsync(DatesDto dates)
        {
            bool result = false;

            DatesEntity datesEntity = _unitOfWork.DatesRepository.FirstOrDefault(x => x.IdDate == dates.IdDate);
            
            if (datesEntity != null)
            {
                datesEntity.Date = dates.Date;
                datesEntity.Contact = dates.Contact;
                datesEntity.IdServices = dates.IdServices;
                datesEntity.IdPet = dates.IdPet;
                datesEntity.Description = dates.Description;
                datesEntity.idUserVet = dates.idUserVet;
                datesEntity.ClosingDate = DateTime.Now;
                datesEntity.IdState = (int)Enums.State.CitaFinalizada;
                datesEntity.Observation = dates.Observation;

                _unitOfWork.DatesRepository.Update(datesEntity);
                result = await _unitOfWork.Save() > 0;  
            }
            return result;
        }

        public async Task<bool> CancelDatesAsync(int idDates, int? idUservet)
        {
            bool result = false;

            DatesEntity dates = _unitOfWork.DatesRepository.FirstOrDefault(x => x.IdDate == idDates);

            if(dates != null)
            {
                dates.IdState = (int)Enums.State.CitaCancelada;
                dates.ClosingDate = DateTime.Now;
                //Si idUserVet es igual a nulo, entonces que sea nulo o si que envie el valor de idUserVet.
                dates.idUserVet = idUservet ?? null;

                _unitOfWork.DatesRepository.Update(dates);
                result = await _unitOfWork.Save() > 0;
            }

            return result;
        }

        #endregion
    }
}
