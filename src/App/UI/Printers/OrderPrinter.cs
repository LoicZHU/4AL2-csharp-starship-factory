namespace core.UI;

public static class OrderPrinter
{
	public static void PrintAddedOrderConfirmation(Guid orderId)
	{
		Printer.PrintMessageWithLinebreak($"Commande ajoutée : {orderId}");
	}
}
