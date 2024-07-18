using CPDatos;
using System.Xml.Linq;
using CPUtilidades;
using static CPEntidades.CLEntidades;
using CPEntidades;
using System.Collections.Generic;
using System.Data;

namespace CPNegocio
{
    public class CLNegocio:CLUtilidades
    {
        CLDatos miDB = null;
        public CLNegocio(string cadenaConexion) {
            try
            {
                if (!(cadenaConexion == string.Empty))
                {
                    miDB = new CLDatos(cadenaConexion);
                }
            }
            catch (Exception ex)
            {
                WriteLog("CLBusiness -> " + ex.Message);
            }
        }

        public bool RunTestConexion(out string miMensajeError)
        {
            return miDB.RunTestConexion(out miMensajeError);
        }

        public bool REGISTRAR_TAREA(TBL_TAREAS TAREA, out string mensajeError)
        {
            if (TAREA.TBL_ID==0)
            {
                return miDB.INSERTA_TAREA(TAREA, out mensajeError);
            }
            else
            {
                return miDB.ACTUALIZA_TAREA(TAREA, out mensajeError);
            }
           
        }

        public bool ELIMINA_TAREA(int TBL_ID, out string mensajeError)
        {
            return miDB.ELIMINA_TAREA(TBL_ID, out mensajeError);
        }

        public List<TBL_TAREAS> GET_TAREAS(out string mensajeError)
        {
            string sql = string.Empty;
            List<TBL_TAREAS> TAREAS = null;
            try
            {
                sql = "SELECT TBL_ID,TBL_TITULO,TBL_DESCRIPCION,TBL_FECHACREACION,TBL_FECHAVENCIMIENTO,TBL_COMPLETADA FROM TBL_TAREAS ORDER BY TBL_FECHACREACION ASC";
                DataTable dt = miDB.RunQuery(sql, out mensajeError);
                if (dt != null)
                {
                    TAREAS= new List<TBL_TAREAS>();
                    foreach (DataRow item in dt.Rows)
                    {
                        TAREAS.Add(new TBL_TAREAS()
                        {
                            TBL_ID= (int)item[0],
                            TBL_TITULO = item[1].ToString(),
                            TBL_DESCRIPCION = item[2].ToString(),
                            TBL_FECHACREACION = DateTime.Parse(item[3].ToString()),
                            TBL_FECHAVENCIMIENTO= DateTime.Parse(item[4].ToString()).Date,
                            TBL_COMPLETADA = bool.Parse(item[5].ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
            }

            return TAREAS;
        }
    }
}
