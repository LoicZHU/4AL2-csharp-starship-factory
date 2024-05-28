using core.UI;

namespace core.Menus;

public class MainMenu
{
	public void Start()
	{
		UserInterface.PrintWelcomeMessage();
		this.InviteUserToInteract();
	}

	private void InviteUserToInteract()
	{
		UserInterface.PrintUserInteractionInvitation();
		UserInterface.HandleUserInstruction();
	}
}
