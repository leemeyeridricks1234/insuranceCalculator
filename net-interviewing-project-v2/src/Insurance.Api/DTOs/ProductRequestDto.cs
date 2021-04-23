using System.ComponentModel.DataAnnotations;

namespace Insurance.Api.DTOs
{
    public class ProductRequestDto
    {
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }
    }
}
