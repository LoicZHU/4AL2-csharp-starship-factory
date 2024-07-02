using core.Repositories.OrderRepository;
using core.UI;
using core.Utils;

namespace core.InputHandlers;

public class SendHandler : IInputHandler
{
	private const String InvalidCommandMessage = "La commande est invalide.";
	private readonly IOrderRepository _orderRepository;

	public SendHandler(IOrderRepository orderRepository)
	{
		_orderRepository = orderRepository;
	}

	public void Handle(String input)
	{
		// if (!HandlerHelper.IsCommandInputValid(input.Split()))
		// {
		// 	this.PrintInvalidCommand(InvalidCommandMessage);
		// 	return;
		// }

		// var splitBySpaceInput = input.Split(new[] { ' ' }, 2);
		// if (!HandlerHelper.IsCommandNameSeparatedByOneSpace(splitBySpaceInput))
		// {
		// 	this.PrintInvalidCommand(InvalidCommandMessage);
		// 	return;
		// }

		var splitBySpaceInput = input.Split(
			new[] { ' ' },
			StringSplitOptions.RemoveEmptyEntries
		);
		var orderId = !Guid.TryParse(splitBySpaceInput[1].TrimEnd(','), out var guid)
			? Guid.Empty
			: guid;
		var order = this._orderRepository.GetOrder(orderId);
		if (UtilsFunction.IsNull(order))
		{
			Console.WriteLine($"Order {orderId} not found.");
			return;
		}

		var splitByCommaInput = input.Split(new[] { ',' }, 2);
		var inputContent = splitByCommaInput[1].Trim();

		// TODO: check if inputContent is empty then remove this if statement
		if (String.IsNullOrEmpty(inputContent))
		{
			this.PrintInvalidCommand(InvalidCommandMessage);
			return;
		}

		var starshipCounts = this.GetStarshipSumsFromInput(inputContent);
		if (HandlerHelper.IsDictionaryEmpty(starshipCounts))
		{
			return;
		}

		this._orderRepository.SendStarshipsOut(orderId, starshipCounts);

		Console.Write($"Remaining for {orderId}: ");

		for (var i = 0; i < starshipCounts.Count; i++)
		{
			var count = starshipCounts.Values.ElementAt(i);
			var starshipName = starshipCounts.Keys.ElementAt(i);
			var message = $"{count} {starshipName}";

			Terminal.PrintMessageWithLinebreak($"{message}");
		}

		Console.WriteLine($"COMPLETED {orderId}");
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
