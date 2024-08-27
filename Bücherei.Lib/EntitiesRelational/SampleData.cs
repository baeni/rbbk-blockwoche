namespace Bücherei.Lib.EntitiesRelational;

public class SampleData
{
    public SampleData() {}

    public List<Autor> Autoren { get; init; } = [];
    
    public List<Bücherei> Büchereien { get; init; } = [];

    public List<Buch> Bücher { get; init; } = [];
}