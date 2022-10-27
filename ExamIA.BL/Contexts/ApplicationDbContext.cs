using ExamIA.BL.Models;
using ExamIA.BL.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExamIA.BL.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Solicitud> Solicitudes { get; set; }
        public DbSet<Mago> Magos { get; set; }
    }
}
