using Insurance.Api.DTOs;
using Insurance.Tests.Builders;
using Xunit;

namespace Insurance.Tests.UnitTests
{
    public  class InsuranceCalculationUnitTests
    {
        [Fact]
        public void InsuranceValue_Given_InsuranceAndSalesPriceLess500Euros_Should_Add500EurosToInsuranceCost()
        {
            var expectedInsuranceValue = 500;

            var result = InsuranceDtoBuilder.BuildValid();
            result.SalesPrice = 400;

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void InsuranceValue_Given_InsuranceAndSalesPriceEqual500Euros_Should_Add500EurosToInsuranceCost()
        {
            var expectedInsuranceValue = 500;

            var result = InsuranceDtoBuilder.BuildValid();
            result.SalesPrice = 500;

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void InsuranceValue_Given_InsuranceAndSalesPriceBetween500And2000Euros_Then_Add1000EurosToInsuranceCost()
        {
            var expectedInsuranceValue = 1000;

            var result = InsuranceDtoBuilder.BuildValid();
            result.SalesPrice = 1200;

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }


        [Fact]
        public void InsuranceValue_Given_InsuranceAndSalesPriceGreater2000Euros_Should_Add2000EurosToInsuranceCost()
        {
            var expectedInsuranceValue = 2000;

            var result = InsuranceDtoBuilder.BuildValid();
            result.SalesPrice = 2500;

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void InsuranceValue_Given_NotInsurance_Should_ZeroInsuranceCost()
        {
            var expectedInsuranceValue = 0;

            var result = InsuranceDtoBuilder.BuildValid();
            result.SalesPrice = 2500;
            result.ProductTypeHasInsurance = false;

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void InsuranceValue_Given_InsuranceAndTypeLaptop_Should_Add500EurosExtraToInsuranceCost()
        {
            var expectedInsuranceValue = 1000;

            var result = InsuranceDtoBuilder.BuildValid();
            result.SalesPrice = 500;
            result.ProductTypeName = ProductTypes.Laptops;

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Fact]
        public void InsuranceValue_Given_InsuranceAndTypeSmartphone_Should_Add500EurosExtraToInsuranceCost()
        {
            var expectedInsuranceValue = 1000;

            var result = InsuranceDtoBuilder.BuildValid();
            result.SalesPrice = 500;
            result.ProductTypeName = ProductTypes.Smartphones;

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }
    }
}
