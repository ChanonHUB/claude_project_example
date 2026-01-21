using System.Security.Claims;
using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class ItemsController : ControllerBase
{
    private readonly IItemService _itemService;
    private readonly ILogger<ItemsController> _logger;

    public ItemsController(IItemService itemService, ILogger<ItemsController> logger)
    {
        _itemService = itemService;
        _logger = logger;
    }

    private int GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
        {
            throw new UnauthorizedAccessException("Invalid user token");
        }
        return userId;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var userId = GetCurrentUserId();
            var items = await _itemService.GetAllItemsAsync(userId);
            return Ok(items);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting items");
            return StatusCode(500, new { message = "An error occurred while retrieving items" });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var userId = GetCurrentUserId();
            var item = await _itemService.GetItemByIdAsync(id, userId);

            if (item == null)
                return NotFound(new { message = "Item not found" });

            return Ok(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting item {ItemId}", id);
            return StatusCode(500, new { message = "An error occurred while retrieving the item" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateItemDto createItemDto)
    {
        try
        {
            var userId = GetCurrentUserId();
            var item = await _itemService.CreateItemAsync(createItemDto, userId);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating item");
            return StatusCode(500, new { message = "An error occurred while creating the item" });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateItemDto updateItemDto)
    {
        try
        {
            var userId = GetCurrentUserId();
            var item = await _itemService.UpdateItemAsync(id, updateItemDto, userId);

            if (item == null)
                return NotFound(new { message = "Item not found" });

            return Ok(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating item {ItemId}", id);
            return StatusCode(500, new { message = "An error occurred while updating the item" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var userId = GetCurrentUserId();
            var success = await _itemService.DeleteItemAsync(id, userId);

            if (!success)
                return NotFound(new { message = "Item not found" });

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting item {ItemId}", id);
            return StatusCode(500, new { message = "An error occurred while deleting the item" });
        }
    }
}
