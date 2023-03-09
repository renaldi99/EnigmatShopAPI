using AutoMapper;
using EnigmatShopAPI.Dto;
using EnigmatShopAPI.Helpers;
using EnigmatShopAPI.Models;
using EnigmatShopAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EnigmatShopAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService service, IMapper mapper, ILogger<ProductController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [RequestSizeLimit(5 * 1024 * 1024)]
        [HttpPost("AddProduct")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductDto entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = 400, msg = "Invalid request" });
            }

            if (!entity.ImageFile.ContentType.Equals("image/png"))
            {
                return BadRequest(new { code = 400, msg = "Upload image must be image/png" });
            }

            IFormFile file = entity.ImageFile;
            var fileImageName = Path.GetFileName(file.FileName);
            var newFileImageName = Guid.NewGuid() + "_" + fileImageName;

            var locationPathResource = Path.Combine(Directory.GetCurrentDirectory(), Constants.PATH_SOURCE_IMAGE);
            var pathResourceFile = Path.Combine(locationPathResource, newFileImageName);

            try
            {
                if (System.IO.File.Exists(pathResourceFile))
                {
                    return BadRequest(new { code = 400, msg = "File Already Exists" });
                }

                using (var filestream = new FileStream(pathResourceFile, FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                    filestream.Flush();
                }

                var _productDto = new ProductDto
                {
                    ProductName = entity.ProductName,
                    ProductPrice = entity.ProductPrice,
                    Stock = entity.Stock,
                    Image = newFileImageName
                };

                var mapProduct = _mapper.Map<ProductDto, Product>(_productDto);

                var res = await _service.CreateProduct(mapProduct);
                if (res == 0)
                {
                    if (System.IO.File.Exists(pathResourceFile))
                    {
                        System.IO.File.Delete(pathResourceFile);
                    }

                    return BadRequest(new { code = 400, msg = "Error request" });
                }
                return Ok(new { code = 200, msg = "Success create product" });
            }
            catch
            {
                if (System.IO.File.Exists(pathResourceFile))
                {
                    System.IO.File.Delete(pathResourceFile);
                }
                throw;
            }
            
        }

        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductDto entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = 400, msg = "Invalid request" });
            }

            IFormFile file = entity.ImageFile;
            var fileImageName = Path.GetFileName(file.FileName);
            var newFileImageName = Guid.NewGuid() + "_" + fileImageName;

            var locationPathResource = Path.Combine(Directory.GetCurrentDirectory(), Constants.PATH_SOURCE_IMAGE);
            var pathResourceFile = Path.Combine(locationPathResource, newFileImageName);

            try
            {
                if (System.IO.File.Exists(pathResourceFile))
                {
                    return BadRequest(new { code = 400, msg = "File Already Exists" });
                }

                using (var filestream = new FileStream(pathResourceFile, FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                    filestream.Flush();
                }

                var _productDto = new UpdateProductDto
                {
                    Id = entity.Id,
                    ProductName = entity.ProductName,
                    ProductPrice = entity.ProductPrice,
                    Stock = entity.Stock,
                    Image = newFileImageName
                };

                var mapProduct = _mapper.Map<UpdateProductDto, Product>(_productDto);
                //GET: product sebelumnya dan hapus image yang ada diresource
                var productObj = await _service.GetProductById(entity.Id.ToString());
                //REMOVE: image sebelumnya
                var fileCurrent = Path.Combine(locationPathResource, productObj.Image);
                if (System.IO.File.Exists(fileCurrent))
                {
                    System.IO.File.Delete(fileCurrent);
                }

                var res = await _service.UpdateProduct(mapProduct);
                if (res == 0)
                {

                    if (System.IO.File.Exists(pathResourceFile))
                    {
                        System.IO.File.Delete(pathResourceFile);
                    }

                    return BadRequest(new { code = 400, msg = "Invalid update request" });

                }

                return Ok(new { code = 200, msg = "Success update product" });
            }
            catch
            {
                if (System.IO.File.Exists(pathResourceFile))
                {
                    System.IO.File.Delete(pathResourceFile);
                }
                throw;
            }
           
        }

        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById([FromQuery] string productId)
        {
            if (productId == "")
            {
                return BadRequest(new { code = 400, msg = "ProductId is required" });
            }

            var product = await _service.GetProductById(productId);
            var pathFileProductFromResource = Path.Combine(Directory.GetCurrentDirectory(), Constants.PATH_SOURCE_IMAGE, product.Image);
            product.Image = pathFileProductFromResource;

            return Ok(new { code = 200, data = product });
        }

        [RequestSizeLimit(5 * 1024 * 1024)]
        [HttpPost("ExampleUploadFile")]
        public async Task<IActionResult> ExampleUploadImageFormData([FromForm] UploadDataDto upload)
        {

            if (!upload.Image.ContentType.Equals("image/png"))
            {
                return BadRequest(new { code = 400, msg = "Invalid Upload Image" });
            }

            //var file = HttpContext.Request.Form.Files;

            var filename = Path.GetFileName(upload.Image.FileName);
            var newFileName = Guid.NewGuid() + "_" + filename;

            var currentResource = Path.Combine(Directory.GetCurrentDirectory(), "Resource\\ProductImage");
            var pathResource = Path.Combine(currentResource, newFileName);

            if (System.IO.File.Exists(pathResource))
            {
                return BadRequest(new { code = 400, msg = "File Already Exists" });
            }

            using (var fileStream = new FileStream(pathResource, FileMode.Create))
            {
                await upload.Image.CopyToAsync(fileStream);
                fileStream.Flush();
            }

            return Ok(new { code = 200, msg = "Success upload image" });
        }
    }
}
