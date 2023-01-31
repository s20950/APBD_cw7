using System;
using System.Linq;
using System.Threading.Tasks;
using Cw7.Controllers;
using Cw7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cw7.Services
{
    public class ClientDbService : IClientDbService
    {
        private readonly s20950Context _context;

        public ClientDbService(s20950Context context)
        {
            _context = context;
        }

        public async Task<int> DeleteClientAsync(int idClient)
        {
            bool hasTrips = await _context.ClientTrips.AnyAsync(row => row.IdClient == idClient);

            if (hasTrips) throw new Exception("Client has one or more trips!");

            Client client = await _context.Clients.Where(row => row.IdClient == idClient).FirstOrDefaultAsync();
            _context.Remove(client);

            return await _context.SaveChangesAsync();
        }
    }
}