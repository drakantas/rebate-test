using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Types.Incentives;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Types.Incentives;

public sealed class FixedRateRebateIncentiveTest
{
    [Fact]
    public void TestValidateSuccess()
    {
        var product = GetProduct();
        var rebate = GetRebate();
        var volume = 100m;

        var incentive = new FixedRateRebateIncentive();

        var result = incentive.Validate(product, rebate, volume);

        Assert.True(result);
    }

    [Fact]
    public void TestValidateVolumeError()
    {
        var product = GetProduct();
        var rebate = GetRebate();
        var volume = 0m;

        var incentive = new FixedRateRebateIncentive();

        var result = incentive.Validate(product, rebate, volume);

        Assert.False(result);
    }

    [Fact]
    public void TestCalculateSuccess()
    {
        var product = GetProduct();
        var rebate = GetRebate();
        var volume = 100m;
        var expectedCalculation = 70500m;

        var incentive = new FixedRateRebateIncentive();

        var result = incentive.Calculate(product, rebate, volume);

        Assert.Equal(expectedCalculation, result);
    }


    private static Product GetProduct() => new()
    {
        Id = 1,
        Identifier = "potato",
        Price = 1000.0m,
        SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
        Uom = "KG"
    };

    private static Rebate GetRebate() => new()
    {
        Amount = 1000,
        Identifier = "potato_rebate",
        Incentive = IncentiveType.FixedRateRebate,
        Percentage = 0.705m
    };
}

