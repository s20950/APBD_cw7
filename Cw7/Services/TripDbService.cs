using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw7.Controllers;
using Cw7.Models;
using Cw7.Models.DTO.Request;
using Cw7.Models.DTO.Response;
using Microsoft.EntityFrameworkCore;

namespace Cw7.Services
{
    public class TripDbService : ITripDbService
    {
        private readonly s20950Context _context;

        public TripDbService(s20950Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetTripsResponseDto>> GetTripsAsync()
        {
            var trips = await _context.Trips.Select(row => new GetTripsResponseDto
            {
                Name = row.Name,
                Description = row.Description,
                DateFrom = row.DateFrom,
                DateTo = row.DateTo,
                MaxPeople = row.MaxPeople,
                Countries = row.CountryTrips.Select(row => new CountryResponseDto
                    { Name = row.IdCountryNavigation.Name }),
                Clients = row.ClientTrips.Select(row => new ClientResponseDto
                {
                    FirstName = row.IdClientNavigation.FirstName,
                    LastName = row.IdClientNavigation.LastName
                })
            }).OrderByDescending(col => col.DateFrom).ToListAsync();

            if (!trips.Any()) throw new Exception("Trips data not found");
            
            return trips;
        }

        public async Task<int> AddTripToClientAsync(int idTrip, AddTripToClientRequestDto dto)
        {
            bool clientExists = await _context.Clients.AnyAsync(row => row.Pesel == dto.Pesel);

            Client client;
            if (!clientExists)
            {
                client = new Client
                {
                    IdClient = await _context.Clients.Select(row => row.IdClient).MaxAsync() + 1,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Telephone = dto.Telephone,
                    Pesel = dto.Pesel
                };
                await _context.Clients.AddAsync(client);
                await _context.SaveChangesAsync();
            }
            else
            {
                client = await _context.Clients.FirstOrDefaultAsync(row => row.Pesel == dto.Pesel);
            }
            bool tripExists = await _context.Trips.AnyAsync(row => row.IdTrip == idTrip);
            if (!tripExists) throw new Exception($"Trip with id {idTrip} not found");
            
            bool isClientAssignedToTrip = await _context.ClientTrips
                .AnyAsync(row => row.IdClient == client.IdClient && row.IdTrip == idTrip);
            if (isClientAssignedToTrip) throw new Exception("Client is already assigned to that trip!");


            await _context.ClientTrips.AddAsync(new ClientTrip
            {
                IdClient = client.IdClient,
                IdTrip = idTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = dto.PaymentDate
            });
            return await _context.SaveChangesAsync();
        }
        }
    }
