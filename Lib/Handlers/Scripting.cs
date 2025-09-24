using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SScripting
{
    [SouvenirQuestion("What was the submitted data type of the variable in {0}?", TwoColumns4Answers, "int", "bool", "float", "char")]
    VariableDataType
}

public partial class SouvenirModule
{
    [SouvenirHandler("KritScripts", "Scripting", typeof(SScripting), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessScripting(ModuleData module)
    {
        var comp = GetComponent(module, "KritScript");

        yield return WaitForSolve;

        var variableType = GetField<string>(comp, "VariableKindValue", isPublic: true).Get();
        yield return question(SScripting.VariableDataType).Answers(variableType);
    }
}