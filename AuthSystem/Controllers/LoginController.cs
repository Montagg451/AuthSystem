using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace AuthSystem.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
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
                    return Json(new { Msg = "Usuário Logado com sucesso!"});
                }
            
            return Json(new { Msg = "Usuário Não encontrado! verifique suas credenciais" });
        }
    }
}
