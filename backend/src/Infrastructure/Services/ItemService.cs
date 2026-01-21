using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ItemService : IItemService
{
    private readonly ApplicationDbContext _context;

    public ItemService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ItemDto>> GetAllItemsAsync(int userId)
    {
        return await _context.Items
            .Where(i => i.CreatedBy == userId)
            .Include(i => i.User)
            .Select(i => new ItemDto
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                CreatedBy = i.CreatedBy,
                CreatedByName = i.User.FullName,
                CreatedAt = i.CreatedAt,
                UpdatedAt = i.UpdatedAt
            })
            .ToListAsync();
    }

    public async Task<ItemDto?> GetItemByIdAsync(int id, int userId)
    {
        var item = await _context.Items
            .Include(i => i.User)
            .FirstOrDefaultAsync(i => i.Id == id && i.CreatedBy == userId);

        if (item == null)
            return null;

        return new ItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            CreatedBy = item.CreatedBy,
            CreatedByName = item.User.FullName,
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.UpdatedAt
        };
    }

    public async Task<ItemDto> CreateItemAsync(CreateItemDto createItemDto, int userId)
    {
        var item = new Item
        {
            Name = createItemDto.Name,
            Description = createItemDto.Description,
            CreatedBy = userId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Items.Add(item);
        await _context.SaveChangesAsync();

        // Load the user relationship
        await _context.Entry(item).Reference(i => i.User).LoadAsync();

        return new ItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            CreatedBy = item.CreatedBy,
            CreatedByName = item.User.FullName,
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.UpdatedAt
        };
    }

    public async Task<ItemDto?> UpdateItemAsync(int id, UpdateItemDto updateItemDto, int userId)
    {
        var item = await _context.Items
            .Include(i => i.User)
            .FirstOrDefaultAsync(i => i.Id == id && i.CreatedBy == userId);

        if (item == null)
            return null;

        item.Name = updateItemDto.Name;
        item.Description = updateItemDto.Description;
        item.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new ItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            CreatedBy = item.CreatedBy,
            CreatedByName = item.User.FullName,
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.UpdatedAt
        };
    }

    public async Task<bool> DeleteItemAsync(int id, int userId)
    {
        var item = await _context.Items
            .FirstOrDefaultAsync(i => i.Id == id && i.CreatedBy == userId);

        if (item == null)
            return false;

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();

        return true;
    }
}
