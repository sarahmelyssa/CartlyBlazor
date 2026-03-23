using Cartly_Blazor.Components;
using Cartly_Blazor.Models;
using Cartly_Blazor.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Supabase;
using Client = Supabase.Client;

var builder = WebApplication.CreateBuilder(args);

// 🔥 ENUM (mantém isto)
NpgsqlConnection.GlobalTypeMapper.MapEnum<AppRole>("app_role");

// 🔥 RAZOR
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 🔥 DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔥 SUPABASE
builder.Services.AddSingleton(sp =>
{
    var url = builder.Configuration["Supabase:Url"];
    var key = builder.Configuration["Supabase:Key"];

    var options = new SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = false
    };

    var client = new Client(url!, key!, options);
    client.InitializeAsync().Wait();
    return client;
});

// 🔥 AUTH SERVICE
builder.Services.AddScoped<AuthService>();

// ========================================
// 🔐 AUTHENTICATION + AUTHORIZATION
// ========================================

// 👉 autenticação (cookies simples)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
    });

// 👉 autorização
builder.Services.AddAuthorization();

// ========================================

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 🔥 ORDEM IMPORTA (MUITO)
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
