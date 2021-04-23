using Insurance.Api.DTOs;
using Insurance.Tests.Builders;
using System.Collections.Generic;

namespace Insurance.Tests.Helpers
{
    public class DataHelper
    {
        public static ProductRequestDto CreateRequest(int productId)
        {
            return new ProductRequestDto
            {
                ProductId = productId,
            };
        }

        public static List<ProductTypeDto> GetProductTypes()
        {
            return new List<ProductTypeDto>() {
                new ProductTypeDto()
                {
                    Id = 1,
                    CanBeInsured = true,
                    Name = "Test"
                } ,
                new ProductTypeDto()
                {
                    Id = 2,
                    CanBeInsured = false,
                    Name = "Not Insured"
                },
                new ProductTypeDto()
                {
                    Id = 3,
                    CanBeInsured = true,
                    Name = "Surcharge"
                }
            };
        }

        public static List<SurchargeDto> GetSurcharges()
        {
            return new List<SurchargeDto>() {
                new SurchargeDto()
                {
                    Id = 1,
                    ProductTypeId = 3,
                    Surcharge = 50
                } };
        }

        public static ProductDto CreateProduct()
        {
            return new ProductDto()
            {
                Id = ProductIds.SmallProduct,
                ProductTypeId = 1,
                SalesPrice = 400
            };
        }
    }
}
