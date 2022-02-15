using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models;
using MyVet.Domain.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyVet.Domain.Services
{
    public class RolServices: IRolServices
    {
        //atributo--Interfaz de solo lectura
        private readonly IUnitOfWork _unitOfWork;

        //El constructor recibe la instacia de unitOfWork por medio de la interfaz
        public RolServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; //Le pasamos el valor de esa instancia a atributo global.
        }

        public List<RolEntity> GetAll()
        {
            return _unitOfWork.RolRepository.GetAll().ToList();
        }
    }
}
