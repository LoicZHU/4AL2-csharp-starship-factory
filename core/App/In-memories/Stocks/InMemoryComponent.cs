using core.Components;
using core.Items.Components;
using core.Utils;

namespace core.In_memories.Items;

public class InMemoryComponent : Singleton<InMemoryComponent>
{
	private readonly Dictionary<Guid, IComponent> _engineCache = new();
	private readonly Dictionary<Guid, IComponent> _hullCache = new();
	private readonly Dictionary<Guid, IComponent> _thrusterCache = new();
	private readonly Dictionary<Guid, IComponent> _wingCache = new();

	public InMemoryComponent()
	{
		SetCaches();
	}

	private void SetCaches()
	{
		this.SetEngineCache();
		this.SetHullCache();
		this.SetThrusterCache();
		this.SetWingCache();
	}

	#region set caches
	private void SetEngineCache()
	{
		for (var i = 1; i <= 3; i++)
		{
			this.Add(Engine.Create(EngineComponent.Engine_EE1));
			this.Add(Engine.Create(EngineComponent.Engine_EC1));
			this.Add(Engine.Create(EngineComponent.Engine_ES1));
		}
	}

	private void SetHullCache()
	{
		for (var i = 1; i <= 3; i++)
		{
			this.Add(Hull.Create(HullComponent.Hull_HC1));
			this.Add(Hull.Create(HullComponent.Hull_HE1));
			this.Add(Hull.Create(HullComponent.Hull_HS1));
		}
	}

	private void SetThrusterCache()
	{
		for (var i = 1; i <= 4; i++)
		{
			this.Add(Thruster.Create(ThrusterComponent.Thruster_TC1));
			this.Add(Thruster.Create(ThrusterComponent.Thruster_TE1));
			this.Add(Thruster.Create(ThrusterComponent.Thruster_TS1));
		}
	}

	private void SetWingCache()
	{
		for (var i = 1; i <= 3; i++)
		{
			this.Add(Wing.Create(WingComponent.Wings_WC1));
			this.Add(Wing.Create(WingComponent.Wings_WE1));
			this.Add(Wing.Create(WingComponent.Wings_WS1));
		}
	}
	#endregion set caches

	public void Add<T>(T item)
	{
		switch (item)
		{
			case Engine engine:
				_engineCache.Add(engine.Id, engine);
				break;
			case Hull hull:
				_hullCache.Add(hull.Id, hull);
				break;
			case Thruster thruster:
				_thrusterCache.Add(thruster.Id, thruster);
				break;
			case Wing wing:
				_wingCache.Add(wing.Id, wing);
				break;
			default:
				throw new ArgumentException(
					$"Invalid component: with type {typeof(T).Name} | with name {nameof(item)}"
				);
		}
	}

	public void Remove(String name)
	{
		if (String.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentException(
				$"Component name cannot be null or empty: {nameof(name)}"
			);
		}

		var cacheMap = new Dictionary<string, Dictionary<Guid, IComponent>>
		{
			{ "Engine", _engineCache },
			{ "Hull", _hullCache },
			{ "Thruster", _thrusterCache },
			{ "Wing", _wingCache }
		};

		foreach (var prefix in cacheMap.Keys)
		{
			if (!name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
			{
				continue;
			}

			var actualCache = cacheMap[prefix];
			var component = actualCache.Values.FirstOrDefault(component =>
				component.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
			);

			if (component is not null)
			{
				actualCache.Remove(component.Id);
			}
		}

		throw new ArgumentException("Invalid component name", nameof(name));
	}

	public List<Dictionary<String, Int32>> GetAllCounts()
	{
		var cacheMap = new Dictionary<string, Dictionary<Guid, IComponent>>
		{
			{ "Engine", _engineCache },
			{ "Hull", _hullCache },
			{ "Thruster", _thrusterCache },
			{ "Wing", _wingCache }
		};

		var result = new List<Dictionary<String, Int32>>();
		foreach (var cache in cacheMap)
		{
			var cacheName = cache.Key;
			var cacheValues = cache.Value.Values;
			var cacheCount = cacheValues.Count();

			var cacheResult = new Dictionary<String, Int32> { { cacheName, cacheCount } };

			result.Add(cacheResult);
		}

		return result;
	}

	public Int32 CountByName(String name)
	{
		if (String.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentException(
				$"Component name cannot be null or empty: {nameof(name)}"
			);
		}

		var cacheMap = new Dictionary<string, Dictionary<Guid, IComponent>>
		{
			{ "Engine", _engineCache },
			{ "Hull", _hullCache },
			{ "Thruster", _thrusterCache },
			{ "Wing", _wingCache }
		};

		foreach (var prefix in cacheMap.Keys)
		{
			if (!name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
			{
				continue;
			}

			var actualCache = cacheMap[prefix];
			var component = actualCache.Values.FirstOrDefault(cache => cache.Name == name);
			if (component is not null)
			{
				return actualCache.Values.Count(component =>
					component.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
				);
			}
		}

		throw new ArgumentException("Invalid name", nameof(name));
	}
}
