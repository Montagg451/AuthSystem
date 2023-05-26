using AuthSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AuthSystem.Controllers
{
    public class LoginController : Controller
    {

        

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Index", "Home");
                }
                TempData["MensagemErro"] = $"Usuário e/ou senha inválidos(s). Por favor tente novamente.";

                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos realizar seu login, tente novamente  {erro.Message}";
                return RedirectToAction("Index");
            }
            
        }    
    } 
}
