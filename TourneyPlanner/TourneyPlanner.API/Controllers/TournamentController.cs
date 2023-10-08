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
        public async Task<ActionResult<TournamentDto>> GetById(int id)
        {
            TournamentDto? tournament = await _tournamentRepository.GetById(id);
            if (tournament == null)
            {
                return NotFound($"A tournament with Id: {id} does not exist. No record was deleted.");
            }
            return Ok(tournament);
        }

        // POST api/<TournamentController>
        [HttpPost]
        public async Task<ActionResult<TournamentDto>> Create([FromBody] CreateTournamentDto dto)
        {
            TournamentDto createdTournament = await _tournamentRepository.Create(dto);
            return Created($"api/Tournament/{createdTournament.Id}", createdTournament);
        }

        // PUT api/<TournamentController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] CreateTournamentDto dtoChanges)
        {
            await _tournamentRepository.Update(id, dtoChanges);
            return NoContent();
        }

        // DELETE api/<TournamentController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if(await _tournamentRepository.GetById(id) == null)
            {
                return NotFound($"A tournament with Id: {id} does not exist. No record was deleted.");
            }

            await _tournamentRepository.Delete(id);
            return NoContent();
        }
    }
}
