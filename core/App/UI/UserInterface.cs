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

	public static void PrintHelp()
	{
		Terminal.PrintHelp();
	}
}
