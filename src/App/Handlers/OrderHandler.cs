using core.Repositories.OrderRepository;
using core.UI;
using core.Utils;

namespace core.InputHandlers;

public class OrderHandler : IInputHandler
{
	private const String InvalidCommandMessage = "La commande est invalide.";
	private readonly IOrderRepository _orderRepository;

	public OrderHandler(IOrderRepository orderRepository)
	{
		_orderRepository = orderRepository;
	}

	public void Handle(String input)
	{
		if (!HandlerHelper.IsCommandInputValid(input.Split()))
		{
			this.PrintInvalidCommand(InvalidCommandMessage);
			return;
		}

		var splitBySpaceInput = input.Split(new[] { ' ' }, 2);
		if (!HandlerHelper.IsCommandNameSeparatedByOneSpace(splitBySpaceInput))
		{
			this.PrintInvalidCommand(InvalidCommandMessage);
			return;
		}

		var inputContent = splitBySpaceInput[1];
		var order = this.GetCompleteOrderFrom(inputContent);
		if (!UtilsFunction.IsNull(order))
		{
			this._orderRepository.Add(order);
			OrderDisplayHandler.PrintAddedOrderConfirmation(order.Id);
		}
	}

	private void PrintInvalidCommand(String message)
	{
		ListOrderDisplayHandler.PrintInvalidCommand(message);
	}

	private Order? GetCompleteOrderFrom(String starshipsPart)
	{
		var order = Order.Create(new Dictionary<String, Int32>());

		foreach (var quantityAndStarship in starshipsPart.Split(", "))
		{
			var (isValid, starshipName, quantity, errorMessage) =
				HandlerHelper.ParseQuantityAndStarship(quantityAndStarship);
			if (!isValid)
			{
				this.PrintInvalidCommand(errorMessage);
				return null;
			}

			order.Add(starshipName, quantity);
		}

		return order;
	}
}
