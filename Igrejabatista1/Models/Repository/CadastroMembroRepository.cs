using AutoMapper;
using IgrejaBatista1.Data;
using IgrejaBatista1.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

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

        public List<CadastroMembrosVO> RecuperarListaMembros()
        {
            string cargo = string.Empty;

            List<CadastroMembrosVO> membros = (from c in _context.CadastroMembro
                                               select new CadastroMembrosVO
                                               {
                                                   Id = c.Id,
                                                   CPF = c.CPF,
                                                   RG = c.RG,
                                                   DataBatismo = c.DataBatismo,
                                                   DataEmissao = c.DataEmissao,
                                                   DataNascimento = c.DataNascimento,
                                                   NomeCompleto = c.NomeCompleto,
                                                   NomeMae = c.NomeMae,
                                                   NomePai = c.NomePai,
                                                   Ativo = c.Ativo
                                               }).OrderBy(th => th.NomeCompleto).ToList();

            var lista = _mapper.Map<List<CadastroMembrosVO>>(membros);

            foreach (var item in membros)
            {
                cargo = string.Empty;

                var cargoCadastroMembro = _context.CargoCadastroMembro.Where(th => th.MembroId == item.Id).ToList();

                foreach (var i in cargoCadastroMembro)
                {
                    var nomeCargo = _context.Cargos.FirstOrDefault(th => th.Id == i.CargoId);

                    cargo += nomeCargo.Nome + "; ";
                }
                item.CargoNome = cargo;
            }

            return lista;
        }

        public void SalvarCadastroMembro(CadastroMembrosVO cadastroMembro)
        {
            if (cadastroMembro.Id != 0)
            {
                cadastroMembro.DataEmissao = DateTime.Now;

                CadastroMembro alterar = new CadastroMembro()
                {
                    Id = cadastroMembro.Id,
                    NomeCompleto = cadastroMembro.NomeCompleto,
                    CPF = cadastroMembro.CPF,
                    RG = cadastroMembro.RG,
                    DataBatismo = cadastroMembro.DataBatismo,
                    DataEmissao = cadastroMembro.DataEmissao,
                    DataNascimento = cadastroMembro.DataNascimento,
                    NomeMae = cadastroMembro.NomeMae,
                    NomePai = cadastroMembro.NomePai,
                    Ativo = cadastroMembro.Ativo
                };

                _context.CadastroMembro.Update(alterar);

                var recuperarUltimoMembro = _context.CargoCadastroMembro.FirstOrDefault(th => th.MembroId == cadastroMembro.Id);

                if (recuperarUltimoMembro != null)
                {
                    var verificarCargoMembro = _context.CargoCadastroMembro.Where(th => th.MembroId == alterar.Id).ToList();

                    if (verificarCargoMembro != null && verificarCargoMembro.Count > 0)
                    {
                        foreach (var i in verificarCargoMembro)
                        {
                            _context.CargoCadastroMembro.Remove(i);
                            _context.SaveChanges();
                        }

                    }
                    foreach (var item in cadastroMembro.CargoId)
                    {
                        if (item != "0")
                        {
                            CargoCadastroMembro cargoMembro = new CargoCadastroMembro()
                            {
                                CargoId = Convert.ToInt32(item),
                                MembroId = recuperarUltimoMembro.MembroId
                            };

                            _context.CargoCadastroMembro.Add(cargoMembro);
                            _context.SaveChanges();
                        }
                    }
                }
                else
                {
                    foreach (var item in cadastroMembro.CargoId)
                    {
                        CargoCadastroMembro cargoMembro = new CargoCadastroMembro()
                        {
                            CargoId = Convert.ToInt32(item),
                            MembroId = cadastroMembro.Id
                        };

                        _context.CargoCadastroMembro.Add(cargoMembro);
                        _context.SaveChanges();
                    }
                }
            }

            else
            {
                cadastroMembro.DataEmissao = DateTime.Now;

                CadastroMembro novo = new CadastroMembro()
                {
                    NomeCompleto = cadastroMembro.NomeCompleto,
                    CPF = cadastroMembro.CPF,
                    RG = cadastroMembro.RG,
                    DataBatismo = cadastroMembro.DataBatismo,
                    DataEmissao = cadastroMembro.DataEmissao,
                    DataNascimento = cadastroMembro.DataNascimento,
                    NomeMae = cadastroMembro.NomeMae,
                    NomePai = cadastroMembro.NomePai,
                    Ativo = cadastroMembro.Ativo
                };
                _context.CadastroMembro.Add(novo);
                _context.SaveChanges();

                var recuperarUltimoMembro = _context.CadastroMembro.OrderByDescending(th => th.Id).FirstOrDefault();

                if (recuperarUltimoMembro != null)
                {
                    foreach (var item in cadastroMembro.CargoId)
                    {
                        if (item != "0")
                        {
                            CargoCadastroMembro cargoMembro = new CargoCadastroMembro()
                            {
                                CargoId = Convert.ToInt32(item),
                                MembroId = recuperarUltimoMembro.Id
                            };

                            _context.CargoCadastroMembro.Add(cargoMembro);
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }

        public void ExcluirCadastroMembro(CadastroMembrosVO registro)
        {
            CadastroMembro vo = new CadastroMembro()
            {
                Id = registro.Id,
                CargoId = Convert.ToInt32(registro.CargoId),
                CPF = registro.CPF,
                DataBatismo = registro.DataBatismo,
                DataEmissao = registro.DataEmissao,
                DataNascimento = registro.DataNascimento,
                NomeCompleto = registro.NomeCompleto,
                NomeMae = registro.NomeMae,
                NomePai = registro.NomePai,
                RG = registro.RG,
                Ativo = registro.Ativo
            };
            _context.CadastroMembro.Remove(vo);
            _context.SaveChanges();
        }

        public IEnumerable<SelectListItem> RecuperarListaCargos()
        {
            var cadastroMembro = _context.Cargos.ToList();
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
                    Text = item.Nome,
                    Value = item.Id.ToString()
                };
                Membros.Add(l);
            }

            return Membros.ToList();
        }

        public IEnumerable<SelectListItem> RecuperarPerfilTipo()
        {
            var cadastroMembro = _context.PerfilTipo.ToList();
            List<SelectListItem> usuarios = new List<SelectListItem>();

            SelectListItem l = new SelectListItem
            {
                Text = "--Selecione--",
                Value = "0",
                Selected = true
            };
            usuarios.Add(l);

            foreach (var item in cadastroMembro)
            {
                l = new SelectListItem
                {
                    Text = item.Nome,
                    Value = item.Id.ToString()
                };
                usuarios.Add(l);
            }

            return usuarios.ToList();
        }

        public IEnumerable<SelectListItem> RecuperarDepartamentoTipo()
        {
            var cadastroMembro = _context.DepartamentoTipo.ToList();
            List<SelectListItem> tipos = new List<SelectListItem>();

            SelectListItem l = new SelectListItem
            {
                Text = "--Selecione--",
                Value = "0",
                Selected = true
            };
            tipos.Add(l);

            foreach (var item in cadastroMembro)
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

        public void SalvarCadastroUsuario(LoginVO login)
        {
            string senha = string.Empty;

            using (MD5 md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(login.Senha));

                StringBuilder sb = new StringBuilder();
                foreach (byte byteValue in data)
                {
                    sb.Append(byteValue.ToString("x2"));
                }

                senha = sb.ToString();
            }

            Login log = new Login()
            {
                LoginUsuario = login.LoginUsuario,
                Senha = senha,
                DataCriacao = DateTime.Now
            };

            _context.Login.Add(log);
            _context.SaveChanges();

            var recuperarLogin = _context.Login.OrderByDescending(th => th.Id).FirstOrDefault();

            if (login.DepartamentoTipoId.Count > 0)
            {
                foreach (var item in login.DepartamentoTipoId)
                {
                    if (item != "0")
                    {
                        Perfil perfil = new Perfil()
                        {
                            TipoPerfilId = login.PerfilTipoId,
                            DepartamentoTipoId = Convert.ToInt32(item),
                            DataCriacao = DateTime.Now,
                            LoginId = recuperarLogin.Id
                        };

                        _context.Perfil.Add(perfil);
                        _context.SaveChanges();
                    }
                    else
                    {
                        Perfil perfil = new Perfil()
                        {
                            TipoPerfilId = login.PerfilTipoId,
                            DepartamentoTipoId = 0,
                            DataCriacao = DateTime.Now,
                            LoginId = recuperarLogin.Id
                        };
                        _context.Perfil.Add(perfil);
                        _context.SaveChanges();
                    }
                }
            }
            else
            {
                Perfil perfil = new Perfil()
                {
                    TipoPerfilId = login.PerfilTipoId,
                    DepartamentoTipoId = 0,
                    DataCriacao = DateTime.Now,
                    LoginId = recuperarLogin.Id
                };
                _context.Perfil.Add(perfil);
                _context.SaveChanges();
            }
        }

        public List<string> RecuperarCargosCadastroMembro(int id)
        {
            return _context.CargoCadastroMembro.Where(th => th.MembroId == id).Select(th => th.CargoId.ToString()).ToList();
        }
    }
}
