using System.Reflection;
using GBMProject.CrossCutting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerConfigurations(AppContext.BaseDirectory, Assembly.GetExecutingAssembly().GetName().Name!);
builder.Services.AddDbConfigurations(builder.Configuration);
builder.Services.AddFluentValidationConfiguration();
builder.Services.AddServicesConfiguration();
builder.Services.AddMediatR();

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();