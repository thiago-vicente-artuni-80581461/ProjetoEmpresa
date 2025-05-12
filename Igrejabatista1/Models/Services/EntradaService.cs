using IgrejaBatista1.Models.Repository;
using IgrejaBatista1.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IgrejaBatista1.Models.Services
{
    public class EntradaService : IEntradaService
    {
        public readonly IEntradaRepository _entradaRepository;
        public EntradaService(IEntradaRepository entradaRepository)
        {
            _entradaRepository = entradaRepository;
        }
        public IEnumerable<SelectListItem> RecuperarDadosCadastroMembro()
        {
            return _entradaRepository.RecuperarDadosCadastroMembro();
        }

        public IEnumerable<EntradaVO> RecuperarListaEntrada(int perfilId, int departamentoTipoId, int? mes, int? ano, string membro, string usuarioLogin)
        {
            return _entradaRepository.RecuperarListaEntrada(perfilId, departamentoTipoId, mes, ano, membro, usuarioLogin);
        }

        public IEnumerable<SelectListItem> RecuperarDadosContribuicaoTipo()
        {
            return _entradaRepository.RecuperarDadosContribuicaoTipo();
        }

        public IEnumerable<SelectListItem> RecuperarDadosEvento()
        {
            return _entradaRepository.RecuperarDadosEvento();
        }
        public void SalvarEntrada(EntradaVO entrada)
        {
            _entradaRepository.SalvarEntrada(entrada);
        }

        public IEnumerable<SelectListItem> RecuperarDadosDepartamentoTipo(string usuarioLogin, int departamentoId)
        {
            return _entradaRepository.RecuperarDadosDepartamentoTipo(usuarioLogin, departamentoId);
        }

        public Entrada RecuperarInformacoesEntrada(int id)
        {
            return _entradaRepository.RecuperarInformacoesEntrada(id);
        }

        public void ExcluirEntrada(EntradaVO registro)
        {
            _entradaRepository.ExcluirEntrada(registro);
        }

        public Entrada VerificarEntradaMembro(int id)
        {
           return _entradaRepository.VerificarEntradaMembro(id);
        }

        public int RecuperarPerfilCorreto(string usuarioLogin, int departamentoId)
        {
            return _entradaRepository.RecuperarPerfilCorreto(usuarioLogin, departamentoId);
        }
    }
}
