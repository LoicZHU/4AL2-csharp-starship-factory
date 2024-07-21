using core.Components;
using core.Items.Components;
using core.Repositories.ComponentRepository;
using core.UI;
using core.Utils;

namespace core.In_memories.Items;

public class InMemoryComponent : IComponentRepository
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
		try
		{
			for (var i = 1; i <= 3; i++)
			{
				this.Add(Engine.Create(EngineComponent.EngineEe1));
				this.Add(Engine.Create(EngineComponent.EngineEc1));
				this.Add(Engine.Create(EngineComponent.EngineEs1));
			}
		}
		catch (Exception e)
		{
			Printer.PrintMessageWithLinebreak(e.Message);
		}
	}

	private void SetHullCache()
	{
		try
		{
			for (var i = 1; i <= 3; i++)
			{
				this.Add(Hull.Create(HullComponent.HullHc1));
				this.Add(Hull.Create(HullComponent.HullHe1));
				this.Add(Hull.Create(HullComponent.HullHs1));
			}
		}
		catch (Exception e)
		{
			Printer.PrintMessageWithLinebreak(e.Message);
		}
	}

	private void SetThrusterCache()
	{
		try
		{
			for (var i = 1; i <= 6; i++)
			{
				this.Add(Thruster.Create(ThrusterComponent.ThrusterTc1));
				this.Add(Thruster.Create(ThrusterComponent.ThrusterTe1));
				this.Add(Thruster.Create(ThrusterComponent.ThrusterTs1));
			}
		}
		catch (Exception e)
		{
			Printer.PrintMessageWithLinebreak(e.Message);
		}
	}

	private void SetWingCache()
	{
		try
		{
			for (var i = 1; i <= 6; i++)
			{
				this.Add(Wing.Create(WingComponent.WingWc1));
				this.Add(Wing.Create(WingComponent.WingWe1));
				this.Add(Wing.Create(WingComponent.WingWs1));
			}
		}
		catch (Exception e)
		{
			Printer.PrintMessageWithLinebreak(e.Message);
		}
	}
	#endregion set caches

	private void Add<TItem>(TItem item)
	{
		switch (item)
		{
			case Engine engine:
				if (_engineCache.ContainsKey(engine.Id))
				{
					throw new ArgumentException($"Moteur ID {engine.Id} déjà existant");
				}

				_engineCache.Add(engine.Id, engine);
				break;
			case Hull hull:
				if (_hullCache.ContainsKey(hull.Id))
				{
					throw new ArgumentException($"Coque ID {hull.Id} déjà existante");
				}

				_hullCache.Add(hull.Id, hull);
				break;
			case Thruster thruster:
				if (_thrusterCache.ContainsKey(thruster.Id))
				{
					throw new ArgumentException($"Propulseur ID {thruster.Id} déjà existant");
				}

				_thrusterCache.Add(thruster.Id, thruster);
				break;
			case Wing wing:
				if (_wingCache.ContainsKey(wing.Id))
				{
					throw new ArgumentException($"Aile ID {wing.Id} déjà existante");
				}

				_wingCache.Add(wing.Id, wing);
				break;
			default:
				throw new ArgumentException(
					$"Composant invalide : avec type {typeof(TItem).Name} | avec nom {nameof(item)}"
				);
		}
	}

	public void Remove(String name)
	{
		if (String.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentException(
				$"Composant ne peut pas être null ou vide: {nameof(name)}"
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

			if (!UtilsFunction.IsNull(component))
			{
				actualCache.Remove(component.Id);
			}
		}
	}

	public List<Dictionary<String, Int32>> GetStockOfEachComponent()
	{
		var cacheMap = new Dictionary<string, Dictionary<Guid, IComponent>>
		{
			{ "Engine", _engineCache },
			{ "Hull", _hullCache },
			{ "Thruster", _thrusterCache },
			{ "Wing", _wingCache }
		};

		var counts = new List<Dictionary<String, Int32>>();
		foreach (var prefix in cacheMap.Keys)
		{
			var count = new Dictionary<String, Int32>();
			foreach (var component in cacheMap[prefix].Values)
			{
				count[component.Name] = !count.ContainsKey(component.Name)
					? 1
					: count[component.Name] + 1;
			}

			counts.Add(count);
		}

		return counts;
	}

	public Int32 GetCountByName(String name)
	{
		if (String.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentException(
				$"Composant ne peut pas être null ou vide : {nameof(name)}"
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
			var component = actualCache.Values.FirstOrDefault(cache =>
				cache.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
			);

			if (!UtilsFunction.IsNull(component))
			{
				return actualCache.Values.Count(component =>
					component.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
				);
			}
		}

		return 0;
	}
}
