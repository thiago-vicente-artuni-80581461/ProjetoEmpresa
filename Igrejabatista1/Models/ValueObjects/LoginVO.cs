namespace IgrejaBatista1.Models.ValueObjects
{
    public class LoginVO
    {
        public int Id { get; set; }
        public string LoginUsuario { get; set; }
        public string Senha { get; set; }

        public string Mensagem { get; set; }
    }
}
