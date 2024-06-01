using core.Assemblies;
using core.Components;
using core.Utils;

namespace core.In_memories;

public class InMemoryComponentAssembly : AbstractSingleton<InMemoryComponentAssembly>
{
	private readonly Dictionary<Guid, ComponentAssembly> _cache;

	public InMemoryComponentAssembly()
	{
		_cache = new();
		this.SetCache();
	}

	private void SetCache()
	{
		try
		{
			this.Add(
				ComponentAssembly.Create(
					String.Empty,
					new List<String> { HullComponent.Hull_HC1, EngineComponent.Engine_EC1 }
				)
			);
			this.Add(
				ComponentAssembly.Create(
					"Assembly1",
					new List<String> { HullComponent.Hull_HE1, EngineComponent.Engine_EE1 }
				)
			);
			this.Add(
				ComponentAssembly.Create(
					"Assembly2",
					new List<String> { HullComponent.Hull_HS1, EngineComponent.Engine_ES1 }
				)
			);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
		}
	}

	public void Add(ComponentAssembly componentAssembly)
	{
		_cache.Add(componentAssembly.Id, componentAssembly);
	}

	public void AddComponent(Guid id, String component)
	{
		var componentAssembly = _cache[id];

		componentAssembly.Components.Add(component);
	}

	public ComponentAssembly GetComponentAssembly(Guid id)
	{
		return _cache[id];
	}

	public void Remove(Guid id)
	{
		this._cache.Remove(id);
	}
}
