using core.App.UI;
using core.In_memories;
using core.In_memories.Items;
using core.Repositories.ComponentAssemblyRepository;
using core.Repositories.ComponentRepository;
using core.Repositories.OrderRepository;
using core.Repositories.StarshipRepository;
using core.UI;

namespace core;

public static class Program
{
	private static IUserInterface? _userInterface;

	public static void Main(string[] args)
	{
		ConfigureDependencies();
		StartUserInterface();
	}

	private static void ConfigureDependencies()
	{
		IComponentAssemblyRepository componentAssemblyRepository =
			new ComponentAssemblyRepository(new InMemoryComponentAssembly());
		IComponentRepository componentRepository = new ComponentRepository(
			new InMemoryComponent()
		);
		IOrderRepository orderRepository = new OrderRepository(new InMemoryOrder());
		IStarshipRepository starshipRepository = new StarshipRepository(
			new InMemoryStarship()
		);

		_userInterface = new Menu(
			componentAssemblyRepository,
			componentRepository,
			orderRepository,
			starshipRepository
		);
	}

	private static void StartUserInterface()
	{
		_userInterface?.Start();
	}
}
