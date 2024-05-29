namespace core.Utils;

public abstract class Singleton<TClass>
	where TClass : new()
{
	private static TClass _instance;
	private static object _lock = new();

	public static TClass Instance
	{
		get
		{
			lock (_lock)
			{
				if (_instance is null)
				{
					_instance = new TClass();
				}

				return _instance;
			}
		}
	}
}
