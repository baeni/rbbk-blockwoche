namespace Bücherei.Lib.Entities;

public class Buch
{
    public int Id { get; set; }

    public int BüchereiId { get; set; }

    public string Titel { get; set; }

    public Autor Autor { get; set; }
}