using core.Assemblies;
using core.Repositories.ComponentRepository;
using core.UI;

namespace core.Services;

public class ComponentService
{
	private readonly IComponentRepository _componentRepository;

	public ComponentService(IComponentRepository componentRepository)
	{
		this._componentRepository = componentRepository;
	}

	public void GetComponentsOutFromStock(String starshipName)
	{
		foreach (var (componentName, quantity) in StarshipAssembly.Components[starshipName])
		{
			InstructionsDisplayHandler.PrintGetOutStock(quantity, componentName);

			for (var i = 1; i <= quantity; i++)
			{
				this._componentRepository.Remove(componentName);
			}
		}
	}
}
