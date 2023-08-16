using System;
namespace Smartwyre.DeveloperTest.Types.Readers;

public abstract class BaseRequestReader
{
    protected static decimal ReadDecimalLine(string name)
    {
        var valueStr = ReadLine(name);

        return decimal.Parse(valueStr);
    }

    // we could use a generic for a more robust implementation
    protected static string ReadLine(string name)
    {
        Console.WriteLine($"Type the {name}:");

        var line = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(line))
        {
            Console.WriteLine($"Please type something, the {name} cannot be empty");

            line = Console.ReadLine();
        }

        return line.Trim();
    }
}

