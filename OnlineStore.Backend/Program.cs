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
app.MapGet("/get_product", GetProductById);
app.MapPost("/add_product", AddProduct);
app.MapPost("/delete_product", DeleteProduct);
app.MapPost("/update_product", UpdateProduct);

async Task UpdateProduct([FromQuery]Guid productId, [FromBody] Product newProduct,AppDbContext dbContext)
{
    var product = await dbContext.Products.Where(product => product.Id == productId).FirstAsync();
    product.Title = newProduct.Title;
    product.Price = newProduct.Price;
    product.Author = newProduct.Author;
    product.Description = newProduct.Description;
    await dbContext.SaveChangesAsync();
}

async Task<Product> GetProductById([FromQuery] Guid productId, AppDbContext dbContext)
{
    return await dbContext.Products.Where(product => product.Id == productId).FirstAsync();
}

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
