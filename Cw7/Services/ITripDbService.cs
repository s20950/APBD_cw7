using System.Collections.Generic;
using System.Threading.Tasks;
using Cw7.Models.DTO.Request;
using Cw7.Models.DTO.Response;

namespace Cw7.Services
{
    public interface ITripDbService
    {
        Task<IEnumerable<GetTripsResponseDto>> GetTripsAsync();
        Task<int> AddTripToClientAsync(int idTrip, AddTripToClientRequestDto dto);
    }
}