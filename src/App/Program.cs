using core.App.Handlers;
using core.App.UI;
using core.InputHandlers;
using core.In_memories;
using core.In_memories.Items;
using core.Repositories.ComponentAssemblyRepository;
using core.Repositories.ComponentRepository;
using core.Repositories.OrderRepository;
using core.Repositories.StarshipRepository;
using core.Services;
using core.UI;
using core.UI.constants;
using core.Utils;

namespace core;

public static class Program
{
	private static IUserInterface? _userInterface;

	public static void Main(string[] args)
	{
		ConfigureDependencies();
		StartUserInterface();
	}

	private static void ConfigureDependencies()
	{
		IComponentAssemblyRepository componentAssemblyRepository =
			AbstractSingleton<InMemoryComponentAssembly>.Instance;
		IComponentRepository componentRepository =
			AbstractSingleton<InMemoryComponent>.Instance;
		IOrderRepository orderRepository = AbstractSingleton<InMemoryOrder>.Instance;
		IStarshipRepository starshipRepository = AbstractSingleton<InMemoryStarship>.Instance;

		var handlers = new Dictionary<String, IHandler>
		{
			{ Command.Exit, new ExitHandler() },
			{ Command.Help, new HelpDisplayHandler() },
			{ Command.ListOrder, new ListOrderHandler(orderRepository) },
			{ Command.Stocks, new StockHandler(starshipRepository, componentRepository) },
		};

		var inputHandlers = new Dictionary<String, IInputHandler>
		{
			{
				Command.Instructions,
				new InstructionsHandler(
					new ComponentAssemblyService(componentAssemblyRepository),
					new ComponentService(componentRepository),
					new InventoryService(),
					new StarshipService(starshipRepository, componentRepository)
				)
			},
			{ Command.NeededStocks, new NeededStocksHandler() },
			{ Command.Order, new OrderHandler(orderRepository) },
			{
				Command.Produce,
				new ProduceHandler(
					new ComponentService(componentRepository),
					new InventoryService(),
					new StarshipService(starshipRepository, componentRepository)
				)
			},
			{ Command.Send, new SendHandler(orderRepository, starshipRepository) },
			{
				Command.Verify,
				new VerifyHandler(
					new InventoryService(),
					new StarshipService(starshipRepository, componentRepository)
				)
			},
		};

		_userInterface = new Menu(handlers, inputHandlers);
	}

	private static void StartUserInterface()
	{
		_userInterface?.Start();
	}
}
