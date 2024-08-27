namespace Bücherei.Lib.EntitiesRelational;

public class Bücherei
{   
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<Autor> Autoren { get; set; } = [];
}