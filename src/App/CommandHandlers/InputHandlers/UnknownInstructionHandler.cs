using core.UI;
using core.UI.constants;

namespace core.InputHandlers;

public class UnknownInstructionHandler : IInputHandler
{
	public void Handle(String input)
	{
		var message = $"🚫 Instruction inconnue : {input} ({Command.Help} pour de l'aide) :";
		this.PrintUnknownInstruction(message);
	}

	private void PrintUnknownInstruction(String message)
	{
		UnknownInstructionDisplayHandler.PrintUnknownInstruction(message);
	}
}
