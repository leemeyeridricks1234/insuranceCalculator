using System.ComponentModel.DataAnnotations;

namespace Insurance.Api.DTOs
{
    public class ProductTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool CanBeInsured { get; set; }

        public static ProductTypeDto CreateProductTypeFromJson(dynamic productType)
        {
            var dto = new ProductTypeDto();
            dto.Id = productType.id;
            dto.Name = productType.name;
            dto.CanBeInsured = productType.canBeInsured;
            return dto;
        }
    }
}
