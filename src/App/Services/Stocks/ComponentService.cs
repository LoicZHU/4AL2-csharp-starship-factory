using core.Assemblies;
using core.Components;
using core.Repositories.ComponentRepository;
using core.Starships;
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

	public Boolean IsComponentStockInsufficient(Dictionary<String, Int32> starshipCounts)
	{
		foreach (var (starshipName, quantity) in starshipCounts)
		{
			var (engineCount, hullCount, wingCount, thrusterCount) =
				this.GetComponentsCountFromInventories(starshipName);

			if (
				this.IsMoreInventoryRequired(
					starshipName,
					quantity,
					hullCount,
					engineCount,
					wingCount,
					thrusterCount
				)
			)
			{
				return true;
			}
		}

		return false;
	}

	public Boolean IsMoreInventoryRequired(
		String starshipName,
		Int32 quantity,
		Int32 hullCount,
		Int32 engineCount,
		Int32 wingCount,
		Int32 thrusterCount
	)
	{
		if (HandlerHelper.IsSpeederStarship(starshipName))
		{
			return this.IsMoreInventoryRequiredForSpeederStarship(
				quantity,
				hullCount,
				engineCount,
				wingCount,
				thrusterCount
			);
		}

		if (HandlerHelper.IsCargoOrExplorerStarship(starshipName))
		{
			return this.IsMoreInventoryRequiredForCargoOrExplorerStarship(
				quantity,
				hullCount,
				engineCount,
				wingCount,
				thrusterCount
			);
		}

		return false;
	}

	public Boolean IsMoreInventoryRequiredForSpeederStarship(
		Int32 quantity,
		Int32 hullCount,
		Int32 engineCount,
		Int32 wingCount,
		Int32 thrusterCount
	)
	{
		var components = StarshipAssembly.Components[StarshipName.Speeder];

		return hullCount < components[HullComponent.HullHs1] * quantity
			|| engineCount < components[EngineComponent.EngineEs1] * quantity
			|| wingCount < components[WingComponent.WingWs1] * quantity
			|| thrusterCount < components[ThrusterComponent.ThrusterTs1] * quantity;
	}

	public Boolean IsMoreInventoryRequiredForCargoOrExplorerStarship(
		Int32 quantity,
		Int32 hullCount,
		Int32 engineCount,
		Int32 wingCount,
		Int32 thrusterCount
	)
	{
		var components = StarshipAssembly.Components[StarshipName.Cargo];

		return hullCount < components[HullComponent.HullHc1] * quantity
			|| engineCount < components[EngineComponent.EngineEc1] * quantity
			|| wingCount < components[WingComponent.WingWc1] * quantity
			|| thrusterCount < components[ThrusterComponent.ThrusterTc1] * quantity;
	}
}
