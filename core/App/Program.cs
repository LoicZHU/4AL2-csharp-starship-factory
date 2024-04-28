using core.Inventory;
using core.Inventory.Starships.Components;

namespace core;

static class Program
{
	private static InMemoryStarship _inMemoryStarship;
	private static InMemoryEngine _inMemoryEngine;
	private static InMemoryHull _inMemoryHull;
	private static InMemoryWing _inMemoryWing;
	private static InMemoryThruster _inMemoryThruster;
	private static Menu _menu;

	public static void Main(string[] args)
	{
		SetAllInMemoryStuff();
		SetAndStartMenu();
	}

	private static void SetAllInMemoryStuff()
	{
		_inMemoryStarship = new InMemoryStarship();
		_inMemoryEngine = new InMemoryEngine();
		_inMemoryHull = new InMemoryHull();
		_inMemoryWing = new InMemoryWing();
		_inMemoryThruster = new InMemoryThruster();
	}

	private static void SetAndStartMenu()
	{
		SetMenu();
		StartMenu();
	}

	private static void SetMenu()
	{
		_menu = new Menu(
			_inMemoryStarship,
			_inMemoryEngine,
			_inMemoryHull,
			_inMemoryWing,
			_inMemoryThruster
		);
	}

	private static void StartMenu()
	{
		_menu.Start();
	}
}
