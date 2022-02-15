using Infraestructure.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyVet.Domain.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVet.Controllers
{
    //Obliga a que el usuario este logueada para acceder a estas rutas
    [Authorize]
    public class UserController : Controller
    {
        //Atributo creado a partir de la interfaz
        private readonly IUserServices _userServices;
        private readonly IRolServices _rolServices;

        //Se le inyecta el servicio por medio de la instancia de la interfaz
        public UserController(IUserServices userServices, IRolServices rolServices)
        {
            _userServices = userServices; //Le pasamos el valor de esa instancia al atributo global
            _rolServices = rolServices;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            //Listado de usuarios
            List<UserEntity> users = _userServices.GetAll();
            return View(users);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            IActionResult response;

            if (id == null)
                response = NotFound();

            UserEntity user = _userServices.GetUser((int)id);
            if (user == null)
                response = NotFound();
            else
                response = View(user);
            
            return response;
        }        
        
        [HttpPost]
        public async Task<IActionResult> Edit(UserEntity user)
        {
            IActionResult response;

            bool result = await _userServices.UpdateUser(user);
            
            if (result)
            {
                response = RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "No se pudo Actualizar el Usuario");
                response = RedirectToAction(nameof(Index));
            }
            return response;
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            IActionResult response;

            if (id == null)
                response = NotFound();

            UserEntity user = _userServices.GetUser((int)id);
            if (user == null)
                response = NotFound();
            else
                response = View(user);

            return response;
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int idUser)
        {
            IActionResult response;
            if (idUser == 0)
            {
                response = NotFound();
            }
            else
            {
                bool result = await _userServices.DeleteUser(idUser);
                if (result)
                {
                    response = RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No se pudo eliminar el Usuario");
                    response = RedirectToAction(nameof(Index));
                }
            }
            return response;
        }

        [HttpGet]
        public IActionResult Create()
        {
            //'Son las sesiones' => El select List me trae los datos en forma de lista
            ViewData["Roles"] = new SelectList(_rolServices.GetAll(), "IdRol", "Rol");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserEntity user)
        {
            IActionResult response;

            var result = await _userServices.CreateUser(user);

            if (result.IsSuccess)
            {
                response = RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["Roles"] = new SelectList(_rolServices.GetAll(), "IdRol", "Rol");
                ModelState.AddModelError(string.Empty, result.Message);
                response = View(user);
            }
            return response;
        }

    }
}
