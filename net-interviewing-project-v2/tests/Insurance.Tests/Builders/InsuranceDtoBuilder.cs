using Insurance.Api.DTOs;

namespace Insurance.Tests.Builders
{
    public class InsuranceDtoBuilder
    {
        public static InsuranceDto BuildValid()
        {
            var result = new InsuranceDto();
            result.ProductId = 1;
            result.ProductTypeHasInsurance = true;
            result.ProductTypeName = "";// ProductTypes.Laptops;
            result.SalesPrice = 1200;
            return result;
        }
    }
}
