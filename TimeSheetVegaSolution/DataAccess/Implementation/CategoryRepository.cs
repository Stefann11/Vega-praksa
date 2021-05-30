using Contracts.Interface.Repository;
using DataAccess.Adaptee;
using DataAccess.Adapter;
using DataAccess.Target;
using Microsoft.Extensions.Configuration;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

namespace DataAccess.Implementation
{
    public class CategoryRepository : Repository, ICategoryRepository
    {
        public ITarget _target = new CategoryAdapter(new CategoryAdaptee());
        public CategoryRepository(IConfiguration configuration) : base(configuration) { }
        public IEnumerable<Category> GetAll()
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.Category");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (Category)_target.ConvertSql(dataRow)).ToList();
        }

        public Category GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Category Save(Category obj)
        {
            throw new NotImplementedException();
        }

        public Category Edit(Category obj)
        {
            throw new NotImplementedException();
        }

        public Category Delete(Category obj)
        {
            throw new NotImplementedException();
        }      
    }
}
