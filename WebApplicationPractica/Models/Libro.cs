
using System.ComponentModel.DataAnnotations;

namespace WebApplicationPractica.Models
{
    public class Libro
    {
        [Key]
        public int id_libro { get; set; }
        public string titulo { get; set; }
        public int aniopublicacion { get; set; }
        public int autorid { get; set; }
        public int categoriaid { get; set; }
        public string resumen { get; set; }

    }
}
