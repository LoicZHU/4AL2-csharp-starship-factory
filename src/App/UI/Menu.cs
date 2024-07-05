using core.App.Handlers;
using core.App.UI;
using core.InputHandlers;
using core.Repositories.ComponentAssemblyRepository;
using core.Repositories.ComponentRepository;
using core.Repositories.OrderRepository;
using core.Repositories.StarshipRepository;
using core.UI.constants;
using core.Utils;

namespace core.UI;

public class Menu : IUserInterface
{
	private readonly Dictionary<String, IHandler> _handlers;
	private readonly Dictionary<String, IInputHandler> _inputHandlers;

	private readonly IComponentAssemblyRepository _componentAssemblyRepository;
	private readonly IComponentRepository _componentRepository;
	private readonly IOrderRepository _orderRepository;
	private readonly IStarshipRepository _starshipRepository;

	public Menu(
		IComponentAssemblyRepository componentAssemblyRepository,
		IComponentRepository componentRepository,
		IOrderRepository orderRepository,
		IStarshipRepository starshipRepository
	)
	{
		this._handlers = this.GetHandlers();
		this._inputHandlers = this.GetInputHandlers();

		this._componentAssemblyRepository = componentAssemblyRepository;
		this._componentRepository = componentRepository;
		this._orderRepository = orderRepository;
		this._starshipRepository = starshipRepository;
	}

	private Dictionary<String, IHandler> GetHandlers()
	{
		return new Dictionary<String, IHandler>
		{
			{ Command.Exit, new ExitHandler() },
			{ Command.Help, new HelpDisplayHandler() },
			{ Command.ListOrder, new ListOrderHandler(_orderRepository) },
			{ Command.Stocks, new StockHandler(_starshipRepository, _componentRepository) },
		};
	}

	private Dictionary<String, IInputHandler> GetInputHandlers()
	{
		return new Dictionary<String, IInputHandler>
		{
			{
				Command.Instructions,
				new InstructionsHandler(_componentAssemblyRepository, _componentRepository)
			},
			{ Command.NeededStocks, new NeededStocksHandler() },
			{ Command.Order, new OrderHandler(_orderRepository) },
			{
				Command.Produce,
				new ProduceHandler(_componentAssemblyRepository, _componentRepository)
			},
			{ Command.Send, new SendHandler(_orderRepository, _starshipRepository) },
			{ Command.Verify, new VerifyHandler(_componentRepository) },
		};
	}

	public void Start()
	{
		this.PrintStartingMessages();
		this.HandleTerminalUserInteractions();
	}

	private void PrintStartingMessages()
	{
		MenuDisplayHandler.PrintWelcome();
		MenuDisplayHandler.PrintUserInteractionInvitation();
	}

	private void HandleTerminalUserInteractions()
	{
		while (true)
		{
			var input = GetUserInput()?.ToUpper();
			if (UtilsFunction.IsNullOrWhiteSpace(input))
			{
				this.PrintEmptyInstructionMessage("🚫 Instruction vide. ('HELP' pour de l'aide)");
				continue;
			}

			var handler = this._handlers.FirstOrDefault(handler =>
				this.IsInputStartingWithCommand(input, handler.Key)
			);
			if (!UtilsFunction.IsNull(handler.Value))
			{
				this.Handle(handler.Value);

				if (IsInputEqualsToCommand(input, Command.Exit))
				{
					return;
				}
				continue;
			}

			var inputHandler = this._inputHandlers.FirstOrDefault(inputHandler =>
				this.IsInputStartingWithCommand(input, inputHandler.Key)
			);
			if (!UtilsFunction.IsNull(inputHandler.Value))
			{
				this.Handle(inputHandler.Value, input);
				continue;
			}

			this.Handle(new UnknownInstructionHandler(), input);
		}
	}

	private String? GetUserInput()
	{
		return Console.ReadLine();
	}

	private Boolean IsInputEqualsToCommand(String input, String command)
	{
		return String.Equals(input, command, StringComparison.OrdinalIgnoreCase);
	}

	private void PrintEmptyInstructionMessage(String message)
	{
		MenuDisplayHandler.PrintEmptyInstructionMessage(message);
	}

	private Boolean IsInputStartingWithCommand(String input, String command)
	{
		return input.StartsWith(command, StringComparison.OrdinalIgnoreCase);
	}

	private void Handle(IHandler handler)
	{
		handler.Handle();
	}

	private void Handle(IInputHandler inputHandler, String input)
	{
		inputHandler.Handle(input);
	}
}
