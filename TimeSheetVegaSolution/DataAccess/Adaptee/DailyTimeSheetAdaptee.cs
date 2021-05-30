using Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess.Adaptee
{
    public class DailyTimeSheetAdaptee
    {
        public DailyTimeSheet ConvertSqlDataReaderToDailyTimeSheet(DataRow dataRow)
        {
            return new DailyTimeSheet
            {
                Id = int.Parse(dataRow[0].ToString()),
                Date = DateTime.Parse(dataRow[1].ToString().Trim()),
                Description = dataRow[2].ToString().Trim(),
                Time = float.Parse(dataRow[3].ToString().Trim()),
                Overtime = float.Parse(dataRow[4].ToString().Trim()),
                IsRemoved = bool.Parse(dataRow[8].ToString().Trim()),
                Project = new Project
                {
                    Id = int.Parse(dataRow[9].ToString()),
                    Name = dataRow[10].ToString().Trim(),
                    Description = dataRow[11].ToString().Trim(),
                    ProjectStatus = (ProjectStatus)int.Parse(dataRow[12].ToString().Trim()),
                    IsRemoved = bool.Parse(dataRow[16].ToString().Trim()),
                    Employee = new Employee
                    {
                        Id = int.Parse(dataRow[19].ToString()),
                        Name = dataRow[20].ToString().Trim(),
                        Username = dataRow[21].ToString().Trim(),
                        Email = dataRow[22].ToString().Trim(),
                        Password = dataRow[23].ToString().Trim(),
                        HoursPerWeek = float.Parse(dataRow[24].ToString().Trim()),
                        EmployeeStatus = (EmployeeStatus)int.Parse(dataRow[25].ToString().Trim()),
                        Role = (Role)int.Parse(dataRow[26].ToString().Trim())
                    },
                    Client = new Client
                    {
                        Id = int.Parse(dataRow[27].ToString()),
                        Name = dataRow[28].ToString().Trim(),
                        Address = dataRow[29].ToString().Trim(),
                        City = dataRow[30].ToString().Trim(),
                        ZipCode = dataRow[31].ToString().Trim(),
                        Country = new Country
                        {
                            Id = int.Parse(dataRow[33].ToString()),
                            Name = dataRow[34].ToString().Trim()
                        }
                    }
                },
                Employee = new Employee
                {
                    Id = int.Parse(dataRow[19].ToString()),
                    Name = dataRow[20].ToString().Trim(),
                    Username = dataRow[21].ToString().Trim(),
                    Email = dataRow[22].ToString().Trim(),
                    Password = dataRow[23].ToString().Trim(),
                    HoursPerWeek = float.Parse(dataRow[24].ToString().Trim()),
                    EmployeeStatus = (EmployeeStatus)int.Parse(dataRow[25].ToString().Trim()),
                    Role = (Role)int.Parse(dataRow[26].ToString().Trim())
                },
                Category = new Category
                {
                    Id = int.Parse(dataRow[17].ToString()),
                    Name = dataRow[18].ToString().Trim()
                }
            };
        }
    }
}
