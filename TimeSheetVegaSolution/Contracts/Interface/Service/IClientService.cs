using Model.Dtos;
using Model.Entities;
using System.Collections.Generic;

namespace Contracts.Interface.Service
{
    public interface IClientService : IService<Client, ClientDto>
    {
        IEnumerable<ClientDto> GetAllByPageAndQuery(string name, int page, string letter);
    }
}
