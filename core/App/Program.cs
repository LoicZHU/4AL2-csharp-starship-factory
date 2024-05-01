using core.App.UI;
using core.In_memory;
using core.In_memory.Inventory;
using core.In_memory.Inventory.Components;

namespace core.App;

static class Program
{
	private static UserInterface? _ui;
	private static Menu? _menu;
	private static InMemoryStarship? _inMemoryStarship;
	private static InMemoryEngine? _inMemoryEngine;
	private static InMemoryHull? _inMemoryHull;
	private static InMemoryWing? _inMemoryWing;
	private static InMemoryThruster? _inMemoryThruster;
	private static InMemoryComponentAssembly? _inMemoryComponentAssembly;
	private static InMemoryUserInstruction? _inMemoryUserInstruction;

	public static void Main(string[] args)
	{
		SetUpDependencies();
		if (!AreDependenciesSetUp())
		{
			Console.WriteLine("Les dépendances ne sont pas correctement configurées...");
			return;
		}

		StartMenu();
	}

	private static Boolean AreDependenciesSetUp()
	{
		return _ui is not null
			&& _menu is not null
			&& _inMemoryStarship is not null
			&& _inMemoryEngine is not null
			&& _inMemoryHull is not null
			&& _inMemoryWing is not null
			&& _inMemoryThruster is not null
			&& _inMemoryComponentAssembly is not null
			&& _inMemoryUserInstruction is not null;
	}

	private static void SetUpDependencies()
	{
		SetUserInterface();
		SetAllInMemories();
		SetMenu();
	}

	private static void SetUserInterface()
	{
		_ui = new UserInterface();
	}

	private static void SetAllInMemories()
	{
		SetComponentInMemories();
		_inMemoryComponentAssembly = new InMemoryComponentAssembly();
		_inMemoryUserInstruction = new InMemoryUserInstruction();
	}

	private static void SetComponentInMemories()
	{
		_inMemoryStarship = new InMemoryStarship();
		_inMemoryEngine = new InMemoryEngine();
		_inMemoryHull = new InMemoryHull();
		_inMemoryWing = new InMemoryWing();
		_inMemoryThruster = new InMemoryThruster();
	}

	private static void SetMenu()
	{
		if (
			_ui is null
			|| _inMemoryStarship is null
			|| _inMemoryEngine is null
			|| _inMemoryHull is null
			|| _inMemoryWing is null
			|| _inMemoryThruster is null
			|| _inMemoryComponentAssembly is null
			|| _inMemoryUserInstruction is null
		) // cannot kill the build warning when extracting this to a method (even in a variable) 🤌
		{
			Console.WriteLine(
				"Les dépendances du menu ne sont pas correctement configurées..."
			);

			return;
		}

		_menu = new Menu(
			_ui,
			_inMemoryStarship,
			_inMemoryEngine,
			_inMemoryHull,
			_inMemoryWing,
			_inMemoryThruster,
			_inMemoryComponentAssembly,
			_inMemoryUserInstruction
		);
	}

	private static void StartMenu()
	{
		_menu?.Start();
	}
}
