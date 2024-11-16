using Microsoft.AspNetCore.Mvc;
using InsightWorks.Services;
using InsightWorks.Models;
using InsightWorks.DTOs.Product;
using InsightWorks.DTOs.Common;

namespace InsightWorks.Controllers;

[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<ProductModel>>>> GetAllProducts()
    {
        try
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(ApiResponse<IEnumerable<ProductModel>>.Ok(products));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<IEnumerable<ProductModel>>.Fail(ex.Message));
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ProductModel>>> GetProductById(Guid id)
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(ApiResponse<ProductModel>.Fail($"未找到ID为 {id} 的产品型号"));
            }
            return Ok(ApiResponse<ProductModel>.Ok(product));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<ProductModel>.Fail(ex.Message));
        }
    }

    [HttpPost("create")]
    public async Task<ActionResult<ApiResponse<ProductModel>>> CreateProduct([FromBody] CreateProductDTO data)
    {
        try
        {
            var product = await _productService.CreateProductAsync(data);
            return Ok(ApiResponse<ProductModel>.Ok(product, "产品型号创建成功"));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ApiResponse<ProductModel>.Fail(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<ProductModel>.Fail(ex.Message));
        }
    }

    [HttpPost("update")]
    public async Task<ActionResult<ApiResponse<ProductModel>>> UpdateProduct([FromBody] UpdateProductDTO data)
    {
        try
        {
            var product = await _productService.UpdateProductAsync(data);
            return Ok(ApiResponse<ProductModel>.Ok(product, "产品型号更新成功"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<ProductModel>.Fail(ex.Message));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ApiResponse<ProductModel>.Fail(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<ProductModel>.Fail(ex.Message));
        }
    }

    [HttpPost("delete")]
    public async Task<ActionResult<ApiResponse<object>>> DeleteProduct([FromBody] DeleteProductDTO data)
    {
        try
        {
            await _productService.DeleteProductAsync(data);
            return Ok(ApiResponse<object>.Ok(null, "产品型号删除成功"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<object>.Fail(ex.Message));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ApiResponse<object>.Fail(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<object>.Fail(ex.Message));
        }
    }
} 