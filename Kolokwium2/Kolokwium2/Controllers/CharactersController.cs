using Kolokwium2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    public ICharacterService _characterService;
    public CharactersController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    [HttpGet("{characterId}")]
    public async Task<IActionResult> GetCharacterInfo(int characterId)
    {
        try
        {
            var character = await _characterService.GetCharactersInfo(characterId);
            return Ok(character);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving the character information." });
        }
    }

    [HttpPost("{characterId}/backpacks")]
    public async Task<IActionResult> AddItemsToBackpack(int characterId, ICollection<int> itemsId)
    {
        try
        {
            var items = await _characterService.AddItemsToBackpack(characterId, itemsId);
            return Ok(items);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while adding items to the backpack." });
        }
    }
}