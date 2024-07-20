using core.App.Handlers;
using core.App.UI;
using core.InputHandlers;
using core.In_memories;
using core.In_memories.Items;
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

	private static Dictionary<String, IHandler>? _handlers;
	private static Dictionary<String, IInputHandler>? _inputHandlers;

	private static IComponentRepository? _componentRepository;
	private static IOrderRepository? _orderRepository;
	private static IStarshipRepository? _starshipRepository;

	public static void Main(string[] args)
	{
		ConfigureDependencies();
		StartUserInterface();
	}

	private static void ConfigureDependencies()
	{
		SetRepositories();
		SetAllHandlers();
		SetUserInterface();
	}

	private static void StartUserInterface()
	{
		_userInterface?.Start();
	}

	private static void SetRepositories()
	{
		_componentRepository = AbstractSingleton<InMemoryComponent>.Instance;
		_orderRepository = AbstractSingleton<InMemoryOrder>.Instance;
		_starshipRepository = AbstractSingleton<InMemoryStarship>.Instance;
	}

	private static void SetAllHandlers()
	{
		_handlers = new Dictionary<String, IHandler>
		{
			{ Command.Exit, new ExitHandler() },
			{ Command.Help, new HelpDisplayHandler() },
			{ Command.ListOrder, new ListOrderHandler(_orderRepository) },
			{ Command.Stocks, new StockHandler(_starshipRepository, _componentRepository) },
		};

		_inputHandlers = new Dictionary<String, IInputHandler>
		{
			{
				Command.Instructions,
				new InstructionsHandler(
					GetNewComponentService(),
					GetNewInventoryService(),
					GetNewStarshipService()
				)
			},
			{ Command.NeededStocks, new NeededStocksHandler() },
			{ Command.Order, new OrderHandler(_orderRepository) },
			{
				Command.Produce,
				new ProduceHandler(
					GetNewComponentService(),
					GetNewInventoryService(),
					GetNewStarshipService()
				)
			},
			{ Command.Send, new SendHandler(_orderRepository, _starshipRepository) },
			{
				Command.Verify,
				new VerifyHandler(
					GetNewComponentService(),
					GetNewInventoryService(),
					GetNewStarshipService()
				)
			},
		};
	}

	private static ComponentService GetNewComponentService()
	{
		return new ComponentService(_componentRepository);
	}

	private static InventoryService GetNewInventoryService()
	{
		return new InventoryService();
	}

	private static StarshipService GetNewStarshipService()
	{
		return new StarshipService(_starshipRepository);
	}

	private static void SetUserInterface()
	{
		_userInterface = new Menu(_handlers, _inputHandlers);
	}
}
