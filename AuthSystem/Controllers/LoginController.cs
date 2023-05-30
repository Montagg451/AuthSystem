using AuthSystem.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace AuthSystem.Controllers
{
    public class LoginController : Controller
    {
       
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            // Defina as configurações de conexão com o MySQL
            string connectionString = "server=localhost;port=3308;database=usuariosdb;uid=root;pwd=3966458;";

            try
            {
                // Crie uma conexão com o MySQL
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Consulta SQL para verificar as credenciais do usuário
                    string query = "SELECT COUNT(*) FROM usuario WHERE Username = @username AND Senha = @senha";

                    // Crie um comando SQL
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", user.Login);
                        command.Parameters.AddWithValue("@senha", user.Senha);

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        if (count > 0)
                        {
                            // Credenciais válidas, redirecionar para a página principal
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            // Credenciais inválidas, exibir mensagem de erro na página de login
                            ViewBag.ErrorMessage = "Credenciais inválidas. Por favor, tente novamente.";
                            return View();
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Ocorreu um erro durante a conexão ou consulta
                ViewBag.ErrorMessage = "Ocorreu um erro durante o login. Por favor, tente novamente mais tarde.";
                return View();
            }
        }
    }

}
