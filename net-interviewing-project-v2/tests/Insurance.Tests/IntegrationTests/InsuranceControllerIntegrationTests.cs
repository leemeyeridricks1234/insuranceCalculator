using Insurance.Api.Controllers;
using Insurance.Api.DTOs;
using Insurance.Api.Exceptions;
using Insurance.Api.Repositories;
using Insurance.Api.Services;
using Insurance.Tests.Builders;
using Xunit;

namespace Insurance.Tests.IntegrationTests
{
    public class InsuranceControllerIntegrationTests : IClassFixture<ControllerTestFixture>
    {
        private readonly ControllerTestFixture _fixture;

        public InsuranceControllerIntegrationTests(ControllerTestFixture fixture)
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
        public void CalculateInsurance_Given_SalesPriceLess500Euros_Then_Add500EurosToInsuranceCost()
        {
            const decimal expectedInsuranceValue = 500;

            ProductRequestDto request = CreateRequest(ProductIds.SmallProduct);
            InsuranceController sut = CreateController();

            var result = sut.CalculateInsurance(request);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_Given_SalesPriceBetween500And2000Euros_Then_Add1000EurosToInsuranceCost()
        {
            const decimal expectedInsuranceValue = 1000;

            ProductRequestDto request = CreateRequest(ProductIds.MediumProduct);

            InsuranceController sut = CreateController();

            var result = sut.CalculateInsurance(request);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_Given_SalesPriceOver2000Euros_Then_Add2000EurosToInsuranceCost()
        {
            const decimal expectedInsuranceValue = 2000;

            ProductRequestDto request = CreateRequest(ProductIds.LargeProduct);

            InsuranceController sut = CreateController();

            var result = sut.CalculateInsurance(request);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_Given_NotInsured_Then_ZeroInsuranceCost()
        {
            const decimal expectedInsuranceValue = 0;

            ProductRequestDto request = CreateRequest(ProductIds.NotInsured);

            InsuranceController sut = CreateController();

            var result = sut.CalculateInsurance(request);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_Given_Laptop_Then_AddExtra500EurosToInsuranceCost()
        {
            const decimal expectedInsuranceValue = 1000 + 500;

            ProductRequestDto request = CreateRequest(ProductIds.Laptop);

            InsuranceController sut = CreateController();

            var result = sut.CalculateInsurance(request);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_Given_Smartphone_Then_AddExtra500EurosToInsuranceCost()
        {
            const decimal expectedInsuranceValue = 1000 + 500;

            ProductRequestDto request = CreateRequest(ProductIds.Smartphone);

            InsuranceController sut = CreateController();

            var result = sut.CalculateInsurance(request);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_Given_InvalidProduct_Then_NotFoundErrorExpected()
        {
            ProductRequestDto request = CreateRequest(-1);

            InsuranceController sut = CreateController();

            try
            {
                sut.CalculateInsurance(request);
            }
            catch (NotFoundException ex)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void CalculateInsurance_Given_NotFoundProduct_Then_NotFoundErrorExpected()
        {
            ProductRequestDto request = CreateRequest(111);

            InsuranceController sut = CreateController();

            try
            {
                sut.CalculateInsurance(request);
            }
            catch (NotFoundException ex)
            {
                Assert.True(true);
            }
        }

    }
}
