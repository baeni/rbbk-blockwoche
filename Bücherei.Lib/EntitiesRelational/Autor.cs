namespace Bücherei.Lib.EntitiesRelational;

public class Autor
{
    public int AutorId { get; set; }

    public required string Firstname { get; set; } = null!;

    public required string Surname { get; set; } = null!;

    public ICollection<Buch> Buecher { get; set; } = [];

    public ICollection<BuechereiRel> Buechereien { get; set; } = [];
}