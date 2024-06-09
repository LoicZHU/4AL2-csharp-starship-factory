using core.UI;
using core.Utils;

namespace core.Menus;

public class UserInterface : AbstractSingleton<UserInterface>
{
	public void Start()
	{
		Menu.Instance.Start();
	}
}
