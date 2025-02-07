namespace Fiap.Hackathon.Medicos.Application.Abstractions.GeoLocalizacao.Responses
{
    public class NominatimResponse
    {
        public string DisplayName { get; set; } = default!;
        public Address? Address { get; set; }
    }

    public class Address
    {
        public string Country { get; set; } = default!;
        public string State { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Town { get; set; } = default!;
        public string Village { get; set; } = default!;
    }
}
