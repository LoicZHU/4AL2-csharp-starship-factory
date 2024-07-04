namespace core;

public sealed class Order
{
	public Guid Id { get; }
	public Dictionary<String, Int32> Orders { get; }

	private Order(Guid id, Dictionary<String, Int32> orders)
	{
		this.Id = id;
		this.Orders = orders;
	}

	public static Order Create(Dictionary<String, Int32> orders)
	{
		return new Order(Guid.NewGuid(), orders);
	}

	public void Add(String name, Int32 quantity)
	{
		Orders[name] = !Orders.ContainsKey(name) ? quantity : Orders[name] + quantity;
	}
}
