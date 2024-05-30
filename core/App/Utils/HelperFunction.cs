namespace core.Utils;

public static class HelperFunction
{
	public static Boolean IsNull<TInput>(TInput input)
	{
		return input is null;
	}
}
