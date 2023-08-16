﻿using System.Threading.Tasks;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public interface IRebateService
{
    Task<CalculateRebateResult> CalculateAsync(CalculateRebateRequest request);
}
