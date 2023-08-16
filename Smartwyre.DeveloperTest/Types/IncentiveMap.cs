using System.Collections.Generic;
using Smartwyre.DeveloperTest.Types.Incentives;

namespace Smartwyre.DeveloperTest.Types;

public sealed class IncentiveMap
{
    private static readonly IDictionary<IncentiveType, IIncentive> map = new Dictionary<IncentiveType, IIncentive>()
    {
        { IncentiveType.AmountPerUom, new AmountPerUomIncentive() },
        { IncentiveType.FixedCashAmount, new FixedCashAmountIncentive() },
        { IncentiveType.FixedRateRebate, new FixedRateRebateIncentive() }
    };

    public static IIncentive Get(IncentiveType incentiveType)
    {
        return map[incentiveType];
    }
}

