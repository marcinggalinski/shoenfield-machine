using System.Text.RegularExpressions;

namespace ShoenfieldMachine.Types;

public abstract record Instruction;

public partial record IncreaseInstruction(uint Register) : Instruction
{
    [GeneratedRegex(@"^\s*INCREASE\s+(?<register>\d+)\s*$", RegexOptions.Compiled)]
    private static partial Regex Regex();
    
    public static IncreaseInstruction? TryParse(string input)
    {
        var match = Regex().Match(input);
        return match.Success ? new IncreaseInstruction(uint.Parse(match.Groups["register"].Value)) : null;
    }
}

public partial record DecreaseInstruction(uint Register, int InstructionNumber) : Instruction
{
    [GeneratedRegex(@"^\s*DECREASE\s+(?<register>\d+)\s*,\s*(?<numline>\d+)\s*$", RegexOptions.Compiled)]
    private static partial Regex Regex();
    
    public static DecreaseInstruction? TryParse(string input)
    {
        var match = Regex().Match(input);
        return match.Success
            ? new DecreaseInstruction(
                uint.Parse(match.Groups["register"].Value),
                int.Parse(match.Groups["numline"].Value))
            : null;
    }
}

public partial record GoToInstruction(int InstructionNumber) : Instruction
{
    [GeneratedRegex(@"^\s*GO TO\s+(?<numline>\d+)\s*$", RegexOptions.Compiled)]
    private static partial Regex Regex();

    public static GoToInstruction? TryParse(string input)
    {
        var match = Regex().Match(input);
        return match.Success ? new GoToInstruction(int.Parse(match.Groups["numline"].Value)) : null;
    }
}