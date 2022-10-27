using ExamIA.BL.Contexts;
using ExamIA.BL.DomainObjects;
using ExamIA.BL.Services.Interfaces;
using ExamIA.BL.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace ExamIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MagoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMagoService service;

        public MagoController(ApplicationDbContext context, MagoService service)
        {
            this.context = context;
            this.service = service;
        }

        // GET api/Mago
        /// <summary>
        /// Obtener todos los registros de los Magos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ResponseObject>> Get()
        {
            var result = await this.service.Get();
            return result;
        }
    }
}
