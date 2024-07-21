using System.Text;
using core.Repositories.OrderRepository;
using core.Repositories.StarshipRepository;
using core.UI;
using core.Utils;

namespace core.InputHandlers;

public class SendHandler : IHandlerWithArgs
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
			this.PrintInvalidCommand("Commande ID incorrecte.");
			return;
		}

		try
		{
			var order = this._orderRepository.GetOrder(orderId);
			if (UtilsFunction.IsNull(order))
			{
				this.PrintInvalidCommand("Commande inexistante.");
				return;
			}

			this.RemoveStarshipsFromStock(order, orderId);

			order = this._orderRepository.GetOrder(orderId);
			if (this.IsOrderCompleted(order))
			{
				this.RemoveOrderAndPrintCompleted(orderId);
				return;
			}

			this.GetOrderRemainingStarshipsAndPrintIt(order, orderId);
		}
		catch (Exception e)
		{
			Terminal.PrintMessageWithLinebreak(e.Message);
		}
	}

	private Boolean IsOrderCompleted(Dictionary<String, Int32> order)
	{
		return order.All(starshipAndCount => starshipAndCount.Value == 0);
	}

	private void RemoveOrderAndPrintCompleted(Guid orderId)
	{
		this._orderRepository.Remove(orderId);
		SendDisplayHandler.PrintCompletedMessage(orderId.ToString());
	}

	private Boolean IsCommandInputValid(String[] input)
	{
		return input.Length != 2;
	}

	private void PrintInvalidCommand(String message)
	{
		SendDisplayHandler.PrintInvalidCommand(message);
	}

	private void RemoveStarshipsFromStock(Dictionary<String, Int32>? order, Guid orderId)
	{
		foreach (var (starshipName, count) in order)
		{
			for (var i = 0; i < count; i++)
			{
				if (!this._starshipRepository.Exists(starshipName))
				{
					break;
				}

				try
				{
					this._starshipRepository.Remove(starshipName);
					this._orderRepository.RemoveStarshipByOrderIdAndByName(orderId, starshipName);
				}
				catch (Exception e)
				{
					Terminal.PrintMessageWithLinebreak(e.Message);
				}
			}
		}
	}

	private void GetOrderRemainingStarshipsAndPrintIt(
		Dictionary<String, Int32> order,
		Guid orderId
	)
	{
		var message = this.GetOrderRemainingStarshipsMessage(order, orderId);
		SendDisplayHandler.PrintOrderRemainingStarships(message);
	}

	private String GetOrderRemainingStarshipsMessage(
		Dictionary<String, Int32> order,
		Guid orderId
	)
	{
		var stringBuilder = new StringBuilder();

		foreach (var (starshipName, count) in order)
		{
			if (UtilsFunction.IsEqualToZero(count))
			{
				continue;
			}

			if (!UtilsFunction.IsStringBuilderEmpty(stringBuilder))
			{
				stringBuilder.Append(", ");
			}

			stringBuilder.Append($"{starshipName} {count}");
		}

		return $"Remaining for {orderId}: {stringBuilder}";
	}
}
