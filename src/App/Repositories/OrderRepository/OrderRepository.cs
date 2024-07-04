using core.In_memories;

namespace core.Repositories.OrderRepository;

public class OrderRepository : IOrderRepository
{
	private readonly InMemoryOrder _inMemoryOrder;

	public OrderRepository(InMemoryOrder inMemoryOrder)
	{
		_inMemoryOrder = inMemoryOrder;
	}

	public void Add(Order order)
	{
		this._inMemoryOrder.Add(order);
	}

	public Dictionary<String, Int32>? GetOrder(Guid id)
	{
		return this._inMemoryOrder.GetOrder(id);
	}

	public Dictionary<Guid, Dictionary<String, Int32>> GetOrders()
	{
		return this._inMemoryOrder.GetOrders();
	}

	public void Remove(Guid id)
	{
		this._inMemoryOrder.Remove(id);
	}

	public void RemoveStarshipByOrderIdAndByName(Guid id, String starshipName)
	{
		this._inMemoryOrder.RemoveStarshipByOrderIdAndByName(id, starshipName);
	}
}
