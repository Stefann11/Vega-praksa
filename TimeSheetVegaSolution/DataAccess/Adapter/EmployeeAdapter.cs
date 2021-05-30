using DataAccess.Adaptee;
using DataAccess.Target;
using System.Data;

namespace DataAccess.Adapter
{
    public class EmployeeAdapter : ITarget
    {
        private readonly EmployeeAdaptee employeeAdaptee;
        public EmployeeAdapter(EmployeeAdaptee employeeAdaptee)
        {
            this.employeeAdaptee = employeeAdaptee;
        }

        public object ConvertSql(DataRow dataRow)
        {
            return employeeAdaptee.ConvertSqlDataReaderToEmployee(dataRow);
        }
    }
}
