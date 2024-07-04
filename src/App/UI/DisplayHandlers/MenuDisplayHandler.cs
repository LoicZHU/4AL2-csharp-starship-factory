using core.UI.constants;

namespace core.UI;

public static class MenuDisplayHandler
{
	public static void PrintWelcome()
	{
		Terminal.PrintLinebreak();
		MenuTerminal.PrintWelcome("🚀 Bienvenue chez Capsule Corp ! 🚀");
		Terminal.PrintLinebreak();
		Terminal.PrintLinebreak();
	}

	public static void PrintUserInteractionInvitation()
	{
		MenuTerminal.PrintUserInteractionInvitation(
			$"🕹 Entrez une instruction ({Command.Help} pour de l'aide) :"
		);
		Terminal.PrintLinebreak();
	}

	public static void PrintEmptyInstructionMessage(String message)
	{
		MenuTerminal.PrintEmptyInstructionMessage(message);
		Terminal.PrintLinebreak();
	}
}
