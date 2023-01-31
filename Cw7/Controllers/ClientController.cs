using System;
using System.Threading.Tasks;
using Cw7.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cw7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientDbService _clientDbService;

        public ClientController(IClientDbService clientDbService)
        {
            _clientDbService = clientDbService;
        }

        [HttpDelete("{idClient:int}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int idClient)
        {
            try
            {
                var trips = await _clientDbService.DeleteClientAsync(idClient);
                return Ok($"Client with id {idClient} deleted");
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}