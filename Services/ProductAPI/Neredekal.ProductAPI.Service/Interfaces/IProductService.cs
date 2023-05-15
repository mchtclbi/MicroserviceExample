using Neredekal.ProductAPI.Models.Request;
using Neredekal.ProductAPI.Models.Response;
using Neredekal.ProductAPI.Models.Entities;
using Neredekal.Application.Models.Response;

namespace Neredekal.ProductAPI.Service.Interfaces
{
    public interface IProductService
    {
        public BaseResponse<CreateProductResponse> Add(CreateProductRequest request);

        public BaseResponse<DeleteProductResponse> Delete(DeleteProductRequest request);

        public BaseResponse<CreateCommunicationResponse> AddProductCommunication(CreateCommunicationRequest request);

        public BaseResponse<DeleteCommunicationResponse> DeleteProductCommunication(DeleteCommunicationRequest request);

        public BaseResponse<List<GetProductAuthorizedResponse>> GetProductAuthorized(GetProductAuthorizedRequest request);

        public BaseResponse<List<GetProductDetailResponse>> GetProductDetail(GetProductDetailRequest request);

        public BaseResponse<List<ProductCommuncationType>> CreateCommunicationType();
        
        public BaseResponse<List<ProductCommuncationType>> GetCommunicationType();

        public BaseResponse<object> CreateNewReportDemand();

        public BaseResponse<List<GetReportDemandResponse>> GetReportDemand(GetReportDemandRequest request);
    }
}