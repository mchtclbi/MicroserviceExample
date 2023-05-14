using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Neredekal.ProductAPI.Models.Request;
using Neredekal.ProductAPI.Service.Interfaces;

namespace Neredekal.Product.API.Controllers
{
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        //TODO permission control
        //and request model validaton control with fluent validation

        [HttpPost("/api/product/add")]
        public IActionResult Add([FromBody] CreateProductRequest request) => Ok(_productService.Add(request));

        [HttpDelete("/api/product/delete")]
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

        [HttpGet("/api/product/communication-type")]
        public IActionResult GetCommunicationType() => Ok(_productService.GetCommunicationType());

        [HttpPost("api/product/report")]
        public IActionResult CreateNewReportDemand() => Ok(_productService.CreateNewReportDemand());
    }
}