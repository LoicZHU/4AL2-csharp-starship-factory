using core.App.UI;
using core.InputHandlers;
using core.UI.constants;
using core.Utils;

namespace core.UI;

public class Menu : AbstractSingleton<Menu>, IUserInterface
{
	public void Start()
	{
		MenuDisplayHandler.Start();
		this.HandleUserInteractionsWithTerminal();
	}

	private void HandleUserInteractionsWithTerminal()
	{
		var inputHandlers = new Dictionary<String, IInputHandler>
		{
			{ Command.Instructions, new InstructionsHandler() },
			{ Command.NeededStocks, new NeededStocksHandler() },
			{ Command.Produce, new ProduceHandler() },
			{ Command.Verify, new VerifyHandler() },
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
				this.HandleCommand(inputHandler.Value, input);
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
		StockDisplayHandler.PrintStarshipStock();
		StockDisplayHandler.PrintComponentStock();
	}

	private void PrintStarshipCountsForEachInstruction()
	{
		UserInstructionsDisplayHandler.PrintStarshipCountsForEachInstruction();
	}

	private void HandleCommand(IInputHandler inputHandler, String input)
	{
		inputHandler.HandleInput(input);
	}

	private void PrintUnknownInstruction(String input)
	{
		Terminal.PrintMessageWithLinebreak(
			$"🚫 Instruction inconnue : {input} ({Command.Help} pour de l'aide) :"
		);
	}
}
