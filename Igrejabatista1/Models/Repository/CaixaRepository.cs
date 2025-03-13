using AutoMapper;
using IgrejaBatista1.Data;
using IgrejaBatista1.Models.ValueObjects;

namespace IgrejaBatista1.Models.Repository
{
    public class CaixaRepository : ICaixaRepository
    {
        private readonly IgrejaBatista1Context _context;
        private IMapper _mapper;

        public CaixaRepository(IgrejaBatista1Context context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public IEnumerable<DepartamentoIgrejaVO> RecuperarListaCaixa(int departamentoTipoId)
        {
            var lista = (from di in _context.DepartamentoTipo
                         join en in _context.Entrada on di.Id equals en.DepartamentoTipoId
                         where (departamentoTipoId == 1 || di.Id == departamentoTipoId)
                         group en by new { di.Nome} into a
                         select new DepartamentoIgrejaVO
                         {
                             DeparamentoTipoDescricao = a.Key.Nome,
                             ValorTotal = a.Sum(di => di.ValorTotal)
                         });

            return lista.ToList();

        }

        public IEnumerable<SaidaVO> RecuperarListaSaida(int departamentoTipoId , string tipoConta, string dataSaida, string dataSaidaFim)
        {
            var lista = (from di in _context.Saida
                         join en in _context.DepartamentoTipo on di.DepartamentoTipoId equals en.Id
                         where ((departamentoTipoId == 1 || di.Id == departamentoTipoId) ||
                               (string.IsNullOrEmpty(tipoConta) || di.TipoConta.Contains((tipoConta))) ||
                               (string.IsNullOrEmpty(dataSaida) || di.DataSaida >= Convert.ToDateTime(dataSaida)) ||
                               (string.IsNullOrEmpty(dataSaidaFim) || di.DataSaida <= Convert.ToDateTime(dataSaidaFim)))
                         select new SaidaVO
                         {
                             DepartamentoTipoDescricao = en.Nome,
                             Id = di.Id,
                             Descricao = di.Descricao,
                             TipoConta = di.TipoConta,
                             DepartamentoTipoId = di.DepartamentoTipoId,
                             ValorPago = di.ValorPago,
                             DataCriacao = di.DataCriacao,
                             DataSaida = di.DataSaida
                         });

            return lista.ToList();

        }

        public void SalvarSaida(SaidaVO saidaVO)
        {
            Saida saida = new Saida
            {
                Id = saidaVO.Id,
                DataCriacao = DateTime.Now,
                DepartamentoTipoId = saidaVO.DepartamentoTipoId,
                TipoConta = saidaVO.TipoConta,
                Descricao = saidaVO.Descricao,
                ValorPago = saidaVO.ValorPago,
                DataSaida = saidaVO.DataSaida
            };
            if (saidaVO.Id != 0)
            {
                _context.Update(saida);
            }
            else
            {
                _context.Add(saida);
            }

            _context.SaveChanges();
        }
    }
}
