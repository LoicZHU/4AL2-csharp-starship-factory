namespace core.Utils;

public static class UtilsFunction
{
	public static Boolean IsDictionaryEmpty<TKey, TValue>(
		Dictionary<TKey, TValue> dictionary
	)
	{
		return dictionary.Count == 0;
	}

	public static Boolean IsNull<TInput>(TInput input)
	{
		return input is null;
	}

	public static Boolean IsNullOrWhiteSpace(String? input)
	{
		return String.IsNullOrWhiteSpace(input);
	}
}
