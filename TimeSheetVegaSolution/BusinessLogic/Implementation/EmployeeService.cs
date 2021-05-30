using Contracts.Interface.Repository;
using Contracts.Interface.Service;
using Model.Dtos;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            return _employeeRepository.GetAll().Select(employee => new EmployeeDto(employee)).ToList();
        }

        public EmployeeDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public EmployeeDto Save(Employee obj)
        {
            throw new NotImplementedException();
        }

        public EmployeeDto Edit(Employee obj)
        {
            throw new NotImplementedException();
        }

        public EmployeeDto Delete(Employee obj)
        {
            throw new NotImplementedException();
        }     
    }
}
