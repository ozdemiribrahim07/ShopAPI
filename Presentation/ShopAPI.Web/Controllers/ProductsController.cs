using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAPI.Application.Abstraction.Storage;
using ShopAPI.Application.Abstraction.Storage.AWS;
using ShopAPI.Application.Features.ProductImageFiles.Commands.DeleteProductImage;
using ShopAPI.Application.Features.ProductImageFiles.Commands.UploadProductImage;
using ShopAPI.Application.Features.ProductImageFiles.Queries.GetProductImage;
using ShopAPI.Application.Features.Products.Commands.CreateProduct;
using ShopAPI.Application.Features.Products.Commands.DeleteProduct;
using ShopAPI.Application.Features.Products.Commands.UpdateProduct;
using ShopAPI.Application.Features.Products.Queries.GetAllProducts;
using ShopAPI.Application.Features.Products.Queries.GetByIdProducts;
using ShopAPI.Application.Repositories.FileRepo;
using ShopAPI.Application.Repositories.ProductImageRepo;
using ShopAPI.Application.Repositories.ProductRepo;
using ShopAPI.Application.RequestParameters;
using ShopAPI.Application.VMs;
using ShopAPI.Domain.Entities;


namespace ShopAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class ProductsController : ControllerBase
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;
        readonly IWebHostEnvironment _webHostEnvironment;

        readonly IConfiguration _configuration;

        readonly IStorageService storageService;
        readonly IAwsStorage _awsStorage;

        readonly IMediator _mediator;

        readonly IFileWriteRepository _fileWriteRepository;
        readonly IFileReadRepository _fileReadRepository;
        readonly IProductImageReadRepository _productImageReadRepository;
        readonly IProductImageWriteRepository _productImageWriteRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment webHostEnvironment, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IProductImageReadRepository productImageReadRepository, IProductImageWriteRepository productImageWriteRepository, IStorageService storageService, IAwsStorage awsStorage, IConfiguration configuration, IMediator mediator)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _productImageReadRepository = productImageReadRepository;
            _productImageWriteRepository = productImageWriteRepository;
            this.storageService = storageService;
            _awsStorage = awsStorage;
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductsQueryRequest allProductsQueryRequest)
        {
           GetAllProductsQueryResponse response = await _mediator.Send(allProductsQueryRequest);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute]GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
            return Ok(response);
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequest updateProductCommandRequest)
        {
            UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
            return Ok(response);
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest deleteProductCommandRequest)
        {
            DeleteProductCommandResponse response = await _mediator.Send(deleteProductCommandRequest);
            return Ok(response);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandrequest)
        {
            uploadProductImageCommandrequest.Files = Request.Form.Files;
            UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommandrequest);
            return Ok(response);
        }


    
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetImages([FromRoute] GetProductImageQueryRequest getProductImageQuery)
        {
            List<GetProductImageQueryResponse> response = await _mediator.Send(getProductImageQuery);
            return Ok(response);
        }



        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteImage([FromRoute] DeleteProductImageCommandRequest DeleteproductImageCommandRequest, [FromQuery]string imageId)
        {
            DeleteproductImageCommandRequest.ImageId = imageId;
           DeleteProductImageCommandResponse response = await _mediator.Send(DeleteproductImageCommandRequest);
            return Ok(response);

        }




    }
}
