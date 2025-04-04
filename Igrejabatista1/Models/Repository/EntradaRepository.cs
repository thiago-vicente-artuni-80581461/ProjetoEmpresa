using AutoMapper;
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
            var cadastroMembro = _context.CadastroMembro.ToList();
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

        public IEnumerable<EntradaVO> RecuperarListaEntrada(int perfilId, int departamentoTipoId, int? mes, int? ano, string membro)
        {
            return (from e in _context.Entrada
                    join d in _context.DepartamentoTipo on e.DepartamentoTipoId equals d.Id
                    from t in _context.Tipo.Where(ma => ma.Id == e.TipoId).DefaultIfEmpty()
                    from c in _context.CadastroMembro.Where(cm => cm.Id == e.MembroId).DefaultIfEmpty()
                    from v in _context.Evento.Where(ma => ma.Id == e.EventoId).DefaultIfEmpty()
                    where departamentoTipoId == 1 || (e.PerfilId == perfilId && e.DepartamentoTipoId == departamentoTipoId) &&
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
            Entrada entrada = new Entrada
            {
                Id = entradaVO.Id,
                DataCriacao = DateTime.Now,
                MembroId = entradaVO.MembroId == 0 ? null : entradaVO.MembroId,
                TipoId = entradaVO.TipoId == 0 ? null : entradaVO.TipoId,
                EventoId = entradaVO.EventoId == 0 ? null : entradaVO.EventoId,
                ValorTotal = entradaVO.ValorTotal,
                DepartamentoTipoId = entradaVO.DepartamentoTipoId,
                PerfilId = entradaVO.PerfilId,
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

        public IEnumerable<SelectListItem> RecuperarDadosDepartamentoTipo(int perfilId, int departamentoId)
        {

            var tipo = (from d in _context.DepartamentoTipo
                        join p in _context.Perfil on d.Id equals p.DepartamentoTipoId
                        where (departamentoId == 1 || (p.Id == perfilId && p.DepartamentoTipoId == departamentoId))
                        select d).ToList();

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

    }
}
