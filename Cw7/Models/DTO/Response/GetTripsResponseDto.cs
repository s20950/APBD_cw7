using System;
using System.Collections.Generic;

namespace Cw7.Models.DTO.Response
{
    public class GetTripsResponseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int MaxPeople { get; set; }
        public IEnumerable<CountryResponseDto> Countries { get; set; }
        public IEnumerable<ClientResponseDto> Clients { get; set; }
    }
}