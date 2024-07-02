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

	public void SendStarshipOut(Guid id, Dictionary<String, Int32> starshipCounts)
	{
		if (UtilsFunction.IsNull(this.GetOrder(id)))
		{
			return;
		}

		foreach (var (starshipName, quantity) in starshipCounts)
		{
			if (this._cache[id].ContainsKey(starshipName))
			{
				this._cache[id][starshipName] -= quantity;
			}
		}
	}
}
