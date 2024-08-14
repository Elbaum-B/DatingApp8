
using API.Extensions;
using API.MiddleWare;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleWare>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http://localhost:4200", "http://localhost:4201","https://localhost:4200", "https://localhost:4201"));

    app.UseAuthentication();
    app.UseAuthorization();
app.MapControllers();

app.Run();
