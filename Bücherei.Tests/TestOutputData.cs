using Bücherei.Lib.Services;
using Newtonsoft.Json.Serialization;
using System.Text.Json;

namespace Bücherei.Tests;

public class TestOutputData
{
    public List<TimeSpan> CreateSmall { get; set; } = new();
    public List<TimeSpan> CreateMedium { get; set; } = new();
    public List<TimeSpan> CreateLarge { get; set; } = new();

    public List<TimeSpan> SearchSmall { get; set; } = new();
    public List<TimeSpan> SearchMedium { get; set; } = new();
    public List<TimeSpan> SearchLarge { get; set; } = new();


    public float CreateSmallAvg { get; set; } = 0;
    public float CreateMediumAvg { get; set; } = 0;
    public float CreateLargeAvg { get; set; } = 0;

    public float SearchSmallAvg { get; set; } = 0;
    public float SearchMediumAvg { get; set; } = 0;
    public float SearchLargeAvg { get; set; } = 0;

    public void CalculateAverages()
    {
        CalcAvg_CreateSmall();
        CalcAvg_CreateMedium();
        CalcAvg_CreateLarge();

        CalcAvg_SearchSmall();
        CalcAvg_SearchMedium();
        CalcAvg_SearchLarge();
    }

    private void CalcAvg_CreateSmall() {
        if (CreateSmall.Count > 0)
            CreateSmallAvg = (float)CreateSmall.Average(c => c.TotalSeconds);
        else 
            CreateSmallAvg = 0;
    }
    private void CalcAvg_CreateMedium() {
        if (CreateMedium.Count > 0)
            CreateMediumAvg = (float)CreateMedium.Average(c => c.TotalSeconds);
        else
            CreateMediumAvg = 0;
    }
    private void CalcAvg_CreateLarge() {
        if (CreateLarge.Count > 0)
            CreateLargeAvg = (float)CreateLarge.Average(c => c.TotalSeconds);
        else 
            CreateLargeAvg = 0;

    }

    private void CalcAvg_SearchSmall() {
        if (SearchSmall.Count > 0)
            SearchSmallAvg = (float)SearchSmall.Average(c => c.TotalSeconds);
        else 
            SearchSmallAvg = 0;
    }
    private void CalcAvg_SearchMedium() {
        if (SearchMedium.Count > 0)
            SearchMediumAvg = (float)SearchMedium.Average(c => c.TotalSeconds);
        else 
            SearchMediumAvg = 0;
    }
    private void CalcAvg_SearchLarge() {
        if (SearchLarge.Count > 0)
            SearchLargeAvg = (float)SearchLarge.Average(c => c.TotalSeconds);
        else
            SearchLargeAvg = 0;
    }

    public static TestOutputData LoadFromFile(string filepath)
    {
        if (File.Exists(filepath))
        {
            var jsonString = File.ReadAllText(filepath);
            var outputData = JsonSerializer.Deserialize<TestOutputData>(jsonString);
            return outputData;
        }
        else
            return new TestOutputData();
    }

    public void OutputToFile(string filepath)
    {
        var jsonString = JsonSerializer.Serialize(this);

        File.WriteAllText(filepath, jsonString);
    }

}
