using MyVet.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyVet.Domain.Services.Interface
{
    public interface IDatesServices
    {
        List<PetDto> GetAllMyPets(int idUser);

        List<DatesDto> GetAllDates(int idUser);

        List<ServicesDto> GetAllServices();

        List<DatesDto> GetAllMyDates(int idUser);

        Task<bool> InsertDatesAsync(DatesDto dates);

        Task<ResponseDto> DeleteDatesAsync(int idDates);

        Task<bool> UpdateDatesAsync(DatesDto dates);

        Task<bool> UpdateDatesVetAsync(DatesDto dates);

        Task<bool> CancelDatesAsync(int idDates, int? idUservet);
    }
}
