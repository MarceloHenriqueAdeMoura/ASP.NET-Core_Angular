using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using ProjectAngular.API.Pages;
using ProjectAngular.Domain.Models;
using ProjectAngular.Repository.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectAngular.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalestrantesController : ControllerBase
    {
        private readonly IProjectAngularRepository _repository;

        public PalestrantesController(IProjectAngularRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _repository.GetPalestranteByIdAsync(id, true);
                return Ok(result);
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(ErrorModel), new { message = e.Message });
            }
        }

        [HttpGet("getByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var resut = await _repository.GetAllPalestrantesByNameAsync(name, true);
                return Ok(resut);
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(ErrorModel), new { message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Palestrante palestrante)
        {
            try
            {
                _repository.Add(palestrante);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/palestrantes/{palestrante.Id}", palestrante);
                }
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(ErrorModel), new { message = e.Message });
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id, Palestrante palestrante)
        {
            try
            {
                var palestranteId = await _repository.GetEventoByIdAsync(id, false);
                if (palestranteId == null) return NotFound();

                _repository.Update(palestrante);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/palestrante/{palestrante.Id}", palestrante);
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
                var palestranteId = await _repository.GetPalestranteByIdAsync(id, false);
                if (palestranteId == null) return NotFound();

                _repository.Delete(palestranteId);

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
