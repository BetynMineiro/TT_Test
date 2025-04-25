using taller_test;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var products = new List<Product>
{
    new Product() { Id = 1, Name = "car", Price = 10 },
    new Product() { Id = 2, Name = "house", Price = 20 },
    new Product() { Id = 3, Name = "pen", Price = 30 },
};

app.MapGet("api/products", () => Results.Ok(products));
app.MapGet("api/products/{id}", (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product is null)
        return Results.NotFound();
    return Results.Ok(product);
});

app.MapPost("api/products", (Product product) =>
{
    products.Add(product);
    return Results.Created($"/api/products/{product.Id}", product);
});
app.MapDelete("api/products/{id}", (int id) =>
{
    products.RemoveAll(p => p.Id == id);
    return Results.NoContent();
});
app.MapPut("api/products/{id}", (int id, Product product) =>
{
    var productToUpdate = products.FirstOrDefault(p => p.Id == id);
    if (productToUpdate is null)
        return Results.NotFound();

    productToUpdate.Name = product.Name;
    productToUpdate.Price = product.Price;
    return Results.Ok(productToUpdate);
});
app.Run();

public partial class Program { }
