using IgrejaBatista1.Data;
using IgrejaBatista1.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IgrejaBatista1.Models.Repository
{
    public class PatrimonioRepository : IPatrimonioRepository
    {
        public readonly IgrejaBatista1Context context = null;

        public PatrimonioRepository(IgrejaBatista1Context context)
        {
            this.context = context;
        }

        public IEnumerable<CadastroPatrimonioVO> RecuperarListaPatrimonio(int departamentoTipoId, string codigo, string nome)
        {
            return (from e in context.CadastroPatrimonio
                    join d in context.DepartamentoTipo on e.DepartamentoTipoId equals d.Id
                    where departamentoTipoId == 1 || (e.DepartamentoTipoId == departamentoTipoId) &&
                          (string.IsNullOrEmpty(codigo) || e.Codigo.Contains(codigo)) &&
                          (string.IsNullOrEmpty(nome) || e.Nome.Contains(nome))
                    select new CadastroPatrimonioVO
                    {
                        Id = e.Id,
                        DepartamentoDescricao = d.Nome,
                        Codigo = e.Codigo,
                        Nome = e.Nome,
                        DepartamentoTipoId = e.DepartamentoTipoId,
                        Descricao = e.Descricao,
                        Foto = e.Foto,
                        Setor = e.Setor,
                        DataBaixa = e.DataBaixa,
                        TamanhoFoto = e.TamanhoFoto
                    }).ToList();
        }

        public void SalvarPatrimonio(CadastroPatrimonioVO patrimonio)
        {
            CadastroPatrimonio p = new CadastroPatrimonio
            {
                Id = patrimonio.Id,
                Codigo = patrimonio.Codigo,
                Nome = patrimonio.Nome,
                DepartamentoTipoId = patrimonio.DepartamentoTipoId,
                Descricao = patrimonio.Descricao,
                Foto = patrimonio.Foto,
                Setor = patrimonio.Setor,
                DataBaixa = patrimonio.DataBaixa,
                DataCriacao =  DateTime.Now,
                TamanhoFoto = patrimonio.TamanhoFoto
            };

            if (patrimonio.Id != 0)
            {
                context.CadastroPatrimonio.Update(p);
                context.SaveChanges();
            }
            else
            {
                context.CadastroPatrimonio.Add(p);
                context.SaveChanges();
            }
        }
    }
}
