using System.Threading.Tasks;
using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services.Implementations;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Services;

public sealed class RebateServiceTest
{
    private readonly Mock<IProductDataStore> productDataStore = new();
    private readonly Mock<IRebateDataStore> rebateDataStore = new();

    [Fact]
    public async Task Test()
    {
        productDataStore.Setup(s => s.GetProductAsync(It.IsAny<string>())).ReturnsAsync(new Product()
        {
            Id = 1,
            Identifier = "potato",
            Price = 1000.0m,
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
            Uom = "KG"
        });
        rebateDataStore.Setup(s => s.GetRebateAsync(It.IsAny<string>())).ReturnsAsync(new Rebate()
        {
            Amount = 1000,
            Identifier = "potato_rebate",
            Incentive = IncentiveType.FixedRateRebate,
            Percentage = 0.705m
        });

        var service = new RebateService(rebateDataStore.Object, productDataStore.Object);

        var result = await service.CalculateAsync(GetRequest());

        Assert.NotNull(result);
        Assert.True(result.Success);
    }

    private static CalculateRebateRequest GetRequest() => new()
    {
        ProductIdentifier = "potato",
        RebateIdentifier = "potato_rebate",
        Volume = 100m
    };
}

