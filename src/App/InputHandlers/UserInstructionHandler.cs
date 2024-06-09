using System.Text.RegularExpressions;
using core.In_memories;
using core.UI;
using core.Utils;

namespace core.InputHandlers;

public class UserInstructionHandler : IInputHandler
{
	private const String QuantityWithStarshipPattern = @"(\d+)\s+(\w+)";
	private const String InvalidCommandMessage =
		"❌ La commande doit respecter ce format : [USER_INSTRUCTION] <quantité> <nom_du_vaisseau> [, <quantité> <nom_du_vaisseau>, ...]";

	public void HandleInput(String input)
	{
		if (!HandlerHelper.IsCommandInputValid(input.Split()))
		{
			this.PrintInvalidCommand(InvalidCommandMessage);
			return;
		}

		var splittedBySpaceInput = input.Split(new[] { ' ' }, 2);
		if (!HandlerHelper.IsCommandNameSeparatedByOneSpace(splittedBySpaceInput))
		{
			this.PrintInvalidCommand(InvalidCommandMessage);
			return;
		}

		var userInstructionBody = splittedBySpaceInput[1];
		var userInstruction = this.GetCompleteUserInstructionFrom(userInstructionBody);

		InMemoryUserInstruction.Instance.Add(userInstruction);
	}

	private void PrintInvalidCommand(String message)
	{
		UserInstructionsDisplayHandler.PrintInvalidCommand(message);
	}

	private UserInstruction GetCompleteUserInstructionFrom(String starshipsPart)
	{
		var userInstruction = UserInstruction.Create(new Dictionary<String, int>());

		foreach (var quantityAndStarship in starshipsPart.Split(", "))
		{
			var match = Regex.Match(quantityAndStarship.Trim(), QuantityWithStarshipPattern);
			if (!HandlerHelper.IsMatch(match))
			{
				this.PrintInvalidCommand(InvalidCommandMessage);
				continue;
			}

			if (!int.TryParse(match.Groups[1].Value, out var quantity))
			{
				this.PrintInvalidCommand(InvalidCommandMessage);
				continue;
			}

			var starshipNameInput = match.Groups[2].Value;
			var starshipName = HandlerHelper.GetStarshipName(starshipNameInput);
			if (HandlerHelper.IsUnknownStarship(starshipName))
			{
				Terminal.PrintMessageWithLinebreak($"❌ Vaisseau '{starshipNameInput}' inconnu.");
				continue;
			}

			userInstruction.Add(starshipName, quantity);
		}

		return userInstruction;
	}
}
