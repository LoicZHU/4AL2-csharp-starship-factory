using core.Assemblies;
using core.Repositories.ComponentAssemblyRepository;
using core.Services;
using NSubstitute;

namespace UnitTests.App.Services;

public class ComponentAssemblyServiceTests
{
	private readonly IComponentAssemblyRepository _componentAssemblyRepository;
	private readonly ComponentAssemblyService _componentAssemblyService;

	public ComponentAssemblyServiceTests()
	{
		_componentAssemblyRepository = Substitute.For<IComponentAssemblyRepository>();
		_componentAssemblyService = new ComponentAssemblyService(
			_componentAssemblyRepository
		);
	}

	[Fact]
	public void Add_CallsRepositoryAddMethod()
	{
		// Arrange
		var componentAssembly = ComponentAssembly.Create(
			"TestAssembly",
			new List<String> { "Component1", "Component2" }
		);

		// Act
		_componentAssemblyService.Add(componentAssembly);

		// Assert
		_componentAssemblyRepository.Received(1).Add(componentAssembly);
	}

	[Fact]
	public void AddComponentAssemblyToItsInventory_CallsRepositoryAddComponentMethod()
	{
		// Arrange
		var componentAssembly = ComponentAssembly.Create(
			"TestAssembly",
			new List<string> { "Component1", "Component2" }
		);
		var componentName = "NewComponent";

		// Act
		_componentAssemblyService.AddComponentAssemblyToItsInventory(
			componentAssembly,
			componentName
		);

		// Assert
		_componentAssemblyRepository
			.Received(1)
			.AddComponent(componentAssembly.Id, componentName);
	}
}
