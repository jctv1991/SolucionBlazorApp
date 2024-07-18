using Radzen;
var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient(); // Registrar HttpClient

builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7221/") });

builder.Services.AddRadzenComponents();

var app = builder.Build();

// Configurar el pipeline HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); 

app.UseAuthentication(); 

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.UseCors();
app.Run();
