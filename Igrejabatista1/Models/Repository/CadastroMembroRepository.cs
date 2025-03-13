using AutoMapper;
using IgrejaBatista1.Data;
using IgrejaBatista1.Models.ValueObjects;

namespace IgrejaBatista1.Models.Repository
{
    public class CadastroMembroRepository : ICadastroMembroRepository
    {
        private readonly IgrejaBatista1Context _context;
        private IMapper _mapper;

        public CadastroMembroRepository(IgrejaBatista1Context context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<CadastroMembro> RecuperarListaMembros()
        {
            List<CadastroMembro> membros = _context.CadastroMembro.ToList();

            var lista = _mapper.Map<List<CadastroMembro>>(membros);

            return lista;
        }

        public void SalvarCadastroMembro(CadastroMembro cadastroMembro)
        {
            if (cadastroMembro.Id != 0)
            {
                cadastroMembro.DataEmissao = DateTime.Now;
                _context.Update(cadastroMembro);
            }
            else
            {
                 cadastroMembro.DataEmissao = DateTime.Now;
                _context.Add(cadastroMembro);
            }
            _context.SaveChanges();  
        }

        public void ExcluirCadastroMembro(CadastroMembro cadastroMembro)
        {
            _context.Remove(cadastroMembro);
            _context.SaveChanges();
        }
    }
}
