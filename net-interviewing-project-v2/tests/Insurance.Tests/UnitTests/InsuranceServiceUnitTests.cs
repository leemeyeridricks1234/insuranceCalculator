using Insurance.Api.DTOs;
using Insurance.Api.Exceptions;
using Insurance.Api.Repositories;
using Insurance.Api.Services;
using Insurance.Tests.Builders;
using Insurance.Tests.Helpers;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Insurance.Tests.UnitTests
{
    public class InsuranceServiceUnitTests
    {
        Mock<IProductRepository> _repositoryMock;

        public InsuranceServiceUnitTests()
        {
            _repositoryMock = new Mock<IProductRepository>();

            _repositoryMock.Setup(x => x.GetProductTypes()).Returns(DataHelper.GetProductTypes());
            _repositoryMock.Setup(x => x.GetSurcharges()).Returns(DataHelper.GetSurcharges());
        }

        [Fact]
        public void CalculateInsurance_Given_SmallProductId_Should_Return500Insurance()
        {
            //expected
            var expectedInsuranceValue = 500;

            //given
            ProductRequestDto request = DataHelper.CreateRequest(ProductIds.SmallProduct);
            var product = DataHelper.CreateProduct();
            _repositoryMock.Setup(x => x.GetProduct(request.ProductId)).Returns(product);

            //when
            var sut = new InsuranceService(_repositoryMock.Object);

            var response = sut.CalculateInsurance(ProductIds.SmallProduct);

            //then
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: response.InsuranceValue
            );
        }

        [Fact]
        public void CalculateInsurance_Given_InvalidProductResponse_Should_ReturnNotFoundException()
        {
            //given
            ProductRequestDto request = DataHelper.CreateRequest(ProductIds.SmallProduct);
            ProductDto product = null;
            _repositoryMock.Setup(x => x.GetProduct(request.ProductId)).Returns(product);

            //when
            var sut = new InsuranceService(_repositoryMock.Object);
            Assert.Throws<NotFoundException>(() => sut.CalculateInsurance(ProductIds.SmallProduct));
        }

        [Fact]
        public void CalculateInsurance_Given_InvalidProductTypeResponse_Should_ReturnNotFoundException()
        {
            //given
            ProductRequestDto request = DataHelper.CreateRequest(ProductIds.SmallProduct);
            ProductDto product = DataHelper.CreateProduct();
            _repositoryMock.Setup(x => x.GetProduct(request.ProductId)).Returns(product);
            List<ProductTypeDto> productTypes = null;
            _repositoryMock.Setup(x => x.GetProductTypes()).Returns(productTypes);

            //when
            var sut = new InsuranceService(_repositoryMock.Object);
            Assert.Throws<NotFoundException>(() => sut.CalculateInsurance(ProductIds.SmallProduct));
        }

        [Fact]
        public void CalculateInsurance_Given_ProductTypeNotFound_Should_ReturnNotFoundException()
        {
            //given
            ProductRequestDto request = DataHelper.CreateRequest(ProductIds.SmallProduct);
            ProductDto product = DataHelper.CreateProduct();
            product.ProductTypeId = 101;
            _repositoryMock.Setup(x => x.GetProduct(request.ProductId)).Returns(product);

            //when
            var sut = new InsuranceService(_repositoryMock.Object);
            Assert.Throws<NotFoundException>(() => sut.CalculateInsurance(ProductIds.SmallProduct));
        }
    }
}
