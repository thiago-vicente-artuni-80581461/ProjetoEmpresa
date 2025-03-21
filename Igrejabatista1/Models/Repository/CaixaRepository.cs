using AutoMapper;
using IgrejaBatista1.Data;
using IgrejaBatista1.Models.ValueObjects;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            return _context.DepartamentoIgreja.FromSqlRaw("EXEC DadosCaixaIgreja @departamentoTipoId", new SqlParameter("@departamentoTipoId", departamentoTipoId)).ToList();

        }

        public IEnumerable<SaidaDadosVO> RecuperarListaSaida(int departamentoTipoId, string tipoConta, string dataSaida, string dataSaidaFim)
        {
            return _context.SaidaVO.FromSqlRaw("EXEC BuscarDadosSaida @DepartamentoTipoId, @TipoConta, @DataSaida, @DataSaidaFim", 
                 new SqlParameter("@DepartamentoTipoId", departamentoTipoId),
                 new SqlParameter("@TipoConta", tipoConta),
                 new SqlParameter("@DataSaida", dataSaida),
                 new SqlParameter("@DataSaidaFim", dataSaidaFim)).ToList();
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

        public Saida BuscarDadosSaida(int id)
        {
            return _context.Saida.Find(id);
        }
    }
}
