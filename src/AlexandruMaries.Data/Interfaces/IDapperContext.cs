using System.Data;

namespace AlexandruMaries.Data.Interfaces;

public interface IDapperContext
{
    public IDbConnection GetConnection();
}