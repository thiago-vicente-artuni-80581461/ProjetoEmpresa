using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IgrejaBatista1.Data;
using IgrejaBatista1.Models.Services;
using IgrejaBatista1.Models.Repository;
using AutoMapper;
using IgrejaBatista1.Config;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IgrejaBatistaContextConnection") ?? throw new InvalidOperationException("Connection string 'IgrejaBatista1ContextConnection' not found.");;

builder.Services.AddDbContext<IgrejaBatista1Context>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IgrejaBatista1Context>();
builder.Services.AddRazorPages();
builder.Services.AddMvc();

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<IloginService, LoginService>();
builder.Services.AddTransient<ILoginRepository, LoginRepository>();

builder.Services.AddTransient<ICadastroMembroService, CadastroMembroService>();
builder.Services.AddTransient<ICadastroMembroRepository, CadastroMembroRepository>();

builder.Services.AddTransient<IEntradaService, EntradaService>();
builder.Services.AddTransient<IEntradaRepository, EntradaRepository>();

builder.Services.AddTransient<ICaixaService, CaixaService>();
builder.Services.AddTransient<ICaixaRepository, CaixaRepository>();

builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddRazorPages().AddSessionStateTempDataProvider();
builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}")
    .WithStaticAssets();

app.Run();
