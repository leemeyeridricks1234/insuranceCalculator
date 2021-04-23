using Insurance.Api.DTOs;
using Insurance.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Insurance.Api.Controllers
{
    [ApiController]
    public class InsuranceController: Controller
    {
        private readonly IInsuranceService insuranceService;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public InsuranceController(IInsuranceService insuranceService)
        {
            this.insuranceService = insuranceService;
        }

        [HttpPost]
        [Route("api/insurance/product")]
        [ProducesResponseType(typeof(InsuranceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public InsuranceDto CalculateInsurance([FromBody] ProductRequestDto request)
        {
            logger.Info(string.Format("CalculateInsurance started, ProductID={0}", request.ProductId));
            var insurance =  insuranceService.CalculateInsurance(request.ProductId);
            logger.Info(string.Format("CalculateInsurance finished, InsuranceValue={0}", insurance.InsuranceValue));
            return insurance;
        }

        [HttpPost]
        [Route("api/insurance/order")]
        [ProducesResponseType(typeof(OrderSummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public OrderSummaryDto CalculateOrderInsurance([FromBody] OrderRequestDto request)
        {
            logger.Info(string.Format("CalculateOrderInsurance started, Products={0}", request.Products.Count));
            var orderSummary = new OrderSummaryDto();
            foreach (var product in request.Products)
            {
                var insurance = insuranceService.CalculateInsurance(product.ProductId);
                orderSummary.ShoppingCart.Add(insurance);
            }
            logger.Info(string.Format("CalculateOrderInsurance finished, InsuranceValue={0}", orderSummary.InsuranceCost));
            return orderSummary;
        }
    }
}