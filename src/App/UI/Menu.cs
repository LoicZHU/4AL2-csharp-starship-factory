using core.App.UI;
using core.InputHandlers;
using core.Repositories.ComponentAssemblyRepository;
using core.Repositories.ComponentRepository;
using core.Repositories.StarshipRepository;
using core.UI.constants;
using core.Utils;

namespace core.UI;

public class Menu : IUserInterface
{
	private readonly IComponentAssemblyRepository _componentAssemblyRepository;
	private readonly IComponentRepository _componentRepository;
	private readonly IStarshipRepository _starshipRepository;

	public Menu(
		IComponentAssemblyRepository componentAssemblyRepository,
		IComponentRepository componentRepository,
		IStarshipRepository starshipRepository
	)
	{
		this._componentAssemblyRepository = componentAssemblyRepository;
		this._componentRepository = componentRepository;
		this._starshipRepository = starshipRepository;
	}

	public void Start()
	{
		MenuDisplayHandler.Start();
		this.HandleUserInteractionsWithTerminal();
	}

	private void HandleUserInteractionsWithTerminal()
	{
		var inputHandlers = new Dictionary<String, IInputHandler>
		{
			{
				Command.Instructions,
				new InstructionsHandler(_componentAssemblyRepository, _componentRepository)
			},
			{ Command.NeededStocks, new NeededStocksHandler() },
			{
				Command.Produce,
				new ProduceHandler(_componentAssemblyRepository, _componentRepository)
			},
			{ Command.Verify, new VerifyHandler(_componentRepository) },
		};

		while (true)
		{
			var input = Console.ReadLine()?.ToUpper();
			if (UtilsFunction.IsNullOrWhiteSpace(input))
			{
				this.PrintEmptyInstructionMessage();
				continue;
			}

			var inputHandler = inputHandlers.FirstOrDefault(inputHandler =>
				this.IsInputStartingWithCommand(input, inputHandler.Key)
			);
			if (!UtilsFunction.IsNull(inputHandler.Value))
			{
				this.HandleInput(inputHandler.Value, input);
				continue;
			}

			switch (input)
			{
				case Command.Exit:
					this.PrintExitMessage();
					return;
				case Command.Help:
					this.PrintHelpMenu();
					break;
				case Command.Stocks:
					this.PrintStarshipAndComponentStocks();
					break;
				default:
					this.PrintUnknownInstruction(input);
					break;
			}
		}
	}

	private Boolean IsInputStartingWithCommand(String input, String command)
	{
		return input.StartsWith(command, StringComparison.OrdinalIgnoreCase);
	}

	private void PrintEmptyInstructionMessage()
	{
		Terminal.PrintMessageWithLinebreak(
			"🚫 Instruction vide, veuillez réessayer. (Tapez HELP pour de l'aide)"
		);
	}

	private void PrintExitMessage()
	{
		Terminal.PrintMessageWithLinebreak("👋 Merci d'avoir utilisé Capsule Corp !");
	}

	private void PrintHelpMenu()
	{
		MenuDisplayHandler.PrintAvailableCommandsMessage("Commandes disponibles :");
		MenuDisplayHandler.PrintHelp();
	}

	private void PrintStarshipAndComponentStocks()
	{
		var starshipCounts = this._starshipRepository.GetStock();
		StockDisplayHandler.PrintStarshipStock(starshipCounts);

		var componentCounts = this._componentRepository.GetStockOfEachComponent();
		StockDisplayHandler.PrintComponentStock(componentCounts);
	}

	private void HandleInput(IInputHandler inputHandler, String input)
	{
		inputHandler.Handle(input);
	}

	private void PrintUnknownInstruction(String input)
	{
		Terminal.PrintMessageWithLinebreak(
			$"🚫 Instruction inconnue : {input} ({Command.Help} pour de l'aide) :"
		);
	}
}
