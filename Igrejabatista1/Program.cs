using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IgrejaBatista1.Data;
using IgrejaBatista1.Models.Services;
using IgrejaBatista1.Models.Repository;
using AutoMapper;
using IgrejaBatista1.Config;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddDbContext<IgrejaBatista1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IgrejaBatistaContextConnection"))
);

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IgrejaBatista1Context>();

builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();
builder.Services.AddRazorPages().AddSessionStateTempDataProvider();

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

builder.Services.AddTransient<IPatrimonioService, PatrimonioService>();
builder.Services.AddTransient<IPatrimonioRepository, PatrimonioRepository>();

builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddRazorPages().AddSessionStateTempDataProvider();
builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Cookies["jwtToken"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }
                return Task.CompletedTask;
            }
        };

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("L+H57jscLmFhbncdg2BxzKq/WPqOqaQzKsXYu0aL7gA=")),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}")
    .WithStaticAssets();

app.Run();
