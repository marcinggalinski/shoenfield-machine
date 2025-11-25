namespace ShoenfieldMachine;

public interface IShoenfieldMachine
{
    IDictionary<uint, uint> Registers { get; }
    public int ProgramCounter { get; }
    public string Program { get; set; }
    
    public Task RunAsync();
    public void Reset();
}