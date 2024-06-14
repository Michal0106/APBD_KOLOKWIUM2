using System.Collections;
using Kolokwium2.Data;
using Kolokwium2.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2.Services;

public class CharacterService : ICharacterService
{
    private DatabaseContext _dataBaseContext;
    public CharacterService(DatabaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task<GetCharacterDTO> GetCharactersInfo(int id)
    {
        var character = await _dataBaseContext.Characters
            .Include(c => c.Backpacks)
                .ThenInclude(bp => bp.Items)
            .Include(c => c.CharactersTitles)
                .ThenInclude(ct => ct.Titles)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (character is null)
        {
            throw new KeyNotFoundException($"Character with given id = {id} has not been found");
        }

        var charactersInfo = new GetCharacterDTO()
        {
            FirstName = character.FirstName,
            LastName = character.LastName,
            CurrentWeight = character.CurrentWeight,
            MaxWeight = character.MaxWeight,
            BackpackItems = character.Backpacks.Select(bp => new ItemsDto()
            {
                ItemName = bp.Items.Name,
                ItemWeight = bp.Items.Weight,
                Amount = bp.Amount
            }).ToList(),
            Titles = character.CharactersTitles.Select(cht => new TitlesDto()
            {
                Title = cht.Titles.Name,
                AcquiredAt = cht.AcquiredAt
            }).ToList()
        };

        return charactersInfo;
    }

    public async Task<ICollection<ItemsInfoDTO>> AddItemsToBackpack(int characterId, ICollection<int> itemsId)
    {
        if (!await DoItemsExist(itemsId))
        {
            return null;
        }

        if (await IsWeightExceeded(characterId, itemsId))
        {
            return null;
        }
        var character = await _dataBaseContext.Characters
            .Include(ch => ch.Backpacks)
            .FirstOrDefaultAsync(ch => ch.Id == characterId);
        
        var itemsToAdd = await _dataBaseContext.Items
            .Where(i => itemsId.Contains(i.Id))
            .ToListAsync();

        var newItemsWeight = itemsToAdd.Sum(i => i.Weight * itemsId.Count(id => id == i.Id));
        
        character.CurrentWeight += newItemsWeight;

        foreach (var itemId in itemsId)
        {
            var existingBackpackItem = character.Backpacks.FirstOrDefault(bp => bp.ItemId == itemId);

            if (existingBackpackItem != null)
            {
                existingBackpackItem.Amount += 1;
            }
            else
            {
                character.Backpacks.Add(new Backpacks
                {
                    CharacterId = characterId,
                    ItemId = itemId,
                    Amount = 1
                });
            }
        }

        await _dataBaseContext.SaveChangesAsync();

        return character.Backpacks
            .Where(bp => bp.CharacterId == characterId)
            .Select(bp => new ItemsInfoDTO()
                {
                    Amount = bp.Amount,
                    ItemId = bp.ItemId,
                    CharacterId = characterId
                }).ToList();
    }

    public async Task<bool> DoItemsExist(ICollection<int> itemsId)
    {
        foreach (var itemId in itemsId)
        {
            var item = await _dataBaseContext.Items.AnyAsync(i => i.Id == itemId);
            if (!item)
            {
                throw new KeyNotFoundException($"Item with ID = {itemId} was not found in the Items repository.");
            }
        }
        return true;
    }

    public async Task<bool> IsWeightExceeded(int characterId, ICollection<int> itemsId)
    {
        var character = await _dataBaseContext.Characters.FirstOrDefaultAsync(ch => ch.Id == characterId);

        if (character is null)
        {
            throw new KeyNotFoundException($"Character for given id = {characterId} does not exist");
        }
        
        var currentWeight = character.CurrentWeight;
        
        var itemsToAdd = await _dataBaseContext.Items
            .Where(i => itemsId.Contains(i.Id))
            .ToListAsync();

        var newItemsWeight = itemsToAdd.Sum(i => i.Weight * itemsId.Count(id => id == i.Id));
        
        var totalWeight = currentWeight + newItemsWeight;
        if (totalWeight > character.MaxWeight)
        {
            throw new InvalidOperationException("Max weight exceeded");
        }

        return false;
    }
}