using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Services;
using Server.Interface;
using Server.SignalRHub;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add UserManagerDB service
builder.Services.AddDbContext<UserManagerDB>(option =>
    option.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, minor: 3, 0))));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

// Add session services
builder.Services.AddSession(option =>
{
    option.IOTimeout = TimeSpan.FromMinutes(60);
    option.Cookie.Name = ".AspNet";
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
    option.Cookie.SameSite = SameSiteMode.None;
    option.Cookie.SecurePolicy = CookieSecurePolicy.Always;

});

builder.Services.AddHostedService<CleanupService>();

builder.Services.AddTransient<IEmailService, EmailManager>();
builder.Services.AddTransient<IHashService, HashService>();
builder.Services.AddTransient<ICryptoService, CryptoService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<ICleanupService, CleanupService>();
builder.Services.AddScoped<IViewRenderService, ViewRenderService>();

builder.Services.AddSignalR();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Environment.IsDevelopment();

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

app.UseCors("AllowSpecificOrigins");

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapHub<RegisterHub>("/RegisterHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
