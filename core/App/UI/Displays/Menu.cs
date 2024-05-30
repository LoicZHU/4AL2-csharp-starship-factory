using core.In_memories.Items;
using core.UI.constants;
using core.Utils;

namespace core.UI;

public class Menu : Singleton<Menu>
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
			var instruction = Console.ReadLine()?.ToUpper();
			switch (instruction)
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
				case Command.UserInstruction:
					// RegisterOrderMenu ?
					break;
				case Command.UserInstructions:
					// DisplayOrdersMenu ?
					break;
				case Command.Verify:
					// VerifyStocksMenu ?
					break;
				default:
					MainTerminal.PrintUnknownInstruction(
						$"🚫 Instruction inconnue : {instruction} ({Command.Help} pour de l'aide) :"
					);
					break;
			}
		}
	}

	private void PrintHelp()
	{
		MainTerminal.PrintHelp();
	}
}
