using Model.Dtos;
using Model.Entities;

namespace Contracts.Interface.Service
{
    public interface IEmployeeService : IService<Employee, EmployeeDto>
    {
    }
}
