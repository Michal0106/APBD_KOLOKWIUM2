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
            throw new Exception($"Character with given id = {id} has not been found");
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
        
        var newItemsWeight = await _dataBaseContext.Items
            .Where(i => itemsId.Contains(i.Id))
            .SumAsync(i => i.Weight);
        
        character.CurrentWeight += newItemsWeight;

        var items = new List<ItemsInfoDTO>();
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
            items.Add(new ItemsInfoDTO()
            {
                Amount = 1,
                ItemId = itemId,
                CharacterId = characterId
            });
        }

        await _dataBaseContext.SaveChangesAsync();

        return items;
    }

    public async Task<bool> DoItemsExist(ICollection<int> itemsId)
    {
        foreach (var itemId in itemsId)
        {
            var item = await _dataBaseContext.Items.AnyAsync(i => i.Id == itemId);
            if (!item)
            {
                throw new Exception($"Item for given id = {itemId} has been not found in Items repository");
            }
        }
        return true;
    }

    public async Task<bool> IsWeightExceeded(int characterId, ICollection<int> itemsId)
    {
        var character = await _dataBaseContext.Characters.FirstOrDefaultAsync(ch => ch.Id == characterId);

        if (character is null)
        {
            throw new Exception($"Character for given id = {characterId} does not exist");
        }
        
        var currentWeight = character.CurrentWeight;
        var newItemsWeight = await _dataBaseContext.Items.Where(i => itemsId.Contains(i.Id)).SumAsync(i => i.Weight);

        var totalWeight = currentWeight + newItemsWeight;
        if (totalWeight > character.MaxWeight)
        {
            throw new Exception("Max weight exceeded");
        }

        return false;
    }
}