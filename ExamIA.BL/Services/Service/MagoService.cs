using AutoMapper;
using ExamIA.BL.Contexts;
using ExamIA.BL.DomainObjects;
using ExamIA.BL.Models.Dtos;
using ExamIA.BL.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ExamIA.BL.Services.Service
{
    public class MagoService : IMagoService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public IConfiguration configuration { get; }

        public MagoService(ApplicationDbContext _context, IMapper _mapper, IConfiguration configuration)
        {
            this.context = _context;
            this.mapper = _mapper;
            this.configuration = configuration;

        }

        public async Task<ActionResult<ResponseObject>> Get()
        {
            var result = new ResponseObject();
            try
            {
                var magos = await context.Magos.ToListAsync();
                var magosDto = mapper.Map<List<MagoDto>>(magos);

                result.Success = true;
                result.Value = magosDto;
            }
            catch (Exception ex)
            {
                result.Message = "Hubo un error al consultar los magos. " + ex.Message;
                result.Success = false;
            }

            return result;
        }

    }
}
