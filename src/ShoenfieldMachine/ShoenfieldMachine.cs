using System.Text.RegularExpressions;
using ShoenfieldMachine.Types;

namespace ShoenfieldMachine;

public class ShoenfieldMachine : IShoenfieldMachine
{
    public IDictionary<uint, uint> Registers { get; private set; } = new Dictionary<uint, uint>();
    public int ProgramCounter { get; private set; }
    
    public string Program
    {
        get;
        set
        {
            field = value;
            ParseProgram();
        }
    } = string.Empty;

    public List<Instruction> ParsedProgram { get; private set; } = [];
    public bool IsValid { get; private set; }

    private Instruction? ParseLine(string line)
    {
        Instruction? parsed = IncreaseInstruction.TryParse(line);
        if (parsed is not null)
            return parsed;
        
        parsed = DecreaseInstruction.TryParse(line);
        if (parsed is not null)
            return parsed;
        
        parsed = GoToInstruction.TryParse(line);
        if (parsed is not null)
            return parsed;

        return null;
    }

    private void ParseProgram()
    {
        ParsedProgram = [];
        IsValid = false;
        
        var result = new List<Instruction>();
        foreach (var line in Program.Split(["\n", "\r\n", "\r"], StringSplitOptions.None))
        {
            var instruction = ParseLine(line);
            if (instruction is null)
                return;
            result.Add(instruction);
        }
        
        ParsedProgram = result;
        IsValid = true;
    }
    
    public Task RunAsync()
    {
        return Task.Run(() =>
        {
            if (!IsValid)
                throw new InvalidOperationException("The program is not valid.");

            ProgramCounter = 0;
            while (ProgramCounter < ParsedProgram.Count)
            {
                var currentInstruction = ParsedProgram[ProgramCounter];
                switch (currentInstruction)
                {
                    case IncreaseInstruction inc:
                        {
                            Registers[inc.Register] =
                                Registers.TryGetValue(inc.Register, out var value)
                                    ? value + 1
                                    : 1;
                            ProgramCounter++;
                        }
                        break;
                    case DecreaseInstruction dec:
                        {
                            if (Registers.TryGetValue(dec.Register, out var value) && value > 0)
                            {
                                Registers[dec.Register]--;
                                ProgramCounter = dec.InstructionNumber;
                            }
                            else
                                ProgramCounter++;
                        }
                        break;
                    case GoToInstruction goTo:
                        ProgramCounter = goTo.InstructionNumber;
                        break;
                }
            }
        });
    }

    public void Reset()
    {
        Registers = new Dictionary<uint, uint>();
        Program = string.Empty;
    }
}