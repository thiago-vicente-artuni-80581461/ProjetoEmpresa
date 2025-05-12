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

        public PerfilVO RecuperarLoginPerfil(string loginUsuario)
        {
            return (from pt in _context.PerfilTipo
             join p in _context.Perfil on pt.Id equals p.TipoPerfilId
             join l in _context.Login on p.LoginId equals l.Id
             where l.LoginUsuario == loginUsuario
             select new PerfilVO
             {
                 Id = p.Id,
                 DepartamentoTipoId = p.DepartamentoTipoId,
                 PerfilTipo = pt.Nome
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

        public IEnumerable<LoginVO> RecuperarUsuariosLogin(string nome, string usuarioLogin, int departamentoTipoId)
        {
           var query = (from pt in _context.PerfilTipo
                        from p in _context.Perfil.Where(ma => ma.TipoPerfilId == pt.Id).DefaultIfEmpty()
                        from l in _context.Login.Where(ma => ma.Id == p.LoginId).DefaultIfEmpty()
                        from d in _context.DepartamentoTipo.Where(ma => ma.Id == p.DepartamentoTipoId).DefaultIfEmpty()
                    where (departamentoTipoId == 1 || (string.IsNullOrEmpty(nome) || l.LoginUsuario.Contains(nome)) || l.LoginUsuario == usuarioLogin)
                    select new LoginVO
                    {
                        Id = l.Id,
                        LoginUsuario = l.LoginUsuario,
                        PerfilLogin = pt.Nome,
                        Senha = l.Senha,
                        PerfilTipoId = pt.Id
                    }).Distinct().ToList();

            foreach (var item in query)
            {
                var perfil = (from p in _context.Perfil 
                              join dp in _context.DepartamentoTipo on p.DepartamentoTipoId equals dp.Id
                              where p.LoginId == item.Id
                              select new LoginVO
                              {
                                  DepartamentoTipoDescricao = dp.Nome
                              }).ToList();

                foreach (var i in perfil)
                {
                    item.DepartamentoTipoDescricao += i.DepartamentoTipoDescricao + "; ";
                }
            }
            return query;
        }
    }
}
