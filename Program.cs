using Microsoft.EntityFrameworkCore;
using test;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddScoped<Iuser,UserRepository>();

builder.Services.AddDbContext<AppDbContext>(options=>{
    options.UseMySQL(builder.Configuration.GetConnectionString("myConnections"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();


