using Insurance.Api.DTOs;
using Insurance.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Insurance.Api.Controllers
{
    [ApiController]
    public class SurchargeController : Controller
    {
        private readonly IProductRepository productRepository;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SurchargeController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        [Route("api/surcharges")]
        public List<SurchargeDto> GetSurcharges()
        {
            logger.Info("GetSurcharges started.");
            var result = productRepository.GetSurcharges();
            logger.Info("GetSurcharges finished.");
            return result;
        }
    }
}