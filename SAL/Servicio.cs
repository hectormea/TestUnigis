using DAL;
using BEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SAL
{
    public class Servicio
    { 
        private static string urlbase = "https://dog.ceo/";
        private static string urlRazas = "api/breed/hound/list";
        private static string urlimage = "api/breed/hound/images";

        public static async Task<Rootobject> GetApiRazas()
        {
            HttpClient cliente = new HttpClient();
            try
            {
                cliente.BaseAddress = new Uri(urlbase);
                Logger.ProcesoLog("**      Envia Peticion Razas: " + DateTime.Now.ToString() + "       ");
                var httpresponse = await cliente.GetAsync(urlRazas);
                Logger.ProcesoLog("**      Respuesta Peticion Razas: " + httpresponse.IsSuccessStatusCode + "---" + DateTime.Now.ToString() + "       ");

                if (httpresponse.IsSuccessStatusCode)
                {
                    var contenido = await httpresponse.Content.ReadAsStringAsync();
                    var Jobject = JsonSerializer.Deserialize<Rootobject>(contenido);
                    var mensajes = Jobject.message;
                    int agregadas = 0;
                    int noagregadas = 0;
                    Logger.ProcesoLog("**      Se encontaron " + mensajes.Count() + " Razas en la consulta.");
                    if (Jobject != null)
                    {
                        Logger.ProcesoLog("**      Inicia Iteracion Razas: " + DateTime.Now.ToString() + "       ");
                        foreach (var mensaje in mensajes)
                        {
                            if (string.IsNullOrEmpty(mensaje.Trim()))
                            {
                                Logger.ErrorLog("**      Error: Elemento Raza vacio ;( ");
                                noagregadas++;
                                continue;
                            };
                            if (mensaje.Trim().Length > 25)
                            {
                                Logger.ErrorLog("**      Error: Longitud del texto Raza excedido ;( ");
                                noagregadas++;
                                continue;
                            }
                            if (Razas.GetRaza(mensaje.Trim()) != null)
                            {
                                Logger.ErrorLog("**      Error: La Raza " + mensaje + " ya se encuentra en la base de datos ;(");
                                noagregadas++;
                                continue;
                            }
                            Razas.GuardaRaza(mensaje.Trim());
                            agregadas++;
                        }
                        Logger.ProcesoLog("**      Termina Iteracion Razas: " + DateTime.Now.ToString() + "       ");
                    }
                    if (noagregadas > 0) Logger.ErrorLog("**      Error: No se agregaron " + noagregadas + " Razas ;(");
                    if (agregadas > 0) Logger.ProcesoLog("**      Se agregaron " + agregadas + " nuevas Razas en la base de datos.");
                    return Jobject;
                }
                httpresponse = null;
                return null;
            }
            catch (Exception e)
            {
                Logger.ErrorLog("**      Error: " + e.Message + "**");
                return null;
            }
            finally
            {
                cliente = null;
            }
        }

        public static async Task<Rootobject> GetApiImagenes()
        {
            HttpClient cliente = new HttpClient();
            try
            {
                cliente.BaseAddress = new Uri(urlbase);
                Logger.ProcesoLog("**      Envia Peticion Imagenes: " + DateTime.Now.ToString() + "       ");
                var httpresponse = await cliente.GetAsync(urlimage);
                Logger.ProcesoLog("**      Respuesta Peticion Imagenes: " + httpresponse.IsSuccessStatusCode + "-- - " + DateTime.Now.ToString() + "       ");
                

                if (httpresponse.IsSuccessStatusCode)
                {
                    var contenido = await httpresponse.Content.ReadAsStringAsync();
                    var Jobject = JsonSerializer.Deserialize<Rootobject>(contenido);
                    var Imgs = Jobject.message;
                    int agregadas = 0;
                    int noagregadas = 0;
                    Logger.ProcesoLog("**      Se encontaron " + Imgs.Count() + " Imagenes en la consulta.");
                    if (Jobject != null)
                    {
                        Logger.ProcesoLog("**      Inicia Iteracion Imagenes: " + DateTime.Now.ToString() + "       ");
                        foreach (var Img in Imgs)
                        {
                            var im = Img.Split('/');
                            var _img = im[im.Length - 1].Trim();
                            var _raza = im[im.Length - 2];
                            _raza = _raza.Replace("hound-", "");
                            agregadas++;

                            switch (agregadas)
                            {
                                case 100:
                                case 300:
                                case 500:
                                case 700:
                                case 900:
                                    _img = _img + _img + _img;
                                    break;
                                case 200:
                                case 400:
                                case 600:
                                case 800:
                                case 1000:
                                    _raza = " "; 
                                    break;
                            }

                            if (_img.Trim() == "")
                            {
                                Logger.ErrorLog("**      Error: Elemento Imagen vacio ;( ");
                                noagregadas++;
                                continue;
                            };
                            if (_img.Length > 25)
                            {
                                Logger.ErrorLog("**      Error: Longitud del texto Imagen excedido ;( ");
                                noagregadas++;
                                continue;
                            }
                            if (Imagenes.GetImagen(_img) != null)
                            {
                                Logger.ErrorLog("**      Error: La Imagen " + _img + " ya se encuentra en la base de datos ;(");
                                noagregadas++;
                                continue;
                            }
                            if (Razas.GetRaza(_raza) == null)
                            {
                                Logger.ErrorLog("**      Error: No hay una Raza asociada para la imagen " + _img + " ;(");
                                noagregadas++;
                                continue;
                            }
                            Imagenes.GuardaImagen(_img, Razas.GetRaza(_raza).idRaza);
                        }
                        Logger.ProcesoLog("**      Termina Iteracion Imagenes: " + DateTime.Now.ToString() + "       ");
                    }
                    if (noagregadas > 0) Logger.ErrorLog("**      Error: No se agregaron " + noagregadas + " Imagenes ;(");

                    if (agregadas > 0) Logger.ProcesoLog("**      Se agregaron " + (agregadas-noagregadas) + " nuevas Imagenes en la base de datos.");
                    return Jobject;
                }
                httpresponse = null;
                return null;
            }
            catch (Exception e)
            {
                Logger.ErrorLog("**      Error: " + e.Message + "**");
                return null;
            }
            finally
            {
                cliente = null;
            }
        }

    }
}
