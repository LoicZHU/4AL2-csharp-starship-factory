using core.Assemblies;

namespace UnitTests.App.Entities.Stocks;

public class ComponentAssemblyTests
{
	[Fact]
	public void Create_ShouldReturnComponentAssemblyWithValidIdNameAndComponents()
	{
		// Arrange
		var name = "ComponentAssembly1";
		var components = new List<String> { "Component1", "Component2" };

		// Act
		var componentAssembly = ComponentAssembly.Create(name, components);

		// Assert
		Assert.NotNull(componentAssembly);
		Assert.NotEqual(Guid.Empty, componentAssembly.Id);
		Assert.Equal(name, componentAssembly.Name);
		Assert.Equal(components, componentAssembly.Components);
	}
}
