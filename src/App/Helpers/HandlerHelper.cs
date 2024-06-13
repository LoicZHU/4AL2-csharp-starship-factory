using System.Text.RegularExpressions;
using core.Starships;

namespace core.Utils;

public static class HandlerHelper
{
	public static Boolean IsCommandInputValid(String[] input)
	{
		Boolean hasAtLeastThreeArguments = input.Length >= 3;
		Boolean areArgumentsPairedWithQuantityAndStarshipName = input.Length % 2 == 1;

		return hasAtLeastThreeArguments && areArgumentsPairedWithQuantityAndStarshipName;
	}

	public static Boolean IsCommandNameSeparatedByOneSpace(String[] parts)
	{
		return parts.Length == 2;
	}

	public static Boolean IsMatch(Match match)
	{
		return match.Success;
	}

	public static String GetStarshipName(String name)
	{
		if (IsCargoStarship(name))
		{
			return StarshipName.Cargo;
		}

		if (IsExplorerStarship(name))
		{
			return StarshipName.Explorer;
		}

		if (IsSpeederStarship(name))
		{
			return StarshipName.Speeder;
		}

		return StarshipName.Unknown;
	}

	public static Boolean IsCargoStarship(String name)
	{
		return name.Equals(StarshipName.Cargo, StringComparison.OrdinalIgnoreCase);
	}

	public static Boolean IsExplorerStarship(String name)
	{
		return name.Equals(StarshipName.Explorer, StringComparison.OrdinalIgnoreCase);
	}

	public static Boolean IsSpeederStarship(String name)
	{
		return name.Equals(StarshipName.Speeder, StringComparison.OrdinalIgnoreCase);
	}

	public static Boolean IsExplorerOrSpeederStarship(String name)
	{
		return IsExplorerStarship(name) || IsSpeederStarship(name);
	}

	public static Boolean IsUnknownStarship(String name)
	{
		return name.Equals(StarshipName.Unknown, StringComparison.OrdinalIgnoreCase);
	}
}
