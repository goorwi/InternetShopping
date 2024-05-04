using InternetShop.Controllers;
using TestShop;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddScoped(_ => new CategoryBD());
builder.Services.AddScoped(_ => new CustomerBD());
builder.Services.AddScoped(_ => new OrderBD());
builder.Services.AddScoped(_ => new OrderItemBD());
builder.Services.AddScoped(_ => new ProductBD());
builder.Services.AddScoped(_ => new StockroomBD());
builder.Services.AddScoped(_ => new SupplierBD());

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:5173"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowSpecificOrigin");

app.MapFallbackToFile("/index.html");

app.Run();
