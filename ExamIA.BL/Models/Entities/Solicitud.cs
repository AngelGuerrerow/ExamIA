using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamIA.BL.Models.Entities
{
    public class Solicitud
    {
        public int Id { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$")]
        [MaxLength(20)]
        public string Nombre { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$")]
        [MaxLength(20)]
        public string Apellido { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9]*$")]
        [MaxLength(10)]
        public string Identificacion { get; set; }
        [Range(0,99)]
        public int Edad { get; set; }
        public string Afinidad_Magica { get; set; }
        public int Estatus { get; set; }
    }
}
