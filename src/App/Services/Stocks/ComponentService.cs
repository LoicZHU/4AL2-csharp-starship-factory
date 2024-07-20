using core.Components;
using core.Repositories.ComponentRepository;
using core.UI;
using core.Utils;

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

	public (Int32, Int32, Int32, Int32) GetComponentsCountFromInventories(
		String starshipName
	)
	{
		try
		{
			if (HandlerHelper.IsCargoStarship(starshipName))
			{
				return (
					this._componentRepository.GetCountByName(EngineComponent.EngineEc1),
					this._componentRepository.GetCountByName(HullComponent.HullHc1),
					this._componentRepository.GetCountByName(WingComponent.WingWc1),
					this._componentRepository.GetCountByName(ThrusterComponent.ThrusterTc1)
				);
			}

			if (HandlerHelper.IsExplorerStarship(starshipName))
			{
				return (
					this._componentRepository.GetCountByName(EngineComponent.EngineEe1),
					this._componentRepository.GetCountByName(HullComponent.HullHe1),
					this._componentRepository.GetCountByName(WingComponent.WingWe1),
					this._componentRepository.GetCountByName(ThrusterComponent.ThrusterTe1)
				);
			}

			if (HandlerHelper.IsSpeederStarship(starshipName))
			{
				return (
					this._componentRepository.GetCountByName(EngineComponent.EngineEs1),
					this._componentRepository.GetCountByName(HullComponent.HullHs1),
					this._componentRepository.GetCountByName(WingComponent.WingWs1),
					this._componentRepository.GetCountByName(ThrusterComponent.ThrusterTs1)
				);
			}

			return (0, 0, 0, 0);
		}
		catch (Exception e)
		{
			Terminal.PrintMessageWithLinebreak(e.Message);
			return (0, 0, 0, 0);
		}
	}
}
