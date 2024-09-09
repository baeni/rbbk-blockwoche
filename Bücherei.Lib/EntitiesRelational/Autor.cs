namespace Bücherei.Lib.EntitiesRelational;

public class Autor
{
    public int Id { get; set; }

    public required string Name { get; set; } = null!;

    public ICollection<Buch> Buecher { get; set; } = [];

    public ICollection<BuechereiRel> Buechereien { get; set; } = [];
}