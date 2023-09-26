using AssessmentApi.Data;
using AssessmentApi.Services;
using AssessmentApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connection = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<DataContext>(o => o.UseMySql(connection, ServerVersion.AutoDetect(connection)));

var neww = builder.Configuration.GetConnectionString("Default2");
builder.Services.AddDbContext<ServerContext>(o => o.UseMySql(neww, ServerVersion.AutoDetect(neww)));


builder.Services.AddScoped<IUserInterface, UserRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
