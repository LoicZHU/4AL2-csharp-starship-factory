namespace core;

public class Menu
{
	public void Start()
	{
		var ui = new UserInterface();

		ui.PrintWelcomeMessage();
		ui.PrintUserCommandInvitation();
	}
}
