using System;

public interface IDataPackage
{
    string GetData();
}

public class BaseDataPackage : IDataPackage
{
    private string _data;

    public BaseDataPackage(string data)
    {
        _data = data;
    }

    public string GetData()
    {
        return _data;
    }
}

public class ChecksumDecorator : IDataPackage
{
    private IDataPackage _dataPackage;
    private string _checksum;

    public ChecksumDecorator(IDataPackage dataPackage)
    {
        _dataPackage = dataPackage;
        _checksum = CalculateChecksum(dataPackage.GetData());
    }

    public string GetData()
    {
        return $"{_dataPackage.GetData()} [Checksum: {_checksum}]";
    }

    private string CalculateChecksum(string data)
    {
        int checksum = 0;
        foreach (char c in data)
        {
            checksum += (int)c;
        }
        return checksum.ToString();
    }
}

public class MetadataDecorator : IDataPackage
{
    private IDataPackage _dataPackage;
    private string _metadata;

    public MetadataDecorator(IDataPackage dataPackage, string metadata)
    {
        _dataPackage = dataPackage;
        _metadata = metadata;
    }

    public string GetData()
    {
        return $"{_dataPackage.GetData()} [Metadata: {_metadata}]";
    }
}

class Program
{
    static void Main(string[] args)
    {
        IDataPackage basePackage = new BaseDataPackage("Hello, world!");

        IDataPackage packageWithChecksum = new ChecksumDecorator(basePackage);

        IDataPackage packageWithMetadata = new MetadataDecorator(basePackage, "Created on 2022-04-01");

        Console.WriteLine("Base Package Data: " + basePackage.GetData());
        Console.WriteLine("Package with Checksum: " + packageWithChecksum.GetData());
        Console.WriteLine("Package with Metadata: " + packageWithMetadata.GetData());
    }
}
