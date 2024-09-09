namespace Bücherei.Lib.EntitiesRelational;

public class BuechereiRel
{   
    public int Id { get; set; }

    public required string Name { get; set; }

    public ICollection<Autor> Autoren { get; set; } = [];
}