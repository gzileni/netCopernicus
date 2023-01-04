using System;
using System.Configuration;
using Npgsql;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Copernicus
{
	public class CopernicusDB
	{
        protected string urlConnection = "";
        private NpgsqlConnection? dataSource;

        public CopernicusDB()
		{
            var appSettings = ConfigurationManager.AppSettings;
            string? host = appSettings["Host"];
            string? dbName = appSettings["Database"];
            string? username = appSettings["Username"];
            string? password = appSettings["Password"];
            string? port = appSettings["Port"];
            this.urlConnection = $"Host={host};Port={port};Database={dbName};Username={username};Password={password}";
        }

        public bool Connect()
        {
            this.dataSource = new NpgsqlConnection(this.urlConnection);
            this.dataSource.OpenAsync();
            return true;
        }

    }
}

