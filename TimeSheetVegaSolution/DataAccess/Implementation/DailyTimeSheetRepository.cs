using Contracts.Interface.Repository;
using DataAccess.Adaptee;
using DataAccess.Adapter;
using DataAccess.Target;
using Microsoft.Extensions.Configuration;
using Model.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System;
using Model.Dtos;
using System.Globalization;

namespace DataAccess.Implementation
{
    public class DailyTimeSheetRepository : Repository, IDailyTimeSheetRepository
    {
        public ITarget _target = new DailyTimeSheetAdapter(new DailyTimeSheetAdaptee());
        public DailyTimeSheetRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<DailyTimeSheet> GetAll()
        {
            StringBuilder queryBuilder = GetAllDailyTimeSheetsQuery();

            List<SqlParameter> parameters = new List<SqlParameter>();

            string query = queryBuilder.ToString();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (DailyTimeSheet)_target.ConvertSql(dataRow)).ToList();
        }

        public IEnumerable<DailyTimeSheet> GetAllByDate(DateTime date, int employeeId)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * FROM dbo.DailyTimeSheet AS d, ");
            queryBuilder.Append("dbo.Project AS p, dbo.Category AS c, dbo.Employee AS e, ");
            queryBuilder.Append("dbo.Client AS cl, dbo.Country AS co ");
            queryBuilder.Append("WHERE d.ProjectId = p.Id AND d.CategoryId = c.Id AND d.EmployeeId = e.Id ");
            queryBuilder.Append("AND p.ClientId = cl.Id AND cl.CountryId = co.Id AND d.IsRemoved = 0 ");
            queryBuilder.Append("AND DAY(DATE) = @Day AND MONTH(Date) = @Month AND YEAR(Date) = @Year ");
            queryBuilder.Append("AND e.Id = @Id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Day", SqlDbType.Int) { Value = date.Day },
                new SqlParameter("@Month", SqlDbType.Int) { Value = date.Month },
                new SqlParameter("@Year", SqlDbType.Int) { Value = date.Year },
                new SqlParameter("@Id", SqlDbType.Int) { Value = employeeId }
            };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (DailyTimeSheet)_target.ConvertSql(dataRow)).ToList();
        }


        public IEnumerable<DailyTimeSheet> GetAllBetweenDates(DateTime startDate, DateTime endDate, int employeeId)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * FROM dbo.DailyTimeSheet AS d, ");
            queryBuilder.Append("dbo.Project AS p, dbo.Category AS c, dbo.Employee AS e, ");
            queryBuilder.Append("dbo.Client AS cl, dbo.Country AS co ");
            queryBuilder.Append("WHERE d.ProjectId = p.Id AND d.CategoryId = c.Id AND d.EmployeeId = e.Id ");
            queryBuilder.Append("AND p.ClientId = cl.Id AND cl.CountryId = co.Id AND d.IsRemoved = 0 ");
            queryBuilder.Append("AND (Date between @Start and @End) AND e.Id = @Id");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Start", SqlDbType.VarChar) { Value = startDate.Year + "-" + startDate.Month + "-" +  startDate.Day },
                new SqlParameter("@End", SqlDbType.VarChar) { Value = endDate.Year + "-" + endDate.Month + "-" + endDate.Day },
                new SqlParameter("@Id", SqlDbType.Int) { Value = employeeId }
            };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (DailyTimeSheet)_target.ConvertSql(dataRow)).ToList();
        }

        public DailyTimeSheet GetById(int id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * FROM dbo.DailyTimeSheet AS d, ");
            queryBuilder.Append("dbo.Project AS p, dbo.Category AS c, dbo.Employee AS e, ");
            queryBuilder.Append("dbo.Client AS cl, dbo.Country AS co ");
            queryBuilder.Append("WHERE d.ProjectId = p.Id AND d.CategoryId = c.Id AND d.EmployeeId = e.Id ");
            queryBuilder.Append("AND p.ClientId = cl.Id AND cl.CountryId = co.Id AND d.Id = @Id ");
            queryBuilder.Append("AND d.IsRemoved = 0;");
            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.Int) { Value = id };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            return (DailyTimeSheet)_target.ConvertSql(
                ExecuteQuery(query, parameters).Rows[0]
            );
        }

        public DailyTimeSheet Save(DailyTimeSheet dailyTimeSheet, int employeeId)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.DailyTimeSheet ");
            queryBuilder.Append("(Date, Description, Time, Overtime, ProjectId, CategoryId, ");
            queryBuilder.Append("EmployeeId, IsRemoved) ");
            queryBuilder.Append("VALUES (@Date, @Description, @Time, @Overtime, @ProjectId, @CategoryId, ");
            queryBuilder.Append("@EmployeeId, 0);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Date", SqlDbType.VarChar) { Value = dailyTimeSheet.Date },
                new SqlParameter("@Description", SqlDbType.VarChar) { Value = dailyTimeSheet.Description },
                new SqlParameter("@Time", SqlDbType.Float) { Value = dailyTimeSheet.Time },
                new SqlParameter("@Overtime", SqlDbType.Float) { Value = dailyTimeSheet.Overtime },
                new SqlParameter("@ProjectId", SqlDbType.Int) { Value = dailyTimeSheet.Project.Id },
                new SqlParameter("@CategoryId", SqlDbType.Int) { Value = dailyTimeSheet.Category.Id },
                new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = employeeId }
            };

            ExecuteQuery(query, parameters);

            dailyTimeSheet.Id = GetLatestId();

            return dailyTimeSheet;
        }

        public DailyTimeSheet Edit(DailyTimeSheet dailyTimeSheet, int employeeId)
        {
            StringBuilder queryBuilder = new StringBuilder("UPDATE dbo.DailyTimeSheet ");
            queryBuilder.Append("SET Date = @Date, Description = @Description, Time = @Time, Overtime = @Overtime, ");
            queryBuilder.Append("ProjectId = @ProjectId, CategoryId = @CategoryId, EmployeeId = @EmployeeId ");
            queryBuilder.Append("WHERE Id = @Id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Date", SqlDbType.VarChar) { Value = dailyTimeSheet.Date },
                new SqlParameter("@Description", SqlDbType.VarChar) { Value = dailyTimeSheet.Description },
                new SqlParameter("@Time", SqlDbType.Float) { Value = dailyTimeSheet.Time },
                new SqlParameter("@Overtime", SqlDbType.Float) { Value = dailyTimeSheet.Overtime },
                new SqlParameter("@ProjectId", SqlDbType.Int) { Value = dailyTimeSheet.Project.Id },
                new SqlParameter("@CategoryId", SqlDbType.Int) { Value = dailyTimeSheet.Category.Id },
                new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = employeeId },
                new SqlParameter("@Id", SqlDbType.Int) { Value = dailyTimeSheet.Id }
            };

            ExecuteQuery(query, parameters);

            return dailyTimeSheet;
        }

        public DailyTimeSheet Delete(DailyTimeSheet dailyTimeSheet)
        {
            StringBuilder queryBuilder = new StringBuilder("UPDATE dbo.DailyTimeSheet ");
            queryBuilder.Append("SET IsRemoved = 1 ");
            queryBuilder.Append("WHERE Id = @Id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = dailyTimeSheet.Id }
            };

            ExecuteQuery(query, parameters);
            return dailyTimeSheet;
        }

        private int GetLatestId()
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT IDENT_CURRENT('dbo.DailyTimeSheet')");

            string query = queryBuilder.ToString();

            return ExecuteScalar(query);
        }

        public IEnumerable<DailyTimeSheet> GetAllBySearch(SearchDto searchDto)
        {
            StringBuilder queryBuilder = GetAllDailyTimeSheetsQuery();
            List<SqlParameter> parameters = new List<SqlParameter>();

            AppendEmployeeQuery(searchDto.Employee, queryBuilder, parameters);
            AppendClientQuery(searchDto.Client, queryBuilder, parameters);
            AppendProjectQuery(searchDto.Project, queryBuilder, parameters);
            AppendCategoryQuery(searchDto.Category, queryBuilder, parameters);
            AppendStartDateQuery(searchDto.StartDate, queryBuilder, parameters);
            AppendEndDateQuery(searchDto.EndDate, queryBuilder, parameters);

            string query = queryBuilder.ToString();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (DailyTimeSheet)_target.ConvertSql(dataRow)).ToList();
        }

        public IEnumerable<DailyTimeSheet> GetByEmployee(SearchDto searchDto)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * FROM dbo.DailyTimeSheet AS d, ");
            queryBuilder.Append("dbo.Project AS p, dbo.Category AS c, dbo.Employee AS e, ");
            queryBuilder.Append("dbo.Client AS cl, dbo.Country AS co ");
            queryBuilder.Append("WHERE d.ProjectId = p.Id AND d.CategoryId = c.Id AND d.EmployeeId = e.Id ");
            queryBuilder.Append("AND p.ClientId = cl.Id AND cl.CountryId = co.Id AND e.Id = @Id ");
            queryBuilder.Append("AND d.IsRemoved = 0 ");
            
            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.Int) { Value = searchDto.Employee };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            AppendClientQuery(searchDto.Client, queryBuilder, parameters);
            AppendProjectQuery(searchDto.Project, queryBuilder, parameters);
            AppendCategoryQuery(searchDto.Category, queryBuilder, parameters);
            AppendStartDateQuery(searchDto.StartDate, queryBuilder, parameters);
            AppendEndDateQuery(searchDto.EndDate, queryBuilder, parameters);

            string query = queryBuilder.ToString();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (DailyTimeSheet)_target.ConvertSql(dataRow)).ToList();
        }

        private StringBuilder GetAllDailyTimeSheetsQuery()
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * FROM dbo.DailyTimeSheet AS d, ");
            queryBuilder.Append("dbo.Project AS p, dbo.Category AS c, dbo.Employee AS e, ");
            queryBuilder.Append("dbo.Client AS cl, dbo.Country AS co ");
            queryBuilder.Append("WHERE d.ProjectId = p.Id AND d.CategoryId = c.Id AND d.EmployeeId = e.Id ");
            queryBuilder.Append("AND p.ClientId = cl.Id AND cl.CountryId = co.Id AND d.IsRemoved = 0 ");

            return queryBuilder;
        }

        private void AppendEmployeeQuery(string employee, StringBuilder queryBuilder, List<SqlParameter> parameters)
        {
            if (!string.IsNullOrEmpty(employee))
            {
                queryBuilder.Append("AND e.Id = @Id ");
                parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = int.Parse(employee) });
            }
        }

        private void AppendClientQuery(string client, StringBuilder queryBuilder, List<SqlParameter> parameters)
        {
            if (!string.IsNullOrEmpty(client))
            {
                queryBuilder.Append("AND cl.Id = @ClientId ");
                parameters.Add(new SqlParameter("@ClientId", SqlDbType.Int) { Value = int.Parse(client) });
            }
        }

        private void AppendProjectQuery(string project, StringBuilder queryBuilder, List<SqlParameter> parameters)
        {
            if (!string.IsNullOrEmpty(project))
            {
                queryBuilder.Append("AND p.Id = @ProjectId ");
                parameters.Add(new SqlParameter("@ProjectId", SqlDbType.Int) { Value = int.Parse(project) });
            }
        }

        private void AppendCategoryQuery(string category, StringBuilder queryBuilder, List<SqlParameter> parameters)
        {
            if (!string.IsNullOrEmpty(category))
            {
                queryBuilder.Append("AND c.Id = @CategoryId ");
                parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int) { Value = int.Parse(category) });
            }
        }

        private void AppendStartDateQuery(string startDate, StringBuilder queryBuilder, List<SqlParameter> parameters)
        {
            if (!string.IsNullOrEmpty(startDate))
            {
                string exactDate = DateTime.ParseExact(startDate.Substring(4, 11), "MMM dd yyyy",
                    CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                queryBuilder.Append("AND Date >= Convert(datetime, @Start) ");
                parameters.Add(new SqlParameter("@Start", SqlDbType.VarChar) { Value = exactDate });
            }
        }

        private void AppendEndDateQuery(string endDate, StringBuilder queryBuilder, List<SqlParameter> parameters)
        {
            if (!string.IsNullOrEmpty(endDate))
            {
                string exactDate = DateTime.ParseExact(endDate.Substring(4, 11), "MMM dd yyyy",
                     CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                queryBuilder.Append("AND Date <= Convert(datetime, @End) ");
                parameters.Add(new SqlParameter("@End", SqlDbType.VarChar) { Value = exactDate });
            }
        }
    }
}
