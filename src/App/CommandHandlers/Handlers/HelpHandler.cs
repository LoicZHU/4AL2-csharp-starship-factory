using core.App.Handlers;

namespace core.UI;

public class HelpHandler : IHandler
{
	public void Handle()
	{
		this.PrintHelpMenu();
	}

	private void PrintHelpMenu()
	{
		HelpPrinter.PrintHelpMenu();
	}
}
