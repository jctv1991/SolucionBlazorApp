using System.Reflection;

namespace CPUtilidades
{
    public class CLUtilidades
    {
        public void WriteLog(string texto)
        {
            try
            {
                string ruta = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\LOGS";
                if (!System.IO.Directory.Exists(ruta))
                    System.IO.Directory.CreateDirectory(ruta);
                string dir_file = ruta + "/LOGS_" + DateTime.Now.Day.ToString("0#") + "_" + DateTime.Now.Month.ToString("0#") + "_" + DateTime.Now.Year + ".txt";
                if (!System.IO.File.Exists(dir_file))
                {
                    System.IO.FileStream stream = new System.IO.FileStream(dir_file, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                    stream.Close();
                    stream.Dispose();
                }
                System.IO.StreamWriter writer = new System.IO.StreamWriter(dir_file, true);
                writer.WriteLine(DateTime.Now.ToString("HH:mm:ss") + ": " + texto);
                writer.Close();
                writer.Dispose();
            }
            catch (Exception ex)
            {

            }
        }
        public string GetFechaForHTML(DateTime miFecha, bool APLICA_HORA)
        {
            if (APLICA_HORA)
            {
                return miFecha.ToString("dd-MM-yyyy HH:mm:ss");
            }
            else
            {
                return miFecha.ToString("dd-MM-yyyy");
            }

        }
    }
}
