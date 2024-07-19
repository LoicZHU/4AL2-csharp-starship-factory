using core.Assemblies;
using core.Components;
using core.Starships;
using core.Utils;

namespace core.Services;

public class InventoryService
{
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
