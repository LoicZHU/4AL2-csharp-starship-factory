using core.UI;

namespace core.App.Handlers;

public class ExitHandler : IHandler
{
	public void Handle()
	{
		this.PrintExitMessage("👋 Merci d'avoir utilisé Capsule Corp !");
	}

	private void PrintExitMessage(String message)
	{
		ExitPrintingHandler.PrintExitMessage(message);
	}
}
