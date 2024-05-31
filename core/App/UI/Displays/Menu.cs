using core.InputHandlers;
using core.In_memories;
using core.UI.constants;
using core.Utils;

namespace core.UI;

public class Menu : AbstractSingleton<Menu>
{
	public void Start()
	{
		this.PrintWelcomeMessage();
		this.PrintUserInteractionInvitation();

		this.HandleUserInstruction();
	}

	public void PrintWelcomeMessage()
	{
		MainTerminal.PrintWelcomeMessage("Bienvenue chez Capsule Corp ! 🚀");
	}

	public void PrintUserInteractionInvitation()
	{
		MainTerminal.PrintInvitationToUserInteraction(
			$"🕹 Entrez une instruction ({Command.Help} pour de l'aide) :"
		);
	}

	public void HandleUserInstruction()
	{
		while (true)
		{
			var input = Console.ReadLine()?.ToUpper();
			if (String.IsNullOrWhiteSpace(input))
			{
				MainTerminal.PrintUnknownInstruction(
					"🚫 Instruction vide, veuillez réessayer. (Tapez HELP pour de l'aide)"
				);
				continue;
			}

			switch (input)
			{
				case Command.Exit:
					MainTerminal.PrintGoodbyeMessage("👋 Merci d'avoir utilisé Capsule Corp !");
					return;
				case Command.Help:
					this.PrintHelp();
					break;
				case Command.Instructions:
					// AssembleShipsMenu ?
					break;
				case Command.Produce:
					// ProduceShipsMenu ?
					break;
				case Command.Stocks:
					StockDisplay.Instance.PrintStarshipStock();
					StockDisplay.Instance.PrintComponentStock();
					break;
				case Command.UserInstructions:
					// DisplayOrdersMenu ?
					var userInstructions = InMemoryUserInstruction.Instance.GetUserInstructions();
					foreach (var (guidKey, instructions) in userInstructions)
					{
						MainTerminal.PrintMessage($"Commande n°{guidKey}");
						foreach (var (starship, count) in instructions)
						{
							MainTerminal.PrintMessage($"{starship} : {count}");
						}

						TerminalHelper.PrintLineBreak();
					}
					break;
				case Command.Verify:
					// VerifyStocksMenu ?
					break;
				default:
					if (IsUserInstructionCommand(input))
					{
						UserInstructionHandler.HandleInput(input);
						break;
					}

					MainTerminal.PrintUnknownInstruction(
						$"🚫 Instruction inconnue : {input} ({Command.Help} pour de l'aide) :"
					);
					break;
			}
		}
	}

	private void PrintHelp()
	{
		MainTerminal.PrintHelp();
	}

	private Boolean IsUserInstructionCommand(String input)
	{
		return input.StartsWith(Command.UserInstruction, StringComparison.OrdinalIgnoreCase);
	}
}
