using System.Collections.ObjectModel;
using Bücherei.Lib.Entities;

namespace Bücherei.Cli;

public class Data
{
    public Data() {}

    public List<Autor> Autoren { get; init; } = [];
    
    public List<Bücherei.Lib.Entities.Bücherei> Büchereien { get; init; } = [];

    public List<Buch> Bücher { get; init; } = [];
}