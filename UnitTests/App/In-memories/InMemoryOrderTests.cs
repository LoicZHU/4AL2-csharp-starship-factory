using core;
using core.In_memories;

namespace UnitTests;

public class InMemoryOrderTests
{
	[Fact]
	public void Add_AddsOrderToCache()
	{
		// Arrange
		var repository = new InMemoryOrder();
		var order = Order.Create(new Dictionary<String, Int32> { { "Starship1", 2 } });

		// Act
		repository.Add(order);

		// Assert
		var cachedOrder = repository.GetOrder(order.Id);
		Assert.NotNull(cachedOrder);
		Assert.Equal(2, cachedOrder["Starship1"]);
	}

	[Fact]
	public void GetOrder_ReturnsCorrectOrder()
	{
		// Arrange
		var repository = new InMemoryOrder();
		var order = Order.Create(new Dictionary<String, Int32> { { "Starship1", 2 } });
		repository.Add(order);

		// Act
		var result = repository.GetOrder(order.Id);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(order.Orders, result);
	}

	[Fact]
	public void GetOrder_ReturnsNullForNonExistingOrder()
	{
		// Arrange
		var repository = new InMemoryOrder();
		var nonExistingId = Guid.NewGuid();

		// Act
		var result = repository.GetOrder(nonExistingId);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void GetOrders_ReturnsAllOrders()
	{
		// Arrange
		var repository = new InMemoryOrder();
		var order1 = Order.Create(new Dictionary<String, Int32> { { "Starship1", 2 } });
		var order2 = Order.Create(new Dictionary<String, Int32> { { "Starship2", 3 } });
		repository.Add(order1);
		repository.Add(order2);

		// Act
		var result = repository.GetOrders();

		// Assert
		Assert.Equal(2, result.Count);
		Assert.Equal(order1.Orders, result[order1.Id]);
		Assert.Equal(order2.Orders, result[order2.Id]);
	}

	[Fact]
	public void Remove_RemovesOrderFromCache()
	{
		// Arrange
		var repository = new InMemoryOrder();
		var order = Order.Create(new Dictionary<String, Int32> { { "Starship1", 2 } });
		repository.Add(order);

		// Act
		repository.Remove(order.Id);

		// Assert
		var result = repository.GetOrder(order.Id);
		Assert.Null(result);
	}

	[Fact]
	public void RemoveStarshipByOrderIdAndByName_DecrementsStarshipCount()
	{
		// Arrange
		var repository = new InMemoryOrder();
		var order = Order.Create(new Dictionary<String, Int32> { { "Starship1", 2 } });
		repository.Add(order);

		// Act
		repository.RemoveStarshipByOrderIdAndByName(order.Id, "Starship1");

		// Assert
		var result = repository.GetOrder(order.Id);
		Assert.NotNull(result);
		Assert.Equal(1, result["Starship1"]);
	}

	[Fact]
	public void RemoveStarshipByOrderIdAndByName_DoesNothingForNonExistingOrder()
	{
		// Arrange
		var repository = new InMemoryOrder();
		var nonExistingId = Guid.NewGuid();

		// Act
		repository.RemoveStarshipByOrderIdAndByName(nonExistingId, "Starship1");

		// Assert
		var result = repository.GetOrder(nonExistingId);
		Assert.Null(result);
	}

	[Fact]
	public void RemoveStarshipByOrderIdAndByName_DoesNothingForNonExistingStarship()
	{
		// Arrange
		var repository = new InMemoryOrder();
		var order = Order.Create(new Dictionary<String, Int32>());
		repository.Add(order);

		// Act
		repository.RemoveStarshipByOrderIdAndByName(order.Id, "NonExistingStarship");

		// Assert
		var result = repository.GetOrder(order.Id);
		Assert.Empty(result);
	}
}
