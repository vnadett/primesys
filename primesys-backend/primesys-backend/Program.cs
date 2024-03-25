using primesys_backend.Interfaces;
using primesys_backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(builder =>
       builder
       .WithOrigins("http://localhost:5256", "https://localhost:44302")
       .AllowAnyMethod()
       .AllowAnyHeader());

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
