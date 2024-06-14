using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table("backpacks")]
[PrimaryKey(nameof(CharacterId),nameof(ItemId))]
public class Backpacks
{
    public int CharacterId { get; set; }
    public int ItemId { get; set; }
    public int Amount { get; set; }

    [ForeignKey(nameof(CharacterId))] 
    public Characters Characters { get; set; } = null!;

    [ForeignKey(nameof(ItemId))] 
    public Items Items { get; set; } = null!;
}