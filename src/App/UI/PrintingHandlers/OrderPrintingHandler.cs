namespace core.UI;

public static class OrderDisplayHandler
{
	public static void PrintAddedOrderConfirmation(Guid orderId)
	{
		OrderPrinter.PrintAddedOrderConfirmation(orderId);
		TerminalHelper.PrintLineBreak();
	}
}
