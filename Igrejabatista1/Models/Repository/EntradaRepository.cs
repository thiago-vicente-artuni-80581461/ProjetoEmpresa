using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using IgrejaBatista1.Data;
using IgrejaBatista1.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IgrejaBatista1.Models.Repository
{
    public class EntradaRepository : IEntradaRepository
    {
        private readonly IgrejaBatista1Context _context;
        private IMapper _mapper;

        public EntradaRepository(IgrejaBatista1Context context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public IEnumerable<SelectListItem> RecuperarDadosCadastroMembro()
        {
            var cadastroMembro = _context.CadastroMembro.Where(th => th.Ativo == true).ToList();
            List<SelectListItem> Membros = new List<SelectListItem>();

            SelectListItem l = new SelectListItem
            {
                Text = "--Selecione--",
                Value = "0",
                Selected = true
            };
            Membros.Add(l);

            foreach (var item in cadastroMembro)
            {
                l = new SelectListItem
                {
                    Text = item.NomeCompleto,
                    Value = item.Id.ToString()
                };
                Membros.Add(l);
            }

            return Membros.ToList();
        }

        public IEnumerable<EntradaVO> RecuperarListaEntrada(int perfilId, int departamentoTipoId, int? mes, int? ano, string membro, string usuarioLogin)
        {
            return (from e in _context.Entrada
                    join d in _context.DepartamentoTipo on e.DepartamentoTipoId equals d.Id
                    from p in _context.Perfil.Where(ma => ma.Id == e.PerfilId).DefaultIfEmpty()
                    from l in _context.Login.Where(ma => ma.Id == p.LoginId).DefaultIfEmpty()
                    from t in _context.Tipo.Where(ma => ma.Id == e.TipoId).DefaultIfEmpty()
                    from c in _context.CadastroMembro.Where(cm => cm.Id == e.MembroId).DefaultIfEmpty()
                    from v in _context.Evento.Where(ma => ma.Id == e.EventoId).DefaultIfEmpty()
                    where (departamentoTipoId == 1 || (l.LoginUsuario == usuarioLogin)) &&
                          (mes == null || e.Mes == mes) &&
                          (ano == null || e.Ano == ano) &&
                          (string.IsNullOrEmpty(membro) || c.NomeCompleto.Contains(membro))
                    select new EntradaVO
                    {
                        Id = e.Id,
                        MembroCadastroDescricao = c.NomeCompleto,
                        TipoDescricao = t.Tipo,
                        EventoDescricao = v.Nome,
                        ValorTotal = e.ValorTotal,
                        DepartamentoTipoDescricao = d.Nome,
                        Mes = e.Mes,
                        Ano = e.Ano
                    }).ToList();

        }

        public IEnumerable<SelectListItem> RecuperarDadosContribuicaoTipo()
        {
            var tipoContribuicao = _context.Tipo.ToList();
            List<SelectListItem> tipos = new List<SelectListItem>();

            SelectListItem l = new SelectListItem
            {
                Text = "--Selecione--",
                Value = "0"
            };
            tipos.Add(l);

            foreach (var item in tipoContribuicao)
            {
                l = new SelectListItem
                {
                    Text = item.Tipo,
                    Value = item.Id.ToString()
                };
                tipos.Add(l);
            }

            return tipos.ToList();
        }

        public IEnumerable<SelectListItem> RecuperarDadosEvento()
        {
            var evento = _context.Evento.ToList();
            List<SelectListItem> eventos = new List<SelectListItem>();

            SelectListItem l = new SelectListItem
            {
                Text = "--Selecione--",
                Value = "0"
            };
            eventos.Add(l);

            foreach (var item in evento)
            {
                l = new SelectListItem
                {
                    Text = item.Nome,
                    Value = item.Id.ToString()
                };
                eventos.Add(l);
            }

            return eventos.ToList();
        }

        public void SalvarEntrada(EntradaVO entradaVO)
        {
            int recuperarPerfil = 0;
            if (!entradaVO.PerfilTipo.Equals("Administrador"))
            {
                recuperarPerfil = RecuperarPerfilCorreto(entradaVO.UsuarioLogin, entradaVO.DepartamentoTipoId);
            }

            Entrada entrada = new Entrada
            {
                Id = entradaVO.Id,
                DataCriacao = DateTime.Now,
                MembroId = entradaVO.MembroId == 0 ? null : entradaVO.MembroId,
                TipoId = entradaVO.TipoId == 0 ? null : entradaVO.TipoId,
                EventoId = entradaVO.EventoId == 0 ? null : entradaVO.EventoId,
                ValorTotal = entradaVO.ValorTotal,
                DepartamentoTipoId = entradaVO.DepartamentoTipoId,
                PerfilId = recuperarPerfil == 0 ? 1 : recuperarPerfil,
                Mes = entradaVO.Mes,
                Ano = entradaVO.Ano
            };
            if (entradaVO.Id != 0)
            {
                _context.Update(entrada);
            }
            else
            {
                _context.Add(entrada);
            }

            _context.SaveChanges();
        }

        public IEnumerable<SelectListItem> RecuperarDadosDepartamentoTipo(string usuarioLogin, int departamentoId)
        {

            var tipo = (from d in _context.DepartamentoTipo
                        from p in _context.Perfil.Where(th => th.DepartamentoTipoId == d.Id).DefaultIfEmpty()
                        from lo in _context.Login.Where(th => th.Id == p.LoginId).DefaultIfEmpty()
                        where (departamentoId == 1 || (lo.LoginUsuario == usuarioLogin))
                        select d).Distinct().ToList();
         
            List<SelectListItem> tipos = new List<SelectListItem>();

            SelectListItem l = new SelectListItem
            {
                Text = "--Selecione--",
                Value = "0"
            };
            tipos.Add(l);

            foreach (var item in tipo)
            {
                l = new SelectListItem
                {
                    Text = item.Nome,
                    Value = item.Id.ToString()
                };
                tipos.Add(l);
            }

            return tipos.ToList();
        }

        public Entrada RecuperarInformacoesEntrada(int id)
        {
            return _context.Entrada.Where(th => th.Id == id).FirstOrDefault();
        }

        public void ExcluirEntrada(EntradaVO registro)
        {
            Entrada entrada = new Entrada();
            entrada = _context.Entrada.Where(th => th.Id == registro.Id).FirstOrDefault();

            _context.Remove(entrada);
            _context.SaveChanges();
        }

        public Entrada VerificarEntradaMembro(int id)
        {
            return _context.Entrada.Where(th => th.MembroId == id).FirstOrDefault();
        }

        public int RecuperarPerfilCorreto(string usuarioLogin, int departamentoId)
        {
            var recuperarPerfil = (from p in _context.Perfil
                                   join l in _context.Login on p.LoginId equals l.Id
                                   where l.LoginUsuario == usuarioLogin && p.DepartamentoTipoId == departamentoId
                                   select new Perfil
                                   {
                                       Id = p.Id
                                   }).FirstOrDefault();

            return recuperarPerfil.Id;
        }

    }
}
