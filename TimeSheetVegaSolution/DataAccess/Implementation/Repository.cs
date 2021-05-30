﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess.Implementation
{
    public class Repository
    {
        protected readonly string configurationString;
        protected readonly int itemsOnPage;

        public Repository(IConfiguration configuration)
        {
            configurationString = configuration.GetConnectionString("TimeSheetDatabase");
            itemsOnPage = int.Parse(configuration["ItemsOnPage"]);
        }

        public DataTable ExecuteQuery(string query, IEnumerable<SqlParameter> parameters)
        {
            using SqlConnection sqlConnection = new SqlConnection(configurationString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddRange(parameters.ToArray());
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            try { dataTable.Load(sqlDataReader); }
            catch (Exception) { }
            
            return dataTable;
        }

        public int ExecuteScalar(string query)
        {
            using SqlConnection sqlConnection = new SqlConnection(configurationString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            return Convert.ToInt32(sqlCommand.ExecuteScalar());
        }
    }
}
