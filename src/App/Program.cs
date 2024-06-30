using core.App.UI;
using core.UI;

namespace core;

public static class Program
{
	private static IUserInterface? _userInterface;

	public static void Main(string[] args)
	{
		ConfigureDependencies(Menu.Instance);
		StartUserInterface();
	}

	private static void ConfigureDependencies(IUserInterface userInterface)
	{
		_userInterface = userInterface;
	}

	private static void StartUserInterface()
	{
		_userInterface?.Start();
	}
}
