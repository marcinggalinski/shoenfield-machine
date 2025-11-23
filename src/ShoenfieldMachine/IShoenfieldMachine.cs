namespace ShoenfieldMachine;

public interface IShoenfieldMachine
{
    IDictionary<uint, uint> Registers { get; }
    public uint ProgramCounter { get; }
    public string Program { get; set; }
    
    public Task RunAsync();
}