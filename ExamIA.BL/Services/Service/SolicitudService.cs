using AutoMapper;
using ExamIA.BL.Contexts;
using ExamIA.BL.DomainObjects;
using ExamIA.BL.Models.Dtos;
using ExamIA.BL.Models.Entities;
using ExamIA.BL.Models.Enum;
using ExamIA.BL.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ExamIA.BL.Services.Service
{
    public class SolicitudService : IService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public IConfiguration configuration { get; }

        public SolicitudService(ApplicationDbContext _context, IMapper _mapper, IConfiguration configuration)
        {
            this.context = _context;
            this.mapper = _mapper;
            this.configuration = configuration;

        }

        public async Task<ActionResult<ResponseObject>> Create(object objectDto)
        {
            var result = new ResponseObject();
            var afinidades = new List<string>();
            try
            {
                var solicitud = mapper.Map<Solicitud>((NuevaSolicitudDto)objectDto);

                if (solicitud.Nombre == null || solicitud.Apellido == null || solicitud.Identificacion == null || solicitud.Edad == 0 || solicitud.Afinidad_Magica == null)
                {
                    solicitud.Estatus = (int)EstatusSolicitudes.Rechazado;
                }
                else
                {
                    foreach (string item in Enum.GetNames(typeof(Afilidades)))//Obtener los nombres de los enums.
                    {
                        afinidades.Add(item);
                    }
                    var existeAfinidad = afinidades.Exists(s => s.Equals(solicitud.Afinidad_Magica.ToLower()));//Si existe la finidad entre los 6 existentes.

                    if (!existeAfinidad)
                    {
                        solicitud.Estatus = (int)EstatusSolicitudes.Rechazado;
                    }
                    else
                    {
                        solicitud.Estatus = (int)EstatusSolicitudes.Pendiente;
                    }
                }

                await context.AddAsync(solicitud);
                await context.SaveChangesAsync();
                objectDto = mapper.Map<SolicitudDto>(solicitud);
                result.Success = true;
                result.Value = objectDto;
            }
            catch (Exception ex)
            {
                result.Message = "Hubo un error al guardar la solicitud. " + ex.Message;
                result.Success = false;
            }

            return result;
        }

        public async Task<ActionResult<ResponseObject>> Delete(int id)
        {
            var result = new ResponseObject();
            try
            {
                var solicitudId = await context.Solicitudes.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);

                if (solicitudId == default(int))
                {
                    result.Message = "La solicitud que trata de eliminar no existe.";
                    result.Success = false;
                    return result;
                }

                context.Remove(new Solicitud { Id = solicitudId });
                await context.SaveChangesAsync();
                result.Success = true;
                result.Value = solicitudId;
            }
            catch (Exception ex)
            {
                result.Message = "Hubo un error al eliminar la solicitud: " + id +". "+ ex.Message;
                result.Success = false;
            }

            return result;
        }

        public async Task<ActionResult<ResponseObject>> Get()
        {
            var result = new ResponseObject();
            try
            {
                var solicitudes = await context.Solicitudes.ToListAsync();
                var solicitudesDto = mapper.Map<List<SolicitudDto>>(solicitudes);

                result.Success = true;
                result.Value = solicitudesDto;
            }
            catch (Exception ex)
            {
                result.Message = "Hubo un error al consultar las solicitudes. " + ex.Message;
                result.Success = false;
            }

            return result;
        }

        public async Task<ActionResult<ResponseObject>> GetById(int id)
        {
            var result = new ResponseObject();
            try
            {
                var solicitud = await context.Solicitudes.FirstOrDefaultAsync(x => x.Id == id);
                var solicitudDto = mapper.Map<SolicitudDto>(solicitud);
                result.Success = true;
                result.Value = solicitudDto;
            }
            catch (Exception ex)
            {
                result.Message = "Hubo un error al consultar la solicitud. " + ex.Message;
                result.Success = false;
            }

            return result;
        }

        public async Task<ActionResult<ResponseObject>> PartialUpdate(int id, JsonPatchDocument<object> patchDocument)
        {
            var result = new ResponseObject();
            var grimonios = new List<string>();
            try
            {
                if (patchDocument == null)
                {
                    result.Message = "No existe informacion para actualizar. ";
                    result.Success = false;
                    result.Value = -1;
                    return result;
                }

                var solicitud = await context.Solicitudes.FirstOrDefaultAsync(x => x.Id == id);

                if (solicitud == null)
                {
                    result.Message = "La solicitud que se intenta actualizar no existe. ";
                    result.Success = false;
                    result.Value = -2;
                    return result;
                }

                if (solicitud.Estatus == (int)EstatusSolicitudes.Aprobado)
                {
                    result.Message = "La solicitud que se intenta actualizar ha sido aprobada. ";
                    result.Success = false;
                    result.Value = -3;
                    return result;
                }

                var solicitudDto = mapper.Map<SolicitudDto>(solicitud);
                patchDocument.ApplyTo(solicitudDto);

                mapper.Map(solicitudDto, solicitud);
                await context.SaveChangesAsync();

                var solicitudActulizada = await context.Solicitudes.FirstOrDefaultAsync(x => x.Id == id);
                if (solicitudActulizada.Estatus == (int)EstatusSolicitudes.Aprobado)
                {
                    foreach (string item in Enum.GetNames(typeof(Grimonios)))//Obtener los nombres de los enums.
                    {
                        grimonios.Add(item);
                    }

                    Random randNum = new Random();
                    int index = randNum.Next(grimonios.Count); //grimonio al azar
                    string grimonio = grimonios[index];

                    var nuevoMago = new MagoDto { 
                        Nombre = solicitudActulizada.Nombre, 
                        Apellido = solicitudActulizada.Apellido, 
                        Identificacion = solicitudActulizada.Identificacion,
                        Edad = solicitudActulizada.Edad,
                        Afinidad_Magica = solicitudActulizada.Afinidad_Magica,
                        Grimonio = grimonio,
                    };
                    var mago = mapper.Map<Mago>((MagoDto)nuevoMago);
                    await context.AddAsync(mago);
                    await context.SaveChangesAsync();
                }

                result.Success = true;
                result.Value = solicitudDto;
            }
            catch (Exception ex)
            {
                result.Message = "Hubo un error al actualizar la solicitud. " + ex.Message;
                result.Success = false;
            }

            return result;
        }

        public async Task<ActionResult<ResponseObject>> Update(int id, object objectDto)
        {
            var result = new ResponseObject();
            try
            {
                var solicitud = mapper.Map<Solicitud>((NuevaSolicitudDto)objectDto);
                solicitud.Id = id;
                solicitud.Estatus = (int)EstatusSolicitudes.Pendiente;
                context.Entry(solicitud).State = EntityState.Modified;
                await context.SaveChangesAsync();
                result.Success = true;
                result.Value = solicitud;
            }
            catch (Exception ex)
            {
                result.Message = "Hubo un error al actualizar la solicitud. " + ex.Message;
                result.Success = false;
            }

            return result;
        }
    }
}
