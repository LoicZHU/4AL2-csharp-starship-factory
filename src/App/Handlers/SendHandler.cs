using core.Repositories.OrderRepository;
using core.Repositories.StarshipRepository;
using core.UI;
using core.Utils;

namespace core.InputHandlers;

public class SendHandler : IInputHandler
{
	private const String InvalidCommandMessage = "La commande est invalide.";
	private readonly IOrderRepository _orderRepository;
	private readonly IStarshipRepository _starshipRepository;

	public SendHandler(
		IOrderRepository orderRepository,
		IStarshipRepository starshipRepository
	)
	{
		this._orderRepository = orderRepository;
		this._starshipRepository = starshipRepository;
	}

	private Boolean IsCommandInputValid(String[] input)
	{
		return input.Length != 2;
	}

	public void Handle(String input)
	{
		var splitBySpaceInput = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		if (this.IsCommandInputValid(splitBySpaceInput))
		{
			this.PrintInvalidCommand(InvalidCommandMessage);
			return;
		}

		if (!Guid.TryParse(splitBySpaceInput[1].TrimEnd(','), out var orderId))
		{
			this.PrintInvalidCommand("Commande ID incorrect.");
			return;
		}

		var order = this._orderRepository.GetOrder(orderId);
		if (UtilsFunction.IsNull(order))
		{
			this.PrintInvalidCommand("Commande inexistante.");
			return;
		}

		// remove starship from stock using starshipCounts
		foreach (var (starshipName, count) in order)
		{
			for (var i = 0; i < count; i++)
			{
				if (!this._starshipRepository.Exists(starshipName))
				{
					break;
				}

				this._starshipRepository.Remove(starshipName);
				this._orderRepository.RemoveStarshipByOrderIdAndByName(orderId, starshipName);
			}
		}

		order = this._orderRepository.GetOrder(orderId);
		if (order.All(starshipAndCount => starshipAndCount.Value == 0))
		{
			this._orderRepository.Remove(orderId);
			Console.WriteLine($"COMPLETED {orderId}");
		}
		else
		{
			Console.WriteLine($"Remaining for {orderId}:");
			// TODO
		}

		Terminal.PrintLinebreak();
	}

	// TODO
	private void PrintInvalidCommand(String message)
	{
		// SendDisplayHandler.PrintInvalidCommand(message);
		Console.WriteLine(message);
	}

	private Dictionary<String, Int32> GetStarshipSumsFromInput(String input)
	{
		var starshipCounts = new Dictionary<String, Int32>();

		foreach (var quantityAndStarship in input.Split(", "))
		{
			var (isValid, starshipName, quantity, errorMessage) =
				HandlerHelper.ParseQuantityAndStarship(quantityAndStarship);
			if (!isValid)
			{
				this.PrintInvalidCommand(errorMessage);
				return new Dictionary<String, Int32>();
			}

			if (!starshipCounts.ContainsKey(starshipName))
			{
				starshipCounts.Add(starshipName, quantity);
			}
			else
			{
				starshipCounts[starshipName] += quantity;
			}
		}

		return starshipCounts;
	}
}
