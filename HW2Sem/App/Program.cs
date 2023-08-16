using System.Net;
using System.Reflection;
using App;
using Core1.Interfaces;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.PostgreSql;
using Hellang.Middleware.ProblemDetails;
using HW2Sem;
using HW2Sem.Entities;
using HW2Sem.Exceptions;
using HW2Sem.Models;
using HW2Sem.Repositories;
using HW2Sem.Repositories.Interfaces;
using Infrastructure;
using Infrastructure1;
using Infrastructure1.Repositories;
using Infrastructure1.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using IAuthenticationService = Core1.Interfaces.IAuthenticationService;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
    .WriteTo.Console()
    .WriteTo.File(new CompactJsonFormatter(), "Log/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information("Starting Web app");
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Services.AddSwaggerGen(conf =>
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory , xmlFile);
        conf.IncludeXmlComments(xmlPath);
    });
    
    builder.Services.AddAuthorization();

    builder.Services.AddControllersWithViews();

    builder.Services.AddRazorPages(opt =>
    {
        opt.Conventions.AuthorizePage("/Athorize");
    });
    
    builder.Services.AddEntityFrameworkNpgsql().AddDbContext<Context>
    (opt =>
        opt.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

    builder.Services.AddHangfire(config => 
        config.UsePostgreSqlStorage(
            builder.Configuration.GetConnectionString("HangfireConnection")));

    builder.Services.AddHangfireServer();

    builder.Services.AddHttpClient();
    
    builder.Services.AddScoped<ICoinHistoryService, CoinHistoryService>();

    builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

    builder.Services.AddScoped<IAuthorService, AuthorService>();
    
    builder.Services.AddScoped<ICoinGeckoApiClient, CoinGeckoApiClient>();

    builder.Services.AddScoped<IPostRepository, PostRepository>();

    builder.Services.AddScoped<IUserRepository, UserRepository>();
    
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    builder.Services.AddScoped<IBuyingСoinService, BuyingCoinService>();

    builder.Services.AddScoped<IPurchaseRepository,PurchaseRepository>();

    builder.Services.AddScoped<IUserCoinsRepository, UserCoinsRepository>();

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Authorize";
            options.AccessDeniedPath = "/accessdenied";
        });
    
    builder.Services.AddAuthorization();
    
    builder.Services.AddScoped<IAuthenticationService, Infrastructure1.Services.AuthenticationService>();

    builder.Services.AddScoped<IRegisterService,RegisterService>();
    
    builder.Services.AddTransient<IMailService, MailService>();
    
    builder.Services.Configure<MailSettings>(
        builder.Configuration.GetSection("MailSettings"));
    
    builder.Services.AddScoped<IReplenishmentOfTheBalanceService,ReplenishmentOfTheBalance>();
    
    builder.Services.AddScoped<IDashboardAuthorizationFilter,MyAuthorizationFilter>();

    builder.Services.AddScoped<IUserService, UserService>();

    builder.Services.AddProblemDetails(options =>
    {
        options.Map<UnauthorizedException>(exception => new ProblemDetails()
        {
            Title = exception.Title,
            Detail = exception.StackTrace,
            Type = exception.GetType().Name,
            Status = 401
        });
        options.Map<BadRequest>(exception => new ProblemDetails()
        {
            Title = exception.Title,
            Detail = exception.StackTrace,
            Type = exception.GetType().Name,
            Status = 400
        });
    });
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    var app = builder.Build();
    
    app.UseStatusCodePages();
    

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler();
        app.UseHsts();
    }
    
    ///Razor Страницы автоматически защищены от XSRF/CSRF.
    
    app.Use(async (context, next) =>
    {
        context.Response.Headers.Add("Content-Security-Policy", 
            "default-src 'self';" +
            " script-src 'self' https://ajax.googleapis.com https://cdn.jsdelivr.net " +
            "'unsafe-inline';" +
            " style-src 'self' https://cdn.jsdelivr.net 'unsafe-inline';" +
            " img-src 'self' data: https://assets.coingecko.com;" +
            " font-src 'self' http://www.w3.org;" +
            " connect-src 'self' https://api.coingecko.com");
        await next();
    });

    app.UseStaticFiles();

    app.UseRouting();

    app.MapRazorPages();

    app.MapControllers();

    app.UseAuthentication();

    app.UseAuthorization();
    
    app.UseHangfireServer();

    app.UseSwagger();
    
    app.UseSwaggerUI();

    app.MapGet(
        "/market",
        async ctx =>
        {
            ctx.Response.ContentType = "text/html";
            await ctx.Response.SendFileAsync("wwwroot/CoinsList.html");
        });
    app.MapGet(
        "/priceCrypto",
        async (ICoinGeckoApiClient apiHelper) =>
        {
            var content = await apiHelper.GetAllCoinsPrice();
            return Results.Stream(await content.ReadAsStreamAsync(), "application/json");
        });

    app.MapGet("/accessdenied" , async (HttpContext ctx) =>
    {
        ctx.Response.StatusCode = 403;
        await ctx.Response.WriteAsync("Access Denied");
    });

    app.MapGet("/logout", async (IAuthenticationService auth)   =>
    {
        auth.LogOut();
        return Results.Ok();
    });

    app.Map("/admin", [Authorize(Roles = "admin")]() =>
    {
        app.MapGet("/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
            string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));
    });
    
    app.UseHangfireDashboard("/dashboard", new DashboardOptions
    {
        Authorization = new [] {new MyAuthorizationFilter()}
    });

    ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;
    
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, $"{ex.Message}");
}
finally
{
    Log.CloseAndFlush();
}