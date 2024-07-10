using System.Text;
using core.Utils;

namespace UnitTests.App.Utils;

public class UtilsFunctionTests
{
	[Fact]
	public void IsDictionaryEmpty_WithEmptyDictionary_ReturnsTrue()
	{
		// Arrange
		var dictionary = new Dictionary<String, String>();

		// Act
		var result = UtilsFunction.IsDictionaryEmpty(dictionary);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void IsDictionaryEmpty_WithNonEmptyDictionary_ReturnsFalse()
	{
		// Arrange
		var dictionary = new Dictionary<String, String> { { "key", "value" } };

		// Act
		var result = UtilsFunction.IsDictionaryEmpty(dictionary);

		// Assert
		Assert.False(result);
	}

	[Theory]
	[InlineData(null, true)]
	[InlineData("", false)]
	[InlineData("test", false)]
	[InlineData(123, false)]
	public void IsNull_InputIsNull_ReturnsTrue(Object input, Boolean expected)
	{
		// Act
		var result = UtilsFunction.IsNull(input);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData(null, true)]
	[InlineData("", true)]
	[InlineData(" ", true)]
	[InlineData("test", false)]
	public void IsNullOrWhiteSpace_InputValues_ReturnsExpectedResult(
		String input,
		Boolean expected
	)
	{
		// Act
		var result = UtilsFunction.IsNullOrWhiteSpace(input);

		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void IsStringBuilderEmpty_ShouldReturnTrue_WhenStringBuilderIsEmpty()
	{
		// Arrange
		var stringBuilder = new StringBuilder();

		// Act
		var result = UtilsFunction.IsStringBuilderEmpty(stringBuilder);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void IsStringBuilderEmpty_ShouldReturnFalse_WhenStringBuilderIsNotEmpty()
	{
		// Arrange
		var stringBuilder = new StringBuilder("Hello");

		// Act
		var result = UtilsFunction.IsStringBuilderEmpty(stringBuilder);

		// Assert
		Assert.False(result);
	}
}
