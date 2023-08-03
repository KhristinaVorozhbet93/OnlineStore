using Microsoft.EntityFrameworkCore;
using OnlineStore.Backend;
using OnlineStore.Backend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var path = "myApp.db";
builder.Services.AddDbContext<AppDbContext>
    (options => options.UseSqlite($"DataSource={path}"));

//var dbPath = "myapp.db";
//builder.Services.AddDbContext<AppDbContext>(
//   options => options.UseSqlite($"Data Source={dbPath}"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/get_products", GetProducts);

async Task<List<Product>> GetProducts(AppDbContext dbContext)
{
    return await dbContext.Products.ToListAsync();
}

app.Run();
