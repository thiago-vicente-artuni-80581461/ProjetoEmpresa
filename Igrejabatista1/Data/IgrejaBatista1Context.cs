using IgrejaBatista1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace IgrejaBatista1.Data;

public class IgrejaBatista1Context : DbContext
{
    public IgrejaBatista1Context() { }
    public IgrejaBatista1Context(DbContextOptions<IgrejaBatista1Context> options) : base(options) { }
    public DbSet<Login> Login { get; set; }

    public DbSet<Entrada> Entrada { get; set; }

    public DbSet<TipoContribuicao> Tipo { get; set; }

    public DbSet<Evento> Evento { get; set; }

    public DbSet<DepartamentoTipo> DepartamentoTipo { get; set; }

    public DbSet<CadastroMembro> CadastroMembro { get; set; }

    public DbSet<Perfil> Perfil { get; set; }

    public DbSet<PerfilTipo> PerfilTipo { get; set; }

    public DbSet<PerfilLogin> PerfilLogin { get; set; }

    public DbSet<Saida> Saida { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}

