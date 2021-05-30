using Model.Entities;
using System.Data;

namespace DataAccess.Adaptee
{
    public class EmployeeAdaptee
    {
        public Employee ConvertSqlDataReaderToEmployee(DataRow dataRow)
        {
            return new Employee
            {
                Id = int.Parse(dataRow[0].ToString()),
                Name = dataRow[1].ToString().Trim(),
                Username = dataRow[2].ToString().Trim(),
                Email = dataRow[3].ToString().Trim(),
                Password = dataRow[4].ToString().Trim(),
                HoursPerWeek = float.Parse(dataRow[5].ToString()),
                EmployeeStatus = (EmployeeStatus)int.Parse(dataRow[6].ToString().Trim()),
                Role = (Role)int.Parse(dataRow[7].ToString().Trim())
            };
        }
    }
}
