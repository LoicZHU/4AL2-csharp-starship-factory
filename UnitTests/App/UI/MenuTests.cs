using core.UI;

namespace UnitTests.App.UI;

public class MenuTests
{
	[Fact]
	public void Menu_Should_Be_Singleton()
	{
		// Arrange & Act
		var instance1 = Menu.Instance;
		var instance2 = Menu.Instance;

		// Assert
		Assert.Same(instance1, instance2);
	}
}
