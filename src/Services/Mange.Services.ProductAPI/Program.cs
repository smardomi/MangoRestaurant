using Mange.Services.ProductAPI.DbContext;
using Mange.Services.ProductAPI.Middlewares;
using Mange.Services.ProductAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<ApplicationDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationConnectionString"))
);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddTransient<CustomExceptionHandlerMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<CustomExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
