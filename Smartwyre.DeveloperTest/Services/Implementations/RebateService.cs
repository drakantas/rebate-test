using System.Threading.Tasks;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Implementations;

public sealed class RebateService : IRebateService
{
    private readonly IRebateDataStore rebateDataStore;
    private readonly IProductDataStore productDataStore;

    public RebateService(IRebateDataStore rebateDataStore, IProductDataStore productDataStore)
    {
        this.rebateDataStore = rebateDataStore;
        this.productDataStore = productDataStore;
    }

    public async Task<CalculateRebateResult> CalculateAsync(CalculateRebateRequest request)
    {
        var rebate = await rebateDataStore.GetRebateAsync(request.RebateIdentifier);
        var product = await productDataStore.GetProductAsync(request.ProductIdentifier);

        var result = new CalculateRebateResult();

        if (rebate == null)
        {
            result.Success = false;

            goto leave;
        }
        else if (product == null)
        {
            result.Success = false;

            goto leave;
        }

        var rebateAmount = 0m;

        var incentive = IncentiveMap.Get(rebate.Incentive);

        if (!incentive.Validate(product, rebate, request.Volume))
        {
            result.Success = false;
        }
        else
        {
            result.Success = true;
            rebateAmount = incentive.Calculate(product, rebate, request.Volume);
        }

        if (result.Success)
        {
            await rebateDataStore.StoreCalculationResultAsync(rebate, product.Identifier, rebateAmount);
        }

    leave:
        return result;
    }
}
