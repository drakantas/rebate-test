using System;
namespace Smartwyre.DeveloperTest.Types.Readers;

public sealed class CalculateRebateRequestReader : BaseRequestReader
{
    public CalculateRebateRequest Request { get; private set; }

    public CalculateRebateRequestReader Read()
    {
        string rebateId, productId;
        decimal volume;

        rebateId = ReadLine("Rebate Identifier");
        productId = ReadLine("Product Identifier");
        volume = ReadDecimalLine("Volume");

        Request = new CalculateRebateRequest()
        {
            ProductIdentifier = productId,
            RebateIdentifier = rebateId,
            Volume = volume
        };

        return this;
    }
}

