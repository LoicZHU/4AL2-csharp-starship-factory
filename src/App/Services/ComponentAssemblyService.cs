using core.Assemblies;
using core.Repositories.ComponentAssemblyRepository;
using core.UI;

namespace core.Services;

public class ComponentAssemblyService
{
	private readonly IComponentAssemblyRepository _componentAssemblyRepository;

	public ComponentAssemblyService(
		IComponentAssemblyRepository componentAssemblyRepository
	)
	{
		this._componentAssemblyRepository = componentAssemblyRepository;
	}

	public void Add(ComponentAssembly componentAssembly)
	{
		this._componentAssemblyRepository.Add(componentAssembly);
	}

	public void AddComponentAssemblyToItsInventory(
		ComponentAssembly componentAssembly,
		String componentName
	)
	{
		InstructionsDisplayHandler.PrintAssemblingComponents(
			componentAssembly,
			componentName
		);

		this._componentAssemblyRepository.AddComponent(componentAssembly.Id, componentName);
	}
}
