namespace Insurance.Api.DTOs
{
    public class InsuranceDto
    {
        public int ProductId { get; set; }
        public decimal InsuranceValue
        {
            get
            {
                decimal insuranceValue = 0;
                if (SalesPrice <= 500)
                    insuranceValue = 500;
                else
                {
                    if (ProductTypeHasInsurance)
                    {
                        if (SalesPrice > 500 && SalesPrice < 2000)
                            insuranceValue = 1000;
                        else if (SalesPrice >= 2000)
                            insuranceValue = 2000;
                    }
                }
                if (ProductTypeName == ProductTypes.Laptops || ProductTypeName == ProductTypes.Smartphones)
                    insuranceValue += 500;

                insuranceValue += Surcharge;

                return insuranceValue;
            }
        }

        public string ProductTypeName { get; set; }
        public bool ProductTypeHasInsurance { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal Surcharge { get; set; }


        public static InsuranceDto Create(ProductDto product, ProductTypeDto productType, SurchargeDto surcharge)
        {
            var insurance = new InsuranceDto();
            insurance.SalesPrice = product.SalesPrice;
            insurance.ProductId = product.Id;
            insurance.ProductTypeName = productType.Name;
            insurance.ProductTypeHasInsurance = productType.CanBeInsured;
            if (surcharge != null)
                insurance.Surcharge = surcharge.Surcharge;
            return insurance;
        }
    }
}
