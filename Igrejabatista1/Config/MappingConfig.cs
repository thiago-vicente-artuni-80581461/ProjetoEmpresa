using AutoMapper;
using IgrejaBatista1.Models;
using IgrejaBatista1.Models.ValueObjects;

namespace IgrejaBatista1.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<LoginVO, Login>();
                config.CreateMap<Login, LoginVO>();
                config.CreateMap<CadastroMembrosVO, CadastroMembro>();
                config.CreateMap<CadastroMembro, CadastroMembrosVO>();
                config.CreateMap<Entrada, EntradaVO>();
                config.CreateMap<EntradaVO, Entrada>();
                config.CreateMap<TipoContribuicao, TipoContribuicao>();
                config.CreateMap<Evento, Evento>();
            });
            return mappingConfig;
        }
    }
}
