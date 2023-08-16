namespace Smartwyre.DeveloperTest.Types.Incentives;

public sealed class FixedRateRebateIncentive : IIncentive
{
    public decimal Calculate(Product product, Rebate rebate, decimal volume)
    {
        return product.Price * rebate.Percentage * volume;
    }

    public bool Validate(Product product, Rebate rebate, decimal volume)
    {
        if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate))
        {
            return false;
        }
        else if (rebate.Percentage == 0 || product.Price == 0 || volume == 0)
        {
            return false;
        }

        return true;
    }
}

