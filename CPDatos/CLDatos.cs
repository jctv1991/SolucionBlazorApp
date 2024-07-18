using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPUtilidades;
using CPEntidades;
using System.Data;
namespace CPDatos
{
    public class CLDatos: CLEntidades
    {
        string miCadenaConexion = string.Empty;
        CLUtilidades utilies = new CLUtilidades();
        public CLDatos(string cadenaConexion)
        {
            miCadenaConexion = cadenaConexion;
        }

        public bool RunTestConexion(out string miMensajeError)
        {
            miMensajeError = string.Empty;
            bool result = false;

            try
            {
                using (var miConexion = new SqlConnection(miCadenaConexion))
                {
                    miConexion.Open();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                miMensajeError = ex.Message;
                utilies.WriteLog("RunTestConexion -> " + ex.Message);
            }

            return result;
        }
        public DataTable RunQuery(string sql, out string miMensajeError)
        {
            DataTable miResultado = new DataTable();
            miMensajeError = string.Empty;

            try
            {
                using (SqlConnection miConexion = new SqlConnection(miCadenaConexion))
                {
                    miConexion.Open();

                    using (SqlCommand miComando = new SqlCommand(sql, miConexion))
                    {
                        miComando.CommandType = CommandType.Text;

                        using (SqlDataAdapter miAdaptador = new SqlDataAdapter(miComando))
                        {
                            miAdaptador.Fill(miResultado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                miResultado = null;
                miMensajeError = ex.Message;
                utilies.WriteLog("RunQuery -> " + ex.Message + ":" + sql);
            }

            return miResultado;
        }

        public bool INSERTA_TAREA(TBL_TAREAS TAREA, out string mensajeError)
        {
            mensajeError = string.Empty;
            try
            {
                using (SqlConnection miConexion = new SqlConnection(miCadenaConexion))
                {
                    SqlCommand miComando = new SqlCommand("SP_TBL_TAREAS_INSERTAR", miConexion);
                    miComando.CommandType = CommandType.StoredProcedure;
                    miComando.Parameters.AddWithValue("@TBL_TITULO", TAREA.TBL_TITULO);
                    miComando.Parameters.AddWithValue("@TBL_DESCRIPCION", TAREA.TBL_DESCRIPCION);
                    miComando.Parameters.AddWithValue("@TBL_FECHAVENCIMIENTO", TAREA.TBL_FECHACREACION);
                    miComando.Parameters.AddWithValue("@TBL_COMPLETADA", TAREA.TBL_COMPLETADA);
                    miConexion.Open();
                    miComando.ExecuteNonQuery();
                    miConexion.Close();
                    return true; // La inserción fue exitosa

                }
            }
            catch (Exception ex)
            {
                utilies.WriteLog("INSERTA_USUARIO_ADMIN->" + ex.Message);
                mensajeError = ex.Message;
                return false; // La inserción no fue exitosa
            }
        }

        public bool ACTUALIZA_TAREA(TBL_TAREAS TAREA, out string mensajeError)
        {
            mensajeError = string.Empty;
            try
            {
                using (SqlConnection miConexion = new SqlConnection(miCadenaConexion))
                {
                    SqlCommand miComando = new SqlCommand("SP_TBL_TAREAS_ACTUALIZAR", miConexion);
                    miComando.CommandType = CommandType.StoredProcedure;
                    miComando.Parameters.AddWithValue("@TBL_TITULO", TAREA.TBL_TITULO);
                    miComando.Parameters.AddWithValue("@TBL_DESCRIPCION", TAREA.TBL_DESCRIPCION);
                    miComando.Parameters.AddWithValue("@TBL_FECHAVENCIMIENTO", TAREA.TBL_FECHAVENCIMIENTO);
                    miComando.Parameters.AddWithValue("@TBL_COMPLETADA", TAREA.TBL_COMPLETADA);
                    miComando.Parameters.AddWithValue("@TBL_ID", TAREA.TBL_ID);
                    miConexion.Open();
                    miComando.ExecuteNonQuery();
                    miConexion.Close();
                    return true; // La inserción fue exitosa
                }
            }
            catch (Exception ex)
            {
                utilies.WriteLog("INSERTA_USUARIO_ADMIN->" + ex.Message);
                mensajeError = ex.Message;
                return false; // La inserción no fue exitosa
            }
        }

        public bool ELIMINA_TAREA(int TBL_ID, out string mensajeError)
        {
            mensajeError = string.Empty;
            try
            {
                using (SqlConnection miConexion = new SqlConnection(miCadenaConexion))
                {
                    SqlCommand miComando = new SqlCommand("SP_TBL_TAREAS_ELIMINAR", miConexion);
                    miComando.CommandType = CommandType.StoredProcedure;
                    miComando.Parameters.AddWithValue("@TBL_ID", TBL_ID);
                    miConexion.Open();
                    miComando.ExecuteNonQuery();
                    miConexion.Close();
                    return true; // La inserción fue exitosa
                }
            }
            catch (Exception ex)
            {
                utilies.WriteLog("INSERTA_USUARIO_ADMIN->" + ex.Message);
                mensajeError = ex.Message;
                return false; // La inserción no fue exitosa
            }
        }

    }
}
