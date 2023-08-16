using Smartwyre.DeveloperTest.Types;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Data;

public interface IRebateDataStore
{
    Task<Rebate> GetRebateAsync(string rebateIdentifier);

    Task StoreCalculationResultAsync(Rebate account, string productIdentifier, decimal rebateAmount);
}

