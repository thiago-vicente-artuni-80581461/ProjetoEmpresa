﻿using IgrejaBatista1.Models.ValueObjects;

namespace IgrejaBatista1.Models.Repository
{
    public interface ICaixaRepository
    {
        Saida BuscarDadosSaida(int id);
        IEnumerable<DepartamentoIgrejaVO> RecuperarListaCaixa(int departamentoTipoId);
        IEnumerable<DepartamentoIgrejaVO> RecuperarListaCaixaRelatorio(int departamentoTipoId, int? mes, int? ano);
        IEnumerable<DepartamentoIgrejaVO> RecuperarListaEntradaRelatorio(int departamentoTipoId, int? mes, int? ano);
        IEnumerable<SaidaDadosVO> RecuperarListaSaida(int departamentoTipoId, string tipoConta, string dataSaida);
        IEnumerable<SaidaDadosVO> RecuperarListaSaidaRelatorio(int departamentoTipoId, int? mes, int? ano);
        void SalvarSaida(SaidaVO saida);
    }
}
