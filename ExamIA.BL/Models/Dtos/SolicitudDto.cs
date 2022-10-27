using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamIA.BL.Models.Dtos
{
    public class SolicitudDto : NuevaSolicitudDto
    {
        public int Id { get; set; }
        public int Estatus { get; set; }
    }
}
