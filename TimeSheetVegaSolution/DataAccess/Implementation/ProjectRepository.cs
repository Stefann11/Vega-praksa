using Microsoft.Extensions.Configuration;
using Model.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Contracts.Interface.Repository;
using DataAccess.Adapter;
using System.Linq;
using DataAccess.Target;
using DataAccess.Adaptee;

namespace DataAccess.Implementation
{
    public class ProjectRepository : Repository, IProjectRepository
    {
        private readonly ITarget _target = new ProjectAdapter(new ProjectAdaptee());
        public ProjectRepository(IConfiguration configuration) : base(configuration) { }

        public IEnumerable<Project> GetAll()
        {
            StringBuilder queryBuilder = GetAllProjectsQuery();

            List<SqlParameter> parameters = new List<SqlParameter>();    

            string query = queryBuilder.ToString();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (Project)_target.ConvertSql(dataRow)).ToList();
        }

        public IEnumerable<Project> GetAllByPageAndQuery(string name, int page, string letter)
        {
            StringBuilder queryBuilder = GetAllProjectsQuery();
            List<SqlParameter> parameters = new List<SqlParameter>();

            AppendNameQuery(name, queryBuilder, parameters);
            AppendLetterQuery(letter, queryBuilder, parameters);
            AppendPageQuery(page, queryBuilder, parameters);

            string query = queryBuilder.ToString();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (Project)_target.ConvertSql(dataRow)).ToList();
        }

        public Project GetById(int id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.Project AS p, dbo.Employee AS e, dbo.Country AS co, dbo.Client AS c ");
            queryBuilder.Append("WHERE p.EmployeeId=e.Id AND p.ClientId = c.Id AND c.CountryId = co.id AND p.Id = @Id ");
            queryBuilder.Append("AND IsRemoved = 0;");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.Int) { Value = id };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            return (Project)_target.ConvertSql(
                ExecuteQuery(query, parameters).Rows[0]
            );
        }

        public Project Save(Project project)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.Project ");
            queryBuilder.Append("(Name, Description, ProjectStatus, EmployeeId, ClientId, IsRemoved) ");
            queryBuilder.Append("VALUES (@Name, @Description, @ProjectStatus, @EmployeeId, @ClientId, 0);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", SqlDbType.VarChar) { Value = project.Name },
                new SqlParameter("@Description", SqlDbType.VarChar) { Value = project.Description },
                new SqlParameter("@ProjectStatus", SqlDbType.Int) { Value = (int)project.ProjectStatus },
                new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = project.Employee.Id },
                new SqlParameter("@ClientId", SqlDbType.Int) { Value = project.Client.Id }
            };

            ExecuteQuery(query,parameters);

            project.Id = GetLatestId();

            return project;
        }

        public Project Edit(Project project)
        {
            StringBuilder queryBuilder = new StringBuilder("UPDATE dbo.Project ");
            queryBuilder.Append("SET Name = @Name, Description = @Description, ProjectStatus = @ProjectStatus, ");
            queryBuilder.Append("EmployeeId = @EmployeeId, ClientId = @ClientId ");
            queryBuilder.Append("WHERE Id = @Id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", SqlDbType.VarChar) { Value = project.Name },
                new SqlParameter("@Description", SqlDbType.VarChar) { Value = project.Description },
                new SqlParameter("@ProjectStatus", SqlDbType.Int) { Value = (int)project.ProjectStatus },
                new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = project.Employee.Id },
                new SqlParameter("@ClientId", SqlDbType.Int) { Value = project.Client.Id },
                new SqlParameter("@Id", SqlDbType.Int) { Value = project.Id }
            };

            ExecuteQuery(query, parameters);

            return project;
        }

        public Project Delete(Project project)
        {     
            StringBuilder queryBuilder = new StringBuilder("UPDATE dbo.Project ");
            queryBuilder.Append("SET IsRemoved = 1 ");
            queryBuilder.Append("WHERE Id = @Id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = project.Id }
            };

            ExecuteQuery(query, parameters);
            return project;
        }

        public int GetNumberOfPages()
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT COUNT(*) FROM dbo.Project ");
            queryBuilder.Append("WHERE IsRemoved = 0;");

            string query = queryBuilder.ToString();

            return (ExecuteScalar(query) + itemsOnPage - 1) / 3;
        }

        private int GetLatestId()
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT IDENT_CURRENT('dbo.Project')");

            string query = queryBuilder.ToString();

            return ExecuteScalar(query);
        }

        private void AppendPageQuery(int page, StringBuilder queryBuilder, List<SqlParameter> parameters)
        {
            int start = (page - 1) * itemsOnPage;

            queryBuilder.Append("ORDER BY p.Id ASC ");
            queryBuilder.Append("OFFSET @Start ROWS FETCH NEXT @End ROWS ONLY;");
            parameters.Add(new SqlParameter("@Start", SqlDbType.Int) { Value = start });
            parameters.Add(new SqlParameter("@End", SqlDbType.Int) { Value = itemsOnPage });
        }

        private void AppendLetterQuery(string letter, StringBuilder queryBuilder, List<SqlParameter> parameters)
        {
            if (!string.IsNullOrEmpty(letter))
            {
                queryBuilder.Append("AND p.Name LIKE @Letter + '%' ");
                parameters.Add(new SqlParameter("@Letter", SqlDbType.VarChar) { Value = letter });
            }
        }

        private void AppendNameQuery(string name, StringBuilder queryBuilder, List<SqlParameter> parameters)
        {
            if (!string.IsNullOrEmpty(name))
            {
                queryBuilder.Append("AND p.Name LIKE '%' + @Name + '%' ");
                parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar) { Value = name });
            }
        }

        private StringBuilder GetAllProjectsQuery()
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.Project AS p, dbo.Employee AS e, dbo.Country AS co, dbo.Client AS c ");
            queryBuilder.Append("WHERE p.EmployeeId=e.Id AND p.ClientId = c.Id AND c.CountryId = co.id ");
            queryBuilder.Append("AND IsRemoved = 0 ");
            return queryBuilder;
        }

        public IEnumerable<Project> GetAllByClient(int clientId)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.Project AS p, dbo.Employee AS e, dbo.Country AS co, dbo.Client AS c ");
            queryBuilder.Append("WHERE p.EmployeeId=e.Id AND p.ClientId = c.Id AND c.CountryId = co.id ");
            queryBuilder.Append("AND IsRemoved = 0 AND p.ClientId = @Id;");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.Int) { Value = clientId };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (Project)_target.ConvertSql(dataRow)).ToList();
        }
    }
}
