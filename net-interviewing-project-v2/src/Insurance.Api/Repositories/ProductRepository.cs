using Insurance.Api.DTOs;
using Insurance.Api.Exceptions;
using Insurance.Api.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace Insurance.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string baseAddressProductAPI;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ProductRepository(IConfiguration configuration) :
            this(configuration.GetSection("ConnectionStrings:ProductApi").Value)
        {
        }

        public ProductRepository(string baseAddressProductAPI)
        {
            this.baseAddressProductAPI = baseAddressProductAPI;
        }

        public List<ProductTypeDto> GetProductTypes()
        {
            logger.Info("GetProductTypes starting.");
            using (HttpClient client = HttpClientHelper.CreateHttpConnection(baseAddressProductAPI))
            {
                string json = client.GetAsync("/product_types").Result.Content.ReadAsStringAsync().Result;
                var productTypes = JsonConvert.DeserializeObject<dynamic>(json);
                logger.Info(string.Format("Product Types found = {0}", (int)productTypes.Count));
                var dtos = new List<ProductTypeDto>();

                for (int i = 0; i < productTypes.Count; i++)
                {
                    var productType = productTypes[i];
                    var dto = ProductTypeDto.CreateProductTypeFromJson(productType);
                    
                    logger.Info(string.Format("Created ProductTypeDTO with ID = {0}", (int)dto.Id));
                    dtos.Add(dto);
                }
                logger.Info("GetProductTypes finished.");
                return dtos;
            }
        }

        public ProductDto GetProduct(int productId)
        {
            logger.Info("GetProduct starting.");
            using (HttpClient client = HttpClientHelper.CreateHttpConnection(baseAddressProductAPI))
            {
                var json = client.GetAsync(string.Format("/products/{0:G}", productId)).Result.Content.ReadAsStringAsync().Result;
                var product = JsonConvert.DeserializeObject<dynamic>(json);
                if (product == null || product.status == "404")
                {
                    logger.Error(string.Format("Product not found: {0}. Returned with 404", productId));
                    throw new NotFoundException("Product", productId);
                }
                    
                var dto = ProductDto.CreateProductFromJson(product);
                logger.Info(string.Format("Created Product with ID={0}", (int)dto.Id));
                logger.Info("GetProduct finished.");
                return dto;
            }
        }

        public List<SurchargeDto> GetSurcharges()
        {
            logger.Info("GetSurcharges starting.");
            var surcharges = new List<SurchargeDto>();

            surcharges.Add(new SurchargeDto
            {
                Id = 1,
                ProductTypeId = 32,
                Surcharge = 50
            });
            surcharges.Add(new SurchargeDto
            {
                Id = 2,
                ProductTypeId = 12,
                Surcharge = 60
            });

            logger.Info("GetSurcharges finished.");
            return surcharges;
        }
    }
}
