using System.Data;
using AlexandruMaries.Data.Interfaces;
using Microsoft.Data.SqlClient;

namespace AlexandruMaries.Data;

public class DapperContext : IDapperContext
{
    private readonly string _connectionString;

    public DapperContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }
}