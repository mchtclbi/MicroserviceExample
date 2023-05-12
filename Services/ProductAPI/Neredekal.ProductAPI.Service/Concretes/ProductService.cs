using Neredekal.Data.Interfaces;
using Neredekal.ProductAPI.Models.Request;
using Neredekal.ProductAPI.Models.Entities;
using Neredekal.ProductAPI.Models.Response;
using Neredekal.Application.Models.Response;
using Neredekal.ProductAPI.Service.Interfaces;

namespace Neredekal.ProductAPI.Service.Concretes
{
    public class ProductService : IProductService
    {
        private readonly IMongoRepository<Product> _productRepository;

        public BaseResponse<CreateProductResponse> Add(CreateProductRequest request)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<DeleteProductResponse> Delete(DeleteProductRequest request)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<CreateCommunicationResponse> AddProductCommunication(CreateCommunicationRequest request)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<DeleteCommunicationResponse> DeleteProductCommunication(DeleteCommunicationRequest request)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<List<GetProductAuthorizedResponse>> GetProductAuthorized(GetProductAuthorizedRequest request)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<List<GetProductDetailResponse>> GetProductDetail(GetProductDetailRequest request)
        {
            throw new NotImplementedException();
        }
    }
}