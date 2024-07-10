using core.Assemblies;
using core.Components;
using core.UI;
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
					new List<String> { HullComponent.HullHc1, EngineComponent.EngineEc1 }
				)
			);
			this.Add(
				ComponentAssembly.Create(
					"Assembly1",
					new List<String> { HullComponent.HullHe1, EngineComponent.EngineEe1 }
				)
			);
			this.Add(
				ComponentAssembly.Create(
					"Assembly2",
					new List<String> { HullComponent.HullHs1, EngineComponent.EngineEs1 }
				)
			);
		}
		catch (Exception e)
		{
			Terminal.PrintMessageWithLinebreak(e.Message);
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
}
