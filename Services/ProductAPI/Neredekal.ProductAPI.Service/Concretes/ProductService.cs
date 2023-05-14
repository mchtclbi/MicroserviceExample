using Neredekal.Data.Concretes;
using Neredekal.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Neredekal.ProductAPI.Models;
using Microsoft.Extensions.Primitives;
using Neredekal.ProductAPI.Models.Enums;
using Neredekal.ProductAPI.Models.Request;
using Neredekal.ProductAPI.Models.Entities;
using Neredekal.ProductAPI.Models.Response;
using Neredekal.Application.Models.Response;
using Neredekal.ProductAPI.Service.Interfaces;

namespace Neredekal.ProductAPI.Service.Concretes
{
    public class ProductService : IProductService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IRabbitMQSProducer _rabbitMQSProducer;

        private readonly IMongoRepository<Product> _productRepository;
        private readonly IMongoRepository<ReportDemand> _reportDemandRepository;
        private readonly IMongoRepository<ProductCommunication> _productCommunicationRepository;
        private readonly IMongoRepository<ProductCommuncationType> _productCommuncationTypeRepository;


        public ProductService(IHttpContextAccessor httpContext, IRabbitMQSProducer rabbitMQSProducer)
        {
            _httpContext = httpContext;
            _rabbitMQSProducer = rabbitMQSProducer;

            _productRepository = new MongoRepository<Product>();
            _reportDemandRepository = new MongoRepository<ReportDemand>();
            _productCommunicationRepository = new MongoRepository<ProductCommunication>();
            _productCommuncationTypeRepository = new MongoRepository<ProductCommuncationType>();
        }

        public BaseResponse<CreateProductResponse> Add(CreateProductRequest request)
        {
            var response = new BaseResponse<CreateProductResponse>();

            try
            {
                var product = new Product()
                {
                    Id = Guid.NewGuid(),
                    AuthorizedName = request.AuthorizedName,
                    AuthorizedSurname = request.AuthorizedSurname,
                    CompanyTitle = request.CompanyTitle,
                    IsActive = true
                };

                _productRepository.Add(product);

                if (request.Communcations != null && request.Communcations.Any())
                {
                    request.Communcations.ForEach(q =>
                    {
                        _productCommunicationRepository.Add(new ProductCommunication()
                        {
                            Id = Guid.NewGuid(),
                            ProductId = product.Id,
                            TypeId = q.Id,
                            Value = q.Value,
                            IsActive = true
                        });
                    });
                }

                response.SetMessage(ConstantMessage.TransactionSuccess, true);
            }
            catch (Exception)
            {
                response.SetMessage(ConstantMessage.ExceptionMessage);
            }

            return response;
        }

        public BaseResponse<DeleteProductResponse> Delete(DeleteProductRequest request)
        {
            var response = new BaseResponse<DeleteProductResponse>();

            try
            {
                var product = _productRepository.Get(q => q.Id == request.Id);
                if (product == null)
                {
                    response.SetMessage("product not found");
                    return response;
                }

                _productRepository.Delete(product);
                response.SetMessage(ConstantMessage.TransactionSuccess, true);
            }
            catch (Exception)
            {
                response.SetMessage(ConstantMessage.ExceptionMessage);
            }

            return response;
        }

        public BaseResponse<CreateCommunicationResponse> AddProductCommunication(CreateCommunicationRequest request)
        {
            var response = new BaseResponse<CreateCommunicationResponse>();

            try
            {
                request.Communications.ForEach(q =>
                {
                    _productCommunicationRepository.Add(new ProductCommunication()
                    {
                        Id = Guid.NewGuid(),
                        TypeId = q.Id,
                        ProductId = request.ProductId,
                        Value = q.Value,
                        IsActive = true
                    });
                });

                response.SetMessage(ConstantMessage.TransactionSuccess, true);
            }
            catch (Exception)
            {
                response.SetMessage(ConstantMessage.ExceptionMessage);
            }

            return response;
        }

        public BaseResponse<DeleteCommunicationResponse> DeleteProductCommunication(DeleteCommunicationRequest request)
        {
            var response = new BaseResponse<DeleteCommunicationResponse>();

            try
            {
                var productCommunication = _productCommunicationRepository.Get(q => q.Id == request.Id);
                if (productCommunication == null)
                {
                    response.SetMessage("product communication not found");
                    return response;
                }

                _productCommunicationRepository.Delete(productCommunication);
                response.SetMessage(ConstantMessage.TransactionSuccess, true);
            }
            catch (Exception)
            {
                response.SetMessage(ConstantMessage.ExceptionMessage);
            }

            return response;
        }

        public BaseResponse<List<GetProductAuthorizedResponse>> GetProductAuthorized(GetProductAuthorizedRequest request)
        {
            var response = new BaseResponse<List<GetProductAuthorizedResponse>>();

            try
            {
                var data = new List<GetProductAuthorizedResponse>();

                _productRepository.GetAll(q => q.IsActive).ForEach(q =>
                {
                    data.Add(new GetProductAuthorizedResponse()
                    {
                        ProductTitle = q.CompanyTitle,
                        AuthorizedName = q.AuthorizedName,
                        AuthorizedSurname = q.AuthorizedSurname
                    });
                });

                response.Data = data;
                response.SetMessage(ConstantMessage.TransactionSuccess, true);
            }
            catch (Exception)
            {
                response.SetMessage(ConstantMessage.ExceptionMessage);
            }

            return response;
        }

        public BaseResponse<List<GetProductDetailResponse>> GetProductDetail(GetProductDetailRequest request)
        {
            var response = new BaseResponse<List<GetProductDetailResponse>>();

            try
            {
                var products = _productRepository.GetAll(q => q.IsActive);

                var productIds = products.Select(s => s.Id).ToList();
                var productCommunications = _productCommunicationRepository.GetAll(q => q.IsActive);
                var productCommunicationType = _productCommuncationTypeRepository.GetAll(q => q.IsActive);

                var data = new List<GetProductDetailResponse>();

                products.ForEach(q =>
                {
                    data.Add(new GetProductDetailResponse()
                    {
                        Id = q.Id,
                        AuthorizedName = q.AuthorizedName,
                        AuthorizedSurname = q.AuthorizedSurname,
                        CompanyTitle = q.CompanyTitle,
                        Communications = productCommunications.Where(w => w.ProductId == q.Id)
                        .Select(s => new CommunicationViewModel()
                        {
                            Value = s.Value,
                            Name = productCommunicationType.First(q => q.Id == s.TypeId).Name
                        }).ToList()
                    });
                });

                response.Data = data;
                response.SetMessage(ConstantMessage.TransactionSuccess, true);
            }
            catch (Exception)
            {
                response.SetMessage(ConstantMessage.ExceptionMessage);
            }

            return response;
        }

        public BaseResponse<List<ProductCommuncationType>> CreateCommunicationType()
        {
            var response = new BaseResponse<List<ProductCommuncationType>>();

            try
            {
                var items = new List<ProductCommuncationType>()
                {
                    new ProductCommuncationType() { Id = Guid.NewGuid(), Name = "Konum", IsActive = true },
                    new ProductCommuncationType() { Id = Guid.NewGuid(), Name = "E-mail Adresi", IsActive = true },
                    new ProductCommuncationType() { Id = Guid.NewGuid(), Name = "Telefon Numarası", IsActive = true }
                };

                items.ForEach(q =>
                {
                    _productCommuncationTypeRepository.Add(q);
                });

                response.Data = items;
                response.SetMessage(ConstantMessage.TransactionSuccess, true);
            }
            catch (Exception)
            {
                response.SetMessage(ConstantMessage.ExceptionMessage);
            }

            return response;
        }

        public BaseResponse<List<ProductCommuncationType>> GetCommunicationType()
        {
            var response = new BaseResponse<List<ProductCommuncationType>>();

            try
            {
                response.Data = _productCommuncationTypeRepository.GetAll(q => q.IsActive);
                response.SetMessage(ConstantMessage.TransactionSuccess, true);
            }
            catch (Exception)
            {
                response.SetMessage(ConstantMessage.ExceptionMessage);
            }

            return response;
        }

        public BaseResponse<object> CreateNewReportDemand()
        {
            var response = new BaseResponse<object>();

            try
            {
                var token = GetTokenWithHeader();
                if (token is null)
                {
                    response.SetMessage("transaction is fail");
                    return response;
                }

                var reportDemand = new ReportDemand()
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse(new TokenService().GetUserIdWithToken(token)),
                    DemandDate = DateTime.Now,
                    Status = ReportDemandStatus.Preparing,
                    IsActive = true
                };

                _reportDemandRepository.Add(reportDemand);
                _rabbitMQSProducer.SendProductMessage(reportDemand.Id.ToString());

                response.SetMessage(ConstantMessage.TransactionSuccess, true);
            }
            catch (Exception)
            {
                response.SetMessage(ConstantMessage.ExceptionMessage);
            }

            return response;
        }

        private string? GetTokenWithHeader()
        {
            StringValues token = new StringValues();

            var result = _httpContext.HttpContext?.Request.Headers.TryGetValue("Authorization", out token);
            if (result.HasValue && !result.Value) return null;

            return token.ToString().Split("Bearer ").Last();
        }
    }
}