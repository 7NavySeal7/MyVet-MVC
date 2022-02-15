using MyVet.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyVet.Domain.Services.Interface
{
    public interface IPetServices
    {
        List<PetDto> GetAllMyPets(int idUser);
        List<TypePetDto> GetAllTypePet();
        List<SexDto> GetAllSex();
        Task<ResponseDto> DeletePet(int idPet);
        Task<bool> InsertPetAsync(PetDto pet);
        Task<bool> UpdatePetAsync(PetDto pet);
    }
}
