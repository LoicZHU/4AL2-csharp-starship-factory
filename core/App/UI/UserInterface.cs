using core.UI.constants;

namespace core.UI;

public static class UserInterface
{
	public static void PrintWelcomeMessage()
	{
		Terminal.PrintWelcomeMessage("Bienvenue chez Capsule Corp ! 🚀");
	}

	public static void PrintUserInteractionInvitation()
	{
		Terminal.PrintInvitationToUserInteraction(
			$"🕹 Entrez une instruction ({Command.Help} pour de l'aide) :"
		);
	}

	public static void HandleUserInstruction()
	{
		while (true)
		{
			var instruction = Console.ReadLine()?.ToUpper();
			switch (instruction)
			{
				case Command.Exit:
					Terminal.PrintGoodbyeMessage("👋 Merci d'avoir utilisé Capsule Corp !");
					return;
				case Command.Help:
					PrintHelp();
					break;
				case Command.Instructions:
					// AssembleShipsMenu ?
					break;
				case Command.Produce:
					// ProduceShipsMenu ?
					break;
				case Command.Stocks:
					// DisplayStocksMenu ?
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
					Terminal.PrintUnknownInstruction(
						$"🚫 Instruction inconnue : {instruction} ({Command.Help} pour de l'aide) :"
					);
					break;
			}
		}
	}

	private static void PrintHelp()
	{
		Terminal.PrintHelp();
	}
}
