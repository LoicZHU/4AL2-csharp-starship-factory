using core.App.UI;
using core.Inventory.Starships;
using core.Inventory.Starships.ComponentAssembly;
using core.Inventory.Starships.Components;

namespace core.App;

static class Program
{
	private static UserInterface _ui;
	private static Menu _menu;
	private static InMemoryStarship _inMemoryStarship;
	private static InMemoryEngine _inMemoryEngine;
	private static InMemoryHull _inMemoryHull;
	private static InMemoryWing _inMemoryWing;
	private static InMemoryThruster _inMemoryThruster;
	private static InMemoryComponentAssembly _inMemoryComponentAssembly;

	public static void Main(string[] args)
	{
		SetUserInterface();
		SetAllInMemoryStuff();
		SetAndStartMenu();
	}

	private static void SetUserInterface()
	{
		_ui = new UserInterface();
	}

	private static void SetAllInMemoryStuff()
	{
		_inMemoryStarship = new InMemoryStarship();
		_inMemoryEngine = new InMemoryEngine();
		_inMemoryHull = new InMemoryHull();
		_inMemoryWing = new InMemoryWing();
		_inMemoryThruster = new InMemoryThruster();
		_inMemoryComponentAssembly = new InMemoryComponentAssembly();
	}

	private static void SetAndStartMenu()
	{
		SetMenu();
		StartMenu();
	}

	private static void SetMenu()
	{
		_menu = new Menu(
			_ui,
			_inMemoryStarship,
			_inMemoryEngine,
			_inMemoryHull,
			_inMemoryWing,
			_inMemoryThruster,
			_inMemoryComponentAssembly
		);
	}

	private static void StartMenu()
	{
		_menu.Start();
	}
}
