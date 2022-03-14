using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEL
{
    public class Logger
    {
        public static void ErrorLog(string AppError)
        {
            string Ruta = Directory.GetCurrentDirectory() + "\\LOG\\";
            string NombreArchivo = DateTime.Now.ToString("ddMMyyyy") + "_ErrorLog.txt";
            string Archivo = Ruta + NombreArchivo;

            ValidaDirectorio(Ruta);
            ValidaArchivo(Archivo);

            try
            {
                StreamWriter Editar = File.AppendText(Archivo);
                Editar.WriteLine(AppError);
                Editar.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public static void ProcesoLog(string AppLog)
        {
            string Ruta = Directory.GetCurrentDirectory() + "\\LOG\\";
            string NombreArchivo = DateTime.Now.ToString("ddMMyyyy") + "_ProcesoLog.txt";
            string Archivo = Ruta + NombreArchivo;

            ValidaDirectorio(Ruta);
            ValidaArchivo(Archivo);

            try
            {
                StreamWriter Editar = File.AppendText(Archivo);
                Editar.WriteLine(AppLog);
                Editar.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public static void ValidaDirectorio(string Ruta)
        {
            if (!Directory.Exists(Ruta))
            {
                try
                {
                    Directory.CreateDirectory(Ruta);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
        }

        public static void ValidaArchivo(string Archivo)
        {
            if (!File.Exists(Archivo))
            {
                try
                {
                    TextWriter Crear = new StreamWriter(Archivo);
                    Crear.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
        }

    }
}
