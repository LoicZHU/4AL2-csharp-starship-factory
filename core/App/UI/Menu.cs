using core.InputHandlers;
using core.In_memories;
using core.UI.constants;
using core.Utils;

namespace core.UI;

public class Menu : AbstractSingleton<Menu>
{
	public void Start()
	{
		MenuDisplay.Start();
		this.HandleUserInteractionsWithTerminal();
	}

	public void HandleUserInteractionsWithTerminal()
	{
		while (true)
		{
			var input = Console.ReadLine()?.ToUpper();
			if (HelperFunction.IsNullOrWhiteSpace(input))
			{
				this.PrintEmptyInstructionMessage();
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
				case Command.Instructions:
					// AssembleShipsMenu ?

					break;
				case Command.Produce:
					// ProduceShipsMenu ?
					break;
				case Command.Stocks:
					this.PrintStarshipAndComponentStocks();
					break;
				case Command.UserInstructions:
					this.PrintStarshipCountsForEachInstruction();
					break;
				case Command.Verify:
					// VerifyStocksMenu ?
					break;
				default:
					if (IsUserInstructionCommand(input))
					{
						this.HandleUserInstructionCommand(input);
						break;
					}

					this.PrintUnknownInstruction(input);
					break;
			}
		}
	}

	private void PrintEmptyInstructionMessage()
	{
		MainTerminal.PrintMessage(
			"🚫 Instruction vide, veuillez réessayer. (Tapez HELP pour de l'aide)"
		);
	}

	private void PrintExitMessage()
	{
		MainTerminal.PrintMessage("👋 Merci d'avoir utilisé Capsule Corp !");
	}

	private void PrintHelpMenu()
	{
		MainTerminal.PrintHelp();
	}

	private void PrintStarshipAndComponentStocks()
	{
		StockDisplay.PrintStarshipStock();
		StockDisplay.PrintComponentStock();
	}

	private void PrintStarshipCountsForEachInstruction()
	{
		UserInstructionsDisplay.PrintStarshipCountsForEachInstruction();
	}

	private Boolean IsUserInstructionCommand(String input)
	{
		return input.StartsWith(Command.UserInstruction, StringComparison.OrdinalIgnoreCase);
	}

	private void HandleUserInstructionCommand(string input)
	{
		UserInstructionHandler.HandleInput(input);
	}

	private void PrintUnknownInstruction(String input)
	{
		MainTerminal.PrintMessage(
			$"🚫 Instruction inconnue : {input} ({Command.Help} pour de l'aide) :"
		);
	}
}
