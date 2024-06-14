using Kolokwium2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    public ICharacterService _CharacterService;
    public CharactersController(ICharacterService characterService)
    {
        _CharacterService = characterService;
    }

    [HttpGet("{characterId}")]
    public async Task<IActionResult> GetCharacterInfo(int characterId)
    {
        try
        {
            var character = await _CharacterService.GetCharactersInfo(characterId);
            return Ok(character);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("{characterId}/backpacks")]
    public async Task<IActionResult> AddItemsToBackpack(int characterId, ICollection<int> itemsId)
    {
        try
        {
            var items = await _CharacterService.AddItemsToBackpack(characterId,itemsId);
            return Ok(items);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}