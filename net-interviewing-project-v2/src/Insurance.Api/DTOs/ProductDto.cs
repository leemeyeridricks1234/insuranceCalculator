using System.ComponentModel.DataAnnotations;

namespace Insurance.Api.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal SalesPrice { get; set; }
        public int ProductTypeId { get; set; }

        public static ProductDto CreateProductFromJson(dynamic product)
        {
            var dto = new ProductDto();
            dto.Id = product.id;
            dto.SalesPrice = product.salesPrice;
            dto.Name = product.name;
            dto.ProductTypeId = product.productTypeId;
            return dto;
        }
    }
}
