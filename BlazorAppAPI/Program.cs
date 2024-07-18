var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add distributed memory cache
builder.Services.AddDistributedMemoryCache();

// Add authentication services
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Account/Login"; // Ajusta esto según tu ruta de login
        options.LogoutPath = "/Account/Logout"; // Ajusta esto según tu ruta de logout
    });

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Establece el tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true; // Cookie solo accesible mediante HTTP
    options.Cookie.IsEssential = true; // La cookie es esencial para el funcionamiento de la aplicación
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSession(); // Middleware para habilitar sesiones

app.UseAuthentication(); // Middleware para autenticación
app.UseAuthorization();

app.MapControllers();

app.Run();
