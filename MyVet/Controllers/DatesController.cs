using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyVet.Domain.Dto;
using MyVet.Domain.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Common.Utils.Constant.Const;

namespace MyVet.Controllers
{
    [Authorize]
    public class DatesController : Controller
    {
        #region Attribute
        private readonly IDatesServices _datesServices;
        #endregion

        #region Builder
        public DatesController(IDatesServices datesServices)
        {
            _datesServices = datesServices;
        }
        #endregion

        #region Views
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }         
        
        [HttpGet]
        public IActionResult DatesVet()
        {
            return View();
        }
        #endregion

        #region Methods
        [HttpGet]
        public IActionResult GetAllDates()
        {
            var user = HttpContext.User;
            string idUser = user.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value;

            List<DatesDto> list = _datesServices.GetAllDates(Convert.ToInt32(idUser));
            return Ok(list);
        }

        [HttpGet]
        public IActionResult GetAllMyDates()
        {
            var user = HttpContext.User;
            string idUser = user.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value;

            List<DatesDto> list = _datesServices.GetAllMyDates(Convert.ToInt32(idUser));
            return Ok(list);
        }

        //[HttpGet]
        //public IActionResult GetAllMyPets()
        //{
        //    var user = HttpContext.User;
        //    string idUser = user.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value;

        //    List<PetDto> list = _datesServices.GetAllMyPets(Convert.ToInt32(idUser));
        //    return Ok(list);
        //}

        [HttpGet]
        public IActionResult GetAllService()
        {
            List<ServicesDto> response = _datesServices.GetAllServices();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> InsertDates(DatesDto datesDto)
        {
            bool response = await _datesServices.InsertDatesAsync(datesDto);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDates(int idDate)
        {
            ResponseDto response = await _datesServices.DeleteDatesAsync(idDate);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDates(DatesDto datesDto)
        {
            bool response = await _datesServices.UpdateDatesAsync(datesDto);
            return Ok(response);
        }        
        
        [HttpPut]
        public async Task<IActionResult> UpdateDatesVet(DatesDto datesDto)
        {
            var user = HttpContext.User;
            string idUser = user.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value;
            datesDto.idUserVet = Convert.ToInt32(idUser);

            bool response = await _datesServices.UpdateDatesVetAsync(datesDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> CancelDates(int idDate)
        {
            bool result = await _datesServices.CancelDatesAsync(idDate, idUservet: null);

            return Ok(result);
        }        
        
        [HttpPut]
        public async Task<IActionResult> CancelDatesVet(int idDate)
        {
            var user = HttpContext.User;
            string idUser = user.Claims.FirstOrDefault(x => x.Type == TypeClaims.IdUser).Value;

            bool result = await _datesServices.CancelDatesAsync(idDate, Convert.ToInt32(idUser));

            return Ok(result);
        }
        #endregion
    }
}
