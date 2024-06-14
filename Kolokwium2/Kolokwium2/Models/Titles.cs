using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


[Table("titles")]
public class Titles
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }

    public ICollection<CharactersTitles> CharactersTitles { get; set; } = new HashSet<CharactersTitles>();
}