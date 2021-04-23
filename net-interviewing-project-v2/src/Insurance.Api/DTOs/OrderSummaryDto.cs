using System.Collections.Generic;
using System.Linq;

namespace Insurance.Api.DTOs
{
    public class OrderSummaryDto
    {
        public OrderSummaryDto()
        {
            ShoppingCart = new List<InsuranceDto>();
        }

        public decimal InsuranceCost
        {
            get
            {
                if (ShoppingCart != null)
                {
                    var total = ShoppingCart.Sum(x => x.InsuranceValue);
                    if (ShoppingCart.Any(x => x.ProductTypeName == ProductTypes.DigitalCamera))
                    {
                        total += 500;
                    }
                    return total;
                }
                return 0;
            }
        }
        public List<InsuranceDto> ShoppingCart { get; set; }
    }
}
