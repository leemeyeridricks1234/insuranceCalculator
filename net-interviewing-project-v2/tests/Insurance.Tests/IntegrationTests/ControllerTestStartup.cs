using Insurance.Api.DTOs;
using Insurance.Tests.Builders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace Insurance.Tests.IntegrationTests
{
    public class ControllerTestStartup
    {
        private static string GetProduct(int id)
        {
            switch (id)
            {
                case 1: 
                    var product = new
                    {
                        id = ProductIds.SmallProduct,
                        name = "Small Insured Product",
                        productTypeId = 1,
                        salesPrice = 400
                    };
                    return JsonConvert.SerializeObject(product);
                case 2:
                    var product2 = new
                    {
                        id = ProductIds.MediumProduct,
                        name = "Medium Insured Product",
                        productTypeId = 1,
                        salesPrice = 1100
                    };
                    return JsonConvert.SerializeObject(product2);
                case 3:
                    var product3 = new
                    {
                        id = ProductIds.LargeProduct,
                        name = "Large Insured Product",
                        productTypeId = 1,
                        salesPrice = 2100
                    };
                    return JsonConvert.SerializeObject(product3);
                case 4:
                    var product4 = new
                    {
                        id = ProductIds.NotInsured,
                        name = "Not Insured Product",
                        productTypeId = 2,
                        salesPrice = 1000
                    };
                    return JsonConvert.SerializeObject(product4);
                case 5:
                    var product5 = new
                    {
                        id = ProductIds.Laptop,
                        name = "Laptop Product",
                        productTypeId = 3,
                        salesPrice = 1000
                    };
                    return JsonConvert.SerializeObject(product5);
                case 6:
                    var product6 = new
                    {
                        id = ProductIds.Smartphone,
                        name = "Smartphone Product",
                        productTypeId = 4,
                        salesPrice = 1000
                    };
                    return JsonConvert.SerializeObject(product6);
                case 7:
                    var product7 = new
                    {
                        id = ProductIds.DigitalCamera,
                        name = "Digital Camera Product",
                        productTypeId = 5,
                        salesPrice = 400
                    };
                    return JsonConvert.SerializeObject(product7);
                case 8:
                    var product8 = new
                    {
                        id = ProductIds.SurchargeProduct,
                        name = "Surcharge Product",
                        productTypeId = 32,
                        salesPrice = 400
                    };
                    return JsonConvert.SerializeObject(product8);
            }
            return "";
        }


        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(
                ep =>
                {
                    ep.MapGet(
                        "products/{id:int}",
                        context =>
                        {
                            int productId = int.Parse((string)context.Request.RouteValues["id"]);
                            return context.Response.WriteAsync(GetProduct(productId));
                        }
                    );
                    ep.MapGet(
                        "product_types",
                        context =>
                        {
                            var productTypes = new[]
                                               {
                                                   new
                                                   {
                                                       id = 1,
                                                       name = "Insured Test type",
                                                       canBeInsured = true
                                                   },
                                                   new
                                                   {
                                                       id = 2,
                                                       name = "Not Insured Test type",
                                                       canBeInsured = false
                                                   },
                                                   new
                                                   {
                                                       id = 3,
                                                       name = ProductTypes.Laptops,
                                                       canBeInsured = true
                                                   },
                                                   new
                                                   {
                                                       id = 4,
                                                       name = ProductTypes.Smartphones,
                                                       canBeInsured = true
                                                   },
                                                   new
                                                   {
                                                       id = 5,
                                                       name = ProductTypes.DigitalCamera,
                                                       canBeInsured = true
                                                   },
                                                   new
                                                   {
                                                       id = 32,
                                                       name = ProductTypes.DigitalCamera,
                                                       canBeInsured = true
                                                   }
                                               };
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(productTypes));
                        }
                    );
                }
            );
        }
    }
}
