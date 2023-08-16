using System.Data;
using System.Threading.Tasks;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data.Implementations;

public sealed class ProductDataStore : BaseDataStore, IProductDataStore
{
    public ProductDataStore(IDbConnection db) : base(db)
    {
    }

    public async Task<Product> GetProductAsync(string productIdentifier)
    {
        // Access database to retrieve account, code removed for brevity 
        return await QueryFirstAsync<Product>("SELECT * FROM get_product(@identifier)", new
        {
            identifier = productIdentifier
        });
    }
}
