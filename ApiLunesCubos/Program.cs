using ApiLunesCubos.Data;
using ApiLunesCubos.Helpers;
using ApiLunesCubos.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NSwag;
using NSwag.Generation.Processors.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionstring = builder.Configuration.GetConnectionString("SqlAzure");
builder.Services.AddTransient<RepositoryCubos>();
builder.Services.AddDbContext<CubosContext>(options => options.UseSqlServer(connectionstring));
HelperOAuthToken helper = new HelperOAuthToken(builder.Configuration);
builder.Services.AddSingleton<HelperOAuthToken>();
builder.Services.AddAuthentication(helper.GetAuthenticationOptions()).AddJwtBearer(helper.GetJwtOptions());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "Api CRUD CUBOS",
//        Description = "Probando CUBOS",
//        Version = "v1",
//        Contact = new OpenApiContact()
//        {
//            Name = "Jorge Salinero",
//            Email = "jorge.salinero@tajamar365.com"
//        }
//    });
//});
builder.Services.AddOpenApiDocument(document => {
    document.Title = "Api OAuth CUBOS";
    document.Description = "Api Token CUBOS 2023.  Ejemplo OAuth";
    // CONFIGURAMOS LA SEGURIDAD JWT PARA SWAGGER,    // PERMITE AÑADIR EL TOKEN JWT A LA CABECERA.
    document.AddSecurity("JWT", Enumerable.Empty<string>(),
    new NSwag.OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = OpenApiSecurityApiKeyLocation.Header,
        Description = "Copia y pega el Token en el campo 'Value:' así: Bearer {Token JWT}."
    }
        );
    document.OperationProcessors.Add(
        new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});

var app = builder.Build();
//app.UseSwagger();
app.UseOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Api CRUD CUBOS");
    options.RoutePrefix = "";
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
