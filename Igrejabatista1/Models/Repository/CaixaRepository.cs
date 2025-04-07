using AutoMapper;
using Humanizer;
using IgrejaBatista1.Data;
using IgrejaBatista1.Models.ValueObjects;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

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
            return _context.DepartamentoIgreja.FromSqlRaw("EXEC DadosCaixaIgreja @departamentoId", new SqlParameter("@departamentoId", departamentoTipoId)).ToList();

        }

        public IEnumerable<SaidaDadosVO> RecuperarListaSaida(int departamentoTipoId, string tipoConta, string dataSaida)
        {
            string dSaida = null;

            if (!string.IsNullOrEmpty(dataSaida))
            {
                DateTime date = Convert.ToDateTime(dataSaida);
                dSaida = date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            return _context.SaidaVO.FromSqlRaw("EXEC BuscarDadosSaida @DepartamentoTipoId = {0}, @TipoConta = {1}, @DataSaida = {2}", departamentoTipoId, tipoConta, dSaida).ToList();
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

        public IEnumerable<DepartamentoIgrejaVO> RecuperarListaCaixaRelatorio(int departamentoTipoId, int? mes, int? ano)
        {
            return _context.DepartamentoIgreja.FromSqlRaw("EXEC DadosCaixaIgrejaRelatorio @departamentoId = {0}, @mes = {1}, @ano = {2}", departamentoTipoId, mes, ano).AsNoTracking().ToList();
        }
        public IEnumerable<DepartamentoIgrejaVO> RecuperarListaEntradaRelatorio(int departamentoTipoId, int? mes, int? ano)
        {
            return _context.DepartamentoIgreja.FromSqlRaw("EXEC CaixaIgrejaTipoContribuicao @departamentoId = {0}, @mes = {1}, @ano = {2}", departamentoTipoId, mes, ano).AsNoTracking().ToList();
        }

        public IEnumerable<SaidaDadosVO> RecuperarListaSaidaRelatorio(int departamentoTipoId, int? mes, int? ano)
        {
            return _context.SaidaVO.FromSqlRaw( "SELECT S.*, DT.Nome AS DepartamentoTipoDescricao FROM Saida S INNER JOIN " +
                                               "               DepartamentoTipo DT ON DT.Id = S.DepartamentoTipoId " +
                                               "          WHERE ({0} = 1 OR S.DepartamentoTipoId = {0} ) AND " +
                                               "                ({1} IS NULL OR DATEPART(MONTH, S.DataSaida) = {1} ) AND" +
                                               "                ({2} IS NULL OR DATEPART(YEAR, S.DataSaida) = {2} )  ", departamentoTipoId, mes, ano).AsNoTracking().ToList();
        }
    }
}
