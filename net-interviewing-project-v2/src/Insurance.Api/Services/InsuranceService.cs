using Insurance.Api.DTOs;
using Insurance.Api.Exceptions;
using Insurance.Api.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Insurance.Api.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly IProductRepository productRepository;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public InsuranceService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public InsuranceDto CalculateInsurance(int productId)
        {
            logger.Info("CalculateInsurance starting.");
            ProductDto product = TryGetProduct(productId);
            logger.Info(string.Format("Product found with ID = {0}", (int)productId));

            List<ProductTypeDto> productTypes = productRepository.GetProductTypes();
            ProductTypeDto productType = TryGetProductType(productTypes, product.ProductTypeId);
            logger.Info(string.Format("Product Type found with ID = {0}", (int)productType.Id));

            List<SurchargeDto> surcharges = productRepository.GetSurcharges();
            var surcharge = surcharges.FirstOrDefault(x => x.ProductTypeId == product.ProductTypeId);
            logger.Info("Surcharge processed.");

            var insurance = InsuranceDto.Create(product, productType, surcharge);
            logger.Info(string.Format("Insurance Calculated with Value = {0}", (decimal)insurance.InsuranceValue));

            logger.Info("CalculateInsurance finished.");
            return insurance;
        }

        private ProductDto TryGetProduct(int productId)
        {
            var product = productRepository.GetProduct(productId);
            if (product == null)
            {
                logger.Error(string.Format("Product not found with ID = {0}", productId));
                throw new NotFoundException("ProductId", productId);
            }
            return product;
        }

        private ProductTypeDto TryGetProductType(List<ProductTypeDto> productTypes, int productTypeId)
        {
            if (productTypes == null)
            {
                logger.Error(string.Format("ProductTypes empty"));
                throw new NotFoundException("ProductTypes", null);
            }

            var productType = productTypes.FirstOrDefault(x => x.Id == productTypeId);
            if (productType == null)
            {
                logger.Error(string.Format("ProductType not found with ID = {0}", productTypeId));
                throw new NotFoundException("ProductTypeId", productTypeId);
            }

            return productType;
        }
    }
}
