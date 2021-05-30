using Contracts.Interface.Repository;
using DataAccess.Adapter;
using Microsoft.Extensions.Configuration;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using DataAccess.Adaptee;
using DataAccess.Target;

namespace DataAccess.Implementation
{
    public class ClientRepository : Repository, IClientRepository
    {
        public ITarget _target = new ClientAdapter(new ClientAdaptee());
        public ClientRepository(IConfiguration configuration) : base(configuration){ }

        public IEnumerable<Client> GetAll()
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.Client AS c, dbo.Country AS co ");
            queryBuilder.Append("WHERE c.CountryId = co.id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>();

            DataTable dataTable = ExecuteQuery(query, parameters);
            return (from DataRow dataRow in dataTable.Rows
                    select (Client)_target.ConvertSql(dataRow)).ToList();
        }

        public IEnumerable<Client> GetAllByPageAndQuery(string name, int page, string letter)
        {
            throw new NotImplementedException();
        }

        public Client GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Client Save(Client obj)
        {
            throw new NotImplementedException();
        }

        public Client Edit(Client obj)
        {
            throw new NotImplementedException();
        }

        public Client Delete(Client obj)
        {
            throw new NotImplementedException();
        }
    }
}
