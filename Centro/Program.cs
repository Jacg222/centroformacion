using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Configurar la autenticación
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Ruta de inicio de sesión
        options.LogoutPath = "/Account/Logout"; // Ruta de cierre de sesión
    });

// Agregar el servicio de sesiones
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".YourApp.Session"; // Nombre de la cookie de sesión
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Tiempo de expiración de la sesión
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.Use(async (context, next) =>
    {
        context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
        await next();
    });
}

app.UseStaticFiles();
app.UseRouting();

// Habilitar la autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Agregar el middleware de sesiones
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Index}/{id?}");

app.Run();
