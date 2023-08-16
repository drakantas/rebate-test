namespace Smartwyre.DeveloperTest.Types.Incentives;

public sealed class FixedCashAmountIncentive : IIncentive
{
    public decimal Calculate(Product product, Rebate rebate, decimal volume)
    {
        return rebate.Amount;
    }

    public bool Validate(Product product, Rebate rebate, decimal volume)
    {
        if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount))
        {
            return false;
        }
        else if (rebate.Amount == 0)
        {
            return false;
        }

        return true;
    }
}

