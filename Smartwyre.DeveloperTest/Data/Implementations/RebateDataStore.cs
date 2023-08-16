using System.Data;
using System.Threading.Tasks;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data.Implementations;

public sealed class RebateDataStore : BaseDataStore, IRebateDataStore
{
    public RebateDataStore(IDbConnection db) : base(db)
    {
    }

    public async Task<Rebate> GetRebateAsync(string rebateIdentifier)
    {
        // Access database to retrieve account, code removed for brevity 
        return await QueryFirstAsync<Rebate>("SELECT * FROM get_rebate(@identifier)", new
        {
            identifier = rebateIdentifier
        });
    }

    public async Task StoreCalculationResultAsync(Rebate account, string productIdentifier, decimal rebateAmount)
    {
        // Update account in database, code removed for brevity
        await ExecuteAsync("CALL save_rebate_calculation(@productIdentifier, @rebateIdentifier, @amount)", new
        {
            productIdentifier,
            rebateIdentifier = account.Identifier,
            amount = rebateAmount
        });
    }
}
