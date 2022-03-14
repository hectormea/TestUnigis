using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEL
{
    public class Razas
    {
        public static tb_Razas GetRaza(string Raza)
        {
            try
            {
                using (TestUnigisEntities db = new TestUnigisEntities())
                {
                    //Logger.ProcesoLog("**      Consulta Razas: " + Raza + " " + DateTime.Now.ToString() + "       ");
                    return db.tb_Razas.Where(r => r.Raza == Raza).FirstOrDefault<tb_Razas>();
                }
            }
            catch (Exception e)
            {
                Logger.ErrorLog("**      Error: " + e.Message + "**");
                return null;
            }
        }

        public static void GuardaRaza(string Raza)
        {
            try
            {
                using (var db = new TestUnigisEntities())
                {
                    tb_Razas raza = new tb_Razas();
                    raza.Raza = Raza;
                    raza.Activo = true;
                    db.tb_Razas.Add(raza);
                    //Logger.ProcesoLog("**      Guarda Raza: " + Raza + " "+ DateTime.Now.ToString() + "       ");
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
