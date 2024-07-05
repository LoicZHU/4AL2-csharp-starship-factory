using core.Repositories.ComponentRepository;
using core.Repositories.StarshipRepository;
using core.UI;

namespace core.App.Handlers;

public class StockHandler : IHandler
{
	private readonly IStarshipRepository _starshipRepository;
	private readonly IComponentRepository _componentRepository;

	public StockHandler(
		IStarshipRepository starshipRepository,
		IComponentRepository componentRepository
	)
	{
		this._starshipRepository = starshipRepository;
		this._componentRepository = componentRepository;
	}

	public void Handle()
	{
		this.PrintStarshipStock();
		this.PrintComponentStock();
	}

	private void PrintComponentStock()
	{
		var componentCounts = this._componentRepository.GetStockOfEachComponent();
		StockDisplayHandler.PrintComponentStock(componentCounts);
	}

	private void PrintStarshipStock()
	{
		var starshipCounts = this._starshipRepository.GetStock();
		StockDisplayHandler.PrintStarshipStock(starshipCounts);
	}
}
