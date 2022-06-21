using E_Library.ControllersApi;
using E_Library.Data;
using E_Library.GraphQL;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

// If using Kestrel:
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;

});

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services
builder.Services.AddPooledDbContextFactory<LibraryContext>(opt => opt.UseSqlServer("Server=DESKTOP-SH6HTAN;Database=library;Trusted_Connection=True;"));
    
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            SecurityKey signingKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("MySuperSecretKey"));

            options.TokenValidationParameters =
                new TokenValidationParameters {
                    ValidIssuer = "https://localhost:5000",
                    ValidAudience = "https://localhost:5000",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                };
        });

//builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<UsersController>();
builder.Services.AddScoped<Query>();

builder.Services.AddDbContext<LibraryContext>(opt => opt.UseSqlServer("Server=DESKTOP-SH6HTAN;Database=library;Trusted_Connection=True;"));



builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthorization();



//GraphQl services

builder.Services
    .AddGraphQLServer()
    .ModifyRequestOptions(o =>
    {
        o.Complexity.Enable = true;
        o.Complexity.MaximumAllowed = 300000;
        o.Complexity.ApplyDefaults = true;
        o.Complexity.DefaultComplexity = 2;
        o.Complexity.DefaultResolverComplexity = 3;
    })
    .RegisterDbContext<LibraryContext>(DbContextKind.Pooled)
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddMaxExecutionDepthRule(30, true)
    .AddAuthorization()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddHttpRequestInterceptor<HttpRequestInterceptor>(); ;



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});



app.Run();
