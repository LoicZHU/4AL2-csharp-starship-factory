using core.Products.Starship.Components.Engine;
using core.Products.Starship.Components.Hull;

namespace core.Inventory.Starships.ComponentAssembly;

using core.Products.Starship.ComponentAssembly;

public class InMemoryComponentAssembly
{
	private readonly Dictionary<Guid, ComponentAssembly> _cache;

	public InMemoryComponentAssembly()
	{
		_cache = new()
		{
			{
				Guid.NewGuid(),
				new ComponentAssembly(
					String.Empty,
					new List<String> { HullModel.Hull_HC1, EngineModel.Engine_EC1 }
				)
			},
			{
				Guid.NewGuid(),
				new ComponentAssembly(
					"Assembly1",
					new List<String> { HullModel.Hull_HE1, EngineModel.Engine_EE1 }
				)
			},
			{
				Guid.NewGuid(),
				new ComponentAssembly(
					"Assembly2",
					new List<String> { HullModel.Hull_HS1, EngineModel.Engine_ES1 }
				)
			}
		};
	}

	public void PrintInventory()
	{
		foreach (var componentAssembly in _cache.Values)
		{
			var name = componentAssembly.Name;

			if (!name.Equals(String.Empty))
			{
				Console.WriteLine(name);
			}
			else
			{
				Console.WriteLine($"[{string.Join(", ", componentAssembly.Components)}]");
			}
		}
	}
}
