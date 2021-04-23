using Insurance.Api.DTOs;
using System.Collections.Generic;

namespace Insurance.Api.Services
{
    public interface IInsuranceService
    {
        InsuranceDto CalculateInsurance(int productId);
    }
}