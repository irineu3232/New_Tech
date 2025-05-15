using Microsoft.AspNetCore.Mvc;
using New_Tech.Models;
using New_Tech.Repositorio;
using Org.BouncyCastle.Tls;


namespace New_Tech.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginRepositorio _loginRepositorio;

        public LoginController(LoginRepositorio loginRepositorio)
        {
            _loginRepositorio = loginRepositorio;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string senha)
        {
            var usuario = _loginRepositorio.ObterUsuario(Email);
            if (usuario != null && usuario.Senha == senha)
            {
                return RedirectToAction("Index", "Produto");
            }
           
            ModelState.AddModelError("", "Erro, informações invalidas");
            return View();
        }
    }
}