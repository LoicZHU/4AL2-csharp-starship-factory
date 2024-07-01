using core.Assemblies;

namespace core.Repositories.ComponentAssemblyRepository;

public interface IComponentAssemblyRepository
{
	public void Add(ComponentAssembly componentAssembly);
	public void AddComponent(Guid id, String component);
}
