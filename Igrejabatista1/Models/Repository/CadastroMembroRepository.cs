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
            List<CadastroMembrosVO> membros = (from c in _context.CadastroMembro
                                               from ca in _context.Cargos.Where(th => th.Id == c.CargoId).DefaultIfEmpty()
                                               select new CadastroMembrosVO
                                               {
                                                   Id = c.Id,
                                                   CargoId = c.CargoId,
                                                   CargoNome = ca.Nome,
                                                   CPF = c.CPF,
                                                   RG = c.RG,
                                                   DataBatismo = c.DataBatismo,
                                                   DataEmissao = c.DataEmissao,
                                                   DataNascimento = c.DataNascimento,
                                                   NomeCompleto = c.NomeCompleto,
                                                   NomeMae = c.NomeMae,
                                                   NomePai = c.NomePai
                                               }).ToList();

            var lista = _mapper.Map<List<CadastroMembrosVO>>(membros);

            return lista;
        }

        public void SalvarCadastroMembro(CadastroMembro cadastroMembro)
        {
            if (cadastroMembro.CargoId == 0)
                cadastroMembro.CargoId = null;
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

        public void ExcluirCadastroMembro(CadastroMembrosVO registro)
        {
            CadastroMembro vo = new CadastroMembro()
            {
                Id = registro.Id,
                CargoId = registro.CargoId,
                CPF = registro.CPF,
                DataBatismo = registro.DataBatismo,
                DataEmissao = registro.DataEmissao,
                DataNascimento = registro.DataNascimento,
                NomeCompleto = registro.NomeCompleto,
                NomeMae = registro.NomeMae,
                NomePai = registro.NomePai,
                RG = registro.RG
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

            Perfil perfil = new Perfil()
            {
                TipoPerfilId = login.PerfilTipoId,
                DepartamentoTipoId = login.DepartamentoTipoId,
                DataCriacao = DateTime.Now
            };

            _context.Perfil.Add(perfil);
            _context.SaveChanges();

            var recuperarPerfil = _context.Perfil.OrderByDescending(th => th.Id).FirstOrDefault();

            Login log = new Login()
            {
                LoginUsuario = login.LoginUsuario,
                Senha = senha,
                DataCriacao = DateTime.Now
            };

            _context.Login.Add(log);
            _context.SaveChanges();

            var recuperarLogin = _context.Login.OrderByDescending(th => th.Id).FirstOrDefault();

            PerfilLogin pl = new PerfilLogin()
            {
                LoginId = recuperarLogin.Id,
                PerfilId = recuperarPerfil.Id,
                DataCriacao = DateTime.Now
            };

            _context.PerfilLogin.Add(pl);
            _context.SaveChanges();
        }

    }
}
