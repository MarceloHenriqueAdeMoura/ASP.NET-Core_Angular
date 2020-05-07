using Microsoft.EntityFrameworkCore;
using ProjectAngular.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAngular.Repository.Data
{
    public class ProjectAngularContext : DbContext
    {
        public ProjectAngularContext(DbContextOptions<ProjectAngularContext> options) : base(options)
        {
        }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestranteEventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<RedeSocial> RedeSociais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PalestranteEvento>().HasKey(pe => new { pe.EventoId, pe.PalestranteId });
        }
    }
}
