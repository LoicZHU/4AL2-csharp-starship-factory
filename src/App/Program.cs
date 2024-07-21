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

	private static Dictionary<String, IHandler>? _commandHandlers;
	private static Dictionary<String, IHandlerWithArgs>? _commandHandlersWithArgs;

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
		SetCommandsHandlers();
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

	private static void SetCommandsHandlers()
	{
		_commandHandlers = new Dictionary<String, IHandler>
		{
			{ Command.Exit, new ExitHandler() },
			{ Command.Help, new HelpHandler() },
			{ Command.ListOrder, new ListOrderHandler(_orderRepository) },
			{ Command.Stocks, new StockHandler(_starshipRepository, _componentRepository) },
		};

		_commandHandlersWithArgs = new Dictionary<String, IHandlerWithArgs>
		{
			{
				Command.Instructions,
				new InstructionsHandler(GetComponentService(), GetStarshipService())
			},
			{ Command.NeededStocks, new NeededStocksHandler() },
			{ Command.Order, new OrderHandler(_orderRepository) },
			{
				Command.Produce,
				new ProduceHandler(GetComponentService(), GetStarshipService())
			},
			{ Command.Send, new SendHandler(_orderRepository, _starshipRepository) },
			{ Command.Verify, new VerifyHandler(GetComponentService(), GetStarshipService()) },
		};
	}

	private static ComponentService GetComponentService()
	{
		return new ComponentService(_componentRepository);
	}

	private static StarshipService GetStarshipService()
	{
		return new StarshipService(_starshipRepository);
	}

	private static void SetUserInterface()
	{
		_userInterface = new Menu(_commandHandlers, _commandHandlersWithArgs);
	}
}
