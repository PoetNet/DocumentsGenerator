namespace Shared.Interfaces;

public interface IModule
{
    string Name { get; }
    void Execute();
}
