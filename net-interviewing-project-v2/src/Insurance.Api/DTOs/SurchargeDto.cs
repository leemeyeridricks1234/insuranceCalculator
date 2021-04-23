namespace Insurance.Api.DTOs
{
    public class SurchargeDto
    {
        public int Id { get; set; }
        public decimal Surcharge { get; set; }
        public int ProductTypeId { get; set; }

        public static SurchargeDto CreateSurchargeFromJson(dynamic surcharge)
        {
            var dto = new SurchargeDto();
            dto.Id = surcharge.id;
            dto.Surcharge = surcharge.surcharge;
            dto.ProductTypeId = surcharge.productTypeId;
            return dto;
        }
    }
}
