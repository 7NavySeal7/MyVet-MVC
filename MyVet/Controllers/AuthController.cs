using Infraestructure.Entity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MyVet.Domain.Dto;
using MyVet.Domain.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Common.Utils.Constant.Const;

namespace MyVet.Controllers
{
    public class AuthController : Controller
    {
        //Atributo creado a partir de la interfaz
        private readonly IUserServices _userServices;

        //Se le inyecta el servicio por medio de la instancia de la interfaz
        public AuthController(IUserServices userServices)
        {
            _userServices = userServices; //Le pasamos el valor de esa instancia al atributo global
        }


        //Esta acción llama a la vista
        [HttpGet]
        public IActionResult Login()
        {
            return View(new UserDto()); 
        }

        //Esta accion Verifica  los datos ingresados
        [HttpPost]
        public async Task <IActionResult> Login(UserDto user)
        {
            ResponseDto response = _userServices.Login(user);

            if (!response.IsSuccess)
            {
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, response.Message);
                return View(user);
            }
            else
            {
                UserEntity userEntity = (UserEntity)response.Result;

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userEntity.FullName),
                    new Claim(TypeClaims.IdUser, userEntity.IdUser.ToString()),
                    new Claim(TypeClaims.UserName, userEntity.Email),
                    new Claim(TypeClaims.IdRol, string.Join(",",userEntity.RolUserEntities.Select(x=>x.IdRol)))
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                    IsPersistent = false,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                            new ClaimsPrincipal(claimsIdentity),
                                            authProperties);
                return Redirect("/Home/Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserDto());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDto user)
        {
            IActionResult response;

            var result = await _userServices.Register(user);
            if (!result.IsSuccess)
            {
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, result.Message);
                return View(user);
            }
            else
            {
                response = RedirectToAction(nameof(Login));
            }
            return response;
        }

    }
}
