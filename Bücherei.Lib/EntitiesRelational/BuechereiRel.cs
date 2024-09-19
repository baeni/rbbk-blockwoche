namespace Bücherei.Lib.EntitiesRelational;

public class BuechereiRel
{   
    public int BuechereiId { get; set; }

    public required string Name { get; set; }

    public ICollection<Autor> Autoren { get; set; } = [];
}