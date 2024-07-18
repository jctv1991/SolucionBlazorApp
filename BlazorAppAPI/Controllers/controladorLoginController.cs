using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorAppAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class controladorLoginController : Controller
    {
        [HttpPost("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion(string USUARIO, string CLAVE)
        {
            string mensajeError = string.Empty;
            bool identificacionOK = false;
            try
            {
                if (USUARIO == "JTUTIVEN" && CLAVE == "1234")
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, USUARIO) };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authPropiedades = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20)
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authPropiedades);
                    identificacionOK = true;
                    mensajeError = string.Empty;
                }
                else
                {
                    mensajeError = "Error de credenciales, intente nuevamente";
                }
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
            }

            // Si las credenciales son inválidas, retornar un error
            return Json(new { resultado = identificacionOK, mensaje = mensajeError });
        }
    }
}
