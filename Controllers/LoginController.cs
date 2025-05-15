using Microsoft.AspNetCore.Mvc;
using New_Tech.Models;
using New_Tech.Repositorio;


namespace New_Tech.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginRepositorio _loginRepositorio;

        public LoginController(LoginRepositorio loginRepositorio)
        {
            _loginRepositorio = loginRepositorio;
        }


        public IActionResult ChecarUsuario()
        {
            return View();
        }



        [HttpPost]
        public IActionResult ChecarUsuario(string email, string senha)
        {
            var usuario = _loginRepositorio.ObterUsuario(email);
            if (usuario != null && usuario.Senha == senha)
            {
                return RedirectToAction("Index", "Produto");
            }

            ModelState.AddModelError("", "Erro, informações invalidas");
            return View();
        }
    }
}

