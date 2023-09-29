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
var autoportaltraining = builder.Configuration.GetConnectionString("autoportaltraining");
builder.Services.AddDbContext<DataContext>(o => o.UseMySql(autoportaltraining, ServerVersion.AutoDetect(autoportaltraining)));

var autopastraining = builder.Configuration.GetConnectionString("autopastraining");
builder.Services.AddDbContext<ServerContext>(o => o.UseMySql(autopastraining, ServerVersion.AutoDetect(autopastraining)));


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
