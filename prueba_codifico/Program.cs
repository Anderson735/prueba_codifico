using Microsoft.EntityFrameworkCore;
using prueba_codifico.DataAccess.Repository.DML.Sales;
using prueba_codifico.DataAccess;
using prueba_codifico.DTO.Interfaces.Sales;
using System.Text.Json.Serialization;
using prueba_codifico.DataAccess.Repository.DML.HR;
using prueba_codifico.DTO.Interfaces.HR;
using prueba_codifico.DTO.Interfaces.Production;
using prueba_codifico.DataAccess.Repository.DML.Production;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddRazorPages();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddDbContext<StoreSampleContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

// Registrar OrdersDMLRepository
builder.Services.AddScoped<IOrders>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("cadenaSQL");
#pragma warning disable CS8604 // Posible argumento de referencia nulo
    return new OrdersDMLRepository(connectionString);
#pragma warning restore CS8604 // Posible argumento de referencia nulo
});

// Registrar EmployeeService
builder.Services.AddScoped<IEmployee>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("cadenaSQL");
#pragma warning disable CS8604 // Posible argumento de referencia nulo
    return new EmployeeDMLRepository(connectionString);
#pragma warning restore CS8604 // Posible argumento de referencia nulo
});

// Registrar ShipperRepository
builder.Services.AddScoped<IShipper>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("cadenaSQL");
#pragma warning disable CS8604 // Posible argumento de referencia nulo
    return new ShippersRepository(connectionString);
#pragma warning restore CS8604 // Posible argumento de referencia nulo
});

// Registrar ProductRepository
builder.Services.AddScoped<IProducts>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("cadenaSQL");
#pragma warning disable CS8604 // Posible argumento de referencia nulo
    return new ProductsEntityRepository(connectionString);
#pragma warning restore CS8604 // Posible argumento de referencia nulo
});

//Registra OrderDetails
builder.Services.AddScoped<IOrderDetails>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("cadenaSQL");
#pragma warning disable CS8604 // Posible argumento de referencia nulo
    return new OrderDetailsEntityRepository(connectionString);
#pragma warning restore CS8604 // Posible argumento de referencia nulo
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configurar el pipeline de solicitud HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.UseCors("NuevaPolitica");

app.Run();
