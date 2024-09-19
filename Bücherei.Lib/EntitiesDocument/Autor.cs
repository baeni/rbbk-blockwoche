namespace Bücherei.Lib.EntitiesDocument;

public class Autor
{
    public int Id { get; set; }

    public required string Firstname { get; set; } = null!;

    public required string Surname { get; set; } = null!;

    public ICollection<Buch> Buecher { get; set; } = [];
}
