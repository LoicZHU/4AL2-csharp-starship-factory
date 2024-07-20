using core.UI;
using core.UI.constants;

namespace core.InputHandlers;

public class UnknownInstructionHandlerWithArgs : IHandlerWithArgs
{
	public void Handle(String input)
	{
		this.PrintUnknownInstruction(
			$"🚫 Instruction inconnue : {input} ({Command.Help} pour de l'aide) :"
		);
	}

	private void PrintUnknownInstruction(String message)
	{
		UnknownInstructionDisplayHandler.PrintUnknownInstruction(message);
	}
}
