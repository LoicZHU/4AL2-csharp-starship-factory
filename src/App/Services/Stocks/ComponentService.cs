using core.Repositories.ComponentRepository;

namespace core.Services;

public class ComponentService
{
	private readonly IComponentRepository _componentRepository;

	public ComponentService(IComponentRepository componentRepository)
	{
		this._componentRepository = componentRepository;
	}

	public void GetComponentsOutFromStock(String componentName, Int32 quantity)
	{
		for (var i = 1; i <= quantity; i++)
		{
			this._componentRepository.Remove(componentName);
		}
	}
}
