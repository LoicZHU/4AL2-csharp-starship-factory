using core.UI.constants;

namespace core.UI;

public static class MenuDisplayHandler
{
	public static void Start()
	{
		PrintStartingMessages();
	}

	private static void PrintStartingMessages()
	{
		MenuTerminal.PrintWelcome("Bienvenue chez Capsule Corp ! 🚀");
		MenuTerminal.PrintUserInteractionInvitation(
			$"🕹 Entrez une instruction ({Command.Help} pour de l'aide) :"
		);
	}

	public static void PrintAvailableCommandsMessage(String message)
	{
		Terminal.PrintLinebreak();
		Terminal.PrintMessageWithLinebreak(message);
	}

	public static void PrintHelp()
	{
		MenuTerminal.PrintHelp();
		Terminal.PrintLinebreak();
	}
}
