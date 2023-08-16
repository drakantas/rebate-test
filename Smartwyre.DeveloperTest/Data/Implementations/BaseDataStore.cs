using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Smartwyre.DeveloperTest.Data.Implementations;

public abstract class BaseDataStore
{
    protected readonly IDbConnection db;

    public BaseDataStore(IDbConnection db)
    {
        this.db = db;
    }

    protected async Task<T> QueryFirstAsync<T>(string query, object parameters) where T : class
    {
        db.Open();

        var result = await db.QueryFirstOrDefaultAsync<T>(query, parameters, commandType: CommandType.Text, commandTimeout: 10);

        db.Close();

        return result;
    }

    protected async Task ExecuteAsync(string storedProcedure, object parameters)
    {
        db.Open();

        await db.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.Text, commandTimeout: 10);

        db.Close();
    }
}

