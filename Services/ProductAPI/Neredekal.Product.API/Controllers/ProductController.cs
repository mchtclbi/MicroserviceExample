using Microsoft.AspNetCore.Mvc;
using Neredekal.ProductAPI.Models.Request;
using Neredekal.ProductAPI.Service.Interfaces;

namespace Neredekal.Product.API.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("/api/product")]
        public IActionResult Add([FromBody] CreateProductRequest request) => Ok(_productService.Add(request));

        [HttpDelete("/api/product")]
        public IActionResult Delete([FromBody] CreateProductRequest request) => Ok(_productService.Add(request));

        [HttpPost("/api/product/communication")]
        public IActionResult AddProductCommunication([FromBody] CreateCommunicationRequest request) =>
            Ok(_productService.AddProductCommunication(request));

        [HttpDelete("/api/product/communication")]
        public IActionResult DeleteProductCommunication([FromBody] DeleteCommunicationRequest request) =>
            Ok(_productService.DeleteProductCommunication(request));

        [HttpGet("/api/product/authorized")]
        public IActionResult GetProductAuthorized([FromQuery] GetProductAuthorizedRequest request) =>
            Ok(_productService.GetProductAuthorized(request));

        [HttpGet("/api/product/details")]
        public IActionResult GetProductDetail([FromQuery] GetProductDetailRequest request) =>
            Ok(_productService.GetProductDetail(request));

        [HttpPost("/api/product/communication-type")]
        public IActionResult CreateCommunicationType() => Ok(_productService.CreateCommunicationType());
    }
}