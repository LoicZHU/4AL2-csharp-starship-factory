namespace core.UI;

public static class OrderDisplayHandler
{
	public static void PrintAddedOrderConfirmation(Guid orderId)
	{
		OrderTerminal.PrintAddedOrderConfirmation(orderId);
		TerminalHelper.PrintLineBreak();
	}
}
