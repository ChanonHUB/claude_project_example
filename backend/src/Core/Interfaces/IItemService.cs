using Core.DTOs;

namespace Core.Interfaces;

public interface IItemService
{
    Task<IEnumerable<ItemDto>> GetAllItemsAsync(int userId);
    Task<ItemDto?> GetItemByIdAsync(int id, int userId);
    Task<ItemDto> CreateItemAsync(CreateItemDto createItemDto, int userId);
    Task<ItemDto?> UpdateItemAsync(int id, UpdateItemDto updateItemDto, int userId);
    Task<bool> DeleteItemAsync(int id, int userId);
}
