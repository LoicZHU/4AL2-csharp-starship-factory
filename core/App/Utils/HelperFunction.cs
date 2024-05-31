namespace core.Utils;

public static class HelperFunction
{
	public static Boolean IsNull<TInput>(TInput input)
	{
		return input is null;
	}

	public static Boolean IsNullOrWhiteSpace(String? input)
	{
		return String.IsNullOrWhiteSpace(input);
	}
}
