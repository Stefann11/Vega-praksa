using Model.Entities;
using System.Data;

namespace DataAccess.Adaptee
{
    public class ProjectAdaptee
    {
        public Project ConvertSqlDataReaderToProject(DataRow dataRow)
        {
            return new Project
            {
                Id = int.Parse(dataRow[0].ToString()),
                Name = dataRow[1].ToString().Trim(),
                Description = dataRow[2].ToString().Trim(),
                ProjectStatus = (ProjectStatus)int.Parse(dataRow[3].ToString().Trim()),
                IsRemoved = bool.Parse(dataRow[7].ToString().Trim()),
                Employee = new Employee
                {
                    Id = int.Parse(dataRow[8].ToString()),
                    Name = dataRow[9].ToString().Trim(),
                    Username = dataRow[10].ToString().Trim(),
                    Email = dataRow[11].ToString().Trim(),
                    Password = dataRow[12].ToString().Trim(),
                    HoursPerWeek = float.Parse(dataRow[13].ToString().Trim()),
                    EmployeeStatus = (EmployeeStatus)int.Parse(dataRow[14].ToString().Trim()),
                    Role = (Role)int.Parse(dataRow[15].ToString().Trim())
                },
                Client = new Client
                {
                    Id = int.Parse(dataRow[18].ToString()),
                    Name = dataRow[19].ToString().Trim(),
                    Address = dataRow[20].ToString().Trim(),
                    City = dataRow[21].ToString().Trim(),
                    ZipCode = dataRow[22].ToString().Trim(),
                    Country = new Country
                    {
                        Id = int.Parse(dataRow[16].ToString()),
                        Name = dataRow[17].ToString().Trim()
                    }
                }
            };
        }
    }
}
