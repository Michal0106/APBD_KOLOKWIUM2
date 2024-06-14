namespace Kolokwium2.DTOs;

public class GetCharacterDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    public ICollection<ItemsDto> BackpackItems { get; set; } = new HashSet<ItemsDto>();
    public ICollection<TitlesDto> Titles { get; set; } = new HashSet<TitlesDto>();
}
public class ItemsDto
{
    public string ItemName { get; set; } = String.Empty;
    public int ItemWeight { get; set; }
    public int Amount { get; set; }
}

public class TitlesDto
{
    public string Title { get; set; } = string.Empty;
    public DateTime AcquiredAt { get; set; }
}