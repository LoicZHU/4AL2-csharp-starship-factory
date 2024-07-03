namespace core.Repositories.OrderRepository;

public interface IOrderRepository
{
	public void Add(Order order);
	public Dictionary<String, Int32>? GetOrder(Guid id);
	public Dictionary<Guid, Dictionary<String, Int32>> GetOrders();
	public void Remove(Guid id);
	public void RemoveStarshipByOrderIdAndByName(Guid id, String starshipName);
}
