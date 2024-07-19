using core.Components;
using core.Repositories.ComponentRepository;
using core.Repositories.StarshipRepository;
using core.Starships;
using core.UI;
using core.Utils;

namespace core.Services;

public class StarshipService
{
	private readonly IComponentRepository _componentRepository;
	private readonly IStarshipRepository _starshipRepository;

	public StarshipService(
		IStarshipRepository starshipRepository,
		IComponentRepository componentRepository
	)
	{
		_starshipRepository = starshipRepository;
		this._componentRepository = componentRepository;
	}

	public Dictionary<String, Int32> GetStarshipSumsFromInput(
		String input,
		Action<String> printInvalidCommand
	)
	{
		var starshipCounts = new Dictionary<String, Int32>();

		foreach (var quantityAndStarship in input.Split(", "))
		{
			var (isValid, starshipName, quantity, errorMessage) =
				HandlerHelper.ParseQuantityAndStarship(quantityAndStarship);
			if (!isValid)
			{
				printInvalidCommand(errorMessage);
				return new Dictionary<String, Int32>();
			}

			if (!starshipCounts.ContainsKey(starshipName))
			{
				starshipCounts.Add(starshipName, quantity);
			}
			else
			{
				starshipCounts[starshipName] += quantity;
			}
		}

		return starshipCounts;
	}

	public (Int32, Int32, Int32, Int32) GetStarshipComponentsCountFromInventories(
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

	// Other methods
	public void AddStarship(Starship starship)
	{
		this._starshipRepository.Add(starship);
	}
}
