using core.App.Handlers;

namespace core.UI;

public class HelpDisplayHandler : IHandler
{
	public void Handle()
	{
		this.PrintHelpMenu();
	}

	private void PrintHelpMenu()
	{
		HelpTerminal.PrintHelpMenu();
	}
}
