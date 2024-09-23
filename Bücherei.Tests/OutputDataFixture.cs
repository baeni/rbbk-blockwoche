namespace Bücherei.Tests;

public class OutputDataFixture : IDisposable
{
    public const string fileNameRel = "output_Rel.json";
    public const string fileNameDoc = "output_Doc.json";

    public TestOutputData _outputDataRel;
    public TestOutputData _outputDataDoc;

    public OutputDataFixture()
    {
        this._outputDataRel = TestOutputData.LoadFromFile(fileNameRel);
        this._outputDataDoc = TestOutputData.LoadFromFile(fileNameDoc);
    }

    public void Dispose()
    {
        _outputDataRel.CalculateAverages();
        _outputDataDoc.CalculateAverages();

        _outputDataRel.OutputToFile(fileNameRel);
        _outputDataDoc.OutputToFile(fileNameDoc);
    }
}
