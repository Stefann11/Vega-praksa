using Model.Dtos;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Interface.Repository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        public Employee GetByLoginInfo(UserModel login);
    }
}
