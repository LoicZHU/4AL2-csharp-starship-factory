using core.UI;
using core.Utils;

namespace core.Menus;

public class UserInterface : Singleton<UserInterface>
{
	public void Start()
	{
		Menu.Instance.Start();
	}
}
