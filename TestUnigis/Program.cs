using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BEL;
using SAL;

namespace TestUnigis
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var TiempoDeProceso = DateTime.Now;
            try
            {
                Logger.ProcesoLog("");

                Console.WriteLine("**  =>  Inicio General: " + DateTime.Now.ToString() + "        ");
                Logger.ProcesoLog("**  =>  Inicio General: " + DateTime.Now.ToString() + "        ");

                Logger.ProcesoLog("**                                                       ");

                var TiempoDeProcesoGetRazas = DateTime.Now;

                Logger.ProcesoLog("**      Inicio GetRazas: " + DateTime.Now.ToString() + "       ");

                await Servicio.GetApiRazas();

                Logger.ProcesoLog("**      Termino GetRazas: " + DateTime.Now.ToString() + "      ");

                Logger.ProcesoLog("**      Total GetRazas: " + (DateTime.Now - TiempoDeProcesoGetRazas).ToString() + "                 ");

                Logger.ProcesoLog("**                                                       ");
                
                var TiempoDeProcesoGetImagen = DateTime.Now;
                Logger.ProcesoLog("**      Inicio GetImagenes: " + DateTime.Now.ToString() + "    ");

                await Servicio.GetApiImagenes();
                Logger.ProcesoLog("**      Termino GetImagenes: " + DateTime.Now.ToString() + "   ");
                
                Logger.ProcesoLog("**      Total GetImagenes: " + (DateTime.Now - TiempoDeProcesoGetImagen).ToString() + "              ");


                Logger.ProcesoLog("**                                                       ");

                Console.WriteLine("**      Termino General: " + DateTime.Now.ToString() + "       ");
                Logger.ProcesoLog("**      Termino General: " + DateTime.Now.ToString() + "       ");

                Console.WriteLine("**      Total General: " + (DateTime.Now - TiempoDeProceso).ToString() + "              <=  ");
                Logger.ProcesoLog("**      Total General: " + (DateTime.Now - TiempoDeProceso).ToString() + "              <=  ");

                Logger.ProcesoLog("***********************************************************");
                Logger.ErrorLog("***********************************************************");
                Thread.Sleep(5000);
            }
            catch (Exception e)
            {
                Logger.ErrorLog("Error: " + e.Message);
                Logger.ErrorLog("***********************************************************");
                Console.WriteLine("Error: " + e.Message);
                Console.ReadLine();
            }
        }
    }
}
