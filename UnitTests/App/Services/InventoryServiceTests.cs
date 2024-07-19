using core.Services;
using core.Starships;

namespace UnitTests.App.Services;

public class InventoryServiceTests
{
	// private readonly InventoryService _inventoryService;
	//
	// public InventoryServiceTests()
	// {
	// 	_inventoryService = new InventoryService();
	// }
	//
	// [Theory]
	// [InlineData(StarshipName.Speeder, 1, 1, 1, 2, 2, false)]
	// [InlineData(StarshipName.Speeder, 2, 1, 1, 2, 2, true)]
	// [InlineData(StarshipName.Cargo, 1, 1, 1, 2, 1, false)]
	// [InlineData(StarshipName.Cargo, 1, 0, 1, 2, 1, true)]
	// [InlineData(StarshipName.Explorer, 1, 1, 1, 2, 1, false)]
	// [InlineData(StarshipName.Explorer, 1, 1, 1, 1, 1, true)]
	// public void IsMoreInventoryRequired_ReturnsCorrectResult(
	// 	String starshipName,
	// 	Int32 quantity,
	// 	Int32 hullCount,
	// 	Int32 engineCount,
	// 	Int32 wingCount,
	// 	Int32 thrusterCount,
	// 	Boolean expected
	// )
	// {
	// 	// Act
	// 	var result = _inventoryService.IsMoreInventoryRequired(
	// 		starshipName,
	// 		quantity,
	// 		hullCount,
	// 		engineCount,
	// 		wingCount,
	// 		thrusterCount
	// 	);
	//
	// 	// Assert
	// 	Assert.Equal(expected, result);
	// }
	//
	// [Theory]
	// [InlineData(1, 1, 1, 2, 2, false)]
	// [InlineData(2, 1, 1, 2, 2, true)]
	// [InlineData(1, 0, 1, 2, 2, true)]
	// [InlineData(1, 1, 1, 1, 2, true)]
	// public void IsMoreInventoryRequiredForSpeederStarship_ReturnsCorrectResult(
	// 	Int32 quantity,
	// 	Int32 hullCount,
	// 	Int32 engineCount,
	// 	Int32 wingCount,
	// 	Int32 thrusterCount,
	// 	Boolean expected
	// )
	// {
	// 	// Act
	// 	var result = _inventoryService.IsMoreInventoryRequiredForSpeederStarship(
	// 		quantity,
	// 		hullCount,
	// 		engineCount,
	// 		wingCount,
	// 		thrusterCount
	// 	);
	//
	// 	// Assert
	// 	Assert.Equal(expected, result);
	// }
	//
	// [Theory]
	// [InlineData(1, 1, 1, 2, 1, false)]
	// [InlineData(1, 0, 1, 2, 1, true)]
	// [InlineData(1, 1, 1, 1, 1, true)]
	// [InlineData(2, 1, 1, 2, 1, true)]
	// public void IsMoreInventoryRequiredForCargoOrExplorerStarship_ReturnsCorrectResult(
	// 	Int32 quantity,
	// 	Int32 hullCount,
	// 	Int32 engineCount,
	// 	Int32 wingCount,
	// 	Int32 thrusterCount,
	// 	Boolean expected
	// )
	// {
	// 	// Act
	// 	var result = _inventoryService.IsMoreInventoryRequiredForCargoOrExplorerStarship(
	// 		quantity,
	// 		hullCount,
	// 		engineCount,
	// 		wingCount,
	// 		thrusterCount
	// 	);
	//
	// 	// Assert
	// 	Assert.Equal(expected, result);
	// }
}
