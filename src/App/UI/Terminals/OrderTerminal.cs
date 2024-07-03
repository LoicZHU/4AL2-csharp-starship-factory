namespace core.UI;

public static class OrderTerminal
{
	public static void PrintAddedOrderConfirmation(Guid orderId)
	{
		Terminal.PrintMessageWithLinebreak($"Commande ajoutée : {orderId}");
	}
}
