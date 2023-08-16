namespace Smartwyre.DeveloperTest.Types;

public interface IIncentive
{
    bool Validate(Product product, Rebate rebate, decimal volume);
    decimal Calculate(Product product, Rebate rebate, decimal volume);
}
