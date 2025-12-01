using System.CommandLine;
using System.Text;

var rootCommand = new RootCommand("Command line interface for Shoenfield Machine computations.");
var inputOption = new Option<FileInfo>("--input", "-i")
{
    Description = "Input file path.",
    Required = false
};

rootCommand.Options.Add(inputOption);

var parseResult = rootCommand.Parse(args);
if (parseResult.Errors.Any())
{
    foreach (var error in parseResult.Errors)
        Console.Error.WriteLine(error);
    return 1;
}

string program;
if (parseResult.GetValue(inputOption) is FileInfo inputFilePath)
{
    program = File.ReadAllText(inputFilePath.FullName);
}
else
{
    Console.WriteLine("Enter program (blank line to finish):");
    
    var sb = new StringBuilder();
    while (true)
    {
        var line = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(line))
            break;
        sb.AppendLine(line);
    }
    
    program = sb.ToString();
}

var machine = new ShoenfieldMachine.ShoenfieldMachine
{
    Program = program
};

if (!machine.IsValid)
{
    Console.Error.WriteLine("Invalid program.");
    return 1;
}

Console.WriteLine("Enter register values (leave empty to start executing program):");

string? input;
uint register = 0;
do
{
    Console.Write($"R{register} = ");
    input = Console.ReadLine();
    if (uint.TryParse(input, out var value))
        machine.Registers[register++] = value;
} while (!string.IsNullOrWhiteSpace(input));
Console.CursorTop -= 1;
Console.WriteLine(new string(' ', Console.BufferWidth));

await machine.RunAsync();
    
Console.WriteLine("Results:");
foreach (var (key, value) in machine.Registers)
    Console.WriteLine($"R{key} = {value}");

return 0;