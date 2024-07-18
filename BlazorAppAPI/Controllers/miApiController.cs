using CPEntidades;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class miApiController : Controller
    {
        string miCadenaConexion = "Data Source=DESKTOP-CU000FG;Initial Catalog=JUAN_TUTIVEN;User ID=DVACS;Password=Adm1n1strad0r";

        [HttpPost("GetTareas")]
        public IActionResult GetTareas()
        {
            string mensajeError = string.Empty;
            CPNegocio.CLNegocio miCapaNegocio = new CPNegocio.CLNegocio(miCadenaConexion);
            List<CLEntidades.TBL_TAREAS> TAREAS = null;
            try
            {
                TAREAS = miCapaNegocio.GET_TAREAS(out mensajeError);
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
            }
            return Json(new { TAREAS });
        }

        [HttpPost("RegistraTarea")]
        public IActionResult RegistraTarea(CLEntidades.TBL_TAREAS TAREA)
        {
            string mensajeError = string.Empty;
            CPNegocio.CLNegocio miCapaNegocio = new CPNegocio.CLNegocio(miCadenaConexion);
            bool RES = false;
            try
            {
                RES = miCapaNegocio.REGISTRAR_TAREA(TAREA, out mensajeError);
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
            }
            return Json(new { resultado = RES, mensaje = mensajeError });
        }


        [HttpPost("EliminaTarea")]
        public IActionResult EliminaTarea(int ID_TAREA)
        {
            string mensajeError = string.Empty;
            CPNegocio.CLNegocio miCapaNegocio = new CPNegocio.CLNegocio(miCadenaConexion);
            bool RES = false;
            try
            {
                RES = miCapaNegocio.ELIMINA_TAREA(ID_TAREA, out mensajeError);
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
            }
            return Json(new { resultado = RES, mensaje = mensajeError });
        }


        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            HttpContext.Response.Headers["Pragma"] = "no-cache";
            HttpContext.Response.Headers["Expires"] = "0";
            HttpContext.Response.Cookies.Delete(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { message = "Sesión cerrada exitosamente" });
        }
    }
}
