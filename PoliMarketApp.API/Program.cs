using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PoliMarketApp.Application.Interfaces;
using PoliMarketApp.Application.Services;
using PoliMarketApp.Infrastructure.Data;
using PoliMarketApp.Infrastructure.Mappings;
using PoliMarketApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Configure DbContext
builder.Services.AddDbContext<PoliMarketDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PoliMarketDBConn")));

// Register Repositories
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProveedorRepository, ProveedorRepository>();
builder.Services.AddScoped<IVendedorRepository, VendedorRepository>();
builder.Services.AddScoped<IMovimientoBodegaRepository, MovimientoBodegaRepository>();
builder.Services.AddScoped<ICompraProveedorRepository, CompraProveedorRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IPedidoVentaRepository, PedidoVentaRepository>();
builder.Services.AddScoped<IOrdenEntregaRepository, OrdenEntregaRepository>();

// Register Services
builder.Services.AddScoped<IHumanResourcesService, HumanResourcesService>();
builder.Services.AddScoped<ISalesService, SalesService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IDeliveryService, DeliveryService>();

// Register AutoMapper with all mapping profiles
builder.Services.AddAutoMapper(cfg => { }, typeof(VendorMapping));
builder.Services.AddAutoMapper(cfg => { }, typeof(ClienteMapping));
builder.Services.AddAutoMapper(cfg => { }, typeof(ProductoMapping));
builder.Services.AddAutoMapper(cfg => { }, typeof(PedidoVentaMapping));
builder.Services.AddAutoMapper(cfg => { }, typeof(OrdenEntregaMapping));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
