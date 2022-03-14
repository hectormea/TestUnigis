using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEL
{
    public class Imagenes
    {
        public static tb_Imagenes GetImagen(string Imagen)
        {
            try
            {
                using (TestUnigisEntities db = new TestUnigisEntities())
                {
                    //Logger.ProcesoLog("**      Consulta Imagen: " + Imagen + " " + DateTime.Now.ToString() + "       ");
                    return db.tb_Imagenes.Where(i => i.Imagen == Imagen).FirstOrDefault<tb_Imagenes>();
                }
            }
            catch (Exception e)
            {
                Logger.ErrorLog("**      Error: " + e.Message + "**");
                return null;
            }
        }

        public static void GuardaImagen(string Imagen, int Raza)
        {
            try
            {
                using (var db = new TestUnigisEntities())
                {
                    tb_Imagenes Img = new tb_Imagenes();
                    Img.Imagen = Imagen;
                    Img.RazaId = Raza;
                    Img.Activo = true;
                    db.tb_Imagenes.Add(Img);
                    //Logger.ProcesoLog("**      Guarda Imagen: " + Imagen + " " + DateTime.Now.ToString() + "       ");
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Logger.ErrorLog("**      Error: " + e.Message + "**");
            }

        }



    }
}
