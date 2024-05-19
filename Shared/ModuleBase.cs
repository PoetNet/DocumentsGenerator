using Shared.Interfaces;

namespace GeneratorService;

public abstract class ModuleBase : IModule
{
    public abstract string Name { get; }
    public abstract void Execute();

}
