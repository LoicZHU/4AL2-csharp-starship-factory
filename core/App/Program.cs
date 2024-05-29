using core.In_memories.Items;
using core.Menus;
using core.UI;

namespace core;

static class Program
{
	private static MainMenu? _mainMenu;
	private static InMemoryComponent? _inMemoryComponent;
	private static InMemoryStarship? _inMemoryStarship;

	public static void Main(string[] args)
	{
		SetUp();
		StartMainMenu();
	}

	private static void SetUp()
	{
		// _mainMenu = new MainMenu();
		// _inMemoryComponent = new InMemoryComponent();
		// _inMemoryStarship = new InMemoryStarship();
		new MainMenu();
	}

	private static void StartMainMenu()
	{
		// _mainMenu?.Start();
		var m = MainMenu.Instance;
		Console.WriteLine("m:");
		m.Start();
	}
}
