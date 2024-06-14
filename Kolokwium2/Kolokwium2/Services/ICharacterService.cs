using Kolokwium2.DTOs;

namespace Kolokwium2.Services;

public interface ICharacterService
{
    Task<GetCharacterDTO> GetCharactersInfo(int id);
    Task<ICollection<ItemsInfoDTO>> AddItemsToBackpack(int characterId,ICollection<int> itemsId);
}