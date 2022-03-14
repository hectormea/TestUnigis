using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    class ImagenesDB
    {
        public int idImagen { get; set; }
        public string Imagen { get; set; }
        public bool Activo { get; set; }
        public int RazaId { get; set; }
    }
}
