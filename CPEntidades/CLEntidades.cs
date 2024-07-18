namespace CPEntidades
{
    public class CLEntidades
    {
        public class LoginModel
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public class RespuestaLogin
        {
            public bool resultado { get; set; } = false;
            public string mensaje { get; set; } = string.Empty;
        }
        public class TBL_TAREAS
        {
            public int TBL_ID { get; set; } = 0;
            public string TBL_TITULO { get; set; } = string.Empty;
            public string TBL_DESCRIPCION {  get; set; } = string.Empty;
            public DateTime TBL_FECHACREACION {  get; set; } = DateTime.Now;
            public DateTime TBL_FECHAVENCIMIENTO {  get; set; } = DateTime.Now;
            public bool TBL_COMPLETADA {  get; set; } = false;
        }
    }
}
