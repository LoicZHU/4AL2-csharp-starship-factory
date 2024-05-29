using core.In_memories.Items;
using core.UI.constants;
using core.Utils;

namespace core.UI;

public class MainMenu : Singleton<MainMenu>
{
	// private readonly InMemoryComponent _inMemoryComponent;
	// private readonly InMemoryStarship _inMemoryStarship;

	public MainMenu(
	// InMemoryComponent inMemoryComponent,
	// InMemoryStarship inMemoryStarship
	)
	{
		// _inMemoryComponent = inMemoryComponent;
		// _inMemoryStarship = inMemoryStarship;
	}

	public void Start()
	{
		this.PrintWelcomeMessage();
		this.PrintUserInteractionInvitation();
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
					// DisplayStocksMenu ?
					// var counts
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
