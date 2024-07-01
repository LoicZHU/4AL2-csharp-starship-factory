using core.Assemblies;
using core.In_memories;

namespace core.Repositories.ComponentAssemblyRepository;

public class ComponentAssemblyRepository : IComponentAssemblyRepository
{
	private readonly InMemoryComponentAssembly _inMemoryComponentAssembly;

	public ComponentAssemblyRepository(InMemoryComponentAssembly inMemoryComponentAssembly)
	{
		this._inMemoryComponentAssembly = inMemoryComponentAssembly;
	}

	public void Add(ComponentAssembly componentAssembly)
	{
		this._inMemoryComponentAssembly.Add(componentAssembly);
	}

	public void AddComponent(Guid id, String component)
	{
		this._inMemoryComponentAssembly.AddComponent(id, component);
	}
}
