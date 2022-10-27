using ExamIA.BL.Contexts;
using ExamIA.BL.DomainObjects;
using ExamIA.BL.Models.Dtos;
using ExamIA.BL.Services.Interfaces;
using ExamIA.BL.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ExamIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IService service;

        public SolicitudesController(ApplicationDbContext context, SolicitudService service)
        {
            this.context = context;
            this.service = service;
        }

        // GET api/Solicitud
        /// <summary>
        /// Obtener todos los registros de Solicitudes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ResponseObject>> Get()
        {
            var result = await this.service.Get();
            return result;
        }

        // GET api/Solicitud/5
        /// <summary>
        /// Obtener un registro especifico de tipo Solicitud.
        /// </summary>
        /// <param name="id">Id del registro a obtener.</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetSolicitud")]
        public async Task<ActionResult<ResponseObject>> Get(int id)
        {
            var result = await this.service.GetById(id);
            return result;
        }

        // POST api/Solicitud
        /// <summary>
        /// Crear nuevo registro de tipo Solicitud.
        /// </summary>
        /// <param name="solicitudCreate">Objeto del registro a crear una Solicitud.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NuevaSolicitudDto solicitudCreate)
        {
            var result = await service.Create(solicitudCreate);
            var solicitudDto = (SolicitudDto)result.Value.Value;
            return new CreatedAtRouteResult("GetSolicitud", new { id = solicitudDto.Id }, result);
        }

        // PUT api/Solicitud/5
        /// <summary>
        /// Editar un registro especifico de la Solicitud.
        /// </summary>
        /// <param name="id">Id del registro a editar.</param>
        /// <param name="solicitudUpdate">Objeto del registro a editar.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseObject>> Put(int id, [FromBody] NuevaSolicitudDto solicitudUpdate)
        {
            var result = await service.Update(id, solicitudUpdate);
            return result;
        }

        // PATCH api/Solicitud/5
        /// <summary>
        /// Editar un campo especifico de un registro de tipo Solicitud.
        /// </summary>
        /// <param name="id">Id del registro a editar.</param>
        /// <param name="patchDocument">Objeto del registro a editar.</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<ActionResult<ResponseObject>> Patch(int id, [FromBody] JsonPatchDocument<object> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var result = await service.PartialUpdate(id, patchDocument);
            return result;
        }

        // DELETE api/Solicitud/5
        /// <summary>
        /// Borrar un registro especifico de Solicitud.
        /// </summary>
        /// <param name="id">Id del registro a borrar.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseObject>> Delete(int id)
        {
            var result = await service.Delete(id);
            return result;
        }

    }
}
