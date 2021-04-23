using Insurance.Api.DTOs;
using System.Collections.Generic;

namespace Insurance.Api.Repositories
{
    public interface IProductRepository
    {
        List<ProductTypeDto> GetProductTypes();
        ProductDto GetProduct(int productId);

        List<SurchargeDto> GetSurcharges();
    }
}