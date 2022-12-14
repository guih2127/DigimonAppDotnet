using AutoMapper;
using DigimonApp.Domain.Models;
using DigimonApp.Domain.Services;
using DigimonApp.Extensions;
using DigimonApp.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigimonApp.Controllers
{
    [Route("/api/[controller]")]
    public class DigimonsController : Controller
    {
        private readonly IDigimonsService _digimonService;
        private readonly IMapper _mapper;

        public DigimonsController(IDigimonsService digimonService, IMapper mapper)
        {
            _digimonService = digimonService;
            _mapper = mapper;
        }

        // TODO - Verificar se faz sentido retornar um IActionResult
        [HttpGet]
        public async Task<IEnumerable<DigimonResource>> GetAllAsync([FromQuery] ListDigimonResource resource)
        {
            var digimons = await _digimonService.ListAsync(resource);
            var resources = _mapper.Map<IEnumerable<Digimon>, IEnumerable<DigimonResource>>(digimons);

            return resources;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostAsync([FromBody] SaveDigimonResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var digimon = _mapper.Map<SaveDigimonResource, Digimon>(resource);
            var result = await _digimonService.SaveAsync(digimon);

            if (!result.Success)
                return BadRequest(result.Message);

            var digimonResource = _mapper.Map<Digimon, DigimonResource>(result.Digimon);
            return Ok(digimonResource);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveDigimonResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var digimon = _mapper.Map<SaveDigimonResource, Digimon>(resource);
            var result = await _digimonService.UpdateAsync(id, digimon);

            if (!result.Success)
                return BadRequest(result.Message);

            var digimonResource = _mapper.Map<Digimon, DigimonResource>(result.Digimon);
            return Ok(digimonResource);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _digimonService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var digimonResource = _mapper.Map<Digimon, DigimonResource>(result.Digimon);
            return Ok(digimonResource);
        }
    }
}
