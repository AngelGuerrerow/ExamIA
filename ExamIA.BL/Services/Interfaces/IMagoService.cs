using ExamIA.BL.DomainObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamIA.BL.Services.Interfaces
{
    public interface IMagoService
    {
        public Task<ActionResult<ResponseObject>> Get();
    }
}
