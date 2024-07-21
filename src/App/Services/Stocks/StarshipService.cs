using core.Repositories.StarshipRepository;
using core.Starships;
using core.Utils;

namespace core.Services;

public class StarshipService
{
	private readonly IStarshipRepository _starshipRepository;

	public StarshipService(IStarshipRepository starshipRepository)
	{
		_starshipRepository = starshipRepository;
	}

	public void AddStarship(Starship starship)
	{
		this._starshipRepository.Add(starship);
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
}
