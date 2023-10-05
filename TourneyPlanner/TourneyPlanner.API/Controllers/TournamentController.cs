using Microsoft.AspNetCore.Mvc;
using TourneyPlanner.API.DTOs;
using TourneyPlanner.API.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TourneyPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentRepository _tournamentRepository;

        public TournamentController(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

        // GET: api/<TournamentController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetAll()
        {
            return Ok(await _tournamentRepository.GetAll());
        }

        // GET api/<TournamentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDto?>> GetById(int id)
        {
            return Ok(await _tournamentRepository.GetById(id));

        }

        // POST api/<TournamentController>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateTournamentDto dto)
        {
            await _tournamentRepository.Create(dto);
            return Ok();
        }

        // PUT api/<TournamentController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] CreateTournamentDto dtoChanges)
        {
            await _tournamentRepository.Update(id, dtoChanges);
            return Ok();
        }

        // DELETE api/<TournamentController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _tournamentRepository.Delete(id);
            return Ok();
        }
    }
}
