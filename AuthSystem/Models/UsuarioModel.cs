using AuthSystem.Enums;

namespace AuthSystem.Models
{
    public class UsuarioModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public PerfilEnum Perfil { get; set; }
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtulizacao { get; set; }
    }
}
