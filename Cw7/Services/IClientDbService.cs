using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cw7.Services
{
    public interface IClientDbService
    {
        Task<int> DeleteClientAsync(int idClient);
    }
}