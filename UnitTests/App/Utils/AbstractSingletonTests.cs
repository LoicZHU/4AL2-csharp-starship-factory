using core.Utils;

namespace UnitTests.App.Utils;

public class SingletonForTesting : AbstractSingleton<SingletonForTesting>
{
	// If needed, add properties & methods.
}

public class AbstractSingletonTests
{
	[Fact]
	public void Instance_WhenCalledMultipleTimes_ReturnsSameInstance()
	{
		// Act
		var instance1 = SingletonForTesting.Instance;
		var instance2 = SingletonForTesting.Instance;

		// Assert
		Assert.Same(instance1, instance2);
	}

	[Fact]
	public void Instance_WhenCalled_IsNotNull()
	{
		// Act
		var instance = SingletonForTesting.Instance;

		// Assert
		Assert.NotNull(instance);
	}
}
