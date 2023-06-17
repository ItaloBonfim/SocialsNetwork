using SocialsNetwork.BackConfigurations;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);



startup.ConfigureServices(builder.Services);
startup.ConfigureDatabaseConnection(builder.Services, "Dev");
startup.ConfigureIdentityFramework(builder.Services);
startup.ConfigureCustomQueries(builder.Services);
startup.ConfigureAuthentication(builder.Services);
startup.ConfigureAuthorization(builder.Services);


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
await app.Seed();

var Endpoints = new ManagerEndpoints(app);

Endpoints.ConfigureClass(app);
Endpoints.ConfigureSocials(app);
//Endpoints.ConfigureStreams(app);
//Endpoints.ConfigureDevelopment(app);
Endpoints.ConfigureToken(app);


app.Run();