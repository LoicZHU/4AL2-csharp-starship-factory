using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using core.Starships;
using core.Utils;

namespace UnitTests.App.Helpers;

public class HandleHelperTests
{
	[Theory]
	[InlineData(new[] { "arg1", "arg2", "arg3" }, true)]
	[InlineData(new[] { "arg1", "arg2" }, false)]
	[InlineData(new[] { "arg1", "arg2", "arg3", "arg4" }, false)]
	[InlineData(new String[] { }, false)]
	public void IsCommandInputValid_ShouldReturnExpectedResult(
		String[] input,
		Boolean expected
	)
	{
		// Act
		var result = HandlerHelper.IsCommandInputValid(input);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData(new[] { "command", "arg" }, true)]
	[InlineData(new[] { "command" }, false)]
	[InlineData(new[] { "command", "arg", "extra" }, false)]
	public void IsCommandNameSeparatedByOneSpace_ShouldReturnExpectedResult(
		String[] parts,
		Boolean expected
	)
	{
		// Act
		var result = HandlerHelper.IsCommandNameSeparatedByOneSpace(parts);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData("two", false, "", 0, "La commande est invalide.")]
	[InlineData("Invalid input", false, "", 0, "La commande est invalide.")]
	[InlineData("12abc Explorer", false, "", 0, "La commande est invalide.")]
	[InlineData("3 unknown", false, StarshipName.Unknown, 3, "Vaisseau inconnu : unknown")]
	[InlineData("3 Explorer", true, "Explorer", 3, "")]
	public void ParseQuantityAndStarship_TestCases(
		String input,
		Boolean expectedIsValid,
		String expectedName,
		Int32 expectedQuantity,
		String expectedErrorMessage
	)
	{
		// Act
		var result = HandlerHelper.ParseQuantityAndStarship(input);

		// Assert
		Assert.Equal(expectedIsValid, result.isValid);
		Assert.Equal(expectedName, result.name);
		Assert.Equal(expectedQuantity, result.quantity);
		Assert.Equal(expectedErrorMessage, result.errorMessage);
	}

	[Fact]
	public void IsMatch_ShouldReturnExpectedResult()
	{
		// Arrange
		var matchSuccess = Regex.Match("input", "input");
		var matchFailure = Regex.Match("input", "no match");

		// Act
		var matchSucceed = HandlerHelper.IsMatch(matchSuccess);
		var matchFailed = HandlerHelper.IsMatch(matchFailure);

		// Assert
		Assert.True(matchSucceed);
		Assert.False(matchFailed);
	}

	[Theory]
	[InlineData("Cargo", StarshipName.Cargo)]
	[InlineData("cargo", StarshipName.Cargo)]
	[InlineData("Explorer", StarshipName.Explorer)]
	[InlineData("explorer", StarshipName.Explorer)]
	[InlineData("Speeder", StarshipName.Speeder)]
	[InlineData("speeder", StarshipName.Speeder)]
	[InlineData("Unknown", StarshipName.Unknown)]
	[InlineData("other", StarshipName.Unknown)]
	public void GetStarshipName_ShouldReturnExpectedResult(String name, String expected)
	{
		// Act
		var result = HandlerHelper.GetStarshipName(name);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData("Cargo", true)]
	[InlineData("cargo", true)]
	[InlineData("Explorer", false)]
	[InlineData("Speeder", false)]
	[InlineData("Unknown", false)]
	[InlineData("other", false)]
	public void IsCargoStarship_ShouldReturnExpectedResult(String name, Boolean expected)
	{
		// Act
		var result = HandlerHelper.IsCargoStarship(name);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData("Explorer", true)]
	[InlineData("explorer", true)]
	[InlineData("Cargo", false)]
	[InlineData("Speeder", false)]
	[InlineData("Unknown", false)]
	[InlineData("other", false)]
	public void IsExplorerStarship_ShouldReturnExpectedResult(String name, Boolean expected)
	{
		// Act
		var result = HandlerHelper.IsExplorerStarship(name);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData("Speeder", true)]
	[InlineData("speeder", true)]
	[InlineData("Cargo", false)]
	[InlineData("Explorer", false)]
	[InlineData("Unknown", false)]
	[InlineData("other", false)]
	public void IsSpeederStarship_ShouldReturnExpectedResult(String name, Boolean expected)
	{
		// Act
		var result = HandlerHelper.IsSpeederStarship(name);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData("Explorer", true)]
	[InlineData("explorer", true)]
	[InlineData("Speeder", true)]
	[InlineData("speeder", true)]
	[InlineData("Cargo", false)]
	[InlineData("Unknown", false)]
	[InlineData("other", false)]
	public void IsExplorerOrSpeederStarship_ShouldReturnExpectedResult(
		String name,
		Boolean expected
	)
	{
		// Act
		var result = HandlerHelper.IsExplorerOrSpeederStarship(name);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData("Unknown", true)]
	[InlineData("unknown", true)]
	[InlineData("Cargo", false)]
	[InlineData("Explorer", false)]
	[InlineData("Speeder", false)]
	[InlineData("other", false)]
	public void IsUnknownStarship_ShouldReturnExpectedResult(String name, Boolean expected)
	{
		// Act
		var result = HandlerHelper.IsUnknownStarship(name);

		// Assert
		Assert.Equal(expected, result);
	}
}
