using Microsoft.EntityFrameworkCore;
using ProjectAngular.Domain.Models;
using ProjectAngular.Repository.Data;
using ProjectAngular.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAngular.Repository
{
    public class ProjectAngularRepository : IProjectAngularRepository
    {
        private readonly ProjectAngularContext _context;
        public ProjectAngularRepository(ProjectAngularContext context)
        {
            _context = context;
        }

        //GERAL
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        //EVENTOS
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            //Pesquisa os Eventos incluindo os Lotes e Redes Sociais
            IQueryable<Evento> query = _context.Eventos.Include(x => x.Lotes)
                                                       .Include(x => x.RedeSociais);

            //Caso passe o Palestrante, será incluído na pesquisa
            if (includePalestrantes)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                             .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking()
                         .OrderByDescending(x => x.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int EventoId, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos.Include(x => x.Lotes)
                                                       .Include(x => x.RedeSociais);

            if (includePalestrantes)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                             .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking()
                         .OrderByDescending(x => x.DataEvento)
                         .Where(x => x.Id == EventoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos.Include(x => x.Lotes)
                                                       .Include(x => x.RedeSociais);

            if (includePalestrantes)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                             .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking()
                         .OrderByDescending(x => x.DataEvento)
                         .Where(x => x.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        //PALESTRANTES
        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            //Pesquisa os Palestrantes incluindo as Redes Sociais
            IQueryable<Palestrante> query = _context.Palestrantes.Include(x => x.RedeSociais);

            //Caso passe o Evento, será incluído na pesquisa
            if (includeEventos)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                             .ThenInclude(e => e.Evento);
            }

            query = query.AsNoTracking()
                         .OrderByDescending(p => p.Id);

            return await query.ToArrayAsync();
        }
        public async Task<Palestrante> GetPalestranteByIdAsync(int PalestranteId, bool includeEventos = false)
        {            
            IQueryable<Palestrante> query = _context.Palestrantes.Include(x => x.RedeSociais);

            if (includeEventos)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                             .ThenInclude(e => e.Evento);
            }

            query = query.AsNoTracking()
                         .OrderBy(x => x.Nome)
                         .Where(x => x.Id == PalestranteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNameAsync(string name, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(x => x.RedeSociais);

            if (includeEventos)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                             .ThenInclude(e => e.Evento);
            }

            query = query.AsNoTracking()
                         .Where(x => x.Nome.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}
