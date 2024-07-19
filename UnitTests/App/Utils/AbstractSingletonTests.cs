using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
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

	[Fact]
	public void Instance_IsThreadSafe()
	{
		// Arrange
		SingletonForTesting instance1 = null;
		SingletonForTesting instance2 = null;

		// Act
		Parallel.Invoke(
			() =>
			{
				instance1 = SingletonForTesting.Instance;
			},
			() =>
			{
				instance2 = SingletonForTesting.Instance;
			}
		);

		// Assert
		Assert.Same(instance1, instance2);
	}

	[Fact]
	public void Instance_CannotBeCreatedWithReflection()
	{
		// Arrange
		var constructor = typeof(SingletonForTesting).GetConstructor(
			BindingFlags.Instance | BindingFlags.NonPublic,
			null,
			new Type[0],
			null
		);

		// Act & Assert
		Assert.Null(constructor);
	}
}
