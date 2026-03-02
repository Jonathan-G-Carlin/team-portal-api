using TeamPortal.Data;
using Microsoft.EntityFrameworkCore;
using TeamPortal.Api.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext registration
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("TeamPortalDbConnection"));
});

// Controller registration
builder.Services.AddControllers();

// Service registration
builder.Services.AddScoped<UserService>();

// OpenAPI registration
builder.Services.AddOpenApi();


var app = builder.Build();

// Authenication and Authorization middlewares would go here (Before Map Controller)

// Map controllers (After all Routing and Auth middlewares)
app.MapControllers();

app.MapOpenApi(); // Add MappOpenApi(), Then  MapScalarApiReference()  because it adds    /openapi endpoint   which is used by the latter);
app.MapScalarApiReference();


app.Run();
