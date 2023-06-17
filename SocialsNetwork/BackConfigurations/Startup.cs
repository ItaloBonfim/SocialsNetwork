using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using SocialsNetwork.Models.Class;
using System.Text;

public class Startup
{
    public IConfiguration Configuration { get; set; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
    public void ConfigureDatabaseConnection(IServiceCollection services, string project)
    {
        if (project.Equals("Dev"))
        {
            services.AddDbContext<AppDbContext>(
               str => str.UseSqlServer(
                   Configuration.GetConnectionString("SqlServer")));
        }
        else
        {
            services.AddDbContext<AppDbContext>(
            str => str.UseSqlServer(
                Configuration.GetConnectionString("SqlServer")));
        }
    }

     public void ConfigureIdentityFramework(IServiceCollection services)
    {
        //services.AddIdentity<IdentityUser, IdentityRole>(
            services.AddIdentity<ApplicationUser, IdentityRole>(
            Options =>
            {
                Options.Password.RequireNonAlphanumeric = false;
                Options.Password.RequireDigit = false;
                Options.Password.RequireUppercase = false;
                Options.Password.RequireLowercase = false; // adicionando 03/06/2023
                Options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<AppDbContext>();
            
        
    }
    public void ConfigureCustomQueries(IServiceCollection services)
    {
        //class
        services.AddScoped<FindAllUsersWithClaims>();
        services.AddTransient<FindUserAndReturnAll>();
        services.AddScoped<FindUserById>();
        services.AddScoped<GetAllFollowsUsers>();
        services.AddScoped<GetAllFriendInvite>();
        services.AddScoped<GetAllFriends>();
        services.AddScoped<FindBlockListUsers>();

        //socials
        services.AddScoped<FindPublicationComments>();
        services.AddScoped<FindReactionsComment>();
        services.AddScoped<FindPublicationsWithClaims>();
        services.AddScoped<FindPublicationReaction>();
        services.AddScoped<FindSubComments>();

        //streams
        services.AddScoped<FindChannelBlockList>();
        services.AddScoped<FindAllStreamCategories>();
        services.AddScoped<FindChannelConfigurations>();
        services.AddScoped<FindChannels>();
        services.AddScoped<FindAllStreamCategories>();
        services.AddScoped<FindChannelSubscribes>();
    }
    public void ConfigureAuthorization(IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {   
            /* Para Habilitar o Swagger novamente esse bloco de codigo deve estar habilitado -- //
             options.FallbackPolicy = new AuthorizationPolicyBuilder()
             .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
             .RequireAuthenticatedUser()
             .Build();
            // -- Para Habilitar o Swagger novamente esse bloco de codigo deve estar habilitado -- */

            options.AddPolicy("AdminPolice", p =>
            p.RequireAuthenticatedUser().RequireClaim("AdminLevel"));
            /*
              Para usar politicas de segurança é necessario:
              No endpoint responsavel
              inserir o Authorize
              [Authorize(Policy = Name Of policy)]
             */
        });
    }
    public void ConfigureAuthentication(IServiceCollection services)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateActor = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.FromMinutes(10),
                ValidIssuer = Configuration["JwtBearerTokenSettigns:Issuer"],
                ValidAudience = Configuration["JwtBearerTokenSettigns:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                Configuration["JwtBearerTokenSettigns:SecretKey"]))
            };
        });
    }
}