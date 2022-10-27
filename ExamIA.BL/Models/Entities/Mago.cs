using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamIA.BL.Models.Entities
{
    public class Mago
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Identificacion { get; set; }
        public int Edad { get; set; }
        public string Afinidad_Magica { get; set; }
        public string Grimonio { get; set; }
    }
}
