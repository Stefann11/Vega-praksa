using Contracts.Interface.Repository;
using Contracts.Interface.Service;
using Model.Dtos;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Implementation
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public IEnumerable<ClientDto> GetAll()
        {
            return _clientRepository.GetAll().Select(client => new ClientDto(client)).ToList();
        }

        public IEnumerable<ClientDto> GetAllByPageAndQuery(string name, int page, string letter)
        {
            throw new NotImplementedException();
        }

        public ClientDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ClientDto Save(Client obj)
        {
            throw new NotImplementedException();
        }

        public ClientDto Edit(Client obj)
        {
            throw new NotImplementedException();
        }

        public ClientDto Delete(Client obj)
        {
            throw new NotImplementedException();
        }   
    }
}
