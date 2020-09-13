﻿using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleApiIntegrationTests;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace IntegrationTestsSimpleApi.Tools
{
    public class DatabaseFixture
    {
        private readonly TestServer _server;
        private readonly IServiceProvider _serviceProvider;
        private IConfiguration _configuration;
        private const string ConnectionTest = "Data Source=localhost;Initial Catalog=SimpleApi_IntegrationTest;Persist Security Info=True;User ID={__USERNAME__};Password={__PASSWORD__};";

        public DatabaseFixture()
        {
            ConnectionChange();

            var servicesProvider = new WebHostBuilder()
                .UseStartup<Startup>()
                .UseEnvironment("Development")
                .UseConfiguration(_configuration);

            _server ??= new TestServer(servicesProvider);
            _serviceProvider = _server.Services;
        }

        private void ConnectionChange()
        {
            _configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json")
                            .Build();

            _configuration["ConnectionStrings:ApiConnection"] = ConnectionTest;
        }

        public void ClearDataBase()
        {
            try
            {
                var script = File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}Script\\ScriptDB.sql");

                using var conn = new SqlConnection(_configuration.GetConnectionString("ApiConnection"));
                conn.Execute(script, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T GetService<T>() => _serviceProvider.GetService<T>();
    }
}
