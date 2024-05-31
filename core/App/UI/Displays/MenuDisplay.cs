using core.UI.constants;

namespace core.UI;

public static class MenuDisplay
{
	public static void Start()
	{
		PrintWelcomeMessage();
		PrintUserInteractionInvitation();
	}

	private static void PrintWelcomeMessage()
	{
		MainTerminal.PrintWelcomeMessage("Bienvenue chez Capsule Corp ! 🚀");
	}

	private static void PrintUserInteractionInvitation()
	{
		MainTerminal.PrintMessage(
			$"🕹 Entrez une instruction ({Command.Help} pour de l'aide) :"
		);
	}
}
