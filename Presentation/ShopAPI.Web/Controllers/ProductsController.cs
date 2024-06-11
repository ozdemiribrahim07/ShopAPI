using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Application.Abstraction.Storage;
using ShopAPI.Application.Abstraction.Storage.AWS;
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
    public class ProductsController : ControllerBase
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;
        readonly IWebHostEnvironment _webHostEnvironment;

        readonly IStorageService storageService;
        readonly IAwsStorage _awsStorage;
        

        readonly IFileWriteRepository _fileWriteRepository;
        readonly IFileReadRepository _fileReadRepository;
        readonly IProductImageReadRepository _productImageReadRepository;
        readonly IProductImageWriteRepository _productImageWriteRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment webHostEnvironment, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IProductImageReadRepository productImageReadRepository, IProductImageWriteRepository productImageWriteRepository, IStorageService storageService, IAwsStorage awsStorage)
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
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Pagination pagination)
        {
           var total = _productReadRepository.GetAll().Count();
           var products = _productReadRepository.GetAll().Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(selector => new
            {
                selector.Id,
                selector.ProductName,
                selector.ProductPrice,
                selector.ProductStock,
                selector.CreatedDate,
                selector.UpdatedDate,
            }).ToList();

            return Ok(new
            {
                total,
                products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(_productReadRepository.GetByIdAsync(id));
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductCreateVM  productCreateVM)
        {
            await _productWriteRepository.AddAsync(
                new()
            {
                ProductName = productCreateVM.ProductName,
                ProductStock = productCreateVM.ProductStock,
                ProductPrice = productCreateVM.ProductPrice
            });
            await _productWriteRepository.SaveAsync();
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> Put(ProductUpdateVM productUpdateVM)
        {
            Product product = await _productReadRepository.GetByIdAsync(productUpdateVM.Id);
            product.ProductName = productUpdateVM.ProductName;
            product.ProductStock = productUpdateVM.ProductStock;
            product.ProductPrice = productUpdateVM.ProductPrice;
            await _productWriteRepository.UpdateAsync(product);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.DeleteAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {

            string path = "images-deneme";
            var datas  = await storageService.UploadAsync(path, Request.Form.Files);
            

            await _productImageWriteRepository.AddRangeAsync(datas.Select(x => new ProductImageFile()
            {
                FileName = x.fileName,
                Path = x.pathOrbucketName,
                Storage = storageService.StorageName,

            }).ToList());

            await _productImageWriteRepository.SaveAsync();

            return Ok();

        }




    }
}
