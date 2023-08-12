using System.Net;
using System.Text.Json.Serialization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SimpleProductDataModels;

namespace SimpleProductAPI
{
    public class GenerateNewProducts
    {
        private readonly ILogger _logger;

        public GenerateNewProducts(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GenerateNewProducts>();
        }

        [Function("GenerateNewProducts")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            var newProducts = GenerateNewProductsList();

            response.WriteString(JsonConvert.SerializeObject(newProducts));

            return response;
        }

        public List<Product> GenerateNewProductsList()
        {
            // NOTE: this is totally contrived for demo purposes only:

            var products = new List<Product>();
            Random r = new Random();
            //create 10-20 products
            var totalProducts = r.Next(10,20);
            for (var i = 0; i < totalProducts; i++)
            {
                var product = new Product();
                product.SKU = Guid.NewGuid().ToString();
                product.Name = $"{GenerateProductName()}";
                product.Price = Math.Round(Convert.ToDecimal(Convert.ToDouble(r.Next(1,100)) + r.NextDouble()), 2);
                product.CategoryId = r.Next(1, 5);
                products.Add(product);
            }

            return products;
        }

        private string GenerateProductName()
        {
            var firstPart = GetFirstPart();
            var secondPart = GetSecondPart();
            return $"{firstPart} {secondPart}";
        }

        private const string FirstParts = "Blue|Red|Green|Orange|Purple|Indigo|Violet|Yellow|Pink";
        private const string SecondParts = "Cat|Dog|Fish|Mouse|Lizard|Hamster|Crab|Snake|Parrot|Parakeet|Guinea Pig";

        private string GetFirstPart()
        {
            var parts = FirstParts.Split('|');
            return GetStringFromParts(parts);
        }

        private static string GetStringFromParts(string[] parts)
        {
            Random r = new Random();
            var index = r.Next(0, parts.Length - 1);
            return parts[index];
        }

        private string GetSecondPart()
        {
            var parts = SecondParts.Split('|');
            return GetStringFromParts(parts);
        }
    }
}
