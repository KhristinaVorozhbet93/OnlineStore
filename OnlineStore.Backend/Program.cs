using Microsoft.AspNetCore.Mvc;
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
app.MapPost("/add_product", AddProduct);
app.MapPost("/delete_product", DeleteProduct);

async Task DeleteProduct([FromBody]Product product,AppDbContext dbContext)
{
    dbContext.Products.Remove(product);
    await dbContext.SaveChangesAsync();
}

async Task AddProduct([FromBody]Product product, AppDbContext dbContext)
{
    await dbContext.Products.AddAsync(product);
    await dbContext.SaveChangesAsync();
}

async Task<List<Product>> GetProducts(AppDbContext dbContext)
{
    return await dbContext.Products.ToListAsync();
}

app.Run();
