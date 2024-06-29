using System.Text.RegularExpressions;
using core.In_memories;
using core.UI;
using core.Utils;

namespace core.InputHandlers;

public class UserInstructionHandler : IInputHandler
{
	private const String InvalidCommandMessage =
		"❌ La commande doit respecter ce format : [USER_INSTRUCTION] <quantité> <nom_du_vaisseau> [, <quantité> <nom_du_vaisseau>, ...]";

	public void HandleInput(String input)
	{
		if (!HandlerHelper.IsCommandInputValid(input.Split()))
		{
			this.PrintInvalidCommand(InvalidCommandMessage);
			return;
		}

		var splitBySpaceInput = input.Split(new[] { ' ' }, 2);
		if (!HandlerHelper.IsCommandNameSeparatedByOneSpace(splitBySpaceInput))
		{
			this.PrintInvalidCommand(InvalidCommandMessage);
			return;
		}

		var inputContent = splitBySpaceInput[1];
		var userInstruction = this.GetCompleteUserInstructionFrom(inputContent);
		if (!UtilsFunction.IsNull(userInstruction))
		{
			InMemoryUserInstruction.Instance.Add(userInstruction);
		}
	}

	private void PrintInvalidCommand(String message)
	{
		UserInstructionsDisplayHandler.PrintInvalidCommand(message);
	}

	private UserInstruction? GetCompleteUserInstructionFrom(String starshipsPart)
	{
		var userInstruction = UserInstruction.Create(new Dictionary<String, Int32>());

		foreach (var quantityAndStarship in starshipsPart.Split(", "))
		{
			var (isValid, starshipName, quantity, errorMessage) =
				HandlerHelper.ParseQuantityAndStarship(quantityAndStarship);
			if (!isValid)
			{
				this.PrintInvalidCommand(errorMessage);
				return null;
			}
			if (HandlerHelper.IsUnknownStarship(starshipName))
			{
				Terminal.PrintMessageWithLinebreak(errorMessage);
				return null;
			}

			userInstruction.Add(starshipName, quantity);
		}

		return userInstruction;
	}
}
