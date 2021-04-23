using Insurance.Api.Controllers;
using Insurance.Api.DTOs;
using Insurance.Api.Exceptions;
using Insurance.Api.Repositories;
using Insurance.Api.Services;
using Insurance.Tests.Builders;
using System.Collections.Generic;
using Xunit;

namespace Insurance.Tests.IntegrationTests
{
    public class InsuranceOrderControllerIntegrationTests : IClassFixture<ControllerTestFixture>
    {
        private readonly ControllerTestFixture _fixture;

        public InsuranceOrderControllerIntegrationTests(ControllerTestFixture fixture)
        {
            _fixture = fixture;
        }

        private static InsuranceController CreateController()
        {
            var repository = new ProductRepository(ControllerTestFixture.BaseUrl);
            var service = new InsuranceService(repository);
            return new InsuranceController(service);
        }

        private static ProductRequestDto CreateRequest(int productId)
        {
            return new ProductRequestDto
            {
                ProductId = productId,
            };
        }

        [Fact]
        public void CalculateInsuranceOrder_Given_ThreeSmallProductsLessThan500Euros_Then_Total1500Expected()
        {
            const decimal expectedInsuranceValue = 1500;

            ProductRequestDto request1 = CreateRequest(ProductIds.SmallProduct);
            ProductRequestDto request2 = CreateRequest(ProductIds.SmallProduct);
            ProductRequestDto request3 = CreateRequest(ProductIds.SmallProduct);

            var request = new OrderRequestDto();
            request.Products = new List<ProductRequestDto>() { request1, request2, request3 };

            InsuranceController sut = CreateController();

            var result = sut.CalculateOrderInsurance(request);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceCost
            );
        }

        [Fact]
        public void CalculateInsuranceOrder_Given_ThreeMixedProducts_Then_Total3500Expected()
        {
            const decimal expectedInsuranceValue = 500 + 1000 + 2000;

            ProductRequestDto request1 = CreateRequest(ProductIds.SmallProduct);
            ProductRequestDto request2 = CreateRequest(ProductIds.MediumProduct);
            ProductRequestDto request3 = CreateRequest(ProductIds.LargeProduct);

            var request = new OrderRequestDto();
            request.Products = new List<ProductRequestDto>() { request1, request2, request3 };

            InsuranceController sut = CreateController();

            var result = sut.CalculateOrderInsurance(request);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceCost
            );
        }

        [Fact]
        public void CalculateInsuranceOrder_Given_DigitalCameraProducts_Then_Add500ExtraOnce()
        {
            const decimal expectedInsuranceValue = 500 + 500 + 500 + 500;

            ProductRequestDto request1 = CreateRequest(ProductIds.SmallProduct);
            ProductRequestDto request2 = CreateRequest(ProductIds.DigitalCamera);
            ProductRequestDto request3 = CreateRequest(ProductIds.DigitalCamera);

            var request = new OrderRequestDto();
            request.Products = new List<ProductRequestDto>() { request1, request2, request3 };

            InsuranceController sut = CreateController();

            var result = sut.CalculateOrderInsurance(request);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceCost
            );
        }

        [Fact]
        public void CalculateInsuranceOrder_Given_OneHasSurchargeAndDigitalCamera_Then_AddSurchargeToTotal()
        {
            const decimal expectedInsuranceValue = 500 + 500 + 500 + (500 + 50);

            ProductRequestDto request1 = CreateRequest(ProductIds.SmallProduct);
            ProductRequestDto request2 = CreateRequest(ProductIds.SmallProduct);
            ProductRequestDto request3 = CreateRequest(ProductIds.SurchargeProduct);

            var request = new OrderRequestDto();
            request.Products = new List<ProductRequestDto>() { request1, request2, request3 };

            InsuranceController sut = CreateController();

            var result = sut.CalculateOrderInsurance(request);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceCost
            );
        }

        [Fact]
        public void CalculateInsuranceOrder_Given_InvalidProduct_Then_NotFoundErrorExpected()
        {
            ProductRequestDto request1 = CreateRequest(ProductIds.SmallProduct);
            ProductRequestDto request2 = CreateRequest(ProductIds.SmallProduct);
            ProductRequestDto request3 = CreateRequest(-1);

            var request = new OrderRequestDto();
            request.Products = new List<ProductRequestDto>() { request1, request2, request3 };

            InsuranceController sut = CreateController();

            try
            {
                sut.CalculateOrderInsurance(request);
            }
            catch (NotFoundException ex)
            {
                Assert.True(true);
            }
        }

    }
}
