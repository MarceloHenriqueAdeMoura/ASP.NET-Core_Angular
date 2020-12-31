using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAngular.Repository.Data;
using ProjectAngular.Domain.Models;
using ProjectAngular.Repository.Interfaces;
using ProjectAngular.Repository;
using ProjectAngular.API.Pages;

namespace ProjectAngular.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly IProjectAngularRepository _repository;

        public EventosController(IProjectAngularRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _repository.GetAllEventosAsync(true);
                return Ok(result);
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(ErrorModel), new { message = e.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _repository.GetEventoByIdAsync(id, true);
                return Ok(result);
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(ErrorModel), new { message = e.Message });
            }
        }

        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var result = await _repository.GetAllEventosByTemaAsync(tema, true);
                return Ok(result);
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(ErrorModel), new { message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Evento evento)
        {
            try
            {
                _repository.Add(evento);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/eventos/{evento.Id}", evento);
                }
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(ErrorModel), new { message = e.Message });
            }
            
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id, Evento evento)
        {
            try
            {
                var eventoId = await _repository.GetEventoByIdAsync(id, false);

                if (eventoId == null) return NotFound();

                _repository.Update(evento);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/eventos/{evento.Id}", evento);
                }
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(ErrorModel), new { message = e.Message });
            }
            
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var eventoId = await _repository.GetEventoByIdAsync(id, false);

                if (eventoId == null) return NotFound();

                _repository.Delete(eventoId);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(ErrorModel), new { message = e.Message });
            }

            return BadRequest();
        }
    }
}