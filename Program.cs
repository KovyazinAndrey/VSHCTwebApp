using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;
using VSHCTwebApp.Components;
using VSHCTwebApp.Components.Account;
using VSHCTwebApp.Components.Services;
using VSHCTwebApp.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http.Features;

namespace VSHCTwebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Настройка логирования
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();
            builder.Logging.AddFile("logs/app-{Date}.txt");

            builder.Services.AddDbContextFactory<VSHCTwebAppContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("VSHCTwebAppContext") ?? throw new InvalidOperationException("Connection string 'VSHCTwebAppContext' not found.")));

            builder.Services.AddQuickGridEntityFrameworkAdapter();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // Настройка максимального размера загружаемого файла
            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 1024 * 1024 * 2; // 2MB
            });

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            });
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDbContextFactory<VSHCTwebAppContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("VSHCTwebAppContext") ??
                    throw new InvalidOperationException(
            "Connection string 'VSHCTwebAppContext' not found.")));


            builder.Services.AddDbContextFactory<VSHCTwebAppContext>(options =>
                options.UseSqlServer(
            builder.Configuration.GetConnectionString("VSHCTwebAppContext") ??
                throw new InvalidOperationException(
                "Connection string 'VSHCTwebAppContext' not found.")));


            builder.Services.AddQuickGridEntityFrameworkAdapter();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            builder.Services.AddSingleton<NoteService>();
            builder.Services.AddSingleton<CommandService>();

            var app = builder.Build();

            // Создаем папку для аватаров при запуске
            var avatarsPath = Path.Combine(builder.Environment.WebRootPath, "images", "avatars");
            if (!Directory.Exists(avatarsPath))
            {
                Directory.CreateDirectory(avatarsPath);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
    app.UseMigrationsEndPoint();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(builder.Environment.WebRootPath, "images")),
                RequestPath = "/images"
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(builder.Environment.WebRootPath, "uploads")),
                RequestPath = "/uploads"
            });
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            app.Run();
        }
    }
}
