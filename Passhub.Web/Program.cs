using Blazorise;
using Blazorise.Bulma;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Passhub.Domain.Identity;
using Passhub.Domain.Passwords;
using Passhub.EfCore;
using Passhub.EfCore.Repositories;
using Passhub.Web.Components;
using Passhub.Web.Components.Account;
using Passhub.Web.Services.Identity;
using Passhub.Web.Services.Passwords;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("Default") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

if (Environment.GetEnvironmentVariable("DB_CONNECTION") != null)
{
    connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
}
else
{
    Console.WriteLine("Not fount env DB_CONNECTION");
}

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddIdentityCore<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddTransient<ICurrentUser, CurrentUser>();
builder.Services.AddTransient<IPasswordEncryptor, PasswordEncryptor>();
builder.Services.AddTransient<IPasswordManager, PasswordManager>();
builder.Services.AddScoped<IPasswordService, PasswordService>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IPasswordRepository, PasswordRepository>();

builder.Services
    .AddBlazorise()
    .AddBulmaProviders();

var app = builder.Build();

await DatabaseInitializer.Seed(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}


app.UseAntiforgery();
app.UseStaticFiles();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

app.Run();

//CKTzPw3k2!EV5tb