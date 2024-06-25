using core.UI.constants;

namespace UnitTests.App.Shared.Constants.UI;

public class VerificationTests
{
	[Fact]
	public void ErrorConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("ERROR", Verification.Error);
	}

	[Fact]
	public void AvailableConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("AVAILABLE", Verification.Available);
	}

	[Fact]
	public void UnavailableConstant_ShouldHaveExpectedValue()
	{
		Assert.Equal("UNAVAILABLE", Verification.Unavailable);
	}
}
