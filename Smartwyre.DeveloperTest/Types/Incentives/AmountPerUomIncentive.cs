namespace Smartwyre.DeveloperTest.Types.Incentives;

public sealed class AmountPerUomIncentive : IIncentive
{
    public decimal Calculate(Product product, Rebate rebate, decimal volume)
    {
        return rebate.Amount * volume;
    }

    public bool Validate(Product product, Rebate rebate, decimal volume)
    {
        if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom))
        {
            return false;
        }
        else if (rebate.Amount == 0 || volume == 0)
        {
            return false;
        }

        return true;
    }
}

