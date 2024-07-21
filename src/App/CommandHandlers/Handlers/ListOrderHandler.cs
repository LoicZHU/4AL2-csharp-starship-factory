using System.Text;
using core.Repositories.OrderRepository;
using core.UI;
using core.Utils;

namespace core.App.Handlers;

public class ListOrderHandler : IHandler
{
	private readonly IOrderRepository _orderRepository;

	public ListOrderHandler(IOrderRepository orderRepository)
	{
		this._orderRepository = orderRepository;
	}

	public void Handle()
	{
		try
		{
			var orders = this._orderRepository.GetOrders();
			if (UtilsFunction.IsDictionaryEmpty(orders))
			{
				ListOrderPrintingHandler.PrintNoOrders("Il n'y a pas de commande en cours.");
				return;
			}

			var listOrderMessage = this.GetListOrderMessage(orders);
			ListOrderPrintingHandler.PrintListOrder(listOrderMessage);
		}
		catch (Exception e)
		{
			Printer.PrintMessageWithLinebreak(e.Message);
		}
	}

	private String GetListOrderMessage(Dictionary<Guid, Dictionary<String, Int32>> orders)
	{
		var finalStringBuilder = new StringBuilder();

		foreach (var (guidKey, arguments) in orders)
		{
			finalStringBuilder.Append($"Commande {guidKey} : ");

			var starshipStringBuilder = new StringBuilder();
			foreach (var (starshipName, count) in arguments)
			{
				if (!UtilsFunction.IsStringBuilderEmpty(starshipStringBuilder))
				{
					starshipStringBuilder.Append(", ");
				}

				starshipStringBuilder.Append($"{count} {starshipName}");
			}

			finalStringBuilder.Append(starshipStringBuilder.AppendLine().ToString());
		}

		return finalStringBuilder.ToString();
	}
}
