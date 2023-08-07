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

app.MapGet("/get_products", GetProductsAsync);
app.MapGet("/get_product", GetProductByIdAsync);
app.MapPost("/add_product", AddProductAsync);
app.MapPost("/delete_product", DeleteProductAsync);
app.MapPost("/update_product", UpdateProductAsync);

async Task<IResult> GetProductsAsync(AppDbContext dbContext)
{
    var products =  await dbContext.Products.ToListAsync();
    if (products is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(products);
}
async Task<IResult> GetProductByIdAsync([FromQuery] Guid productId, AppDbContext dbContext)
{
    var product = 
        await dbContext.Products.Where(product => product.Id == productId).FirstOrDefaultAsync();
    if (product == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(product);
}
async Task AddProductAsync([FromBody] Product product, AppDbContext dbContext)
{
    await dbContext.Products.AddAsync(product);
    await dbContext.SaveChangesAsync();
}
async Task<IResult> UpdateProductAsync([FromQuery]Guid productId, [FromBody] Product newProduct,AppDbContext dbContext)
{
    var product = await dbContext.Products.Where(product => product.Id == productId).FirstOrDefaultAsync();
    if (product is null)
    {
        return Results.NotFound();
    }
    product.Title = newProduct.Title;
    product.Price = newProduct.Price;
    product.Author = newProduct.Author;
    product.Description = newProduct.Description;
    await dbContext.SaveChangesAsync();
    return Results.Ok();
}
async Task DeleteProductAsync([FromBody]Product product,AppDbContext dbContext)
{
    dbContext.Products.Remove(product);
    await dbContext.SaveChangesAsync();
}

app.Run();
