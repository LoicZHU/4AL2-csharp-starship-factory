using core.InputHandlers;
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

	private void HandleUserInteractionsWithTerminal()
	{
		while (true)
		{
			var input = Console.ReadLine()?.ToUpper();
			if (Utils.UtilsFunction.IsNullOrWhiteSpace(input))
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
				case Command.Produce:
					// ProduceShips ?
					break;
				case Command.Stocks:
					this.PrintStarshipAndComponentStocks();
					break;
				case Command.UserInstructions:
					this.PrintStarshipCountsForEachInstruction();
					break;
				default:
					if (this.IsInstructionsCommand(input))
					{
						this.HandleInstructionsCommand(input);
						break;
					}

					if (this.IsVerifyCommand(input))
					{
						this.HandleVerifyCommand(input);
						break;
					}

					if (this.IsUserInstructionCommand(input))
					{
						this.HandleUserInstructionCommand(input);
						break;
					}

					this.PrintUnknownInstruction(input);
					break;
			}
		}
	}

	private Boolean IsInstructionsCommand(String input)
	{
		return input.StartsWith(Command.Instructions, StringComparison.OrdinalIgnoreCase);
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

	private void HandleInstructionsCommand(String input)
	{
		InstructionsHandler.HandleInput(input);
	}

	private Boolean IsVerifyCommand(String input)
	{
		return input.StartsWith(Command.Verify, StringComparison.OrdinalIgnoreCase);
	}

	private void HandleVerifyCommand(String input)
	{
		VerifyHandler.HandleInput(input);
	}

	private Boolean IsUserInstructionCommand(String input)
	{
		return input.StartsWith(Command.UserInstruction, StringComparison.OrdinalIgnoreCase);
	}

	private void HandleUserInstructionCommand(String input)
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
