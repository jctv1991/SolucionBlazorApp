using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorApp.Controllers
{
    [Route("miControlador")]
    [ApiController]
    public class miControladorController : Controller
    {
        [HttpPost("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion(string username, string password)
        {
            string mensajeError=string.Empty;
            bool identificacionOK = false;
            if (username=="JTUTIVEN" && password=="1234")
            {
                var claims = new[] { new Claim(ClaimTypes.Name, username) };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authPropiedades = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20)
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authPropiedades);
                identificacionOK=true;
                mensajeError = string.Empty;
            }
            else
            {
                mensajeError = "Error de credenciales, intente nuevamente";
            }

            // Si las credenciales son inválidas, retornar un error
            return Json(new { resultado = identificacionOK, mensaje = mensajeError });
        }
    }
}
