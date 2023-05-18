using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Security.Claims;

namespace AuthSystem.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                return Json(new { Msg = "Usuário já logado!"});
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logar(string username, string senha)
        {
            MySqlConnection mySqlConnections = new MySqlConnection("server=localhost;database=usuariosdb;uid=root;password=3966458");
            await mySqlConnections.OpenAsync();

            MySqlCommand mySqlCommand = mySqlConnections.CreateCommand();
            mySqlCommand.CommandText = $"Select * FROM usuarios WHERE username = '{username}'AND senha = '{senha}'";

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

                if(await reader.ReadAsync()) 
                {
                    int usuarioId = reader.GetInt32(0);
                    string nome = reader.GetString(1);

                List<Claim> direitosAcesso = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier,usuarioId.ToString()),
                        new Claim(ClaimTypes.Name,nome)
                    };

                    var identity = new ClaimsIdentity(direitosAcesso,"identity.Login");
                    var userPrincipal = new ClaimsPrincipal(new[] { identity });

                    await HttpContext.SignInAsync(userPrincipal);

                    return Json(new { Msg = "Usuário Logado com sucesso!"});
                }
            
            return Json(new { Msg = "Usuário Não encontrado! verifique suas credenciais" });
        }
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }
            return RedirectToAction("Index", "Login");
        }
    }
}
