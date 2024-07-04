using core.Utils;

namespace core.In_memories;

public class InMemoryOrder : AbstractSingleton<InMemoryOrder>
{
	private readonly Dictionary<Guid, Dictionary<String, Int32>> _cache = new();

	public void Add(Order order)
	{
		_cache.Add(order.Id, order.Orders);
	}

	public Dictionary<String, Int32>? GetOrder(Guid orderId)
	{
		return !this._cache.TryGetValue(orderId, out var order) ? null : order;
	}

	public Dictionary<Guid, Dictionary<String, Int32>> GetOrders()
	{
		return this._cache;
	}

	public void Remove(Guid id)
	{
		this._cache.Remove(id);
	}

	public void RemoveStarshipByOrderIdAndByName(Guid id, String starshipName)
	{
		if (!this._cache.ContainsKey(id) || !this._cache[id].ContainsKey(starshipName))
		{
			return;
		}

		var starshipCount = this._cache[id][starshipName];
		this._cache[id][starshipName] = starshipCount == 0 ? 0 : starshipCount - 1;
	}
}
