using AutoMapper;
using IgrejaBatista1.Data;
using IgrejaBatista1.Models.ValueObjects;

namespace IgrejaBatista1.Models.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IgrejaBatista1Context _context;
        private IMapper _mapper;

        public LoginRepository(IgrejaBatista1Context context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public bool ValidarLoginUsuario(LoginVO login)
        {
            bool retorno = false;
            string senhaConvertida = string.Empty;

            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(login.Senha);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                senhaConvertida = Convert.ToHexString(hashBytes).ToLower();
            }

            List<Login> loginR = _context.Login.ToList();

            var registro = _mapper.Map<List<LoginVO>>(loginR);

            var l = registro.Where(th => th.LoginUsuario == login.LoginUsuario && th.Senha == senhaConvertida).FirstOrDefault();


            if (l != null)
            {
                return retorno = true;
            }
            return retorno;
        }

        public Perfil RecuperarLoginPerfil(string loginUsuario)
        {
            return (from pt in _context.PerfilTipo
             join p in _context.Perfil on pt.Id equals p.TipoPerfilId
             join pl in _context.PerfilLogin on p.Id equals pl.PerfilId
             join l in _context.Login on pl.LoginId equals l.Id
             where l.LoginUsuario == loginUsuario
             select new Perfil
             {
                 Id = pl.PerfilId,
                 DepartamentoTipoId = p.DepartamentoTipoId
             }).FirstOrDefault();     
        }

        public void SalvarRegistroAcesso(LoginVO login)
        {
            var loginR = _context.Login.FirstOrDefault(th => th.LoginUsuario == login.LoginUsuario);

            RegistroAcesso novo = new RegistroAcesso()
            {
                LoginId = loginR.Id,
                Usuario = login.LoginUsuario,
                DataRegistro = DateTime.Now
            };

            _context.RegistroAcesso.Add(novo);
            _context.SaveChanges();
        }
    }
}
