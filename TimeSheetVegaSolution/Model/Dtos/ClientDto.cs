using Model.Entities;

namespace Model.Dtos
{
    public class ClientDto
    {
        public ClientDto(Client client)
        {
            Id = client.Id;
            Name = client.Name;
            Address = client.Address;
            City = client.City;
            ZipCode = client.ZipCode;
            Country = client.Country;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public Country Country { get; set; }
    }
}
