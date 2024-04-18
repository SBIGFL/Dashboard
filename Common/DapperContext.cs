using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string? _connectionString;
        public DapperContext(IConfiguration configuration)//,string Connectionstring)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
            //_connectionString = _configuration.GetConnectionString("SqlConnection") == null ? _configuration.GetConnectionString("Server") : _configuration.GetConnectionString("SqlConnection");

        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

    }
}
