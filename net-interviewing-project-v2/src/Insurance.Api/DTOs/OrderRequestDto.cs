using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Insurance.Api.DTOs
{
    public class OrderRequestDto
    {
        [Required]
        [MinLength(1)]
        public List<ProductRequestDto> Products { get; set; }
    }
}
