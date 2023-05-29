
using AuthSystem.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

using System.Security.Claims;


namespace AuthSystem.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public ActionResult Logar(Models.User user)
        {
            if (ModelState.IsValid)
            {
                // Conectar ao MySQL e verificar as credenciais do usuário
                // Substitua os detalhes da conexão pelo seu próprio host, nome de usuário, senha, etc.

                // Exemplo de conexão MySQL usando a biblioteca MySql.Data
                var connectionString = "server=localhost;uid=root;password=3966458;database=usuariosdb";
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    var query = "SELECT * FROM usuario WHERE Username = @username AND Password = @senha";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", user.Login);
                        command.Parameters.AddWithValue("@password", user.Senha);

                        var count = Convert.ToInt32(command.ExecuteScalar());

                        if (count > 0)
                        {
                            // Autenticação bem-sucedida, redirecionar para a página inicial
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

                ModelState.AddModelError("", "Invalid username or password.");
            }

            // Se as credenciais forem inválidas ou ocorrer algum erro, retornar à página de login
            return View(user);
        }

    }
        
}
