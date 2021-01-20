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
using AutoMapper;
using ProjectAngular.API.Dtos;
using System.IO;
using System.Net.Http.Headers;

namespace ProjectAngular.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly IProjectAngularRepository _repository;
        private readonly IMapper _mapper;

        public EventosController(IProjectAngularRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var eventos = await _repository.GetAllEventosAsync(true);

                var result = _mapper.Map<EventoDto[]>(eventos);

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
                var evento = await _repository.GetEventoByIdAsync(id, true);

                var result = _mapper.Map<EventoDto>(evento);

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
                var evento = await _repository.GetAllEventosByTemaAsync(tema, true);

                var result = _mapper.Map<EventoDto>(evento);

                return Ok(result);
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(ErrorModel), new { message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventoDto eventoDto)
        {
            try
            {
                var evento = _mapper.Map<Evento>(eventoDto);

                _repository.Add(evento);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/eventos/{eventoDto.Id}", _mapper.Map<EventoDto>(evento));
                }
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(ErrorModel), new { message = e.Message });
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, EventoDto eventoDto)
        {
            try
            {
                var evento = await _repository.GetEventoByIdAsync(id, false);

                if (evento == null) return NotFound();

                _mapper.Map(eventoDto, evento);

                _repository.Update(evento);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/eventos/{eventoDto.Id}", _mapper.Map<EventoDto>(evento));
                }
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(ErrorModel), new { message = e.Message });
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
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

        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "imagens");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if(file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, fileName.Replace("\"", " ").Trim());

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                return Ok();
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(ErrorModel), new { message = e.Message });
            }

            return BadRequest("Erro ao tentar realizar upload de arquivo!");
        }
    }
}