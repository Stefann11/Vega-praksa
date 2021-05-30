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
using DataAccess.Target;
using DataAccess.Adaptee;
using Model.CustomException;
using Model.Dtos;

namespace DataAccess.Implementation
{
    public class EmployeeRepository : Repository, IEmployeeRepository
    {
        public ITarget _target = new EmployeeAdapter(new EmployeeAdaptee());
        public EmployeeRepository(IConfiguration configuration) : base(configuration){ }

        public IEnumerable<Employee> GetAll()
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.Employee");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>();

            DataTable dataTable = ExecuteQuery(query, parameters);
            
            return (from DataRow dataRow in dataTable.Rows
                    select (Employee)_target.ConvertSql(dataRow)).ToList();
        }

        public Employee GetByLoginInfo(UserModel login)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.Employee ");
            queryBuilder.Append("WHERE Email = @Email AND Password = @Password;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Email", SqlDbType.VarChar) { Value = login.Email },
                new SqlParameter("@Password", SqlDbType.VarChar) { Value = login.Password },
            };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return dataTable.Rows.Count > 0 ?
                (Employee)_target.ConvertSql(dataTable.Rows[0]) :
                throw new HttpStatusException
                (
                    System.Net.HttpStatusCode.BadRequest, "Login informations are not valid"
                );
        }

        public Employee GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Employee Save(Employee obj)
        {
            throw new NotImplementedException();
        }

        public Employee Edit(Employee obj)
        {
            throw new NotImplementedException();
        }

        public Employee Delete(Employee obj)
        {
            throw new NotImplementedException();
        }
    }
}
