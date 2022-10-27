using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamIA.BL.Models.Dtos
{
    public class MagoDto
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
